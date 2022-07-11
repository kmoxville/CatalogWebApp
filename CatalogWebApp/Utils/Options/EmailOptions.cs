namespace CatalogWebApp.Utils.Options
{
    public class EmailOptions
    {
        public const string Position = "Email";

        public string Name { get; set; } = String.Empty;

        public string Host { get; set; } = String.Empty;

        public int Port { get; set; }

        public string Address { get; set; } = String.Empty;

        public string User { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        public RetryPolicyOptions RetryPolicy { get; set; } = null!;
    }

    public class NotificationEmailOptions
    {
        public const string Position = "NotificationEmail";

        public string Address { get; set; } = String.Empty;
    }
}
