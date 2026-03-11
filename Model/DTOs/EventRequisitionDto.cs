using System.ComponentModel.DataAnnotations;

namespace Project.Frontend.Model.DTOs
{
    public class SendEventRequisitionDto
    {
        [Required(ErrorMessage = "Subject is Required")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Body is Required")]
        public string Body { get; set; } = string.Empty;
        public decimal requestedAmoount {  get; set; }
        public Guid EventId { get; set; }
    }

    public class PendingEventRequisitionsDto
    {
        public Guid Id { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public required string Status { get; set; }
        public required ICollection<EventRequirementDto> EventRequirements { get; set; }
    }

    public class PendingEventRequisitionDetailsDto
    {
        public Guid Id { get; set; }
        public required string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public required string Status { get; set; }
    }
}
