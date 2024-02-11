using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Admin;
using API.Helpers;

namespace API.Repository
{
    public interface IUserService
    {
        Task<MstrAgents> Authenticate(string username, string password);
        Task<IEnumerable<MstrAgents>> GetAll();
    }

    public class UserService : IUserService
    {    
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
         private List<MstrAgents> _users = new List<MstrAgents>();
        // {
        //     new MstrAgents { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        // };

        public async Task<MstrAgents> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.cAgentName == username && x.cPassword == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return user.WithoutPassword();
        }

        public async Task<IEnumerable<MstrAgents>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}
