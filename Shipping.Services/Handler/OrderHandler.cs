
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shipping.Core.Model;
using Shipping.Core.Model.OrderAggregate;
using Shipping.Core.Repositries.contract;
using Shipping.Core.Services.contract;
using Shipping.DTO;
using Shipping.DTO.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Services.Handler
{
    public class OrderHandler : IOrderHandler
    {
       
        private readonly ICityReprosatriy cityReposatry;
        private readonly IBranchesRepository branchesRepository;
        private readonly IOrderReposiatry orderReposiatry;
        private readonly IProductRepeosatry productRepeosatry;
        private readonly IWeightReperosatry weightReperosatry;
        private readonly UserManager<AppUser> userManager;
        private readonly ISpecialPriceRopresatry specialPriceRopresatry;
        private readonly IMerchantReprosatry merchantReprosatry;
        public IGovernorateRepository GovernorateRepository;

        public OrderHandler(ICityReprosatriy cityReposatry, IGovernorateRepository governorateRepository,
            IBranchesRepository branchesRepository, IOrderReposiatry orderReposiatry,IProductRepeosatry productRepeosatry
            ,IWeightReperosatry weightReperosatry ,UserManager<AppUser> userManager,ISpecialPriceRopresatry specialPriceRopresatry)

        {
            this.cityReposatry = cityReposatry;
            GovernorateRepository = governorateRepository;
            this.branchesRepository = branchesRepository;
            this.orderReposiatry = orderReposiatry;
            this.productRepeosatry = productRepeosatry;
            this.weightReperosatry = weightReperosatry;
            this.userManager = userManager;
            this.specialPriceRopresatry = specialPriceRopresatry;

        }
        public async Task<AddOrderResultDto> Add(AddOrderDto orderDto)
        {
            double countWeight =CountWeight(orderDto.Products);

            double costAllProducts = Cost_AllProducts(orderDto.Products);
            double costShippingType = await Cost_AllShipping(orderDto.TypeOfDelevery);

            double costAddititonalWeight = await Cost_AdditionalWeight(countWeight);

            double cityShippingPrice = (double)await GetSpecialPricesWithMerchantandCityId(orderDto.MerchantId, orderDto.CityId);
            if (cityShippingPrice == 0)
            {
                cityShippingPrice = await CityShippingPrice(orderDto.CityId);
            }

            Order order = new Order()
            {
                MerchantId = orderDto.MerchantId,
                orderType = orderDto.orderType,
                ClientName = orderDto.ClientName,
                PhoneNumber = orderDto.FirstPhoneNumber,
                Email = orderDto.Email,
                GovernorateId = orderDto.GovernorateId,
                CityId = orderDto.CityId,
                Address = orderDto.Street,
                typeOfDelevery = orderDto.TypeOfDelevery,
                typeOfPaying = orderDto.PaymentType,
                BrancheId= orderDto.BranchId,
                status = Status.New,
                Date = DateTime.Now,
                Notes = orderDto.Notes,
                isDeleted = false,
                Price = costAllProducts,
                OrderShippingTotalCost = costAddititonalWeight + cityShippingPrice + costShippingType,
                Weight = countWeight,
                Products = orderDto.Products.Select(prod => new Product
                {
                    Name = prod.Name,
                    Quantity = prod.Quantity,
                    Price = prod.Price,
                    Weigth =(decimal)prod.Weight,
                    isDeleted = false,
                }).ToList(),
            };

            bool isSuccesfullOrder = orderReposiatry.AddOrder(order);
            bool isSuccesfullProduct = productRepeosatry.AddRange(order.Products.ToList());
            if (isSuccesfullOrder && isSuccesfullProduct)
            {
                bool isSaved = orderReposiatry.SaveChanges();
                if (isSaved)
                {
                    double shippingTotalCost = costAddititonalWeight + cityShippingPrice + costShippingType;

                    return new AddOrderResultDto(true, costAllProducts, shippingTotalCost, countWeight);
                }
            }
            return new AddOrderResultDto(false, null, null, null);
        }
        private double CountWeight(List<ProductDTO> products)
        {
            double weight = 0;
            foreach (var item in products)
            {
                weight += item.Weight * item.Quantity;
            }
            return weight;
        }
        private double Cost_AllProducts(List<ProductDTO> products)
        {
            double price = 0;
            foreach (var item in products)
            {
                price += item.Price * item.Quantity;
            }
            return price;
        }
        private async Task<double> Cost_AdditionalWeight(double totalWeight)
        {
            double cost = 0;
            double defaultWeight = 0;
            double additionalPrice = 0;
            var result = await weightReperosatry.GetWeight();
            if (result != null)
            {
                defaultWeight = result.Select(w => w.DefaultWeight).FirstOrDefault();

                if (totalWeight > defaultWeight)
                {

                    additionalPrice = result.Select(w => w.AdditionalPrice).FirstOrDefault();

                    totalWeight = totalWeight - defaultWeight;

                    cost = totalWeight * additionalPrice;
                }
            }

            return cost;
        }
        private async Task<decimal> GetSpecialPricesWithMerchantandCityId(int merchantId, int cityId)
        {
            decimal totalPrice = 0;
            var result = await specialPriceRopresatry.GetAllAsync();
            if (result != null)
            {
                var specialPrices = result.Where(sp => sp.CityId == cityId & sp.MerchentId == merchantId).FirstOrDefault();
                if (specialPrices != null)
                {
                    totalPrice = specialPrices.Price;
                }
            }
            return totalPrice;

        }
        private async Task<double> CityShippingPrice(int cityId)
        {
            var result = cityReposatry.GetById(cityId);
            if (result != null)
            {
                return result.Price;
            }
            return 0;
        }
        private async Task<double> Cost_AllShipping(DeliveryMethode typeOfDelevery)
        {
            var cost = await orderReposiatry.GetCostByShippingTypeAsync(typeOfDelevery);
            return cost;
        }

        public async Task<UpdateOrderResultDto> Update(UpdateOrderDto order)
        {
            double countWeight = CountWeight(order.Products);

            double costAllProducts = Cost_AllProducts(order.Products);

            double costAddititonalWeight = await Cost_AdditionalWeight(countWeight);

            double costShippingType = await Cost_AllShipping(order.TypeOfDelevery);

            double cityShippingPrice = (double)await GetSpecialPricesWithMerchantandCityId(order.MerchantId, order.CityId);
            if (cityShippingPrice == 0)
            {
                cityShippingPrice = await CityShippingPrice(order.CityId);
            }

            var oldOrder = orderReposiatry.GetOrderById(order.Id);

            if (oldOrder != null)
            {
                oldOrder.orderType =order.orderType;
                oldOrder.ClientName = order.ClientName;
                oldOrder.PhoneNumber = order.FirstPhoneNumber;
                oldOrder.Email = order.Email;
                oldOrder.GovernorateId = order.GovernorateId;
                oldOrder.CityId = order.CityId;
                oldOrder.Address = order.Street;
                oldOrder.typeOfDelevery = order.TypeOfDelevery;
                oldOrder.typeOfPaying = order.PaymentType;
                oldOrder.BrancheId = order.BranchId;
                oldOrder.status = order.orderStatus;
                oldOrder.Date = DateTime.Now;
                oldOrder.Notes = order.Notes;
                oldOrder.isDeleted = false;
                oldOrder.Price = costAllProducts;
                oldOrder.OrderShippingTotalCost = costAddititonalWeight + cityShippingPrice + costShippingType;
                oldOrder.Weight = countWeight;
                var newProducts = order.Products.Select(prod => new Product
                {
                    OrderId = oldOrder.Id,
                    Name = prod.Name,
                    Quantity = prod.Quantity,
                    Price = prod.Price,
                    Weigth =(decimal)prod.Weight,
                    isDeleted = false,
                }).ToList();

                var products = productRepeosatry.GetProductsByOrderId(oldOrder.Id).ToList();
                var deleteFlag = productRepeosatry.DeleteRange(products);
                var addFlag = productRepeosatry.AddRange(newProducts);

                if (deleteFlag && addFlag)
                {
                    bool isSaved = orderReposiatry.SaveChanges();
                    if (isSaved)
                    {
                        double shippingTotalCost = costAddititonalWeight + cityShippingPrice + costShippingType;

                        return new UpdateOrderResultDto(true, costAllProducts, shippingTotalCost, countWeight);
                    }
                }
            }
            return new UpdateOrderResultDto(false, null, null, null);
        }
        public bool Delete(int orderId)
        {
            var order = orderReposiatry.GetOrderById(orderId);

            if (order != null)
            {
                List<Product> products = productRepeosatry.GetProductsByOrderId(orderId);
                bool deleted = productRepeosatry.DeleteRange(products);
                if (deleted)
                {
                    bool isSuccesfull = orderReposiatry.RemoveOrder(order);
                    if (isSuccesfull)
                    {
                        orderReposiatry.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public  UpdateOrderDto GetById(int orderId)
        {
            var order = orderReposiatry.GetOrderById(orderId);
            if (order != null)
            {
                UpdateOrderDto result =  new UpdateOrderDto()
                {
                    Id = order.Id,
                    MerchantId = order.MerchantId,
                    PaymentType = order.typeOfPaying,
                    Email = order.Email,
                    BranchId = order.BrancheId,
                    CityId = order.CityId,
                    FirstPhoneNumber = order.PhoneNumber,
                    GovernorateId = order.GovernorateId,
                    Notes = order.Notes,
                    TypeOfDelevery = order.typeOfDelevery,
                    orderType = order.orderType,
                    Street = order.Address,
                    ClientName = order.ClientName,
                    Products = order.Products.Select(prod => new ProductDTO
                    {
                        Name = prod.Name,
                        Quantity = prod.Quantity,
                        Price = prod.Price,
                        Weight = (double)prod.Weigth,
                    }).ToList()
                };
                return result;
            }
            return null;
        }

        public ReadAllOrderDataDto GetAllDataById(int orderId)
        {
            var order = orderReposiatry.GetOrderById(orderId);

            if (order != null)
            {
                ReadAllOrderDataDto result = new ReadAllOrderDataDto()
                {
                    PaymentType = order.typeOfPaying,
                    Email = order.Email,
                    Branch = order.Branches!.Name,
                    City = order.City!.Name,
                    FirstPhoneNumber = order.PhoneNumber,
                    Governorate = order.Governorate!.Name,
                    Notes = order.Notes,
                    ShippingType = order.typeOfDelevery.ToString(),
                    orderType = order.orderType,
                    Street = order.Address,
                    ClientName = order.ClientName,
                    Products = order.Products.Select(prod => new ProductDTO
                    {
                        Name = prod.Name,
                        Quantity = prod.Quantity,
                        Price = prod.Price,
                        Weight = (double)prod.Weigth,
                    }).ToList()
                };
                return result;
            }
            return null;
        }
        //report
        public IEnumerable<ReadOrderReportsDto> GetAll(int pageNumer, int pageSize)
        {
            return orderReposiatry.GetAll(pageNumer, pageSize).Select(s => new ReadOrderReportsDto
            {
                Merchant = s.Merchant!.AppUser.Name,
                orderStatus = s.status,
                FirstPhoneNumber = s.PhoneNumber,
                OrderShippingTotalCost = s.OrderShippingTotalCost,
                ProductTotalCost = s.Price,
                Date = s.Date,
                ClientName = s.ClientName,
                Governorate = s.Governorate!.Name,
                City = s.City!.Name,
            });
        }
        public int CountAll()
        {
            return orderReposiatry.CountAll();
        }
        public IEnumerable<ReadOrderReportsDto> SearchByDateAndStatus(int pageNumer, int pageSize, DateTime fromDate, DateTime toDate, Status status)
        {
            var result = orderReposiatry.SearchByDateAndStatus(pageNumer, pageSize, fromDate, toDate, status).Select(s => new ReadOrderReportsDto
            {
                Merchant = s.Merchant!.AppUser.Name,
                orderStatus = s.status,
                FirstPhoneNumber = s.PhoneNumber,
                OrderShippingTotalCost = s.OrderShippingTotalCost,
                ProductTotalCost = s.Price,
                Date = s.Date,
                ClientName = s.ClientName,
                Governorate = s.Governorate!.Name,
                City = s.City!.Name,
            });
            return result;
        }
        public int CountOrdersByDateAndStatus(DateTime fromDate, DateTime toDate, Status status)
        {
            return orderReposiatry.CountOrdersByDateAndStatus(fromDate, toDate, status);
        }
        //Employee
        public List<int> CountOrdersForEmployeeByStatus()
        {
            var listOrderStatus = orderReposiatry.CountOrdersForEmployeeByStatus();
            int[] countOrdres = new int[11];

            var statusGroups = listOrderStatus.GroupBy(i => i);

            foreach (var group in statusGroups)
            {
                countOrdres[group.Key] = group.Count();
            }
            return countOrdres.ToList();
        }
        public int GetCountOrdersForEmployee(int statusId, string searchText)
        {
            if (statusId > 10 || statusId < 0)
            {
                return 0;
            }
            return orderReposiatry.GetCountOrdersForEmployee(statusId, searchText);
        }
        public IEnumerable<ReadOrderDto> GetOrdersForEmployee(string searchText, int statusId, int pageNumer, int pageSize)
        {
            if (statusId > 10 || statusId < 0)
            {
                return null!;
            }
            return orderReposiatry.GetOrdersForEmployee(searchText, statusId, pageNumer, pageSize).Select(o => new ReadOrderDto
            {
                Id = o.Id,
                ClientName = o.ClientName,
                Date = o.Date,
                Governorate = o.Governorate!.Name,
                City = o.City!.Name,
                Cost = o.Price + o.OrderShippingTotalCost

            });
        }
        public bool SelectRepresentative(int OrderId, int representativeId)
        {
            var order = orderReposiatry.GetOrderById(OrderId);
            if (order == null)
            {
                return false;
            }
            else
            {
                order.RepresentativeId = representativeId;
                order.status = Status.RepresentitiveDelivered;
                orderReposiatry.SaveChanges();
                return true;
            }
        }
        //Marchant
        public List<int> CountOrdersForMerchantByStatus(int merchantId)
        {
            var listOrderStatus = orderReposiatry.CountOrdersForMerchantByStatus(merchantId);
            int[] countOrdres = new int[11];

            var statusGroups = listOrderStatus.GroupBy(i => i);

            foreach (var group in statusGroups)
            {
                countOrdres[group.Key] = group.Count();
            }
            return countOrdres.ToList();
        }
        public int GetCountOrdersForMerchant(int merchantId, int statusId, string searchText)
        {
            if (statusId > 10 || statusId < 0)
            {
                return 0;
            }
            return orderReposiatry.GetCountOrdersForMerchant(merchantId, statusId, searchText);
        }
        public IEnumerable<ReadOrderDto> GetOrdersForMerchant(string searchText, int merchantId, int statusId, int pageNumer, int pageSize)
        {
            if (statusId > 10 || statusId < 0)
            {
                return null!;
            }
            return orderReposiatry.GetOrdersForMerchant(searchText, merchantId, statusId, pageNumer, pageSize).Select(o => new ReadOrderDto
            {
                Id = o.Id,
                ClientName = o.ClientName,
                Date = o.Date,
                Governorate = o.Governorate!.Name,
                City = o.City!.Name,
                Cost = o.Price + o.OrderShippingTotalCost

            });
        }
        //Employee and Merchant
        public bool ChangeStatus(int OrderId, Status status)
        {
            var order = orderReposiatry.GetOrderById(OrderId);
            if (order == null)
            {
                return false;
            }
            else
            {
                order.status = status;
                orderReposiatry.SaveChanges();
                return true;
            }
        }
        //Representative
        public List<int> CountOrdersForRepresentativeByStatus(int representativeId)
        {
            var listOrderStatus = orderReposiatry.CountOrdersForRepresentativeByStatus(representativeId);
            int[] countOrdres = new int[11];

            var statusGroups = listOrderStatus.GroupBy(i => i);

            foreach (var group in statusGroups)
            {
                countOrdres[group.Key] = group.Count();
            }

            int[] representativeStatus = new int[8];

            Array.Copy(countOrdres, 2, representativeStatus, 0, 8);

            return representativeStatus.ToList();
        }
        public int GetCountOrdersForRepresentative(int representativeId, int statusId, string searchText)
        {
            return orderReposiatry.GetCountOrdersForRepresentative(representativeId, statusId, searchText);
        }
        public IEnumerable<ReadOrderDto> GetOrdersForRepresentative(int representativeId, int statusId, int pageNumer, int pageSize, string searchText)
        {
            return orderReposiatry.GetOrdersForRepresentative(representativeId, statusId, pageNumer, pageSize, searchText).Select(o => new ReadOrderDto
            {
                Id = o.Id,
                ClientName = o.ClientName,
                Date = o.Date,
                Governorate = o.Governorate!.Name,
                City = o.City!.Name,
                Cost = o.Price + o.OrderShippingTotalCost

            });
        }
        public bool ChangeStatusAndReasonRefusal(int OrderId, Status status, int? reasonRefusal)
        {
            var order = orderReposiatry.GetOrderById(OrderId);
            if (order == null)
            {
                return false;
            }
            else
            {
                order.status = status;
                orderReposiatry.SaveChanges();
                return true;
            }
        }






    }

}