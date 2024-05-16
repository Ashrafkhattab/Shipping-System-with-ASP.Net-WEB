using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Model;
using Shipping.Core.Model.OrderAggregate;
using Shipping.Core.Services.contract;
using Shipping.DTO;
using System.Security.Claims;

namespace Shipping.Controllers
{

    public class OrderController : Controller
    {
        private readonly IOrderHandler orderHandler;
        private readonly UserManager<AppUser> userManager;

        public OrderController(IOrderHandler orderHandler, UserManager<AppUser> userManager)
        {
            this.orderHandler = orderHandler;
            this.userManager = userManager;
        }
        //Add Order
        [HttpPost]
        
        public async Task<ActionResult<AddOrderResultDto>> Add(AddOrderDto order)
        {
            var result = await orderHandler.Add(order);
            if (result.IsSuccesfull && ModelState.IsValid)
            {
                return Ok(new { message = "Order was added successfully.", result });
            }
            ModelState.AddModelError("save", "Can't save Order may be some ID'S wrong!");
            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        
        public async Task<ActionResult<UpdateOrderResultDto>> Update(int id ,UpdateOrderDto order)
        {
            var result = await orderHandler.Update(order);
            if (result.IsSuccesfull && ModelState.IsValid)
            {
                return Ok(new { message = "Order was updated successfully.", result });
            }
            ModelState.AddModelError("save", "Can't save Order may be some ID'S wrong!");
            return BadRequest(ModelState);
        }
        [HttpDelete]
        //[Authorize(Policy = "MerchantOnly")]
        public ActionResult Delete(int orderId)
        {
            var result = orderHandler.Delete(orderId);
            if (result)
            {
                return Ok(new { message = "Order was deleted successfully." });
            }
            return BadRequest("Order not found");
        }
        [HttpGet]
        [Route("Get/{id}")]
        public ActionResult<ReadOrderDto> GetById(int id)
        {
            var order = orderHandler.GetById(id);
            if (order != null)
            {
                return Ok(order);
            }
            return BadRequest(new { message = "Item not found" });
        }
        //Employee
        [HttpGet]
        [Route("CountOrdersForEmployeeByStatus")]
        public ActionResult CountOrdersForEmployeeByStatus()
        {
            return Ok(orderHandler.CountOrdersForEmployeeByStatus());
        }
        [HttpGet]
        [Route("GetOrdersForEmployee")]
        public ActionResult<IEnumerable<ReadOrderDto>> GetOrdersForEmployee(int statusId, int pageNubmer, int pageSize, string searchText = "")
        {
            return Ok(orderHandler.GetOrdersForEmployee(searchText, statusId, pageNubmer, pageSize));
        }

        [HttpGet]
        [Route("GetCountOrdersForEmployee")]
        public ActionResult<int> GetCountOrdersForEmployee(int statusId, string searchText = "")
        {
            return Ok(orderHandler.GetCountOrdersForEmployee(statusId, searchText));
        }

        [HttpPut]
        [Route("SelectRepresentative")]
        public ActionResult SelectRepresentative(int orderId, int representativeId)
        {
            bool result = orderHandler.SelectRepresentative(orderId, representativeId);
            if (result)
            {
                return Ok(new { message = "Selected Successfully" });

            }
            return BadRequest(new { message = "Item not found" });
        }
        //Merchant
        [HttpGet]
        [Route("CountOrdersForMerchantByStatus")]
        //[Authorize(Policy = "MerchantOnly")]
        public ActionResult CountOrdersForMerchantByStatus(int id)
        {
            return Ok(orderHandler.CountOrdersForMerchantByStatus(id));
        }

        [HttpGet]
        [Route("GetOrdersForMerchant")]
        //[Authorize(Policy = "MerchantOnly")]
        public ActionResult<IEnumerable<ReadOrderDto>> GetOrdersForMerchant(int merchantId, int statusId, int pageNubmer, int pageSize, string searchText = "")
        {
            return Ok(orderHandler.GetOrdersForMerchant(searchText, merchantId, statusId, pageNubmer, pageSize));
        }

        [HttpGet]
        [Route("GetCountOrdersForMerchant")]
       // [Authorize(Policy = "MerchantOnly")]
        public ActionResult<int> GetCountOrdersForMerchant(int merchantId, int statusId, string searchText = "")
        {
            return Ok(orderHandler.GetCountOrdersForMerchant(merchantId, statusId, searchText));
        }
        //Employee and Merchant
        [HttpPut]
        [Route("ChangeStatus")]
        public ActionResult ChangeStatus(int orderId, Status status)
        {
            bool result = orderHandler.ChangeStatus(orderId, status);
            if (result)
            {
                return Ok(new { message = "Changed Successfully" });

            }
            return BadRequest(new { message = "Item not found" });
        }
        //Representative 
        [HttpGet]
        [Route("CountOrdersForRepresentativeByStatus")]
        //[Authorize(Policy = "RepresentativeOnly")]
        public ActionResult CountOrdersForRepresentativeByStatus(int representativeId)
        {
            return Ok(orderHandler.CountOrdersForRepresentativeByStatus(representativeId));
        }

        [HttpGet]
        [Route("GetOrdersForRepresentative")]
        //[Authorize(Policy = "RepresentativeOnly")]
        public ActionResult<IEnumerable<ReadOrderDto>> GetOrdersForRepresentative(int representativeId, int statusId, int pageNubmer, int pageSize, string searchText = "")
        {
            return Ok(orderHandler.GetOrdersForRepresentative(representativeId, statusId, pageNubmer, pageSize, searchText));
        }

        [HttpGet]
        [Route("GetCountOrdersForRepresentative")]
        //[Authorize(Policy = "RepresentativeOnly")]
        public ActionResult<int> GetCountOrdersForRepresentative(int representativeId, int statusId, string searchText = "")
        {
            return Ok(orderHandler.GetCountOrdersForRepresentative(representativeId, statusId, searchText));
        }

        [HttpPut]
        [Route("ChangeStatusAndReasonRefusal")]
        //[Authorize(Policy = "RepresentativeOnly")]
        public ActionResult ChangeStatusAndReasonRefusal(int orderId, Status status, int? reasonRefusal)
        {
            bool result = orderHandler.ChangeStatusAndReasonRefusal(orderId, status, reasonRefusal);
            if (result)
            {
                return Ok(new { message = "Changed Successfully" });

            }
            return BadRequest(new { message = "Item not found" });
        }


        //Display order
        [HttpGet]
        [Route("GetAllDataById")]
        public ActionResult<ReadAllOrderDataDto> GetAllDataById(int id)
        {
            var order = orderHandler.GetAllDataById(id);
            if (order != null)
            {
                return Ok(order);
            }
            return BadRequest(new { message = "Item not found" });
        }


    }
}
