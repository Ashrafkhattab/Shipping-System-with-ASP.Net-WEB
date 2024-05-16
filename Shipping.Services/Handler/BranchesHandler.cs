
using Shipping.Core.Repositries.contract;
using Shipping.Core.Services.contract;
using Shipping.DTO.Branch;
using Shipping.MiddlWares;
using Shipping.Repositry.Repositories;

namespace Shipping.Services.Handler
{
    public class BranchesHandler :IBranchesHandler
    {
        private readonly IBranchesRepository repository;

        public BranchesHandler(IBranchesRepository repository) 
        {
            this.repository = repository;
        }


        public void ChangeStatusBranchAsync(int id)
        {
            repository.ChangeStatusBranch(id);
            repository.SaveChanges();
        }

        public void CreateBranchAsync(AddBranchDto branchDto)
        {
            if (branchDto == null)
            {
                throw new ExceptionLogic("Empty");
            }
            var branch = repository.GetBranchByName(branchDto.Name);
           if (branchDto.Name == branch.Name)
            { throw new ExceptionLogic("Already Existe"); }
            repository.Add(branchDto);
            repository.SaveChanges();

        }

        public void DeleteBranchAsync(int id)
        {
            repository.Delete(id);
            repository.SaveChanges();
        }

        public List<getBranchByIdDto> GetBranches()
        {
            var branches= repository.GetAll();
            List<getBranchByIdDto> result= new List<getBranchByIdDto>();
            foreach (var branch in branches)
            {
                result.Add(new getBranchByIdDto
                {
                    Name = branch.Name,
                    status = branch.status,
                    DateTime = branch.DateTime,
                    isDeleted = branch.isDeleted,
                });
            }
            return result;
        }
        public getBranchByIdDto GetBranchById(int id)
        {
            var branche = repository.GetById(id);
            return new getBranchByIdDto { Name = branche.Name, status = branche.status, DateTime = branche.DateTime };
        }

        public void UpdateBranchAsync(UpdateBranchDto branchDto)
        {
            if(branchDto == null)
                throw new ExceptionLogic("Empty");
            var branch = repository.GetBranchByName(branchDto.Name);
            if (branchDto.Name == branch.Name)
            { throw new ExceptionLogic("Already Existe"); }
            repository.Update(branchDto);
            repository.SaveChanges();
        }
    }
}
