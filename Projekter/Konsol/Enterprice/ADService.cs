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
                        users.ForEach(u => Console.WriteLine($"{u.UserName} - {u.FullName} - {u.Email}"));
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
                        return;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("1. Get all users");
            Console.WriteLine("2. Get all groups");
            Console.WriteLine("3. Get members of a group");
            Console.WriteLine("4. Create new user");
            Console.WriteLine("5. Exit");
        }

     
    
      
    }

}


