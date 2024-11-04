using BoreWellManager.Business.Operations.Land.Dtos;
using BoreWellManager.Business.Types;
using BoreWellManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Land
{
    public interface ILandService
    {
        Task<ServiceMessage> AddLand(AddLandDto addLandDto);
        Task<LandDto> GetLand(int id);
        Task<List<LandDto>> GetUserLands(string userTc);
        Task<ServiceMessage> ChangeLienStatus(int id,LienType type);
        Task<ServiceMessage> DeleteLand(int id);
        Task<ServiceMessage> UpdateLand(UpdateLandDto updateLandDto);
    }
}
