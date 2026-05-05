using System;

namespace Project.Frontend.Auth.Model;

public class LoginDto
{
    public required string Username { get; set; }
    public required string HashPassword { get; set; }
}
