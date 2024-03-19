using Domain;
namespace Repository
{
    public class DB
    {
        public List<User> userList { get; set; }
        public DB()
        {
            userList = new List<User>();
            userList.Add(new User("New"));

        }

       
    }
}