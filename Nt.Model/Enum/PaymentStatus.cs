
namespace Nt.Model.Enum
{
    /// <summary>
    /// Represents a payment status enumeration
    /// </summary>
    public enum PaymentStatus : int
    {
        /// <summary>
        ///10 Pending
        /// </summary>
        Pending = 10,
        /// <summary>
        ///20 Authorized
        /// </summary>
        Authorized = 20,
        /// <summary>
        ///30 Paid
        /// </summary>
        Paid = 30,
        /// <summary>
        /// Partially Refunded
        /// </summary>
        PartiallyRefunded = 35,
        /// <summary>
        /// Refunded
        /// </summary>
        Refunded = 40,
        /// <summary>
        /// Voided
        /// </summary>
        Voided = 50,
    }
}
