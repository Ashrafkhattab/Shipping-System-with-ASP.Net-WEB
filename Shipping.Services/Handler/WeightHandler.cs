
using System.ComponentModel.DataAnnotations;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.Core.Services.contract;

namespace Shipping.Services.Handler
{
    public class WeightHandler : IWeightHandler
    {
        private readonly IWeightReperosatry reperosatry;

        public WeightHandler(IWeightReperosatry reperosatry)
        {
            this.reperosatry = reperosatry;
        }

        public Task<int> Add(DTO.Weight.Weight.AddWeightDto order)
        {
            Validator.ValidateObject(order, new ValidationContext(order), true);
            return reperosatry.Add(order);
        }

        public Task<Weight> GetWeightByIdAsync(int id)
        {

            return reperosatry.GetWeightByIdAsync(id);
        }

        public Task<int> Update(DTO.Weight.Weight.UpdateWeightDto order)
        {
            Validator.ValidateObject(order, new ValidationContext(order), true);

            return reperosatry.Update(order);
        }
    }
}
