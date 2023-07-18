using BLL.JWT;
using BLL.Operation.Implementations;
using BLL.Operation.Interface;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KimlikDenetimiController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        //private readonly TokenCreate _tokenCreate;

        //private readonly Authenticate _authenticate;

        private readonly IUserService _userService;


        public KimlikDenetimiController(IOptions<JwtSettings> jwtSettings, IUserService userService/*,TokenCreate tokenCreate, Authenticate authenticate*/)
        {
            _jwtSettings = jwtSettings.Value;
            _userService = userService;
            //_tokenCreate = tokenCreate;
            //_authenticate = authenticate;
        }

        [AllowAnonymous] //KimlikDenetimiController başında kimlik denetimi Auhtorize yazıldığı için giriş olmayan kişiler buralara erişemiyordu. Giriş yapmak için buraya ulaşılması gerektiğinden burda [AllowAnonymus] attiribütü ile burayı ulaşılabilir hale getiriyoruz.
        [HttpPost("Giris")]
        public async Task<IActionResult> Giris([FromBody] UserDTO userDTO)
        {
            var kullanici = await KimlikDenetimiYap(userDTO);

            if (kullanici == null)
            {
                return NotFound("Kullanıcı bulunamadı."); /*Eğer verilen kullanıcı adı Databasede yoksa bu cevabı döner.Kullanıcı varsa aşğıda token oluşturulur.*/
            }

            var token = TokenOlustur(userDTO);
            return Ok(token);
        }

        private string TokenOlustur(UserDTO userDTO)
        {
            if (_jwtSettings.Key == null) throw new Exception("Jwt ayarlarındaki anahtar null olamaz!");
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimDizisi = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDTO.UserName!),
                new Claim(ClaimTypes.Role,userDTO.Role)
            };

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimDizisi,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //private UserDTO KimlikDenetimiYap(UserDTO userDTO)
        //{
        //    string kullaniciAdi = userDTO.UserName!.ToLower();
        //    string kullaniciSifre = userDTO.UserPassword!;

        //    var kullanici = _userService.GetUser(kullaniciAdi, kullaniciSifre);

        //    if (kullaniciAdi == userDTO.UserName && kullaniciSifre == userDTO.UserPassword)
        //    {
        //        return userDTO;
        //    }

        //    return null;

        //}

        private async Task<UserDTO> KimlikDenetimiYap(UserDTO userDTO)
        {
            string kullaniciAdi = userDTO.UserName!.ToLower();
            string kullaniciSifre = userDTO.UserPassword!;

            var kullanici = await _userService.GetUser(kullaniciAdi, kullaniciSifre);

            if (kullanici != null)
            {
                return kullanici;
            }

            return null;
        }

    }
}
