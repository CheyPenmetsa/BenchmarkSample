using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BusinessLogic.Data
{
    public class Customer
    {
        [BsonId]
        public string Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}
