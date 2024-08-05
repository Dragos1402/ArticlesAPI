using Newtonsoft.Json;
using BCrypt.Net;
using System.Security.Cryptography;

namespace ArticlesApi.Helpers
{
    public class Globals
    {
        public static IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        public static string conn = Configuration.GetConnectionString("DbConn");

        //Error types
        public const string SUCCESS = "000";

        //Database Errors
        public const string DATABASE_READING_ERROR = "002";
        public const string DATABASE_WRITTING_ERROR = "003";
        public const string DATABASE_UPDATING_ERROR = "004";
        public const string DATABASE_DELETING_ERROR = "005";

        //User errors
        public const string USER_ALREADY_EXISTS = "006";
        public const string INCORRECT_CUSTOMER_CODE = "007";
        public const string INCORRECT_VAT = "008";
        public const string INVALID_PASSWORD_POLICY = "009";
        public const string PASSWORD_MISMATCH = "010";
        public const string USER_DOESNT_EXIST = "011";

        //token errors
        public const string INVALID_TOKEN = "012";
        public const string EXPIRED_TOKEN = "013";
        public const string USED_TOKEN = "014";
        public const string TOKEN_NOT_EXPIRED = "015";

        public const string UNAUTHORIZED = "098";
        public const string GENERIC_CATCH_ERROR = "099";

        public static String CreateJSON(object item)
        {
            return JsonConvert.SerializeObject(item);
        }

        public static string HashPassword(char[] passwordCharArray)
        {
            // Convert the char array to a string
            string plainTextPassword = new string(passwordCharArray);

            // Generate a salt with a work factor of 12
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);

            // Hash the password with the generated salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainTextPassword, salt);

            // Clear the plain text password from memory
            Array.Clear(passwordCharArray, 0, passwordCharArray.Length);

            return hashedPassword;
        }

        public static bool VerifyPassword(string plainTextPassword, string storedHash)
        {
            // Verify the plain text password against the stored bcrypt hash
            bool passwordMatch = BCrypt.Net.BCrypt.Verify(plainTextPassword, storedHash);

            return passwordMatch;
        }

        public static string GenerateVerifyToken()
        {
            const int tokenLength = 128;

            using var rngCryptoServiceProvider = RandomNumberGenerator.Create();
            var randomBytes = new byte[tokenLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            // Convert the random bytes to a hexadecimal string
            string token = BitConverter.ToString(randomBytes).Replace("-", "").ToLower();

            // Serialize the token payload to JSON or any desired format
            string serializedToken = JsonConvert.SerializeObject(token).Replace("\"", "");

            return serializedToken;
        }

        public static string ReadHtmlTemplate(string file)
        {
            string tPath = Path.Combine(AppContext.BaseDirectory, "Templates");
            string filePath = Path.Combine(tPath, file);

            string result = File.ReadAllText(filePath);

            return result;
        }

        public static string GenerateVerifyLink(string resetPageUrl, string token)
        {
            string resetLink = $"{resetPageUrl}?token={token}";
            return resetLink;
        }
    }
}
