namespace BoreWellManager.WebApi.Models
{
    public class ChangePaymentRequest
    {
        public int DocumentId { get; set; }
        public string CustomerFullName { get; set; }
        public bool IsInstallmentPayment { get; set; }
        public decimal InstallmentAmount { get; set; }
        public DateTime LastPaymentDate { get; set; }

    }
}
