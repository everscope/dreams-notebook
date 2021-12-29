using System.Linq;

namespace DreamWeb.Models
{
    public interface IUserService
    {
        public string AddNewAccount(NewUser newUser);
    }

    public class UserService : IUserService
    {
        private DreamsContext _context;
        public UserAccount _user { get; set; }

        private char[] internalIdChars = "abcdefghigklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public UserService(DreamsContext context)
        {
            _context = context;
        }

        public string AddNewAccount(NewUser newUser)
        {
            if(CheckNewAccount(newUser) == null) {
                UserAccount userAccount = new UserAccount();
                userAccount.Nickname = newUser.Login;
                userAccount.Email = newUser.Email;
                userAccount.ExternalId = newUser.ExteranlId;
                userAccount.InternalId = CreateInternalId();
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
            else if (!(newAccount.Email.Contains("@") && newAccount.Email.Contains(".")))
            {
                return "Something is wrong with your email";
            }
            else if (_context.UserAccounts.
                    Any(p => p.Nickname == newAccount.Login))
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
