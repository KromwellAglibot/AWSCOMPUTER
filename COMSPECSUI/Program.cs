using COMSPECSMODEL;
using COMSPECSDATA;
using COMSPECSBUSINESS;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ModelGetServices getServices = new ModelGetServices();

            var comp = getServices.GetAllUsers();

            foreach (var item in comp)
            {
                Console.WriteLine(item.Userid);
                Console.WriteLine(item.password);
            }

        }
    }
}
