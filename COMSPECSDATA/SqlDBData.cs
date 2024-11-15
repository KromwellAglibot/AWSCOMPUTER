using COMSPECSMODEL;
using System.Data.SqlClient;

namespace COMSPECSDATA
{
    public class SqlDbData 
    {

        string connectionString
         = "Data Source =KROMWELL; Initial Catalog = COMSPECSUI; Integrated Security = True;";
        // "Server = tcp:20.255.250.13,1433; Database = COMSPECSUI; User Id = sa; Password = Kromwell1!";
        SqlConnection sqlConnection;

        public SqlDbData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public List<User> GetUsers()
        {
            string selectStatement = "SELECT Userid, password FROM Comp";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            List<User> model = new List<User>();

            SqlDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                string Userid = reader["Userid"].ToString();
                string password = reader["password"].ToString();

                User readModel = new User();
                readModel.Userid = Userid;
                readModel.password = password;

                model.Add(readModel);
            }

            sqlConnection.Close();

            return model;
        }

        public int AddModel(string Userid, string password)
        {
            int success;

            string insertStatement = "INSERT INTO Comp VALUES (@Userid, @password)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@Userid", Userid);
            insertCommand.Parameters.AddWithValue("@password", password);
            sqlConnection.Open();

            success = insertCommand.ExecuteNonQuery();
            //success = insertCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return success;
        }

        public int UpdateModel(string Userid, string password)
        {
            int success;

            string updateStatement = $"UPDATE Comp SET Userid = @Userid WHERE password = @password";
            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            sqlConnection.Open();

            updateCommand.Parameters.AddWithValue("@Userid", Userid);
            updateCommand.Parameters.AddWithValue("@password", password);

            success = updateCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return success;
        }

        public int DeleteModel(string Userid, string password)
        {
            int success;

            string deleteStatement = $"DELETE FROM Comp WHERE Userid = @Userid";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
            sqlConnection.Open();

            deleteCommand.Parameters.AddWithValue("@Userid", Userid);
           

            success = deleteCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return success;
        }
    }
}