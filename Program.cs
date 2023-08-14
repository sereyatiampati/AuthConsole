using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            Console.WriteLine("3. Admin Login");
            Console.WriteLine("4. Exit");
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
                        AdminLogin();
                        break;
                    case 4:
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Invalid entry. Please try again and enter a number between 1 and 4.");
            }
        }
    }

    // Other methods (RegisterUser, SaveUserData, ValidateUserLogin) remain the same...
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
            UserDashboard(username);
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

    static void AdminLogin()
    {
        Console.WriteLine("Admin Login");

        Console.Write("Enter Username: ");
        string username = Console.ReadLine();

        Console.Write("Enter Password: ");
        string password = Console.ReadLine();

        if (ValidateAdminLogin(username, password))
        {
            Console.WriteLine("Admin login successful.");
            AdminDashboard();
        }
        else
        {
            Console.WriteLine("Admin login failed. Invalid credentials.");
        }
    }

    static bool ValidateAdminLogin(string username, string password)
    {
        // Validate admin credentials from the same data file
        string filePath = "Data/users.txt";
        List<string> lines = File.ReadLines(filePath).ToList();

        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].StartsWith("Admin: " + username))
            {
                string storedPassword = lines[i + 1].Replace("Password: ", "");
                return password == storedPassword;
            }
        }
        return false;
    }

    static void UserDashboard(string username)
    {
        while (true)
        {
            Console.WriteLine("User Dashboard");
            Console.WriteLine("1. View Courses");
            Console.WriteLine("2. View Purchased Courses");
            Console.WriteLine("3. Logout");
            Console.Write("Choose an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ViewCourses();
                    break;
                case 2:
                    ViewPurchasedCourses(username);
                    break;
                case 3:
                    Console.WriteLine("Logging out...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void ViewCourses()
{
    Console.WriteLine("Available Courses:");

    // Sample courses
    List<Course> courses = new List<Course>
    {
        new Course { Name = "Introduction to Programming", Description = "Learn the basics of programming with this introductory course." },
        new Course { Name = "Web Development Fundamentals", Description = "Discover the essentials of web development and create your own websites." },
        new Course { Name = "Data Science for Beginners", Description = "Dive into the world of data science and explore data analysis techniques." }
        // Add more sample courses here if needed
    };

    foreach (var course in courses)
    {
        Console.WriteLine($"Course: {course.Name}");
        Console.WriteLine($"Description: {course.Description}");
        Console.WriteLine(new string('-', 20));
    }
}

class Course
{
    public string Name { get; set; }
    public string Description { get; set; }
}

    static void ViewPurchasedCourses(string username)
    {
        // Display purchased courses for the user
        string filePath = $"Data/{username}_purchased.txt";
        if (File.Exists(filePath))
        {
            Console.WriteLine("Purchased Courses:");
            // Add code to read and display purchased courses
        }
        else
        {
            Console.WriteLine("No purchased courses found.");
        }
    }

    static void AdminDashboard()
    {
        while (true)
        {
            Console.WriteLine("Admin Dashboard");
            Console.WriteLine("1. View Courses");
            Console.WriteLine("2. Add Course");
            Console.WriteLine("3. Update Course");
            Console.WriteLine("4. Delete Course");
            Console.WriteLine("5. View Course Analytics");
            Console.WriteLine("6. Logout");
            Console.Write("Choose an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ViewCourses();
                    break;
                case 2:
                    AddCourse();
                    break;
                case 3:
                    UpdateCourse();
                    break;
                case 4:
                    DeleteCourse();
                    break;
                case 5:
                    ViewCourseAnalytics();
                    break;
                case 6:
                    Console.WriteLine("Logging out...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void ViewCourseAnalytics()
    {
        string analyticsFilePath = "Data/analytics.txt";
        if (File.Exists(analyticsFilePath))
        {
            Console.WriteLine("Course Analytics:");
            // Add code to read and display course analytics (course name, number of purchases)
        }
        else
        {
            Console.WriteLine("No analytics data found.");
        }
    }

    static void AddCourse()
{
    Console.WriteLine("Add New Course");

    Console.Write("Enter Course Name: ");
    string courseName = Console.ReadLine();

    Console.Write("Enter Course Description: ");
    string courseDescription = Console.ReadLine();

    // Add the new course to the courses file
    string coursesFilePath = "Data/courses.txt";
    using (StreamWriter writer = new StreamWriter(coursesFilePath, true))
    {
        writer.WriteLine($"Course Name: {courseName}");
        writer.WriteLine($"Description: {courseDescription}");
        writer.WriteLine(new string('*', 20));
    }

    Console.WriteLine("Course added successfully.");
}

static void UpdateCourse()
{
    Console.WriteLine("Update Course");

    Console.Write("Enter Course Name to Update: ");
    string targetCourseName = Console.ReadLine();

    // Read existing courses from the file
    string coursesFilePath = "Data/courses.txt";
    List<string> lines = File.ReadLines(coursesFilePath).ToList();

    bool courseFound = false;

    for (int i = 0; i < lines.Count; i++)
    {
        if (lines[i].StartsWith("Course Name: " + targetCourseName))
        {
            Console.WriteLine("Enter New Course Name: ");
            string newCourseName = Console.ReadLine();

            Console.WriteLine("Enter New Course Description: ");
            string newCourseDescription = Console.ReadLine();

            // Update course information
            lines[i] = $"Course Name: {newCourseName}";
            lines[i + 1] = $"Description: {newCourseDescription}";

            courseFound = true;
            break;
        }
    }

    if (courseFound)
    {
        File.WriteAllLines(coursesFilePath, lines);
        Console.WriteLine("Course updated successfully.");
    }
    else
    {
        Console.WriteLine("Course not found.");
    }
}

static void DeleteCourse()
{
    Console.WriteLine("Delete Course");

    Console.Write("Enter Course Name to Delete: ");
    string targetCourseName = Console.ReadLine();

    // Read existing courses from the file
    string coursesFilePath = "Data/courses.txt";
    List<string> lines = File.ReadLines(coursesFilePath).ToList();

    bool courseFound = false;
    List<string> newLines = new List<string>();

    for (int i = 0; i < lines.Count; i++)
    {
        if (lines[i].StartsWith("Course Name: " + targetCourseName))
        {
            // Skip the lines associated with the target course
            i += 2;
            courseFound = true;
        }
        else
        {
            newLines.Add(lines[i]);
        }
    }

    if (courseFound)
    {
        File.WriteAllLines(coursesFilePath, newLines);
        Console.WriteLine("Course deleted successfully.");
    }
    else
    {
        Console.WriteLine("Course not found.");
    }
}

}
