namespace BoreWellManager.WebApi.Models
{
    public class ChangeDocumentRequest
    {
        public bool DeliveredToInstitution { get; set; }
        public DateTime InstitutionSubmissionDate { get; set; }//kuruma gönderim tarihi
        public string ModifiedBy { get; set; }
    }
}
