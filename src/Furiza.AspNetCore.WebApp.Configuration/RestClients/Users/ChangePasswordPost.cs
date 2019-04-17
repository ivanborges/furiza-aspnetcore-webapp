namespace Furiza.AspNetCore.WebApp.Configuration.RestClients.Users
{
    public class ChangePasswordPost
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}