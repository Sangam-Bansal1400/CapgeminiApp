using Azure.Core;
using CapgAppLibrary;

namespace AuthenticationService.Infrastructure
{
    public interface IUserRepository
    {
        bool Authenticate(AuthenticationRequest request);
        User GetUserDetails(string email);
    }
    public class UserRepository : IUserRepository
    {
        UserDbContext _context;
        public UserRepository(UserDbContext context) => _context = context;
        public bool Authenticate(AuthenticationRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException("request", "cannot be null");
            }
            var item = _context.Users.FirstOrDefault(c => c.Email.Equals(request.Email) &&
            c.Password.Equals(request.Password));
            if (item is null)
            {
                throw new Exception("Requested Item not found.");
            }
            else
            {
                return true;
            }
        }
        public User GetUserDetails(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email required");
            }
            return _context.Users.FirstOrDefault(c => c.Email.Equals(email));
        }
    }
}
