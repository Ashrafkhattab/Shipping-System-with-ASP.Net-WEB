using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.Core.Services.contract;
using Shipping.DTO;
using Shipping.DTO.Merchant;
using Shipping.DTO.RegestarDto;
using Shipping.DTO.SpecailPrices;
using Shipping.MiddlWares;
using System.Security.Claims;
using System.Threading;

namespace Shipping.Services.Handler
{
    public class MerchantHandler : IMerchantHandler
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ISpecialPriceRopresatry specialPricesRepository;
        private readonly IMerchantReprosatry reprosatry;

        public MerchantHandler(UserManager<AppUser> userManager, ISpecialPriceRopresatry specialPricesRepository, IMerchantReprosatry reprosatry, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.specialPricesRepository = specialPricesRepository;
            this.reprosatry = reprosatry;
            this.roleManager = roleManager;
        }

        public async Task<int> DeleteMerchant(int userId)
        {
            var merchant = await reprosatry.GetMerchant(userId);
            if (merchant == null)
            {
                return 0;
            }
            
            await specialPricesRepository.RemoveRangeAsync(merchant.SpecialPrices.ToList());
            merchant.AppUser.IsDeleted = true;
            await userManager.UpdateAsync(merchant.AppUser);

            await reprosatry.SaveChangesAsync();

            return merchant.Id;
        }

        public async Task<MerchantUpdateDto> GetMerchantByIdWithSpecialPrices(int userId)
        {
            var merchant = await reprosatry.GetMerchant(userId);


            if (merchant == null)
            {
                return null;
            }
            

            var merchantDto = new MerchantUpdateDto
            {
                Name = merchant.AppUser.Name,
                PhoneNumber = merchant.AppUser.PhoneNumber,
                Address = merchant.AppUser.Address,
                StoreName = merchant.StoreName,
                ReturnerPercent = (double)merchant.ReturnerPercent,
                BranchId = merchant.AppUser.BranchId ?? 0,
                CityId = (int)merchant.CityId,
                GovernorateId = (int)merchant.GovernorateId,
                SpecialPrices = merchant.SpecialPrices?.Select(sp => new SpecialPricesDTO
                {
                    SpecialPrice = sp.Price,
                    SpecialGovernorateId = sp.GovernorateId,
                    SpecialCityId = sp.CityId
                }).ToList() ?? new()
            };

            return merchantDto;
        }

        public async Task<int> RegisterMerchant(MerchantRegesterDTO registrationDTO)
        {

           
            AppUser user = new AppUser
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

            //if (await roleManager.FindByNameAsync("Merchant") == null)
            //   throw new ExceptionLogic("");

            await userManager.AddToRoleAsync(user, "Merchant");

            await reprosatry.CreateAsync( new Marchant
            {
                AppUserId = user.Id,
                StoreName = registrationDTO.StoreName,
                ReturnerPercent = registrationDTO.ReturnerPercent,
                CityId = registrationDTO.CityId,
                GovernorateId = registrationDTO.GovernorateId,
                SpecialPrices = registrationDTO.SpecialPrices.Select(p => new SpecialPrice
                {
                    Price = p.SpecialPrice,
                    CityId = p.SpecialCityId,
                    GovernorateId = p.SpecialGovernorateId
                }).ToList()
            });
            
            await reprosatry.SaveChangesAsync();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role,  "Merchant")

            };
            await userManager.AddClaimsAsync(user, claims);
            return 1;
        }

        public async Task<int> UpdateMerchant(MerchantUpdateDto updateDto)
        {
            var march = await  reprosatry.GetMerchant(updateDto.Id);
            var user = await userManager.Users.Include(x => x.Marchant).ThenInclude(x => x.SpecialPrices).FirstOrDefaultAsync(x => x.Id == march.AppUser.Id);

            if (user == null)
            {
                return 0;
            }

            user.Name = updateDto.Name;
            user.PhoneNumber = updateDto.PhoneNumber;
            user.BranchId = updateDto.BranchId;
            user.Address = updateDto.Address;
            user.Marchant.StoreName = updateDto.StoreName;
            user.Marchant.ReturnerPercent = updateDto.ReturnerPercent;
            user.Marchant.CityId = updateDto.CityId;
            user.Marchant.GovernorateId = updateDto.GovernorateId;

            var specialPrices = updateDto.SpecialPrices.Select(p => new SpecialPrice
            {
                Price = p.SpecialPrice,
                CityId = p.SpecialCityId,
                GovernorateId = p.SpecialGovernorateId,
                MerchentId = user.Marchant.Id
            }).ToList();

            List<SpecialPrice> existingSpecialPrices = await specialPricesRepository
                                                          .GetSpecialPricesByMerchantId(user.Marchant.Id);
            await specialPricesRepository.RemoveRangeAsync(existingSpecialPrices);
            await specialPricesRepository.AddRangeAsync(specialPrices);
           var result =  await userManager.UpdateAsync(user);
            if (!result.Succeeded) { throw new ExceptionLogic(""); }
            await reprosatry.UpdateMerchant(user.Marchant);
            await reprosatry.SaveChangesAsync();

            return 1;
        }

        public async Task<int> UpdateMerchantPassword(UpdatePasswordDtos updatePassDto)
        {
            var merchant = await userManager.FindByIdAsync(updatePassDto.Id) ??

                throw new ExceptionLogic("");
            if (merchant == null)
            {
                return 0;
            }
            merchant.Email = updatePassDto.Email;
            merchant.PasswordHash = userManager.PasswordHasher.HashPassword(merchant, updatePassDto.Password);

            var result = await userManager.UpdateAsync(merchant);
            if (!result.Succeeded)
                throw new ExceptionLogic("");

            return merchant.Marchant.Id;
        }
        public async Task<List<GetAllMerchantsDto>> GetAllMarchentsAsync()
        {
            IReadOnlyList<Marchant> merchants = (await reprosatry.GetAllMerchants()).ToList();
            var data = merchants.Select(m => new GetAllMerchantsDto
            {
                Id = m.AppUser.Id,
                Name = m.AppUser.Name,
                Email = m.AppUser.Email,
                Phone = m.AppUser.PhoneNumber,
                ReturnerPercent = m.ReturnerPercent,
                StoreName = m.StoreName,
                GovernateName = m.Governorate.Name,
                IsDeleted = m.AppUser.IsDeleted,
                BranchName = m.AppUser.branch.Name,
            }).ToList();

            return data;
        }



        public async Task<(int, string, string)> GetMerchantBystringId(string appUserId)
        {
            return await reprosatry.GetMerchantBystringId(appUserId);
        }









        }
}
