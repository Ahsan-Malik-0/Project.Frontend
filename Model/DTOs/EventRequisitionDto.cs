using System.ComponentModel.DataAnnotations;

namespace Project.Frontend.Model.DTOs
{
    public class CreateEventRequisitionDto
    {
        [Required(ErrorMessage = "Subject is Required")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Body is Required")]
        public string Body { get; set; } = string.Empty;
        public DateTime RequestedDate { get; set; }
        public required decimal RequestedAmount { get; set; }
        public Guid EventId { get; set; }
    }

    public class UpdateEventRequisitionDto
    {
        public required string Subject { get; set; } = string.Empty;
        public required string Body { get; set; } = string.Empty;
        public required DateTime RequestedDate { get; set; }
        public required decimal RequestedAmount { get; set; }
        public required ICollection<EventRequirementDto> EventRequirements { get; set; } 
    }

    public class PendingEventRequisitionDto
    {
        public Guid Id { get; set; }
        public required string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public required string Status { get; set; }
        public string? ReviewMessage { get; set; }
    }

    public class EventRequisitionDetailsDto
    {
        public required string Subject { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime RequestedDate { get; set; }
        public required string Body { get; set; }
        public required ICollection<EventRequirementDto> EventRequirements { get; set; }
        public required string ChairpersonName { get; set;}
        public required string SocietyName { get; set;}
    }

    // For student affairs and administration to view details of a pending requisition
    public class ViewRequisitionRequestDetailsDto
    {
        public Guid Id { get; set; }
        public required string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime RequestedDate { get; set; }
        public required string SocietyName { get; set; }
        public decimal RequestedAmount { get; set; }
        public decimal AllotedAmount { get; set; }

    }

    public class ReviewEventRequisitionDto
    {
        public required string Status { get; set; }
        public string? ReviewMessage { get; set; }
    }

    public class ApproveEventRequisitionDto
    {
        public DateTime AllocatedDate { get; set; }
        public required decimal AllocatedAmount { get; set; }
        public required decimal BiitContribution { get; set; }
    }

    public class ViewRequisitionDetailsForFinanceDto
    {
        public Guid RequisitionId { get; set; }
        public string? ChairpersonName { get; set; }
        public required string SocietyName { get; set; }
        public required string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public Decimal AllotedBudget { get; set; }
    }

    public class ViewRequisitionDetailsForStudentAffairsDto : ViewRequisitionDetailsForFinanceDto 
    {
        public required string Status { get; set; }
        public string? ReviewMessage { get; set; }
    }

    public class ResponseMessageDto
    {
        public required string ResponseMessage { get; set; }
    }
}
