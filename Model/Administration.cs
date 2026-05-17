using System;

namespace Project.Frontend.Model
{

    public class Administration
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string HashPassword { get; set; }
        public required string Role { get; set; }
        public string? Picture { get; set; }
    }
}
