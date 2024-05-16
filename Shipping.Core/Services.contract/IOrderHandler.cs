using Shipping.Core.Model.OrderAggregate;
using Shipping.DTO;

namespace Shipping.Core.Services.contract
{
    public interface IOrderHandler
    {
        Task<AddOrderResultDto> Add(AddOrderDto order);
        Task<UpdateOrderResultDto> Update(UpdateOrderDto order);
        bool Delete(int order);
        UpdateOrderDto GetById(int orderId);
        ReadAllOrderDataDto GetAllDataById(int orderId);

        //Report
        IEnumerable<ReadOrderReportsDto> GetAll(int pageNumer, int pageSize);
        IEnumerable<ReadOrderReportsDto> SearchByDateAndStatus(int pageNumer, int pageSize, DateTime fromDate, DateTime toDate, Status status);
        int CountAll();
        int CountOrdersByDateAndStatus(DateTime fromDate, DateTime toDate, Status status);
        //Employee
        List<int> CountOrdersForEmployeeByStatus();
        IEnumerable<ReadOrderDto> GetOrdersForEmployee(string searchText, int statusId, int pageNumer, int pageSize);
        int GetCountOrdersForEmployee(int statusId, string searchText);
        bool SelectRepresentative(int OrderId, int representativeId);
        //Merchant
        List<int> CountOrdersForMerchantByStatus(int merchantId);
        IEnumerable<ReadOrderDto> GetOrdersForMerchant(string searchText, int merchantId, int statusId, int pageNumer, int pageSize);
        int GetCountOrdersForMerchant(int merchantId, int statusId, string searchText);
        //Employee and Merchant
        bool ChangeStatus(int OrderId, Status status);

        //Representative
        List<int> CountOrdersForRepresentativeByStatus(int representativeId);
        IEnumerable<ReadOrderDto> GetOrdersForRepresentative(int representativeId, int statusId, int pageNumer, int pageSize, string searchText);
        int GetCountOrdersForRepresentative(int representativeId, int statusId, string searchText);
        bool ChangeStatusAndReasonRefusal(int OrderId, Status status, int? reasonRefusal);


    }

}
