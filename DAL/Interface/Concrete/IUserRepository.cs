using DAL.Interface.Abstract;
using DTOs;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Concrete
{
    public interface IUserRepository : IGenericRepository<UserDTO>
    {
        Task<UserDTO> GetUser(string username, string password);
    }
}
