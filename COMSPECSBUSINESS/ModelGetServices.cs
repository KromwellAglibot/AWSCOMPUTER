using COMSPECSDATA;
using COMSPECSMODEL;

namespace COMSPECSBUSINESS
{
    public class ModelGetServices
    {
        public List<User> GetAllUsers()
        {
            CompData compData = new CompData();

            return compData.GetUsers();
            //return compData.GetUsers();

        }

        public User GetUsers(string Userid)
        {
            User foundUser = new User();

            foreach (var users in GetAllUsers())
            {
                if (users.Userid == Userid)
                {
                    foundUser = users;
                }
            }

            return foundUser;
        }

        public User GetUser(string Userid)
        {
            User foundUser = new User();

            foreach (var users in GetAllUsers())
            {
                if (users.Userid == Userid)
                {
                    foundUser = users;
                }
            }

            return foundUser;
        }
    }
}
