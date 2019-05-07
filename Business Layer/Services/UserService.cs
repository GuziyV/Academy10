using AutoMapper;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Data_Access_Layer.Contexts;
using Shared.DTos;
using Shared.Enums;

namespace BL.Services {
	public class UserService {
	    private readonly AirportContext context;
	    private readonly IMapper mapper;
		public UserService(AirportContext context, IMapper mapper) 
		{
			this.context = context;
			this.mapper = mapper;
		}

        public async Task<UserDTO> Authentificate(string nickname, string password)
        {
            if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Username or password can not be empty");

            var user = (await context.Set<User>().SingleOrDefaultAsync(u => u.Login == nickname));
            if(user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new ArgumentException("Wrong email or password");
            }
            await context.SaveChangesAsync();
            return mapper.Map<User, UserDTO>(user);
        }

        public async Task<UserDTO> PostAsync(UserDTO entity)
        {
            if (string.IsNullOrEmpty(entity.Password) || string.IsNullOrEmpty(entity.Login))
            {
                throw new ArgumentException("Login and password are required");
            }
            var user = mapper.Map<UserDTO, User>(entity);
            byte[] passwordHash, passwordSalt;
			CreatePasswordHash(entity.Password, passwordHash:out passwordHash, passwordSalt: out passwordSalt);
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;
			user.Role = Role.User;
            context.Add(user);
            await context.SaveChangesAsync();
            return mapper.Map<User, UserDTO>(user);
        }


		// private helper methods
		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
			if (password == null) throw new ArgumentNullException("password");
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

			using (var hmac = new System.Security.Cryptography.HMACSHA512()) {
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt) {
			if (password == null) throw new ArgumentNullException("password");
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
			if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
			if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

			using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt)) {
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++) {
					if (computedHash[i] != storedHash[i]) return false;
				}
			}

			return true;
		}
	}
}
