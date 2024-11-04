using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Document.Dtos
{
    public class UpdateDocumentDto
    {
        public bool DeliveredToInstitution { get; set; }
        public DateTime InstitutionSubmissionDate { get; set; }//kuruma gönderim tarihi
        public string ModifiedBy { get; set; }
    }
}
