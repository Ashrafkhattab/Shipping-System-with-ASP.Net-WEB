using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.DTO;
using Shipping.MiddlWares;
using Shipping.Repositry.Data;

namespace Shipping.Repositry.Repositories
{
    public class RepresentativeRepository:IRepresentativeRepository
    {
        private readonly ShippingContext context;
        private readonly UserManager<AppUser> userManager;

        public RepresentativeRepository(ShippingContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async void UpdatePassword(UpdatePasswordDtos passwordDTO)
        {
            var Repre = await userManager.FindByIdAsync(passwordDTO.Id);
            if(Repre == null || Repre.IsDeleted== true) { throw new ExceptionLogic(""); }
            Repre.PasswordHash = userManager.PasswordHasher.HashPassword(Repre, Repre.PasswordHash);
             context.Update(Repre);
        }


        public async Task<int> UpdateRepresntative(Representative rep)
        {
            if(rep == null) { return 0; }
             context.Representatives.Update(rep);
            return 1;
           

        }

        public async Task<Representative> GetRepresentativeById(int id)
        {
            var rep =  context.Representatives.Include(r=>r.AppUser).FirstOrDefault(r=>r.Id==id);
            
            if (rep == null || rep.AppUser.IsDeleted== true) { throw new ExceptionLogic(""); }

            return rep;
        }
        public async Task<int> GetRepresentiveBystringId(string id)
        {

            var query = from Representive in context.Representatives
                        join user in userManager.Users on Representive.AppUserId equals user.Id into userJoin
                        from subUser in userJoin.DefaultIfEmpty()
                        where Representive.AppUserId == id
                        select new
                        {
                            Representive.Id,
                        };

            var result = await query.FirstOrDefaultAsync();

            return (result?.Id ?? 0);
        }


        public async Task<List<Representative>> GetAllRepresentatives()
        {
            var userslist = await context.Representatives.Include(i=>i.AppUser).ThenInclude(i=>i.branch).Include(x=>x.Governorate).ToListAsync();

            return userslist;


        }

        public async Task<int> DeleteRepresentative(int id)
        {
            var rep =  context.Representatives.Include(r=>r.AppUser).FirstOrDefault(r=>r.Id  == id);
            if (rep == null || rep.AppUser.IsDeleted == true) {  return 0; }

            rep.AppUser.IsDeleted = true;
            return 1;
        }

        public async Task CreateAsync(Representative representative)
        {
            await context.Representatives.AddAsync(representative);
        }

        public  async Task SaveChanges()
        {
          await  context.SaveChangesAsync();
        }

    }
}
