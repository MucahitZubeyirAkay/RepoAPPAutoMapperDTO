using BLL.Operation.Implementations;
using BLL.Operation.Interface;
using DTOs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.JWT
{
    public class TokenCreate
    {
        //private readonly JwtSettings _jwtSettings;

        //public TokenCreate(IOptions<JwtSettings> jwtSettings)
        //{
        //    _jwtSettings = jwtSettings.Value;
        //}


        //public string TokenOlustur(UserDTO userDTO)
        //{
        //    if (_jwtSettings.Key == null) throw new Exception("Jwt ayarlarındaki anahtar null olamaz!");
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claimDizisi = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, userDTO.UserName!),
        //        new Claim(ClaimTypes.Role,userDTO.Role!)
        //    };

        //    var token = new JwtSecurityToken(
        //        _jwtSettings.Issuer,
        //        _jwtSettings.Audience,
        //        claimDizisi,
        //        expires: DateTime.Now.AddHours(1),
        //        signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

    }
    
}
