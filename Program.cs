using DatabaseConnectivity;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace DatabaseConnecivity;

public class Program
{
    public static void Main()
    {
        //Locations.GetLocations();

        //Countries.DeleteCountries("TH");
        //Countries.UpdateCountries("Thailand", "TH");
        Jobs.GetJobs();

        Jobs.GetByIdJobs("15");

        Console.WriteLine();
        Console.WriteLine();
        Jobs.GetJobs();
        //Locations.GetLocations();

        //Insert
        //Console.WriteLine("Masukkan Nama Region : ");
        //string inputRegion = Console.ReadLine();
        //SetRegions(inputRegion);

        //Update
        //UpdateRegions("South America", 7);

        //Delete
        //DeleteRegions(7);
        Console.WriteLine();
        //GetById

    }

    //Get Region

}