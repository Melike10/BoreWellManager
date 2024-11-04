using BoreWellManager.Data.Enums;

namespace BoreWellManager.WebApi.Models
{
    public class AddDocumentRequest
    {
        public int WellId { get; set; }
        public int InstitutionId { get; set; }
        public string Type { get; set; }
        public DateTime CustomerSubmissionDate { get; set; }//müşteri gönderim tarihi
        public SignutureReceivedType SignaturesReceived { get; set; } // "Received", "Email Sent", "Not Received"
        public bool DeliveredToInstitution { get; set; } = false;
        public bool IsLienCertificate { get; set; } = false;
        public string CustomerFullName { get; set; }
        public decimal DocumentFee { get; set; }
        public bool FeeReceived { get; set; } = false;
        public decimal ReceivedFeeAmount { get; set; } = 0;
        public string CreatedBy { get; set; }
    }
}
