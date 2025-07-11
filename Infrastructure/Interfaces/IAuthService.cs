﻿using Domain.DTOs;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IAuthService
{
    Task<Response<TokenDto>> Login(LoginDto loginDto);
    Task<Response<string>> Register(RegisterDto registerDto);
}