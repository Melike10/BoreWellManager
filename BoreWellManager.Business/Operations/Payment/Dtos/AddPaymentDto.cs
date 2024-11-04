using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Payment.Dtos
{
    public class AddPaymentDto
    {
        public int DocumentId { get; set; }

        public string DepositorFullName { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RemaningAmount { get; set; }
        public string EmployeeWhoReceivedPayment { get; set; }//ödemeyi alan çalışan

        // Taksitli ödeme ile ilgili alanlar
        public bool IsInstallmentPayment { get; set; }//taksitli ödeme mi?
        public decimal InstallmentAmount { get; set; }// taksitli değilse boş bırakılabiliriz
        public DateTime LastPaymentDate { get; set; }
    }
}
