using BoreWellManager.Business.Operations.Well.Dtos;
using BoreWellManager.Business.Types;
using BoreWellManager.Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Well
{
    public interface IWellService
    {
        public Task<ServiceMessage> AddWell(AddWellDto wellDto);
        public Task<WellDto> GetById(int id);
        public Task<ServiceMessage<WellEntity>> ChangeWellMeasureById(int id, AddWellMeasureDto measureDto);
        public Task<ServiceMessage<WellEntity>> UpdateWell(int id,UpdateWellDto wellDto);
        public Task<ServiceMessage> DeleteWell(int id);
    }
}
