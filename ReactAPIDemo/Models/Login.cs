using MongoDB.Bson.Serialization.Attributes;

namespace ReactAPIDemo.Models
{
    public class Login
    {
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

     }
}
