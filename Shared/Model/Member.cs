using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Frontend.Shared.Model
{
    public class Member
    {
        public Guid Id { get; set; }
        [StringLength(20)]
        public required string Name { get; set; }
        [StringLength(20)]
        public required string Username { get; set; }

        //[JsonIgnore]
        public string? HashPassword { get; set; }
        public required string Role { get; set; }
        public required string Picture { get; set; }
        public Guid SocietyId { get; set; }

        public Society? Society { get; set; }
    }
}
