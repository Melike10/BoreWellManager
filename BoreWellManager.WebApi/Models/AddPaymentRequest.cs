using System.ComponentModel.DataAnnotations;

namespace BoreWellManager.WebApi.Models
{
    public class AddPaymentRequest
    {
        [Required]
        public int DocumentId { get; set; }

        public string DepositorFullName { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RemaningAmount { get; set; }
        public string EmployeeWhoReceivedPayment { get; set; }//ödemeyi alan çalışan

        // Taksitli ödeme ile ilgili alanlar
        public bool IsInstallmentPayment { get; set; }//taksitli ödeme mi?
        public decimal? InstallmentAmount { get; set; }// taksitli değilse boş bırakılabiliriz
        public DateTime? LastPaymentDate { get; set; }
    }
}
