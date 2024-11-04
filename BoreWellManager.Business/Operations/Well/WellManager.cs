using BoreWellManager.Business.Operations.Well.Dtos;
using BoreWellManager.Business.Types;
using BoreWellManager.Data.Entitites;
using BoreWellManager.Data.Repository;
using BoreWellManager.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Well
{
    public class WellManager : IWellService
    {
        private readonly IRepository<WellEntity> _wellRepository;
        private readonly IUnitOfWork _unitOfWork;
        public WellManager(IRepository<WellEntity> wellRepository,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _wellRepository = wellRepository;
        }
        public async Task<ServiceMessage> AddWell(AddWellDto wellDto)
        {
             var existingWell = await _wellRepository
                        .GetAll(w =>
                            w.XCordinat == wellDto.XCordinat &&
                            w.YCordinat == wellDto.YCordinat &&
                            w.UserId == wellDto.UserId &&
                            w.LandId == wellDto.LandId).FirstOrDefaultAsync();

                    if (existingWell != null)
                    {
                        return new ServiceMessage { IsSucceed = false, Message = "Benzer bir kayıt sistemde zaten mevcut" };
                    }
               
            var wellEntiity = new WellEntity
            {
                XCordinat = wellDto.XCordinat,
                YCordinat= wellDto.YCordinat,
                UserId = wellDto.UserId,
                LandId = wellDto.LandId,
                
            };
            _wellRepository.Add(wellEntiity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kuyu kaydedilirken bir hata oluştu");
            }
            return new ServiceMessage { IsSucceed = true };
        }

        public async Task<ServiceMessage<WellEntity>> ChangeWellMeasureById(int id, AddWellMeasureDto measureDto)
        {
            var wellEntitiy = _wellRepository.GetById(id);
            if (wellEntitiy is null)
            {
                return new ServiceMessage<WellEntity> { IsSucceed = false, Message = "İlgili id'ye ait bir kuyu bulunamamıştır" };
            }

            
            wellEntitiy.Debi= measureDto.Debi;
            wellEntitiy.StaticLevel = measureDto.StaticLevel;
            wellEntitiy.DynamicLevel = measureDto.DynamicLevel;

            _wellRepository.Update(wellEntitiy);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kuyuya ait verilerin güncellenmesi sırasında bir sıkıntı çıktı");
            }


            return new ServiceMessage<WellEntity> { 
            IsSucceed = true,
            Data = wellEntitiy

            };
        }

        public async Task<ServiceMessage> DeleteWell(int id)
        {
            var well = _wellRepository.GetById(id);
            if (well is null)
                return new ServiceMessage { IsSucceed= false ,Message="Silinecek kuyu bulunamadı." };

            _wellRepository.Delete(well);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kuyu silinirken bir hata oluştu");
            }
            return new ServiceMessage { IsSucceed = true };
        }

        public async Task<WellDto> GetById(int id)
        {
            var well = await _wellRepository.GetAll(x=> x.Id== id).Select(x=> new WellDto {
            Id = x.Id,
            UserId= x.UserId,
            LandId= x.LandId,
            XCordinat= x.XCordinat,
            YCordinat = x.YCordinat,
            Debi= x.Debi??0,
            StaticLevel=x.StaticLevel??0,
            DynamicLevel=x.DynamicLevel??0

            }).FirstOrDefaultAsync();

            return well;
        }

        public async Task<ServiceMessage<WellEntity>> UpdateWell(int id, UpdateWellDto wellDto)
        {
            var wellEntity = _wellRepository.GetById(id);
            if(wellEntity is null)
            { return new ServiceMessage<WellEntity> { IsSucceed= false, Message="İlgili kuyu bulunamamıştır"}; }

            wellEntity.UserId= wellDto.UserId;
            wellEntity.LandId= wellDto.LandId;
            wellEntity.StaticLevel= wellDto.StaticLevel;
            wellEntity.DynamicLevel= wellDto.DynamicLevel;
            wellEntity.Debi= wellDto.Debi;
            wellEntity.XCordinat= wellDto.XCordinat;
            wellEntity.YCordinat= wellDto.YCordinat;
            _wellRepository.Update(wellEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kuyu güncellenmesi sırasında bir hata ile karşılşıldı.");
            }
            return new ServiceMessage<WellEntity> { Data = wellEntity,IsSucceed=true };
        }
    }
}
