﻿using DatabaseConnectivity;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace DatabaseConnecivity;

public class Program
{
    public static void Main()
    {
        Countries.GetCountries();

        Countries.SetCountries("TH", "Thailand", 1);

        Countries.GetCountries();

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