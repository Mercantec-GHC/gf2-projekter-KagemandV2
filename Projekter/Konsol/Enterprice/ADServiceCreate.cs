using Enterprice;
using System.DirectoryServices.Protocols;
using System.Net;

namespace Konsol.Enterprice
{
    public class ADServiceCreate
    {
        public static string _server = "10.133.71.100";
        public static string _username = "CRUD";
        public static string _password = ")e=4To!3@(SKLnCPWLz'[8!";
        public static string _domain = "mags.local";

        // Opret forbindelse til AD med credential + bind test
        public static LdapConnection ConnectCreate()
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
    }
}
