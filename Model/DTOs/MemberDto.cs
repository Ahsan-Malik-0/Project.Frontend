using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Project.Frontend.Model.DTOs
{
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
        public string? OldHashPassword { get; set; }
        public string? NewHashPassword { get; set; }
        public string? Picture { get; set; }
    }

    public class ViewMemberProfileDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
    }

    public class EditMemberProfileDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public required string Username { get; set; }
        [Required(ErrorMessage = "New password is required.")]
        public required string newHashPassword { get; set; }
    }

    public class ChairpersonDetailsForRequisitionDto
    {
        public string? ChairpersonName { get; set; }
        public string? SocietyName { get; set; }
    }
}
