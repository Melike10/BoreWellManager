using BoreWellManager.Business.Operations.User.Dtos;
using BoreWellManager.Business.Types;
using BoreWellManager.Data.Entitites;
using BoreWellManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto addUserDto);
        Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto loginUserDto);
        Task<UserLandsDto> GetLands(string userTc);
        Task<UserInfoDto> GetUserById(int id);
        Task<List<UserEntity>> GetUsers();
        Task<ServiceMessage> ChangeAdress(string tc,string adress);
        Task<ServiceMessage> ToggleIsResponsible(string tc);
    }
}
