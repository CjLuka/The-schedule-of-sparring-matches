﻿using Aplication.Services.Interfaces;
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
        private readonly IBranchClubRepository _branchClubRepository;

        public UserServices(IUserRepository userRepository, IClubRepository clubRepository, IBranchClubRepository branchClubRepository)
        {
            _userRepository = userRepository;
            _clubRepository = clubRepository;
            _branchClubRepository = branchClubRepository;
        }

        public async Task<ServiceResponse<User>> AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
            var checkEmail = await _userRepository.GetEmailAsync(user.Email);
            if (checkEmail == null)
            {
                return new ServiceResponse<User>(user, true);
            }

            return new ServiceResponse<User>(false, "Podany adres email istnieje w bazie danych!");
        }

        public async Task<ServiceResponse<List<User>>> GetAllCoaches()
        {
            var allCoaches = await _userRepository.GetAllCoaches();
            if (allCoaches == null)
            {
                return new ServiceResponse<List<User>>(false, "Brak trenerów");
            }

            return new ServiceResponse<List<User>>(allCoaches, true);
        }

        public async Task<ServiceResponse<List<User>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (users.IsNullOrEmpty())
            {
                return new ServiceResponse<List<User>>(false, "Brak użytkowników");
            }

            return new ServiceResponse<List<User>>(users, true);
        }

        public async Task<ServiceResponse<List<User>>> GetCoaches()
        {
            var coaches = await _userRepository.GetCoaches();
            if (coaches.IsNullOrEmpty())
            {
                return new ServiceResponse<List<User>>(false, "Brak trenerów bez klubu");
            }

            return new ServiceResponse<List<User>>(coaches, true);
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

        public async Task<string> GetUserIdByEmailAsync(string email)
        {
            var userId = await _userRepository.GetUserIdByEmailAsync(email);
            if(userId == string.Empty)
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
                //.Where(user => user.Role == "President" || user.Role == "User")
                .Where(user => !clubs.Any(club => club.UserId == user.Id))
                .ToList();

            return new ServiceResponse<List<User>>(usersWithoutClub, true);
        }

        public void Login(string email)
        {
            var emailFromUser = _userRepository.GetByEmailAsync(email);
            var passwordFromUser = _userRepository.GetPasswordByEmailAsync(email);
            if(emailFromUser== null || passwordFromUser == null ) 
            {
                new ServiceResponse<User>(false, "Email lub hasło nie istnieje");
            }

            new ServiceResponse<User>(true);        
        }

        public async Task<ServiceResponse<List<User>>> GetUsersWithoutAnyFunction()//pobranie uzytkownikow, ktorzy nie pelnią żadnych funkcji
        {
            var users = await _userRepository.GetAllAsync();
            var clubs = await _clubRepository.GetAllAsync();
            var branches = await _branchClubRepository.GetAllBranchClubAsync();
            var usersWithoutFunction = users
                .Where(users => !clubs.Any(club => club.UserId == users.Id)).ToList()
                .Where(users => !branches.Any(branch => branch.UserId == users.Id)).ToList();

            if (usersWithoutFunction.IsNullOrEmpty())
            {
                return new ServiceResponse<List<User>>(false);
            }

            return new ServiceResponse<List<User>>(usersWithoutFunction, true);
        }

        public async Task<ServiceResponse<User>> GetUserById(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<User>(false, "Problem z pobraniem użytkownika o podanym Id");
            }

            return new ServiceResponse<User>(user, true);
        }

        public async Task<ServiceResponse<List<User>>> GetAll()
        {
            List<User> users = await _userRepository.GetAllUsersAsync();

            return new ServiceResponse<List<User>>(users, true);
        }
    }
}
