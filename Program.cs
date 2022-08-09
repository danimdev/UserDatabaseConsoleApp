using Microsoft.EntityFrameworkCore;
using UserDatabaseConsoleApp;

MainOutput();


static void MainOutput()
{
    while (true)
    {
        Console.WriteLine("1. Show Database Table");
        Console.WriteLine("2. Add User to Database Table");
        Console.WriteLine("3. Change Data From ID");
        Console.WriteLine("4. Delete Entry With ID");
        Console.WriteLine("5. Filter Data in Database");
        Console.WriteLine("6. Exit Program");
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
                ChangeDataFromID();
                break;
            case 4:
                DeleteEntryWithID();
                break;
            case 5:
                GiveListInAnorderedOutput();
                break;
            case 6:
                return;
            default:
                Console.WriteLine("Non Avaible Option...");
                break;
        }
    }
}

static void DeleteEntryWithID()
{
    Console.Write("ID: ");

    string id = Console.ReadLine();

    int numericValue;
    bool isNumber = int.TryParse(id, out numericValue);

    if (isNumber)
    {
        using (var context = new UserDB())
        {
            var user = context.Users.Find(numericValue);

            if(user != null)
            {
                context.Users.Remove(user);
            }
            else
            {
                Console.WriteLine("User Does Not Exist in the Database...");
                return;
            }

            context.SaveChanges();
        }
    }

    Console.WriteLine("User Account with id: " + id + " was deleted.");
}

static void GiveListInAnorderedOutput()
{
    using(var context = new UserDB())
    {
        var result = from user in context.Users where user.ID > 0 && user.ID < 3 select user;

        foreach (var item in result)
        {
            Console.WriteLine("ID: " + item.ID + " Username: " + item.Username + " Password: " + item.Password);
        }
    }
}

static void ChangeDataFromID()
{
    Console.Write("ID: ");

    string id = Console.ReadLine();

    int numericValue;
    bool isNumber = int.TryParse(id, out numericValue);

    if (isNumber)
    {
        Console.Write("Username: ");
        string newUsername = Console.ReadLine();
        Console.Write("Password: ");
        string newPassword = Console.ReadLine();

        if(string.IsNullOrEmpty(newUsername) && string.IsNullOrEmpty(newPassword))
        {
            return;
        }
        else
        {
            using (var context = new UserDB())
            {
                var User = context.Users.Find(numericValue);
                User.Username = newUsername;
                User.Password = newPassword;
                context.SaveChanges();
                Console.WriteLine("Changes Was Saved and Applied...");
            }
        }
    }
    else
    {
        Console.WriteLine(id + " is not an Valide ID");
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
    Console.WriteLine("");
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