namespace Movieasy.Domain.Users
{
    public static class UserConstants
    {
        public const int EmailMaxLength = 255;

        public const int FirstNameMaxLength = 80;
        public const int FirstNameMinLength = 3;

        public const int LastNameMaxLength = 80;
        public const int LastNameMinLength = 3;

        public const int PasswordMaxLength = 128;
        public const int PasswordMinLength = 5;

        public const int DetailsMaxLength = 1000;
        public const int DetailsMinLength = 100;
    }
}
