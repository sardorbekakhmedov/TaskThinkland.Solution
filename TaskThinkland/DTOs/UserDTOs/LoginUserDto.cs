﻿namespace TaskThinkland.Api.DTOs.UserDTOs;

public class LoginUserDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}