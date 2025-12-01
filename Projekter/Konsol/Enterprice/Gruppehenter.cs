using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;

namespace Enterprice
{
    public class GroupADService
    {
        // Hent ALLE AD grupper
        public static List<ADGroup> GetAllGroups()
        {
            var groups = new List<ADGroup>();

            using (var connection = ADService.ConnectGet())
            {
                var search = new SearchRequest(
                    "DC=mags,DC=local",
                    "(objectClass=group)",
                    SearchScope.Subtree,
                    "cn",
                    "description",
                    "member"
                );

                var response = (SearchResponse)connection.SendRequest(search);

                foreach (SearchResultEntry group in response.Entries)
                {
                    groups.Add(new ADGroup
                    {
                        Name = group.Attributes["cn"]?[0]?.ToString() ?? "N/A",
                        Description = group.Attributes["description"]?[0]?.ToString() ?? "N/A"
                    });
                }
            }

            return groups;
        }

        // Hent MEDLEMMERNE af en specifik gruppe
        public static List<ADUser> GetMembersOfGroup(string groupName)
        {
            var members = new List<ADUser>();

            using (var connection = ADService.ConnectGet())
            {
                // Find gruppen og dens "member" attribut
                var groupSearch = new SearchRequest(
                    "DC=mags,DC=local",
                    $"(&(objectClass=group)(cn={groupName}))",
                    SearchScope.Subtree,
                    "member"
                );

                var groupResponse = (SearchResponse)connection.SendRequest(groupSearch);

                if (groupResponse.Entries.Count == 0)
                {
                    Console.WriteLine("Group not found.");
                    return members;
                }

                // Hent alle DN'er for medlemmer
                var memberList = groupResponse.Entries[0].Attributes["member"];
                if (memberList == null)
                {
                    Console.WriteLine("This group has no members.");
                    return members;
                }

                // For hvert medlem → hent deres info
                foreach (var dn in memberList)
                {
                    var userSearch = new SearchRequest(
                        dn.ToString(),
                        "(objectClass=user)",
                        SearchScope.Base,
                        "sAMAccountName",
                        "displayName",
                        "mail"
                    );

                    try
                    {
                        var userResponse = (SearchResponse)connection.SendRequest(userSearch);

                        if (userResponse.Entries.Count > 0)
                        {
                            var u = userResponse.Entries[0];
                            members.Add(new ADUser
                            {
                                UserName = u.Attributes["sAMAccountName"]?[0]?.ToString() ?? "N/A",
                                FullName = u.Attributes["displayName"]?[0]?.ToString() ?? "N/A",
                                Email = u.Attributes["mail"]?[0]?.ToString() ?? "N/A"
                            });
                        }
                    }
                    catch
                    {
                        // Nogle "member" entries kan være computere, grupper osv.
                        continue;
                    }
                }
            }

            return members;
        }
        
        public class ADGroup
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }

}

