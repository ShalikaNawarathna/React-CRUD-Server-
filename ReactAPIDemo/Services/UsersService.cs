using ReactAPIDemo.Models;
using MongoDB.Driver;
using ReactAPIDemo.Repositories;
namespace ReactAPIDemo.Services
{
    public class UsersService : IusersService
    {

        private readonly IUserRepo _userRepo;
        public UsersService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async  Task<List<Users>> Get()
        {
            return await  _userRepo.Get();
        }
        public async Task<Users> Get(string email)
        {
            return await _userRepo.Get(email);
        }
        public async Task<Users> Create(Users user)
        {
           return await _userRepo.Create(user);
        }
        public async Task<Users> Update(Users user)
        {
            return await _userRepo.Update(user);
        }
        public async Task Remove(string email)
        {
              await _userRepo.Remove(email);
        }

        public async Task<Users> Login(string email, string password)
        {
           return  await _userRepo.Login(email, password);
        }
    }
}
