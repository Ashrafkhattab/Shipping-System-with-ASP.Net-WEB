
using Microsoft.EntityFrameworkCore;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.Repositry.Data;
using static Shipping.DTO.Weight.Weight;

namespace Shipping.Repositry.Repositories
{
    public class WeightReprosatry : IWeightReperosatry
    {
        private readonly ShippingContext context;

        public WeightReprosatry(ShippingContext context) 
        {
            this.context = context;
        }
        public async Task<int> Add(AddWeightDto orderDto)
        {
            var weight = new Model.Weight
            {
                DefaultWeight = orderDto.DefaultWeight,
                AdditionalPrice = orderDto.AdditionalPrice
            };

            context.Weights.Add(weight);
            await context.SaveChangesAsync();

            return weight.Id;
        }

        public List<Weight> GetWeight()
        {
            return context.Weights.ToList();
        }

        public async Task<Weight> GetWeightByIdAsync(int id)
        {
            return await context.Weights.FirstOrDefaultAsync(c=> c.Id == id);
        }


        public async Task<int> Update(DTO.Weight.Weight.UpdateWeightDto orderDto)
        {
            var weight = await context.Weights.FirstOrDefaultAsync(c => c.Id == orderDto.Id);
            if (weight == null)
            {
                throw new Exception("Weight not found");
            }
            //context.Weights.Update(weight);
            weight.DefaultWeight = orderDto.DefaultWeight;
            weight.AdditionalPrice = orderDto.AdditionalPrice;
            await context.SaveChangesAsync();
            return weight.Id;
        }

    }
}
