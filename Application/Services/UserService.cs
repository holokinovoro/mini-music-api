﻿using Application.Dto;
using Application.Interfaces.Auth;
using Application.Interfaces.IRepository;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMemoryCache _cache;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(
            IMemoryCache memoryCache,
            IJwtProvider jwtProvider,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _cache = memoryCache;
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Register(string userName, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), userName, hashedPassword, email);

            await _userRepository.Add(user);
        }

        public async Task<string> Login(string email, string password)
        {
            _cache.TryGetValue(email, out User? user);

            if(user == null)
            {
                user = await _userRepository.GetByEmail(email);

                if(user != null)
                {
                    var cacheOptions = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                        Priority = 0,
                    };

                    _cache.Set(email, user, cacheOptions);
                }
            }


            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
        
    }
}
