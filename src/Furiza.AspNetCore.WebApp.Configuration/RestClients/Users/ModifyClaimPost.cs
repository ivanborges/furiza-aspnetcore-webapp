namespace Furiza.AspNetCore.WebApp.Configuration.RestClients.Users
{
    public class ModifyClaimPost
    {
        public ModifyClaimOperation? Operation { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}