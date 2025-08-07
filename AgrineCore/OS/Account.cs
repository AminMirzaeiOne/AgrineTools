using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

namespace AgrineCore.OS
{
    

    public static class Account
    {
        /// <summary>
        /// Gets the name of the current logged-in user (DOMAIN\Username or just Username).
        /// </summary>
        public static string GetCurrentUsername()
        {
            return Environment.UserDomainName + "\\" + Environment.UserName;
        }

        /// <summary>
        /// Gets only the current user name.
        /// </summary>
        public static string GetCurrentUserShortName()
        {
            return Environment.UserName;
        }

        /// <summary>
        /// Checks if the current user is a member of the Administrators group.
        /// </summary>
        public static bool IsCurrentUserAdministrator()
        {
            try
            {
                using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
                {
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lists all local user accounts on the system.
        /// </summary>
        public static List<string> GetAllLocalUsers()
        {
            var users = new List<string>();

            try
            {
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Machine))
                {
                    UserPrincipal user = new UserPrincipal(ctx);
                    using (PrincipalSearcher searcher = new PrincipalSearcher(user))
                    {
                        foreach (var result in searcher.FindAll())
                        {
                            users.Add(result.SamAccountName);
                        }
                    }
                }
            }
            catch { }

            return users;
        }

        /// <summary>
        /// Checks if a specific user exists on the system.
        /// </summary>
        public static bool UserExists(string username)
        {
            try
            {
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Machine))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(ctx, username);
                    return user != null;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a given user is currently logged in (session open).
        /// </summary>
        public static bool IsUserLoggedIn(string username)
        {
            try
            {
                string currentUser = Environment.UserName;
                return string.Equals(currentUser, username, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the list of users in the Administrators group.
        /// </summary>
        public static List<string> GetAdministrators()
        {
            var admins = new List<string>();
            try
            {
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Machine))
                using (GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, "Administrators"))
                {
                    if (group != null)
                    {
                        foreach (var member in group.Members)
                        {
                            admins.Add(member.SamAccountName);
                        }
                    }
                }
            }
            catch { }

            return admins;
        }

        /// <summary>
        /// Gets the user's domain name or machine name if not on domain.
        /// </summary>
        public static string GetUserDomain()
        {
            return Environment.UserDomainName;
        }

        /// <summary>
        /// Gets a list of users currently logged in (only one if single-session OS).
        /// </summary>
        public static List<string> GetLoggedInUsers()
        {
            var users = new List<string>();
            try
            {
                string currentUser = Environment.UserName;
                if (!string.IsNullOrWhiteSpace(currentUser))
                    users.Add(currentUser);
            }
            catch { }
            return users;
        }
    }

}
