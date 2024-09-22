using System.ComponentModel.DataAnnotations;

namespace api_dotnet.Models
{
    public class User
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
