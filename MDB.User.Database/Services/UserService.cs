using Microsoft.AspNetCore.Identity;

namespace MDB.User.Database.Services
{
    public class UserService
    {
        private readonly UserManager<MDB.Membership.Database.Entities.User> _userManager;

        public UserService(UserManager<MDB.Membership.Database.Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(string email, string password, string name)
        {
            var user = new MDB.Membership.Database.Entities.User
            {
                UserName = email,
                Email = email,
                //Name = name
            };

            var result = await _userManager.CreateAsync(user, password);
            return result;
        }
    }

}
