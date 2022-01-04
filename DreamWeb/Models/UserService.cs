using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DreamWeb.Models
{
    public interface IUserService
    {
        public Task<string> CreateNewAccount(NewUser newUser);
        public NewUser GetNewUser();
        public Task<string> SignIn(string login, string password);
    }

    public class UserService : IUserService
    {
        private DreamsContext _context;
        private UserAccount _user;
        private static NewUser _userNew;
        private UserManager<UserAccount> _userManager;
        private SignInManager<UserAccount> _signInManager;

        private char[] internalIdChars = "abcdefghigklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public UserService(DreamsContext context, UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public NewUser GetNewUser()
        {
            return _userNew;
        }

        public async Task<string> CreateNewAccount(NewUser newUser)
        {
            _userNew = newUser;
            if(CheckNewAccount(newUser) == null)
            {
                var user = new UserAccount { Email = newUser.Email,
                                            UserName = newUser.Login,
                                            ExternalId = newUser.ExteranlId,
                                            Id = CreateInternalId(),
                                            CreationTime = DateTime.Now
                                            };
                var result = await _userManager.CreateAsync(user, newUser.Password);

                var error = result.Errors.FirstOrDefault();
                if (result.Succeeded)
                {
                    return null;
                }
                return "Something went wrong";
            }
            else
            {
                return CheckNewAccount(newUser);
            }
        }

        private string CheckNewAccount(NewUser newAccount)
        {
            if (newAccount.Password != newAccount.Repassword)
            {
                return "Your passwords doesn't match";
            }
            else if ((newAccount.Email == null) || !(newAccount.Email.Contains("@") && newAccount.Email.Contains(".")))
            {
                return "Something is wrong with your email";
            }
            else if (_context.UserAccounts.
                    Any(p => p.UserName == newAccount.Login))
            {
                return "Nickname is taken";
            }
            else if (_context.UserAccounts.
                    Any(p => p.Email == newAccount.Email))
            {
                return "Email is already registered";
            }
            else if (_context.UserAccounts.
                    Any(p => p.ExternalId == newAccount.ExteranlId)
                    && newAccount.ExteranlId != null)
            {
                return "Exteranal ID is taken";
            }
            else 
            {
                return null;
            }
                
        }

        private string CreateInternalId()
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
                id = CreateInternalId();
            }

            return id;
        }

        public async Task<string> SignIn(string login, string password)
        {


            var result = await _signInManager.PasswordSignInAsync(login, password, true, true);
            if (result.Succeeded)
            {
                return "Success";
            }
            else
            {
                return "Fail";
            }
        }

    }

    public class NewUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Repassword { get; set; }
        public string Email { get; set; }
        public string ExteranlId { get; set; }
    }
}
