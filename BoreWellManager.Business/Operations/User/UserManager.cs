using BoreWellManager.Business.Operations.User.Dtos;
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

namespace BoreWellManager.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UserManager(IRepository<UserEntity> repository,IUnitOfWork unitOfWork)
        {
          _repository= repository;
           _unitOfWork= unitOfWork;
                
        }
        public async Task<ServiceMessage> AddUser(AddUserDto addUserDto)
        {
           var hasUser = _repository.GetAll(x=>x.TC ==  addUserDto.TC);
            if (hasUser.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Sistemde benzer bir kullanıcı vardır"
                };
            }
            var userEntity = new UserEntity { 
                TC = addUserDto.TC,
                Name = addUserDto.Name,
                Phone = addUserDto.Phone,
                Adress = addUserDto.Adress,
                IsResponsible = addUserDto.IsResponsible,
                UserType = addUserDto.UserType
            };
            _repository.Add(userEntity);
            // veritabanları işlemleri için try catch yazıldı
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception ("Veritabına kayıt işlemi sırasında hata alındı.");
            }

            return new ServiceMessage { IsSucceed = true };
        }

        public async Task<ServiceMessage> ChangeAdress(string tc, string adress)
        {
            var user = await _repository.GetAll(x=>x.TC == tc).FirstOrDefaultAsync();

            if (user is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Sistemde ilgili TC'ye ait bir kullanıcı bulunamadı."
                };
            }
            var userEntity = _repository.GetById(user.Id);
            userEntity.Adress = adress;
            _repository.Update(userEntity);

            try
            {
               await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Adres değişikliği sırasında bir hata oluştu.");
            }

            return new ServiceMessage { IsSucceed = true };
        }

        public async Task<UserLandsDto> GetLands(string userTc)
        {
            var userLands = await _repository.GetAll(u => u.TC == userTc)
                .Select(user => new UserLandsDto
                {
                    Id = user.Id,
                    TC = user.TC,
                    Name = user.Name,
                    Phone = user.Phone,
                    Lands = user.LandOwners.Select(lo => lo.Land) // Ara tablodan Land ilişkisi
                        .Select(l => new LandsDto
                        {
                            Id = l.Id,
                            City = l.City,
                            Town = l.Town,
                            Block = l.Block,
                            Plot = l.Plot
                        }).ToList()
                })
                .FirstOrDefaultAsync();



            return userLands;
        }

        public Task<UserInfoDto> GetUserById(int id)
        {
            var user = _repository.GetAll(x=>x.Id== id).Select(x=> new UserInfoDto
            {
                Id= x.Id,
                TC= x.TC,
                Name = x.Name,
                Phone = x.Phone,
                Adress = x.Adress,
                IsResponsible = x.IsResponsible,
                UserType = x.UserType

            }).FirstOrDefaultAsync();

            return user;
            
        }

        public async Task<List<UserEntity>> GetUsers()
        {
            var res =  _repository.GetAll().ToList();   
            return res;
            
        }

        public async Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto loginUserDto)
        {
            var userEntity = _repository.Get(x => x.TC == loginUserDto.TC);

            if (userEntity is null || userEntity.Name != loginUserDto.Name)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "TC veya İsim hatalı."
                };
            }

            return new ServiceMessage<UserInfoDto>
            {
                IsSucceed = true,
                Data = new UserInfoDto // Data nesnesini başlatıyoruz
                {
                    Name = userEntity.Name,
                    TC = userEntity.TC,
                    Phone = userEntity.Phone,
                    Adress = userEntity.Adress,
                    IsResponsible = userEntity.IsResponsible,
                    UserType = userEntity.UserType
                }
            };
        }

        public async Task<ServiceMessage> ToggleIsResponsible(string tc)
        {
            var user = await _repository.GetAll(u=> u.TC == tc).FirstOrDefaultAsync();
            if (user is null)
            {
                return new ServiceMessage { IsSucceed = false, Message = "Böyle bir kullanıcı bulunamadı" };

            }
            user.IsResponsible=!user.IsResponsible;
            _repository.Update(user);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Sorumluluk özelliği değiştirilirken hata oluştu");
            }

            return new ServiceMessage { IsSucceed = true };
        }
    }
}

