using BoreWellManager.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoreWellManager.Data.Entitites
{
    public class DocumentEntity:BaseEntity
    {
        //public int UserId { get; set; }
        //public int LandId { get; set; }
        public int WellId { get; set; }
        public int? PaymentId { get; set; }
        public int InstitutionId { get; set; }
        public string Type { get; set; }
        public DateTime CustomerSubmissionDate { get; set; }//müşteri gönderim tarihi
        public DateTime? InstitutionSubmissionDate { get; set; }//kuruma gönderim tarihi
        public SignutureReceivedType SignaturesReceived { get; set; } // "Received", "Email Sent", "Not Received"
        public bool DeliveredToInstitution { get; set; }=false;
        public bool IsLienCertificate { get; set; }=false;
        public decimal DocumentFee { get; set; }
        public bool FeeReceived { get; set; } = false;
        public  string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

        //Relational Property
        //public UserEntity Owner { get; set; }
        //public LandEntity Land { get; set; }
        [JsonIgnore]
        public WellEntity Well { get; set; }
        [JsonIgnore]
        public PaymentEntity Payment { get; set; }
        [JsonIgnore]
        public InstitutionEntity Institution { get; set; }
    }
    public class DocumentConfiguraiton: BaseConfiguraiton<DocumentEntity>
    {
        public override void Configure(EntityTypeBuilder<DocumentEntity> builder)
        {
            builder.Property(d=>d.InstitutionSubmissionDate).IsRequired(false);
            builder.Property(d => d.ModifiedBy).IsRequired(false);
            base.Configure(builder);
        }
    }
}
