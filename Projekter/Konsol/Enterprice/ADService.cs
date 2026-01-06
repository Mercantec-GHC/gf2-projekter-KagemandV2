using System;
using System.Net;
using System.DirectoryServices.Protocols;

namespace Enterprice
{
    public class ADService
    {
        public static string _server = "10.133.71.100";
        public static string _username = "adReader";
        public static string _password = "Merc1234!";
        public static string _domain = "mags.local";
        public static string Domain => _domain;
        public static string Server => _server;

        // Opret forbindelse til AD med credential + bind test
        public static LdapConnection ConnectGet()
        {
            try
            {
                Console.WriteLine("Connecting to Active Directory...");
                // Opret netværks credential for AD
                var credential = new NetworkCredential(
                    $"{_username}@{_domain}",
                    _password
                );
                // Opret LDAP forbindelse til AD serveren
                var connection = new LdapConnection(_server)
                {
                    Credential = credential,
                    AuthType = AuthType.Negotiate
                };

                // Bind for at teste forbindelsen
                connection.Bind();
                Console.WriteLine("Connected to AD successfully!");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                throw;
            }
        }

        // Menu tilgangspunkt
        public void Start()
        {
            while (true)
            {
                // Viser menuen
                ShowMenu();
                // Læser brugerens valg
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        //Henter alle brugere i AD
                        UserADService userService = new UserADService();
                        var users = userService.GetAllUsers();
                        Console.WriteLine("\n--- LIST OF ALL USERS ---");
                        users.ForEach(u => Console.WriteLine($"{u.UserName} - {u.FullName} - {u.Email}\n{u.LastLogin}"));
                        break;

                    case "2":
                        //Henter alle grupper i AD
                        var groups = GroupADService.GetAllGroups();
                        Console.WriteLine("\n--- LIST OF ALL GROUPS ---");
                        groups.ForEach(g => Console.WriteLine($"{g.Name} - {g.Description}"));
                        break;

                    case "3":
                        //Henter medlemmer af en gruppe
                        Console.Write("\nEnter group name: "); 
                        string gName = Console.ReadLine();

                        var members = GroupADService.GetMembersOfGroup(gName);
                        Console.WriteLine($"\n--- MEMBERS OF GROUP: {gName} ---");
                        members.ForEach(m => Console.WriteLine($"{m.UserName} - {m.FullName} - {m.Email}"));
                        break;

                    case "4":
                        // Add user to a group
                        UserADService.CreateUserInteractive();
                        break;

                    case "5":
                        Console.Write("Indtast brugernavn: ");
                        string username = Console.ReadLine();

                        var user = UserADService.GetUserLastLogin(username);

                        Console.WriteLine($"Bruger: {user.UserName}");
                        Console.WriteLine($"Navn: {user.FullName}");
                        Console.WriteLine($"Email: {user.Email}");

                        if (user.LastLogin.HasValue)
                            Console.WriteLine($"Sidste login: {user.LastLogin}");
                        else
                            Console.WriteLine("Ingen login-data fundet");

                        break;

                    case "6":
                        Console.Write("Indtast brugernavn for check-in: ");
                        string checkInUsername = Console.ReadLine();
                        Console.WriteLine("indtast adgangskode for check-in");
                        string checkInPassword = Console.ReadLine();
                        bool isAuthenticated = UserADService.AuthenticateUser(checkInUsername, checkInPassword);
                        if (isAuthenticated)
                        {
                            Console.WriteLine("Check-in successful!");
                        }
                        else
                        {
                            Console.WriteLine("Check-in failed: Invalid username or password.");
                        }

                        break;


                    case "7":
                        Console.Write("Indtast fulde navn: ");
                        string fullName = Console.ReadLine();
                        Console.Write("Indtast firma: ");
                        string company = Console.ReadLine();
                        Console.Write("Indtast besøgsårsag: ");
                        string visitReason = Console.ReadLine();
                        Console.Write("Indtast email (valgfrit): ");
                        string email = Console.ReadLine();
                        UserADService.CreateGuestContact("CN=Users,DC=mags,DC=local", fullName, company, visitReason, string.IsNullOrWhiteSpace(email) ? null : email);
                        Console.WriteLine("Gæstekontakt oprettet successfully.");
                        break;

                    case "8":
                        UserADService.ViewGuest();
                        break;


                    case "9":
                        return;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("1. Get all users");
            Console.WriteLine("2. Get all groups");
            Console.WriteLine("3. Get members of a group");
            Console.WriteLine("4. Create new user");
            Console.WriteLine("5. Get user login/check in time");
            Console.WriteLine("6. Check in");
            Console.WriteLine("7. Gæstebog");
            Console.WriteLine("8. See all guests");
            Console.WriteLine("9. Exit");
        }

     
    
      
    }

}


