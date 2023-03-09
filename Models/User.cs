using MongoDB.Bson.Serialization.Attributes;


namespace teamwork_1_todo_backend1.Models
{  
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string userID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public List<Todolist> todolist { get; set; }
    }

    public class Todolist
    {
        public string todoID { get; set; }
        public string todoTitle { get; set; }
        public bool isDone { get; set; }
    }

}
