using BoreWellManager.Business.Operations.Institution.Dtos;
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

namespace BoreWellManager.Business.Operations.Institution
{
    public class InstitutionManager : IInstitutionService
    {
        private readonly IRepository<InstitutionEntity> _institutionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public InstitutionManager(IRepository<InstitutionEntity> institutionRepository,IUnitOfWork unitOfWork)
        {
            _institutionRepository = institutionRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceMessage<InstitutionEntity>> AddInstitution(InstitutionDto institution)
        {
            var hasIns = await _institutionRepository.GetAll(x=> x.Name == institution.Name && x.City==institution.City && x.Town == institution.Town).FirstOrDefaultAsync();
            if (hasIns != null)
            {
                return new ServiceMessage<InstitutionEntity> { IsSucceed = false, Message = "Zaten benzer bir kayıt mevcut" };
            }
            var insEntity = new InstitutionEntity
            {
                Name = institution.Name,
                City = institution.City,
                Town = institution.Town
            };
            _institutionRepository.Add(insEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kurum kaydedilirken bir hata oluştu.");
            }

            return new ServiceMessage<InstitutionEntity> { IsSucceed=true,Data=insEntity};
        }

        public async Task<InstitutionEntity> GetByIdInstitution(int id)
        {
            var inst = _institutionRepository.GetById(id);
           
                return inst;
        }

        public async Task<List<InstitutionDto>> GetAllInstitutions()
        {
            var institutions = _institutionRepository.GetAll().Select(x=> new InstitutionDto
            {
                Name = x.Name,
                City = x.City,
                Town=x.Town
            }).ToList();
            return institutions;
        }

        public async Task<ServiceMessage> DeleteInstitution(int id)
        {
            var inst =  _institutionRepository.GetById(id);
            if (inst is null)
                return new ServiceMessage { IsSucceed = false ,Message = "Silinecek kayıt bulunamadı"};
            _institutionRepository.Delete(inst);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Silme işlemi sırasında bir hata alındı");
            }

            return new ServiceMessage { IsSucceed = true };
        }

        public async Task<ServiceMessage<InstitutionEntity>> ChangeAdressInstitution(int id, UpdateInsDto insDto)
        {
            var inst =  _institutionRepository.GetById(id);
            if (inst is null)
                return new ServiceMessage<InstitutionEntity> {IsSucceed=false,Message="Kayıt bulunamadı" };

            inst.City = insDto.City;
            inst.Town = insDto.Town;
            _institutionRepository.Update(inst);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Adres değişikliği sırasında bir hata gerçekleşti");
            }
            return new ServiceMessage<InstitutionEntity> { IsSucceed = true ,Message="Değişiklik başarı şekilde gerçekleşmiştir",Data=inst};
            
        }
    }
}
