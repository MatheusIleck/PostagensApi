using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using PostagensApi.Data;
using PostagensApi.Models;
using PostagensApi.Requests.User;
using PostagensApi.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PostagensApi.Services
{
    public class AccountService(AppDbContext _db) : IAccountInterface
    {
        public async Task<Response<Like>> LikeAPost(LikeAPostRequest request)
        {
            try
            {
                var result = new Like
                {
                    IdPost = request.postId,
                    IdUsuario = request.UserId,
                };

                if (result == null)
                    return new Response<Like>(null, 400, "Solicitação invalida");

                await _db.Likes.AddAsync(result);
                await _db.SaveChangesAsync();

                return new Response<Like>(result, 200, "Post curtido com sucesso!");
            }
            catch
            {
                return new Response<Like>(null, 500, "Solicitação invalida");

            }
        }

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

        public async Task<Response<User>> Register(UserRegisterRequest request)
        {
            try
            {
                var newUser = new User
                {
                    Name = request.name,
                    Password = request.password,
                    Email = request.email,
                    Role = "usuario"
                };
                if (newUser == null)
                    return new Response<User>(null, 400, "Usuario não registrado");

                await _db.User.AddAsync(newUser);
                await _db.SaveChangesAsync();

                return new Response<User>(newUser, 201, "Usuario registrado com sucesso!");


            }
            catch
            {
                    return new Response<User>(null, 500, "Algo deu errado");

            }
        }

        private string gerarTokenJWT(User user)
        {
            string chaveSecreta = Environment.GetEnvironmentVariable("chaveSecreta");

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId", user.Id.ToString())

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

