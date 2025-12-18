using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace Enterprice
{
  
    public class UserADService
    {
        public  List<ADuser> GetAllUsers()
        {
            Console.WriteLine("Getting all users");
            // Opret en tom liste til at gemme alle AD brugere
            var users = new List<ADuser>();
            // Opret forbindelse til Active Directory
            using (var connection = ADService.ConnectGet())
            {
                // Definer søgningen:
                // - Hvor skal vi søge: i "mags.local" domænet
                // - Hvad søger vi efter: alle objekter af typen "user"
                // - Hvilke informationer vil vi have: 
                // - brugernavn (sAMAccountName) og fulde navn (displayName)
                var searchRequest = new SearchRequest(
                    "DC=mags,DC=local", // Søg i dette domæne
                    "(objectClass=user)", // Find alle brugere
                    SearchScope.Subtree, // Søg i hele domænet
                    "sAMAccountName", // Brugernavn
                    "displayName", // Fulde navn
                    "mail", // Email
                    "lastLogon" // Sidste login tidspunkt

                );
                try
                {
                    // Udfør søgningen
                    var response = (SearchResponse)connection.SendRequest(searchRequest);
                    Console.WriteLine($"Søgningen returnerede {response.Entries.Count} brugere.");
                    // For hver bruger vi finder
                    foreach (SearchResultEntry bruger in response.Entries)
                    {
                        // Opret et nyt ADUser objekt med informationerne
                        DateTime? lastLogin = null;

                        if (bruger.Attributes.Contains("lastLogon"))
                        {
                            var rawValue = bruger.Attributes["lastLogon"][0];

                            if (rawValue != null && long.TryParse(rawValue.ToString(), out long fileTime))
                            {
                                lastLogin = DateTime.FromFileTimeUtc(fileTime);
                            }
                        }


                        var nyBruger = new ADuser
                        {
                            UserName = bruger.Attributes["sAMAccountName"]?[0]?.ToString() ?? "N/A",
                            FullName = bruger.Attributes["displayName"]?[0]?.ToString() ?? "N/A",
                            Email = bruger.Attributes["mail"]?[0]?.ToString() ?? "N/A",
                            LastLogin = lastLogin
                        };
                        // Tilføj brugeren til vores liste
                        users.Add(nyBruger);
                        Console.WriteLine($"Tilføjet bruger: {nyBruger.UserName} - {nyBruger.FullName}");
                    }
                    Console.WriteLine("Alle brugere er nu hentet og tilføjet til listen.");
                  
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fejl under hentning af brugere: {ex.Message}");
                }
                return users;

            }

        }

        // ---------------------------------------------------------------
        // INTERAKTIV BRUGEROPRETTELSE (bruges fra menuen)
        // ---------------------------------------------------------------
        public static void CreateUserInteractive()
        {
            Console.WriteLine("=== CREATE NEW AD USER ===");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("First name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            try
            {
                CreateUser("CN=Users,DC=mags,DC=local", username, password, firstName, lastName, email);
                Console.WriteLine("User created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------
        // SELVE LDAP-OPRETTELSEN
        // ---------------------------------------------------------------
        public static void CreateUser(
            string ouPath,
            string username,
            string password,
            string firstName,
            string lastName,
            string email)
        {
            var credential = new NetworkCredential("CRUD",")e=4To!3@(SKLnCPWLz'[8");

            var connection = new LdapConnection("10.133.71.100")
            {
                Credential = credential,
                AuthType = AuthType.Negotiate,
                SessionOptions =
            {
                // LDAPS required for unicodePwd
                SecureSocketLayer = false 
            }
            };

            // ConnectGet + bind
            connection.Bind();

            // Define DN path where user will be created
            string dn = $"CN={firstName} {lastName},{ouPath}";

            // Create the user object (disabled)
            var addRequest = new AddRequest(
                dn,
                new DirectoryAttribute("objectClass", "user"),
                new DirectoryAttribute("sAMAccountName", username),
                new DirectoryAttribute("userPrincipalName", $"{username}@mags.local"),
                new DirectoryAttribute("givenName", firstName),
                new DirectoryAttribute("sn", lastName),
                new DirectoryAttribute("displayName", $"{firstName} {lastName}"),
                new DirectoryAttribute("mail", email),
                new DirectoryAttribute("userAccountControl", "514") // disabled
            );

            connection.SendRequest(addRequest);

            // Set password (UNICODE + \" \")
            string quotedPassword = $"\"{password}\"";
            byte[] pwdBytes = Encoding.Unicode.GetBytes(quotedPassword);

            var passwordRequest = new ModifyRequest(
                dn,
                DirectoryAttributeOperation.Replace,
                "unicodePwd",
                pwdBytes
            );

            connection.SendRequest(passwordRequest);

            // Enable account
            var enableRequest = new ModifyRequest(
                dn,
                DirectoryAttributeOperation.Replace,
                "userAccountControl",
                "512" // NORMAL_ACCOUNT
            );

            connection.SendRequest(enableRequest);

            connection.Dispose();
        }
        public static ADuser GetUserLastLogin(string username)
        {
            using var connection = ADService.ConnectGet();

            // LDAP-søgning efter brugeren
            var searchRequest = new SearchRequest(
                "DC=mags,DC=local",
                $"(sAMAccountName={username})",
                SearchScope.Subtree,
                "sAMAccountName",
                "displayName",
                "mail",
                "lastLogon"
            );

            var response = (SearchResponse)connection.SendRequest(searchRequest);

            if (response.Entries.Count == 0)
                throw new Exception("User not found");

            var entry = response.Entries[0];

            DateTime? lastLogin = null;

            // lastLogonTimestamp er OPTIONAL
            if (entry.Attributes.Contains("lastLogon"))
            {
                long fileTime = (long)entry.Attributes["lastLogon"][0];
                lastLogin = DateTime.FromFileTimeUtc(fileTime);
            }

            return new ADuser
            {
                UserName = entry.Attributes["sAMAccountName"]?[0]?.ToString() ?? "N/A",
                FullName = entry.Attributes["displayName"]?[0]?.ToString() ?? "N/A",
                Email = entry.Attributes["mail"]?[0]?.ToString() ?? "N/A",
                LastLogin = lastLogin
            };
        }

        public static bool AuthenticateUser(string username, string password)
        {
            try
            {
                // Opret credentials med brugerens login
                var credential = new NetworkCredential(
                    $"{username}@{ADService.Domain}", // bruger@domæne
                    password
                );

                // Opret LDAP forbindelse
                var connection = new LdapConnection(ADService.Server)
                {
                    Credential = credential,
                    AuthType = AuthType.Negotiate
                };

                // Forsøger at logge ind (bind)
                connection.Bind();

                // Hvis vi når hertil → login er korrekt
                return true;
            }
            catch (LdapException)
            {
                // Forkert brugernavn / kodeord / konto låst
                return false;
            }
        }
        public static void CreateGuestContact(
            string containerDn,    // Fx "CN=Users,DC=mags,DC=local"
            string fullName,
            string company,
            string visitReason,
            string email = null
)
        {
            // Brug jeres eksisterende globale AD credentials
            using var connection = ADService.ConnectGet();

            // DN for kontakten (Contacts bruger CN – ikke OU)
            string dn = $"CN={fullName},{containerDn}";

            // Opret Contact objektet
            var addRequest = new AddRequest(
                dn,
                new DirectoryAttribute("objectClass", "contact"),
                new DirectoryAttribute("cn", fullName),
                new DirectoryAttribute("displayName", fullName),
                new DirectoryAttribute("company", company),
                new DirectoryAttribute("description", visitReason)
            );

            // Email er valgfri
            if (!string.IsNullOrWhiteSpace(email))
            {
                addRequest.Attributes.Add(
                    new DirectoryAttribute("mail", email)
                );
            }

            // Send LDAP-requesten
            connection.SendRequest(addRequest);
        }

        public class ADuser
        {
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public DateTime? LastLogin { get; set; }

        }
    }
}




