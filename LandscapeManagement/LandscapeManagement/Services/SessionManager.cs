using LandscapeManagement.Models;

namespace LandscapeManagement
{
    public static class SessionManager
    {
        public static User LoggedInUser { get; private set; }

        public static void SetUser(User user)
        {
            LoggedInUser = user;
        }

        public static void Logout()
        {
            LoggedInUser = null;
        }

        public static bool IsUserLoggedIn()
        {
            return LoggedInUser != null;
        }
    }
}
