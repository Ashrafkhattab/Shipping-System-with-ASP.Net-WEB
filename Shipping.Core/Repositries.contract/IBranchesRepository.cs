
using Shipping.Core.Model;
using Shipping.DTO.Branch;


namespace Shipping.Core.Repositries.contract
{
    public interface IBranchesRepository: IGenricRepository<Branches>
    {
         Branches GetBranchByName(string name);
         void ChangeStatusBranch(int id);
        void Add(AddBranchDto branchDto);
    }
}
