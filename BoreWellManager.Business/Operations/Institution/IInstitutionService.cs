using BoreWellManager.Business.Operations.Institution.Dtos;
using BoreWellManager.Business.Types;
using BoreWellManager.Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Institution
{
    public interface IInstitutionService
    {
        public Task<ServiceMessage<InstitutionEntity>> AddInstitution(InstitutionDto institution);
        public Task<InstitutionEntity> GetByIdInstitution(int id);
        public Task<List<InstitutionDto>> GetAllInstitutions();
        public Task<ServiceMessage> DeleteInstitution(int id);
        public Task<ServiceMessage<InstitutionEntity>> ChangeAdressInstitution(int id,UpdateInsDto insDto);
    }
}
