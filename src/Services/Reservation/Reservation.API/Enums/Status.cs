using System.ComponentModel;

namespace Reservation.API.Enums
{
    public enum Status
    {
        [Description("Pending")]
        PENDING,

        [Description("Paid")]
        PAID,

        [Description("Canceled")]
        CANCELED

        //[Description("Refunded")]
        //REFUNDED
    }
}
