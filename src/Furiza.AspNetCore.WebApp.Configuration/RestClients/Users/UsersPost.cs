namespace Furiza.AspNetCore.WebApp.Configuration.RestClients.Users
{
    public class UsersPost
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string HiringType { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public string Password { get; set; }
        public bool? GeneratePassword { get; set; }
    }
}