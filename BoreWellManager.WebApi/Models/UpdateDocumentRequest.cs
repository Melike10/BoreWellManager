using BoreWellManager.Data.Enums;

namespace BoreWellManager.WebApi.Models
{
    public class UpdateDocumentRequest
    {
        public int WellId { get; set; }
        public int PaymentId { get; set; }
        public int InstitutionId { get; set; }
        public string Type { get; set; }
        public DateTime CustomerSubmissionDate { get; set; }//müşteri gönderim tarihi
        public DateTime? InstitutionSubmissionDate { get; set; }//kuruma gönderim tarihi
        public SignutureReceivedType SignaturesReceived { get; set; } // "Received", "Email Sent", "Not Received"
        public bool DeliveredToInstitution { get; set; }
        public bool IsLienCertificate { get; set; }
        public decimal DocumentFee { get; set; }
        public bool FeeReceived { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
