using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace DreamWeb.Models
{
    public interface IUserService
    {
        public string CreateNewAccount(NewUser newUser);
        public NewUser GetNewUser();
        public ClaimsPrincipal GetSignInClaims(string login, string password);
    }

    public class UserService : IUserService
    {
        private DreamsContext _context;
        private UserAccount _user;
        private static NewUser _userNew;

        private char[] internalIdChars = "abcdefghigklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public UserService(DreamsContext context)
        {
            _context = context;
        }

        public NewUser GetNewUser()
        {
            return _userNew;
        }

        public string CreateNewAccount(NewUser newUser)
        {
            _userNew = newUser;
            if(CheckNewAccount(newUser) == null) {
                UserAccount userAccount = new UserAccount();
                userAccount.Login = newUser.Login;
                userAccount.Email = newUser.Email;
                userAccount.ExternalId = newUser.ExteranlId;
                userAccount.Password = newUser.Password;

                userAccount.InternalId = CreateInternalId();
                userAccount.CreationTime = DateTime.Now;

                _context.UserAccounts.Add(userAccount);
                _context.SaveChanges();

                return null;
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
                    Any(p => p.Login == newAccount.Login))
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
               Any(p => p.InternalId == id))
            {
                id = CreateInternalId();
            }

            return id;
        }

        public ClaimsPrincipal GetSignInClaims(string login, string password)
        {
            var p = _context.UserAccounts.First(p => p.Login == login);
            bool b = _context.UserAccounts.Any(p => p.Login == login);
            bool b1 = _context.UserAccounts.First(p => p.Login == login).Password == password;
            var bv = _context.UserAccounts.First(p => p.Login == login).Password;
            if (_context.UserAccounts.Any(p => p.Login == login) && _context.UserAccounts.First(p => p.Login == login).Password == password)
            {
                UserAccount currentUser = _context.UserAccounts.First(p => p.Login == login);

                var claims = new List<Claim>();
                claims.Add(new Claim("nickname", login));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, login));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return claimsPrincipal;
            }
            else
            {
                return null;
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
