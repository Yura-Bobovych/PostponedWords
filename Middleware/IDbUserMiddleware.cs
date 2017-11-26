using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostponedWords.Models.Db;

namespace PostponedWords.Middleware
{
    public interface IDbUserMiddleware
    {
		bool UserExist(string email);
		Task<User> CreateUserAsync(string email, DateTime? date=null);
		User FindUser(string email);
		User FindUser(int id);
	}
}
