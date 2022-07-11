namespace CatalogWebApp.Utils.Options
{
    public class RetryPolicyOptions
    {
        public const string Position = "RetryPolicy";

        public int RetryCount { get; set; }

        public int RetryTimeout { get; set; }
    }
}
