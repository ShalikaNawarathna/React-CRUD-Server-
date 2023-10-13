using ReactAPIDemo.Models;

namespace ReactAPIDemo.Repositories
{
    public interface IUserRepo
    {
       
            Task<List<Users>> Get();

            Task<Users> Get(string email);

            Task<Users> Create(Users user);

            Task<Users> Update(Users user);

            Task Remove(string email);

            Task<Users> Login(string email, string password);
        
    }
}
