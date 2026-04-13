using Ramayan_gita_app.Models;
using System.Linq;

namespace Ramayan_gita_app.DataAccess
{
    public class UserDataAccess
    {
        private readonly ApplicationDbContext _context;

        public UserDataAccess(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Register(User user)
        {
            try
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                    return false;

                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User? Login(string email, string passwordHash)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == passwordHash);
            }
            catch
            {
                return null;
            }
        }
    }
}