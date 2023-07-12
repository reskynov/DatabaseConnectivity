using DatabaseConnectivity;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace DatabaseConnecivity;

public class Program
{
    public static void Main()
    {
        MainMenu();
    }

    public static void MainMenu()
    {
        bool backToMain = true;
        while(backToMain)
        {
            backToMain = false;
            Console.WriteLine("== DATABASE EKY BERSAMA ==");
            Console.WriteLine("== Menu Database HR == ");
            Console.WriteLine("1. Employees");
            Console.WriteLine("2. Departments");
            Console.WriteLine("3. Jobs");
            Console.WriteLine("4. Histories");
            Console.WriteLine("5. Locations");
            Console.WriteLine("6. Countries");
            Console.WriteLine("7. Regions");
            Console.WriteLine("8. Exit");
            Console.Write("Input Menu : ");
            string inputMainMenu = Console.ReadLine();
            switch (inputMainMenu)
            {
                case "1":
                    Console.Clear();
                    backToMain = SubMenu("employees", backToMain);
                    break;
                case "2":
                    Console.Clear();
                    backToMain = SubMenu("departments", backToMain);
                    break;
                case "3":
                    Console.Clear();
                    backToMain = SubMenu("jobs", backToMain);
                    break;
                case "4":
                    Console.Clear();
                    backToMain = SubMenu("histories", backToMain);
                    break;
                case "5":
                    Console.Clear();
                    backToMain = SubMenu("locations", backToMain);
                    break;
                case "6":
                    Console.Clear();
                    backToMain = SubMenu("countries", backToMain);
                    break;
                case "7":
                    Console.Clear();
                    backToMain = SubMenu("regions", backToMain);
                    break;
                case "8":
                    System.Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Invalid Input!!!");
                    backToMain = true;
                    break;
            }
        }
    }

    public static bool SubMenu(string menuName, bool backToMain)
    {
        backToMain = false;
        bool backToSub = true;
        while (backToSub)
        {
            Console.WriteLine("== MENU " + menuName.ToUpper() + " ==");
            Console.WriteLine("1. Show All Data");
            Console.WriteLine("2. Search Data");
            Console.WriteLine("3. Insert Data");
            Console.WriteLine("4. Update Data");
            Console.WriteLine("5. Delete Data");
            Console.WriteLine("6. Back");
            Console.Write("Input Menu : ");
            string inputSubMenu = Console.ReadLine();
            switch (inputSubMenu)
            {

                //CASE SHOW ALL DATA
                case "1":
                    Console.Clear();
                    if (menuName.Equals("employees"))
                    {
                        Employees.GetEmployees();
                    }
                    else if (menuName.Equals("departments"))
                    {
                        Departments.GetDepartments();
                    }
                    else if (menuName.Equals("jobs"))
                    {
                        Jobs.GetJobs();
                    }
                    else if (menuName.Equals("histories"))
                    {
                        Histories.GetHistories();
                    }
                    else if (menuName.Equals("locations"))
                    {
                        Locations.GetLocations();
                    }
                    else if (menuName.Equals("countries"))
                    {
                        Countries.GetCountries();
                    }
                    else if (menuName.Equals("regions"))
                    {
                        Regions.GetRegions();
                    }
                    Console.ReadKey();
                    Console.Clear();
                    break;


                //CASE SEARCH DATA
                case "2":
                    if (menuName.Equals("employees"))
                    {
                        Console.Write("\nInput ID Employee to Search : ");
                        int idEmployee = Int32.Parse(Console.ReadLine());
                        Employees.GetByIdEmployees(idEmployee);
                        Console.WriteLine();
                    }
                    else if (menuName.Equals("departments"))
                    {
                        Console.Write("\nInput ID Department to Search : ");
                        int idDepartment = Int32.Parse(Console.ReadLine());
                        Departments.GetByIdDepartments(idDepartment);
                        Console.WriteLine();
                    }
                    else if (menuName.Equals("jobs"))
                    {
                        Console.Write("\nInput ID Jobs to Search : ");
                        string idJobs = Console.ReadLine();
                        Jobs.GetByIdJobs(idJobs);
                        Console.WriteLine();
                    }
                    else if (menuName.Equals("histories"))
                    {
                        Console.Write("\nInput ID Employee to Search : ");
                        int idEmployee = Int32.Parse(Console.ReadLine());
                        Histories.GetByIdHistories(idEmployee);
                        Console.WriteLine();
                    }
                    else if (menuName.Equals("locations"))
                    {
                        Console.Write("\nInput ID Location to Search : ");
                        int idLocations = Int32.Parse(Console.ReadLine());
                        Locations.GetByIdLocations(idLocations);
                        Console.WriteLine();
                    }
                    else if (menuName.Equals("countries"))
                    {
                        Console.Write("\nInput Countries Name to Search : ");
                        string idCountry = Console.ReadLine();
                        Countries.GetByNameCountries(idCountry);
                        Console.WriteLine();
                    }
                    else if (menuName.Equals("regions"))
                    {
                        Console.Write("\nInput ID Region to Search : ");
                        int ideRegions = Int32.Parse(Console.ReadLine());
                        Regions.GetByIdRegions(ideRegions);
                        Console.WriteLine();
                    }
                    Console.ReadKey();
                    Console.Clear();
                    break;

                //CASE INSERT DATA
                case "3":
                    if (menuName.Equals("employees"))
                    {
                        Console.Write("\nInput Employee's ID: ");
                        int id = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Employee's First Name : ");
                        string fname = Console.ReadLine();

                        Console.Write("Input Employee's Last Name: ");
                        string lname = Console.ReadLine();

                        Console.Write("Input Employee's Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Input Employee's Phone Number: ");
                        string phone = Console.ReadLine();

                        Console.Write("Input Employee's Salary : $");
                        int salary = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Employee's Commisiion PCT : ");
                        double commis = Double.Parse(Console.ReadLine());

                        Console.Write("Input Employee's Manager ID : ");
                        int manId = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Employee's Job ID : ");
                        string jobId = Console.ReadLine();

                        Console.Write("Input Employee's Department ID : ");
                        int deId = Int32.Parse(Console.ReadLine());

                        Employees.SetEmployees(id, fname, lname, email, phone, salary, commis, manId, jobId, deId);
                    }
                    else if (menuName.Equals("departments"))
                    {
                        Console.Write("\nInput Department ID: ");
                        int id = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Department Name : ");
                        string name = Console.ReadLine();

                        Console.Write("Input Location ID: ");
                        int idLoc = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Manager ID: ");
                        int idMan = Int32.Parse(Console.ReadLine());

                        Departments.SetDepartments(id, name, idLoc, idMan);
                    }
                    else if (menuName.Equals("jobs"))
                    {
                        Console.Write("\nInput Jobs ID: ");
                        string id = Console.ReadLine();

                        Console.Write("Input Jobs Title : ");
                        string title = Console.ReadLine();

                        Console.Write("Input Min Salary: $");
                        int min = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Max Salary: $");
                        int max = Int32.Parse(Console.ReadLine());

                        Jobs.SetJobs(id, title, min, max);
                    }
                    else if (menuName.Equals("histories"))
                    {
                        Console.Write("\nInput Employee ID: ");
                        int emId = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Department ID : ");
                        int deId = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Jobs ID: ");
                        string jobId = Console.ReadLine();

                        Histories.SetHistories(emId, deId, jobId);
                    }
                    else if (menuName.Equals("locations"))
                    {
                        Console.Write("\nInput Location ID: ");
                        int locId = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Street Address : ");
                        string street = Console.ReadLine();

                        Console.Write("Input Postal Code : ");
                        string postal = Console.ReadLine();

                        Console.Write("Input City : ");
                        string city = Console.ReadLine();

                        Console.Write("Input State Province : ");
                        string province = Console.ReadLine();

                        Console.Write("Input State Country ID : ");
                        string countryId = Console.ReadLine();

                        Locations.SetLocations(locId, street, postal, city, province, countryId);
                    }
                    else if (menuName.Equals("countries"))
                    {
                        Console.Write("\nInput Country ID: ");
                        string countryId = Console.ReadLine();

                        Console.Write("Input Country Name : ");
                        string name = Console.ReadLine();

                        Console.Write("Input Region ID : ");
                        int regionId = Int32.Parse(Console.ReadLine());

                        Countries.SetCountries(countryId, name, regionId);
                    }
                    else if (menuName.Equals("regions"))
                    {
                        Console.Write("Input Region Name : ");
                        string name = Console.ReadLine();

                        Regions.SetRegions(name);
                    }
                    Console.ReadKey();
                    Console.Clear();
                    break;

                //CASE UPDATE DATA
                case "4":
                    if (menuName.Equals("employees"))
                    {
                        Console.Write("\nInput Employee's ID: ");
                        int id = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Employee's First Name : ");
                        string fname = Console.ReadLine();

                        Console.Write("Input Employee's Last Name: ");
                        string lname = Console.ReadLine();

                        Console.Write("Input Employee's Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Input Employee's Phone Number: ");
                        string phone = Console.ReadLine();

                        Console.Write("Input Employee's Salary : $");
                        int salary = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Employee's Commisiion PCT : ");
                        double commis = Double.Parse(Console.ReadLine());

                        Console.Write("Input Employee's Manager ID : ");
                        int manId = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Employee's Job ID : ");
                        string jobId = Console.ReadLine();

                        Console.Write("Input Employee's Department ID : ");
                        int deId = Int32.Parse(Console.ReadLine());

                        Employees.UpdateEmployees(id, fname, lname, email, phone, salary, commis, manId, jobId, deId);
                    }
                    else if (menuName.Equals("departments"))
                    {
                        Console.Write("\nInput Department ID: ");
                        int id = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Department Name : ");
                        string name = Console.ReadLine();

                        Console.Write("Input Location ID: ");
                        int idLoc = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Manager ID: ");
                        int idMan = Int32.Parse(Console.ReadLine());

                        Departments.UpdateDepartments(id, name, idLoc, idMan);
                    }
                    else if (menuName.Equals("jobs"))
                    {
                        Console.Write("\nInput Jobs ID: ");
                        string id = Console.ReadLine();

                        Console.Write("Input Jobs Title : ");
                        string title = Console.ReadLine();

                        Console.Write("Input Min Salary: $");
                        int min = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Max Salary: $");
                        int max = Int32.Parse(Console.ReadLine());

                        Jobs.UpdateJobs(id, title, min, max);
                    }
                    else if (menuName.Equals("histories"))
                    {
                        Console.Write("\nInput Employee ID: ");
                        int emId = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Department ID : ");
                        int deId = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Jobs ID: ");
                        string jobId = Console.ReadLine();

                        Histories.UpdateHistories(emId, deId, jobId);
                    }
                    else if (menuName.Equals("locations"))
                    {
                        Console.Write("\nInput Location ID: ");
                        int locId = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Street Address : ");
                        string street = Console.ReadLine();

                        Console.Write("Input Postal Code : ");
                        string postal = Console.ReadLine();

                        Console.Write("Input City : ");
                        string city = Console.ReadLine();

                        Console.Write("Input State Province : ");
                        string province = Console.ReadLine();

                        Console.Write("Input State Country ID : ");
                        string countryId = Console.ReadLine();

                        Locations.UpdateLocations(locId, street, postal, city, province, countryId);
                    }
                    else if (menuName.Equals("countries"))
                    {
                        Console.Write("\nInput Country ID: ");
                        string countryId = Console.ReadLine();

                        Console.Write("Input Country Name : ");
                        string name = Console.ReadLine();

                        Console.Write("Input Region ID : ");
                        int regionId = Int32.Parse(Console.ReadLine());

                        Countries.UpdateCountries(countryId, name, regionId);
                    }
                    else if (menuName.Equals("regions"))
                    {
                        Console.Write("Input ID Region : ");
                        int id = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Region Name : ");
                        string name = Console.ReadLine();

                        Regions.UpdateRegions(name, id);
                    }
                    Console.ReadKey();
                    Console.Clear();
                    break;

                //CASE DELETE DATA
                case "5":
                    if (menuName.Equals("employees"))
                    {
                        Console.Write("\nInput Employee ID : ");
                        int id = Int32.Parse(Console.ReadLine());

                        Employees.DeleteEmployees(id);
                    }
                    else if (menuName.Equals("departments"))
                    {
                        Console.Write("\nInput Department ID : ");
                        int id = Int32.Parse(Console.ReadLine());

                        Departments.DeleteDepartments(id);
                    }
                    else if (menuName.Equals("jobs"))
                    {
                        Console.Write("\nInput Job ID : ");
                        string id = Console.ReadLine();

                        Jobs.DeleteJobs(id);
                    }
                    else if (menuName.Equals("histories"))
                    {
                        Console.Write("\nInput Employee ID: ");
                        int emId = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Department ID : ");
                        int deId = Int32.Parse(Console.ReadLine());

                        Console.Write("Input Jobs ID: ");
                        string jobId = Console.ReadLine();

                        Histories.DeleteHistories(emId, deId, jobId);
                    }
                    else if (menuName.Equals("locations"))
                    {
                        Console.Write("\nInput Location ID : ");
                        int id = Int32.Parse(Console.ReadLine());

                        Locations.DeleteLocations(id);
                    }
                    else if (menuName.Equals("countries"))
                    {
                        Console.Write("\nInput Country ID : ");
                        string id = Console.ReadLine();

                        Countries.DeleteCountries(id);
                    }
                    else if (menuName.Equals("regions"))
                    {
                        Console.Write("\nInput Region ID : ");
                        int id = Int32.Parse(Console.ReadLine());

                        Locations.DeleteLocations(id);
                    }
                    Console.ReadKey();
                    Console.Clear();
                    break;

                case "6":
                    backToMain = true;
                    backToSub = false;
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Invalid Input!!!");
                    backToSub = true;
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }
        return backToMain;
    }
}