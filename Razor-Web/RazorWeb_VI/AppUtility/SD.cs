namespace AppUtility;

public class SD
{
    // App User Roles
    public const string ManagerRole = "Manager";
    public const string FrontDeskRole = "FrontDesk";
    public const string KitchenRole = "Kitchen";
    public const string CustomerRole = "Customer";

    // Order Statuses
    public const string StatusPending = "Pending_Payment"; // On Payment: Once the order is placed it will be StatusPending
    public const string StatusSubmitted = "Submitted_PaymentApproved";  // On Payment: Once the payment is successful it will be StatusSubmitted
    public const string StatusRejected = "Rejected_Payment";    // On Payment: Once the payment is failed because of any reason it will be StatusRejected
    public const string StatusInProcess = "Being Prepared"; // Once the chef is cooking the food it will be StatusInProcess
    public const string StatusReady = "Ready for PickUp";   // Once the food is ready (for pickup) it will be StatusReady
    public const string StatusCompleted = "Completed";  // Once the order is picked up i.e. delivered it will be StatusCompleted
    public const string StatusCancelled = "Cancelled";  // Once the order is cancelled for some reason it will be StatusCancelled
    public const string StatusRefunded = "Refunded";    // Once the order refund is issued for cancelled order it will be StatusRefunded
}