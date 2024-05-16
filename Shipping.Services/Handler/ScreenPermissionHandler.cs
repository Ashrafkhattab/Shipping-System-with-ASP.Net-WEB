using Microsoft.AspNetCore.Identity;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.DTO;
using Shipping.MiddlWares;

namespace Shipping.Services.Handler
{
    public class ScreenPermissionHandler
    {

            private readonly IScreenPermisssionReprosatary _repository;
        private RoleManager<IdentityRole> roleManager;

        public ScreenPermissionHandler(IScreenPermisssionReprosatary repository, RoleManager<IdentityRole> roleManager)
        {
            _repository = repository;
            this.roleManager = roleManager;
        }

        public async Task<List<PermissionScreenDTO>> GetPermissions(string roleName)
            {
                if (!await _repository.RoleExists(roleName))
                    throw new ExceptionLogic("Role Not Exist");

                var screens = await _repository.GetAllScreensWithPermissions();
                var permissionScreens = new List<ScreenPermission>();
                var listResponse = new List<PermissionScreenDTO>();
               //var roleResult = await _repository.GetrollId(roleName);
            var role = roleManager.Roles.FirstOrDefault(x => x.Name == roleName);

                foreach (var item in screens)
                {
                    

                    if (item.ScreenPermission == null || item.ScreenPermission.Count() <= 0)
                    {
                        permissionScreens.Add(new ScreenPermission
                        {
                            RoleId = role.Id,
                            ScreenId = item.Id
                        });

                        listResponse.Add(new PermissionScreenDTO
                        {
                            ScreenId = item.Id,
                            ScreenName = item.Name
                        });
                    }
                    else
                    {
                        var permissionScreen = item.ScreenPermission.FirstOrDefault(x => x.RoleId == role.Id);
                        listResponse.Add(new PermissionScreenDTO
                        {
                            ScreenId = item.Id,
                            ScreenName = item.Name,
                            Get = permissionScreen.Get,
                            Add = permissionScreen.Add,
                            Delete = permissionScreen.Delete,
                            Update = permissionScreen.Update
                        });
                    }
                }

                await _repository.AddScreenPermissions(permissionScreens);
                return listResponse;
            }

            public async Task UpdatePermission(PermissionScreensRequestDTO permission)
            {
                if (!await _repository.RoleExists(permission.RoleName))
                    throw new ExceptionLogic("Role Not Exist");

                var roleResult = await _repository.GetrollId(permission.RoleName);
                var screenPermissions = await _repository.GetScreenPermissions(roleResult);

                if (screenPermissions.Count != permission.PermissionScreens.Count)
                    throw new ExceptionLogic("");

                foreach (var item in screenPermissions)
                {
                    var curr = permission.PermissionScreens.FirstOrDefault(x => x.ScreenId == item.ScreenId);
                    item.Get = curr.Get;
                    item.Add = curr.Add;
                    item.Update = curr.Update;
                    item.Delete = curr.Delete;
                }

                 _repository.Savechanges();
            }

    }
}
