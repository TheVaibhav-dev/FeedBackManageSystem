using Microsoft.AspNetCore.Identity;

namespace FeedBackManageSystem.Services.Service
{
    public class PasswordService
    {
        private readonly PasswordHasher<string> _hasher = new();
        public string Hash(string password)
            => _hasher.HashPassword(null, password);

        public bool Verify(string hashedPassword, string providedPassword)
            => _hasher.VerifyHashedPassword(null, hashedPassword, providedPassword) == PasswordVerificationResult.Success;
    }
}
