using BLL.Operation.Interface;
using DAL.Interface.Concrete;
using DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Operation.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Create(UserDTO userDTO)
        {
            await _userRepository.Create(userDTO);
        }

        public async Task Delete(int id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<List<UserDTO>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<UserDTO> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<UserDTO> GetUser(string username, string password)
        {
            return await _userRepository.GetUser(username,password);
        }

        public async Task Update(UserDTO userDTO)
        {
            await _userRepository.Update(userDTO);
        }

    }
}
