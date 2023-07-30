using Aplication.Services.Interfaces;
using Azure;
using Domain.Models.Domain;
using Domain.Response;
using Microsoft.IdentityModel.Tokens;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Aplication.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IClubRepository _clubRepository;

        public UserServices(IUserRepository userRepository, IClubRepository clubRepository)
        {
            _userRepository = userRepository;
            _clubRepository = clubRepository;
        }

        public async Task<ServiceResponse<User>> AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
            var checkEmail = await _userRepository.GetEmailAsync(user.Email);
            if (checkEmail == null)
            {
                return new ServiceResponse<User>()
                {
                    Success = true,
                    Data = user,
                    Message = "Dodano użytkownika!"
                };
            }
            return new ServiceResponse<User>()
            {
                Success = false,
                Message = "Podany adres email istnieje w bazie danych!"
            };
        }

        public async Task<ServiceResponse<List<User>>> GetAllCoaches()
        {
            var allCoaches = await _userRepository.GetAllCoaches();
            if (allCoaches == null)
            {
                return new ServiceResponse<List<User>>()
                {
                    Success = false,
                    Data = null,
                    Message = "Brak trenerów"
                };
            }
            return new ServiceResponse<List<User>> 
            { 
                Success = false,
                Data = allCoaches, 
                Message= "Wszyscy trenerzy"
            };
        }

        public async Task<ServiceResponse<List<User>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (users.IsNullOrEmpty())
            {
                return new ServiceResponse<List<User>>()
                {
                    Success= false,
                    Message= "Brak użytkowników"
                };
            }
            return new ServiceResponse<List<User>>()
            {
                Success = true,
                Data = users,
                Message = "Wszyscy uzytkownicy"
            };
        }

        public async Task<ServiceResponse<List<User>>> GetCoachesWithoutClub()
        {
            var coaches = await _userRepository.GetCoachWithoutClub();
            if (coaches.IsNullOrEmpty())
            {
                return new ServiceResponse<List<User>>()
                {
                    Data = null,
                    Success= false,
                    Message = "Brak trenerow bez klubu"
                };
            }
            return new ServiceResponse<List<User>>
            {
                Data= coaches,
                Success= true,
                Message="Wszyscy trenerzy"
            };
        }

        public async Task<string> GetEmailAsync(string email)
        {
            
            var emailFromUser = await _userRepository.GetEmailAsync(email);
            if (emailFromUser.IsNullOrEmpty())
            {
                return null;
            }
            return emailFromUser;
            
        }

        public async Task<string> GetPasswordByEmailAsync(string email)
        {
            var passwordFromUser = await _userRepository.GetPasswordByEmailAsync(email);
            if (passwordFromUser.IsNullOrEmpty())
            {
                return "Błędne hasło";
            }
            return passwordFromUser;
        }

        public async Task<string> GetRoleByEmailAsync(string email)
        {
            var role = await _userRepository.GetRoleByEmailAsync(email);
            if (role.IsNullOrEmpty())
            {
                
            }
            return role;
            
        }

        public async Task<int> GetUserIdByEmailAsync(string email)
        {
            var userId = await _userRepository.GetUserIdByEmailAsync(email);
            if(userId == 0)
            {
                
            }
            return userId;
        }

        public async Task<ServiceResponse<List<User>>> GetPresidentWithoutClub()
        {
            var users = await _userRepository.GetAllAsync();
            var clubs = await _clubRepository.GetAllAsync();

            //var users = usersResponse;
            //var clubs = clubsResponse;

            var usersWithoutClub = users
                .Where(user => user.Role == "President" || user.Role == "User")
                .Where(user => !clubs.Any(club => club.UserId == user.Id))
                .ToList();
            return new ServiceResponse<List<User>>
            {
                Success=true,
                Data = usersWithoutClub,
                Message="Uzytkownicy bez klubow"
            };
        }

        public void Login(string email)
        {
            var emailFromUser = _userRepository.GetByEmailAsync(email);
            var passwordFromUser = _userRepository.GetPasswordByEmailAsync(email);
            if(emailFromUser== null || passwordFromUser == null ) 
            {
                new ServiceResponse<User>()
                {
                    Message = "Email lub hasło nie istnieje",
                    Success= false,
                };
            }
            
            new ServiceResponse<User>()
            {
                Message = "Zwrócono Usera",
                Success = true
            };
            
        }

        public async Task<ServiceResponse<List<User>>> GetUsersWithoutAnyFunction()//pobranie uzytkownikow, ktorzy nie pelnią żadnych funkcji
        {
            var users = await _userRepository.GetAllAsync();
            var clubs = await _clubRepository.GetAllAsync();
            var usersWithoutFunction = users
                .Where(users => !clubs.Any(club => club.UserId == users.Id)).ToList();

            if (usersWithoutFunction.IsNullOrEmpty())
            {
                return new ServiceResponse<List<User>>()
                {
                    Success = false
                };
            }

            return new ServiceResponse<List<User>>()
            {
                Data = usersWithoutFunction,
                Success = true,
                Message = "Wszyscy użytkownicy bez funkcji w klubach"
            };
        }

        public async Task<ServiceResponse<User>> GetUserById(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<User>()
                {
                    Success = false,
                    Message = "Problem z pobraniem użytkownika o podanym Id"
                };
            }
            return new ServiceResponse<User>()
            {
                Data = user,
                Success = true,
                Message = "Pobrano użytkownika"
            };

        }
    }
}
