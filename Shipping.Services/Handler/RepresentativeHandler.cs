using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Shipping.Core.Services.contract;
using Shipping.Core.Repositries.contract;
using Shipping.Core.Model;
using Shipping.DTO.RepresentiveDTO;
using Shipping.MiddlWares;
using Shipping.DTO;
using Shipping.DTO.RegestarDto;

namespace Shipping.Services.Handler
{
    public class RepresentativeHandler:IRepresentativeHandler
    {
        private readonly IRepresentativeRepository representativeRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public RepresentativeHandler(IRepresentativeRepository representativeRepository,RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager)
        {
            this.representativeRepository = representativeRepository;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async  Task<List<GetAllRepresentaiveDTO>> GetAll()
        {
            IReadOnlyList<Representative> represents = (await representativeRepository.GetAllRepresentatives()).ToList();

            var allReps = represents.Select(e => new GetAllRepresentaiveDTO
            {
                Id = e.AppUser.Id,
                Name = e.AppUser.Name,
                Email = e.AppUser.Email,
                Phone = e.AppUser.PhoneNumber,
                Amount = e.Amount,
                Type = e.Type,
                BranchName = e.AppUser.branch?.Name,
                IsDeleted = e.AppUser.IsDeleted,

            }).ToList();
            return allReps;
        }

        public void UpdateRepPassword(UpdatePasswordDtos upDto)
        {
            representativeRepository.UpdatePassword(upDto);
            representativeRepository.SaveChanges();
        }

        public async Task<int> UpdateRepresentativ(UpdateRepresentavieDTO UpRepDTO)
        {
            var rep = await representativeRepository.GetRepresentativeById(UpRepDTO.Id);
           // var rep = await userManager.Users.Include(u=>u.Representative).FirstOrDefaultAsync(x=>x.Id == reps.AppUser.Id); 
           
            if (rep == null || rep.AppUser.IsDeleted == true) { return 0; }
            rep.AppUser.PhoneNumber = UpRepDTO.PhoneNumber;
            rep.AppUser.Name = UpRepDTO.Name;
            rep.AppUser.BranchId = UpRepDTO.BranchId;
            rep.AppUser.Address = UpRepDTO.Address;
            rep.Amount = UpRepDTO.Amount;
            rep.Type = UpRepDTO.Type;
           var result = await userManager.UpdateAsync(rep.AppUser);
            if (!result.Succeeded) { throw new ExceptionLogic(""); }
           await  representativeRepository.UpdateRepresntative(rep);
             await  representativeRepository.SaveChanges();
            return 1;
        }

        public async void Delete (int id)
        {
           var result = await  representativeRepository.DeleteRepresentative(id);
            if (result == 0) {  throw new ExceptionLogic("Can't delete it"); }

            representativeRepository.SaveChanges();
        }

        public async Task<ShowRepresentativeDTO> GetRepById(int id)
        {
            var rep = await representativeRepository.GetRepresentativeById(id);
            return new ShowRepresentativeDTO
            {
                
                Name = rep.AppUser.Name,
                Email = rep.AppUser.Email,
                PhoneNumber = rep.AppUser.PhoneNumber,
                Amount =(decimal) rep.Amount,
                Type =(AmountType) rep.Type,
                BranchName = rep.AppUser.branch?.Name,
                IsDeleted = rep.AppUser.IsDeleted,

            };
        }

        public async Task<int> RegisteRepresentative(RegisteRepresentativeDTO registrationDTO)
        {
            var user = new AppUser
            {
                Name = registrationDTO.Name,
                UserName = registrationDTO.UserName,
                Email = registrationDTO.Email,
                PhoneNumber = registrationDTO.PhoneNumber,
                BranchId = registrationDTO.BranchId,
                Address = registrationDTO.Address,
                
            };

            var result = await userManager.CreateAsync(user, registrationDTO.Password);

            if (!result.Succeeded)
            {

               return 0;
            }

            await userManager.AddToRoleAsync(user, "Representative");

            await representativeRepository.CreateAsync(new Representative
            {
                AppUserId = user.Id,
                GovernorateId=registrationDTO.GovernorateId,
                Amount = registrationDTO.Amount,
                Type= registrationDTO.Type,
            });
             representativeRepository.SaveChanges();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role,"Representative")

            };

            userManager.AddClaimsAsync(user, claims);
            return 1;
        }
        public async Task<int> GetRepresentativeById(string appUserId)
        {
            return await representativeRepository.GetRepresentiveBystringId(appUserId);
        }



    }
}
