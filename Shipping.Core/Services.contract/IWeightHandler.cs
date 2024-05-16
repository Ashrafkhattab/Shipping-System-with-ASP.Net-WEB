

using static Shipping.DTO.Weight.Weight;

namespace Shipping.Core.Services.contract
{
    public interface IWeightHandler
    {
        Task<int> Add(AddWeightDto order);
        Task<int> Update(UpdateWeightDto order);
        Task<Model.Weight> GetWeightByIdAsync(int id);
    }
}
