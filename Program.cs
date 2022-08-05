using Microsoft.EntityFrameworkCore;
using UserDatabaseConsoleApp;

MainOutput();


static void MainOutput()
{
    while (true)
    {
        Console.WriteLine("1. Show Database Table");
        Console.WriteLine("2. Add User to Database Table");
        Console.WriteLine("3. Exit Program");
        int num = Convert.ToInt32(Console.ReadLine());

        switch (num)
        {
            case 1:
                ShowUserData();
                break;
            case 2:
                AddDataToDatabase();
                break;
            case 3:
                return;
                break;
            default:
                Console.WriteLine("Non Avaible Option...");
                break;
        }
    }
}

static void ShowUserData()
{
    using (var db = new UserDB())
    {
        Console.WriteLine("Data Retrieved From Database:");
        foreach (var user in db.Users)
        {
            Console.WriteLine("ID: " + user.ID + " Username: " + user.Username + " Password: " + user.Password);
        }
    }
}

static void AddDataToDatabase()
{
    string username, password;

    while (true)
    {
        Console.Write("Username: ");
        username = Console.ReadLine();
        Console.Write("Password: ");
        password = Console.ReadLine();

        if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
        {
            using (var db = new UserDB())
            {
                Console.WriteLine("Adding data to Database...");
                var user = new User();
                user.Username = username;
                user.Password = password;

                db.Add(user);

                db.SaveChanges();
            }
            break;
        }
        else
        {
            Console.WriteLine("Username or Password cannot be empty...");
        }
    }

    Console.WriteLine("Data Was Added to Database...");
}