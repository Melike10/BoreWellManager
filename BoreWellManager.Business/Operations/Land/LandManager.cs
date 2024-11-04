using BoreWellManager.Business.Operations.Land.Dtos;
using BoreWellManager.Business.Types;
using BoreWellManager.Data.Entitites;
using BoreWellManager.Data.Enums;
using BoreWellManager.Data.Repository;
using BoreWellManager.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Land
{
   

    public class LandManager : ILandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<LandEntity> _repository;
        private readonly IRepository<LandOwnersEntity> _ownersRepository;
        public LandManager(IUnitOfWork unitOfWork,IRepository<LandEntity> repository, IRepository<LandOwnersEntity> ownersRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _ownersRepository = ownersRepository;
        }

        public async Task<ServiceMessage> AddLand(AddLandDto addLandDto)
        {
            var hasLand = _repository.GetAll(x=>x.City.ToLower() == addLandDto.City.ToLower() 
                                             && x.Town.ToLower() == addLandDto.Town.ToLower() 
                                             && x.Street.ToLower()== addLandDto.Street.ToLower()
                                             && x.Block == addLandDto.Block && x.Plot== addLandDto.Plot 
                                             && x.Location.ToLower()== addLandDto.Location.ToLower());

            if (hasLand.Any()) {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Benzer bir arazi sistemde bulunmaktadır."
                };
            }
            await _unitOfWork.BeginTransaction();

                var landEntity = new LandEntity { 
                    City = addLandDto.City, 
                    Town = addLandDto.Town,
                    Block = addLandDto.Block,
                    Plot = addLandDto.Plot,
                    Street=addLandDto.Street,
                    Location = addLandDto.Location,
                    LandType = addLandDto.LandType,
                    HasLien = addLandDto.HasLien,
                    LienType = addLandDto.LienType
                };

                _repository.Add(landEntity);

                try
                {
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception)
                {
                await _unitOfWork.RollBackTransaction();
                throw new Exception ("Arazi veritabanı kaydı sırasında bir hata oluştu");
                }

               foreach(var landowner in addLandDto.UserIds)
            {
                var landOwnerEntity = new LandOwnersEntity
                {
                    UserId = landowner,
                    LandId = landEntity.Id
                };
                _ownersRepository.Add(landOwnerEntity);
            }
            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();

            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Veritabanı kaydı sırasında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true

            };
        }

        public async Task<ServiceMessage> ChangeLienStatus(int id, LienType type)
        {
            var land = _repository.GetById(id);
            if (land == null)
            {
                return new ServiceMessage { IsSucceed = false, Message = "Arazi bulunamadı" };
            }
                if(type == 0)
                    land.HasLien = false;
                else
                    land.HasLien = true;

            land.LienType = type;
            _repository.Update(land);
                try
                {
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw new Exception("Şerh,Beyan,İrtifak bilgisi değiştirilirken bir hata oluştu");
                }

                return new ServiceMessage {
                    IsSucceed= true
                
                };
        }

        public async Task<ServiceMessage> DeleteLand(int id)
        {
            var land =  _repository.GetById(id);
            if (land is null)
            { return new ServiceMessage {
                IsSucceed = false,
                Message = "Silinmek istenen arazi bulunamadı"
            }; 
            }
            _repository.DeleteById(id);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kayıt silinirken bir hata oluştu.");
            }
            return new ServiceMessage { IsSucceed= true };

        }

        public async Task<LandDto> GetLand(int id)
        {
            var land = await _repository.GetAll(x=> x.Id == id).
                Select(x=> new LandDto
                {
                    Id = x.Id,
                    City = x.City,
                    Town = x.Town,
                    Street = x.Street,
                    Block = x.Block,
                    Plot = x.Plot,
                    Location=x.Location,
                    LandType = x.LandType,
                    LienType= (LienType)x.LienType,
                    IsCksReIsCksRequired = x.IsCksRequired,
                    Owners= x.LandOwners.Select(o=> new LandOwnerDto
                    {
                        Id =o.User.Id,
                        Name = o.User.Name
                    }).ToList()
                }).FirstOrDefaultAsync();

            return land;
        }

        public async Task<List<LandDto>> GetUserLands(string userTc)
        {
            var lands = await _repository.GetAll(l => l.LandOwners.Any(lo => lo.User.TC == userTc)).Select(x => new LandDto
            {
                Id = x.Id,
                City = x.City,
                Town = x.Town,
                Street = x.Street,
                Block = x.Block,
                Plot = x.Plot,
                Location = x.Location,
                LandType = x.LandType,
                LienType = (LienType)x.LienType,
                IsCksReIsCksRequired = x.IsCksRequired,
                Owners = x.LandOwners.Select(o => new LandOwnerDto
                {
                    Id = o.User.Id,
                    Name = o.User.Name
                }).ToList()
            }).ToListAsync(); 
            
            return lands;
        }

        public async Task<ServiceMessage> UpdateLand(UpdateLandDto updateLandDto)
        {
            var land = _repository.GetById(updateLandDto.Id);
            if(land is null)
                return new ServiceMessage { IsSucceed = false };

            await _unitOfWork.BeginTransaction();
            land.City= updateLandDto.City;
            land.Town= updateLandDto.Town;
            land.Street= updateLandDto.Street;
            land.Block= updateLandDto.Block;
            land.Plot= updateLandDto.Plot;
            land.Location= updateLandDto.Location;
            land.IsCksRequired= updateLandDto.IsCksRequired;
            land.HasLien= updateLandDto.HasLien;
            land.LienType = updateLandDto.LienType;

            _repository.Update(land);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Arazi bilgileri değiştirilirken hata oluştu");
            }

            var landOWners = _ownersRepository.GetAll(x => x.LandId == land.Id).ToList();

            foreach(var landOwner in landOWners)
            {
                _ownersRepository.Delete(landOwner,false);
            }

            foreach(var userId in updateLandDto.UserIds)
            {
                var landOwners = new LandOwnersEntity
                {
                    LandId = land.Id,
                    UserId = userId,
                };
                _ownersRepository.Add(landOwners);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Tarla sahipleri tablosu güncellenirken hata alındı");
            }

            return new ServiceMessage { IsSucceed = true };
        }
    }
    
}
