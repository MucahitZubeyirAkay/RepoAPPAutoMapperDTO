using DTOs;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Operation.Interface
{
    public interface IUserService : IGenericService<UserDTO>
    {
        Task<UserDTO> GetUser(string username, string password);

    }
}
