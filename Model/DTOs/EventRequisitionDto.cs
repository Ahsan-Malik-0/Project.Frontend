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
}
