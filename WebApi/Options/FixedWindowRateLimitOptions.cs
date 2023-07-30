namespace WebApi.Options
{
    public class FixedWindowRateLimitOptions
    {
        public int PermitLimit { get; set; }

        public double Window { get; set; }

        public int QueueLimit { get; set; }

        public bool AutoReplenishment { get; set; }
    }
}
