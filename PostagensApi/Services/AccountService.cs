using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using PostagensApi.Models;
using PostagensApi.Requests.User;
using PostagensApi.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PostagensApi.Services
{
    public class AccountService(db_SocialContext _db) : IAccountInterface
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

        public string UserAuth(UserLoginRequest request)
        {
            try
            {
            
                var user = _db.Users.FirstOrDefault(x => x.Name == request.Username && x.Password == request.Password);

                if (user != null)
                {
                    var token = gerarTokenJWT(user);
                    return token;
                }
                return "Invalid username or password";
            }
            catch
            {
                return "Something went wrong";
            }
        }

        public async Task<Response<User>> UserEdit(UserEditRequest request)
        {
            try
            {
                var user = _db.Users.First(x=> x.Id == request.Id);

                if(user == null)
                {
                    return new Response<User>(null, 400, "User not found");
                }

                user.Name = request.Name;
                user.Password = request.Password;
                user.Email = request.Email;
                

                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                return new Response<User>(user, 200, "User edited successfully");

            }
            catch
            {
                return new Response<User>(null, 500, "Something went wrong");

            }
        }

        public async Task<Response<User>> UserRegister(UserRegisterRequest request)
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
                    return new Response<User>(null, 400, "User registration failed");

                await _db.Users.AddAsync(newUser);
                await _db.SaveChangesAsync();

                return new Response<User>(newUser, 201, "User registered successfully!");


            }
            catch
            {
                    return new Response<User>(null, 500, "Something went wrong");

            }
        }

        public async Task<Response<User>> UserRemove(UserRemoveRequest request)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.id);


                if (user == null)
                    return new Response<User>(null, 404, "User not found");

                var UsersPost = await _db.Posts.Where(x => x.AuthorId == user.Id).ToListAsync();
                var UserLikes = await _db.Likes.Where(x=> x.IdUsuario == user.Id).ToListAsync();

                if (UsersPost != null)
                     _db.Posts.RemoveRange(UsersPost);

                if (UserLikes != null)
                    _db.Likes.RemoveRange(UserLikes);


                _db.Users.Remove(user);
                await _db.SaveChangesAsync();

                return new Response<User>(user, 200, "User deleted successfully!");

            }
            catch
            {
                return new Response<User>(null, 500, "Something went wrong");

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

