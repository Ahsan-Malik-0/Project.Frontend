using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Project.Frontend.Model.DTOs
{
    public class MemberDto
    {
    }

    public class MemberProfileDto
    {
        public required string Name { get; set; }
        public required string Username { get; set; }
        //public required string HashPassword { get; set; }
        public string? Picture { get; set; }
        public Guid SocietyId { get; set; }
    }

    public class MemberProfileUpdateDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string OldHashPassword { get; set; }
        public required string NewHashPassword { get; set; }
        public string? Picture { get; set; }
    }

    public class ChairpersonDetailForRequisitionFormDto
    {
        public required string Name { get; set; }
        public required string Role { get; set; }
        public required string SocietyName { get; set; }
    }
}
