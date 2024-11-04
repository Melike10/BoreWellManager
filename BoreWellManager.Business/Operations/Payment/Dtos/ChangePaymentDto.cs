using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Payment.Dtos
{
    public class ChangePaymentDto
    {
        public int DocumentId { get; set; }
        public string CustomerFullName { get; set; }
        public bool IsInstallmentPayment { get; set; }
        public decimal InstallmentAmount { get; set; }
        public DateTime LastPaymentDate { get; set; }

    }
}
