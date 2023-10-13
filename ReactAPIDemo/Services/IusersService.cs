using Microsoft.AspNetCore.Mvc;
using ReactAPIDemo.Models;

namespace ReactAPIDemo.Services
{
    public interface IusersService
    {
        Task<List<Users>> Get();
        Task<Users> Get(string email);
        Task<Users> Create(Users user);
        Task<Users> Update(Users user);
        Task Remove(string email);

        Task<Users> Login(string email, string password);
    }
}
