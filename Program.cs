using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
         string appVersion = "1.0.0";
         string appAuthor = "Emily Tiampati";
        Console.WriteLine("User Registration and Login System Version {0} by {1}", appVersion, appAuthor);

        while (true)
        {
            Console.WriteLine("1. Register");
            Console.WriteLine("2. User Login");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
            
            try
            {
                int choice = int.Parse(Console.ReadLine());
                 switch (choice)
            {
                case 1:
                    RegisterUser();
                    break;
                case 2:
                    UserLogin();
                    break;
                case 3:
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            }
            catch (System.Exception)
            {
                
                System.Console.WriteLine("Invalid entry. Please try again and enter a number between 1 and 3.");
            }

           
        }
    }

    static void RegisterUser()
{
    Console.WriteLine("User Registration");

    Console.Write("Enter Your Username: ");
    string username = Console.ReadLine();

    Console.Write("Enter a Password: ");
    string password = Console.ReadLine();

    Console.Write("Enter Password again to confirm: ");
    string passwordConfirmation = Console.ReadLine();

    if (password == passwordConfirmation)
    {
        User newUser = new User
        {
            Username = username,
            Password = password,
            PasswordConfirmation = passwordConfirmation
        };

        SaveUserData(newUser);

        Console.WriteLine("Registration successful.");
    }
    else
    {
        Console.WriteLine("Password and password confirmation do not match. Registration failed.");
    }
}

static void SaveUserData(User user)
{
    string filePath = "Data/users.txt";

    using (StreamWriter writer = new StreamWriter(filePath, true))
    {
        writer.WriteLine($"Username: {user.Username}");
        writer.WriteLine($"Password: {user.Password}");
        writer.WriteLine($"PasswordConfirmed: {user.PasswordConfirmation}");
        writer.WriteLine(new string('*', 20));
    }
}


   static void UserLogin()
{
    Console.WriteLine("User Login");

    Console.Write("Enter Username: ");
    string username = Console.ReadLine();

    Console.Write("Enter Password: ");
    string password = Console.ReadLine();

    if (ValidateUserLogin(username, password))
    {
        Console.WriteLine("User login successful.");
    }
    else
    {
        Console.WriteLine("User login failed. Invalid credentials.");
    }
}

static bool ValidateUserLogin(string username, string password)
{
    string filePath = "Data/users.txt";
    List<string> lines = File.ReadLines(filePath).ToList();

    for (int i = 0; i < lines.Count; i++)
    {
        if (lines[i].StartsWith("Username: " + username))
        {
            string storedPassword = lines[i + 1].Replace("Password: ", "");
            return password == storedPassword;
        }
    }
    return false;
}

}
