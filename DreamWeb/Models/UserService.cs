using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DreamWeb.Models
{
    public interface IUserService
    {
        public Task<string> CreateInternalIdAsync();

    }

    public class UserService : IUserService
    {
        private DreamsContext _context;
        private char[] internalIdChars = "abcdefghigklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public UserService(DreamsContext context, UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager)
        {
            _context = context;
        }

        public async Task<string> CreateInternalIdAsync()
        {
            Random random = new Random();
            string id = "";
            for(int i = 0; i<12; i++)
            {
                id += internalIdChars[random.Next(0, internalIdChars.Length)];
            }

            if(_context.UserAccounts.
               Any(p => p.Id == id))
            {
                id = await CreateInternalIdAsync();
            }

            return id;
        }

    }

}
