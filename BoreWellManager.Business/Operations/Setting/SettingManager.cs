using BoreWellManager.Data.Entitites;
using BoreWellManager.Data.Repository;
using BoreWellManager.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Setting
{
    public class SettingManager:ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SettingEntity> _settingRepository;

        public SettingManager(IUnitOfWork unitOfWork,IRepository<SettingEntity> settingRepository)
        {
            _unitOfWork = unitOfWork;
            _settingRepository = settingRepository;
            
        }

        public bool GetMaintenenceStat()
        {
            var maintenenceMode = _settingRepository.GetById(1).MaintenenceMode;
            return maintenenceMode;
        }

        public async Task ToggleMaintenence()
        {
            var setting = _settingRepository.GetById(1);
            setting.MaintenenceMode=!setting.MaintenenceMode;
            _settingRepository.Update(setting);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Bakım durumu güncellenirken hata ile karşılaşıldı.");
            }
        }
    }
}
