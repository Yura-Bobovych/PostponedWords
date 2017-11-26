using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostponedWords.Models.Db;

namespace PostponedWords.Middleware
{
    public class DbUserMiddleware:IDbUserMiddleware
    {
		#region properties
		private readonly PostroneWords_v2Context _context;

		#endregion

		public DbUserMiddleware(PostroneWords_v2Context context)
		{
			_context = context;
		}
		
		#region public
		public bool UserExist(string email)
		{
			if (_context.User.Where(u => u.Email == email).FirstOrDefault() != null) { return true; }
			return false;			
		}
		public async Task<User> CreateUserAsync(string email, DateTime? date=null)
		{
			
			if (!UserExist(email))
			{
				User newUser = new User();
				newUser.Email = email;
				if (date == null)
					date = DateTime.Now;
				newUser.RegistrationDate = date.Value;
				_context.User.Add(newUser);
				await _context.SaveChangesAsync();
				return newUser;
			}
			return null;
		}
		public User FindUser(string email)
		{
			return _context.User.Where(u => u.Email == email).FirstOrDefault();
		}
		public User FindUser(int id)
		{
			return  _context.User.Where(u => u.UserId == id).FirstOrDefault();
		}
		#endregion
	}
}
