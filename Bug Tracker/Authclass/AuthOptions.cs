using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bug_Tracker.Authclass
{
    public class AuthOptions
    {
        public const string ISSUER = "YourIssuer";
        public const string AUDIENCE = "YourAudience"; 
        const string KEY = "key";
        public const int LIFETIME = 10;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY)); 
        }



    }
}
