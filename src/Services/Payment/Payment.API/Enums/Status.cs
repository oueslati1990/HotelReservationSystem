using System.ComponentModel;

namespace Payment.API.Enums
{
    public enum Status
    {
        [Description("Pending")]
        PENDING,

        [Description("Paid")]
        PAID,

        [Description("Canceled")]
        CANCELED
    }
}
