
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReactAPIDemo.Models
{


    [BsonIgnoreExtraElements]
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }


        [BsonElement("age")]
        public string Age { get; set; } 

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("university")]
        public string University { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }



    }
}
