
using COMSPECSMODEL;    

namespace COMSPECSDATA
{
    public class CompData
    {
        List<User> users;
        SqlDbData sqlData;
        public CompData()
        {
            users = new List<User>();
            sqlData = new SqlDbData();

            //UserFactory _userFactory = new UserFactory();
            //users = _userFactory.GetDummyUsers();
        }

        public List<User> GetUsers()
        {
            users = sqlData.GetUsers();
            //users = sqlData.GetUsers();
            return users;
        }

        public int AddModel(User users)
        {
            return sqlData.AddModel(users.Userid, users.password);
        }

        public int UpdateModel(User users)
        {
            return sqlData.UpdateModel(users.Userid, users.password);
        }

        public int DeleteModel(User users)
        {
            return sqlData.DeleteModel(users.Userid,users.password);
        }
    }
}