using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using PostagensApi.Data;
using PostagensApi.Data.Models;
using PostagensApi.Requests.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PostagensApi.Services
{
    public class AccountService(AppDbContext _db) : IAccountInterface
    {
        public string Login(UserLoginRequest request)
        {
            try
            {
                var user = _db.User.FirstOrDefault(x => x.Name == request.Username && x.Password == request.Password);

                if (user != null)
                {
                    var token = gerarTokenJWT(user);
                    return token;
                }
                return "Não foi possivel autenticar";
            }
            catch
            {
                return "Algo deu errado";
            }
        }
        private string gerarTokenJWT(User user)
        {
            string chaveSecreta = Environment.GetEnvironmentVariable("chaveSecreta");

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", user.Name),
                new Claim("nome", "administrador"),

            };



            var token = new JwtSecurityToken(

                issuer: "sua_empresa",
                audience: "sua_aplicacao",
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credencial
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

