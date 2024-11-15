namespace COMSPECSBUSINESS
{
    public class ModelValidationServices
    {

        ModelGetServices getservices = new ModelGetServices();

        public bool CheckIfUserExists(string Userid)
        {
            bool result = getservices.GetUser(Userid) != null;
            return result;
        }

        public bool CheckIfUsersExists(string Userid)
        {
            bool result = getservices.GetUser(Userid) != null;
            return result;
        }

    }
}