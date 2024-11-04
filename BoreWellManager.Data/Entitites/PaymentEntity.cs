using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoreWellManager.Data.Entitites
{
    public class PaymentEntity:BaseEntity
    {
        public int DocumentId { get; set; }
        
        public string DepositorFullName { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RemaningAmount { get; set; }
        public string EmployeeWhoReceivedPayment { get; set; }//ödemeyi alan çalışan

        // Taksitli ödeme ile ilgili alanlar
        public bool IsInstallmentPayment { get; set; }//taksitli ödeme mi?
        public decimal? InstallmentAmount { get; set; }// taksitli değilse boş bırakılabiliriz
        public DateTime? LastPaymentDate { get; set; } // Nullable, çünkü taksitli değilse boş kalabilir

        //Relational Property

        [JsonIgnore]
        public DocumentEntity Document { get; set; }
    }
    public  class PaymentConfiguration : BaseConfiguraiton<PaymentEntity>
    {
        public override void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
