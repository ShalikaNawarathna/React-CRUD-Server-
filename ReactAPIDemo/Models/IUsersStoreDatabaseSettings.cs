namespace ReactAPIDemo.Models
{
    public interface IUsersStoreDatabaseSettings 
    {
        string UsersDataCollectionName { get; set;  } 
        string ConnectionString { get; set; }
        string DatabaseName {  get; set; }
    }
}
