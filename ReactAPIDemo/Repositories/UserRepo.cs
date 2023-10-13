using ReactAPIDemo.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;


namespace ReactAPIDemo.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly IMongoCollection<Users> _collection;
        private readonly IOptions<UsersStoreDatabaseSettings> _dbSettings;

        public UserRepo(IMongoClient mongoClient, IUsersStoreDatabaseSettings userStoreDBSettings, IOptions<UsersStoreDatabaseSettings> dbSettings)
        {
            var database = mongoClient.GetDatabase(userStoreDBSettings.DatabaseName);
            _collection = database.GetCollection<Users>(userStoreDBSettings.UsersDataCollectionName);
            _dbSettings = dbSettings;
        }

        public async Task<List<Users>> Get()
        {
            var getUsers = await _collection.Find(user => true).ToListAsync();
            return getUsers;

        }
      
        public async Task<Users> Get(string email)
        {
            var getUser = await _collection.Find(user => user.Email == email).FirstOrDefaultAsync();  
           if(getUser == null)
            {
                throw new ArgumentNullException(nameof(getUser));
            }
           return getUser;
        }
        /*public async Task<Users> Create(Users user)
        {
            var exitingUser = await _collection.Find(u => u.Name == user.Name).FirstOrDefaultAsync();
            if(exitingUser == null)
            {
                await _collection.InsertOneAsync(user);
                return user;
            }
            exitingUser.Name = "Exits";
            return exitingUser;
        }*/
        public async Task<Users> Create(Users user)
        {
            var existingUser = await _collection.Find(u => u.Email == user.Email).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                // A user with the same Email already exists. 
                throw new Exception("User with the same Email already exists.");
            }
           if (user.Id == null)
            {
                user.Id = ObjectId.GenerateNewId().ToString();
            }


            await _collection.InsertOneAsync(user);
            return user;
        }

        public async Task<Users> Update(Users user)
        {
            var updatedUser =  Builders<Users>.Filter.Eq(u => u.Email, user.Email);
            var update = Builders<Users>.Update
                .Set(u => u.Name, user.Name)
                .Set(u => u.Email, user.Email)
                .Set(u => u.Age, user.Age)
                .Set(u => u.Gender, user.Gender)
                .Set(u => u.University, user.University);

            var isUpdated = new FindOneAndUpdateOptions<Users>
            {
                ReturnDocument = ReturnDocument.After
            };

           return await _collection.FindOneAndUpdateAsync(updatedUser, update, isUpdated);
        }
        public async Task Remove(string email)
        {
            try
            {
                var deletingUser = Builders<Users>.Filter.Eq("Email", email);
                await _collection.DeleteOneAsync(deletingUser);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

       public  async Task<Users> Login(string email, string password)
        {
            var user = await _collection.Find(user => user.Email == email).FirstOrDefaultAsync();

            if(user == null)
            {
                throw new Exception("User not found");
            }
            if(user.Password != password)
            {
                throw new Exception("Invalid password");
            }
            return user;
        }
    }
}
