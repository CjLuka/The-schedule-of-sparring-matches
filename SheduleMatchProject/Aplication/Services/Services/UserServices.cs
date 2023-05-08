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

namespace Aplication.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            _userRepository= userRepository;
        }

        public async Task<string> GetEmailAsync(string email)
        {
            
            var emailFromUser = await _userRepository.GetEmailAsync(email);
            if (emailFromUser.IsNullOrEmpty())
            {
                return "Brak emaila w bazie danych";
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
    }
}
