using System.ComponentModel.DataAnnotations;

namespace Project.Frontend.Shared.Model.DTOs
{
    public class AddEventDto
    {
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public Guid SocietyId { get; set; }
        public required ICollection<EventRequirementDto> Requirements { get; set; }

    }

    public class UpdateEventDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public required ICollection<EventRequirementDto> Requirements { get; set; }
    }
}
