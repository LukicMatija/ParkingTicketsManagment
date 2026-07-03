using BCrypt.Net;

namespace ParkingTicketsManagment.Service
{
    public class PassHasher
    {
        public static string hashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool isValid(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}
