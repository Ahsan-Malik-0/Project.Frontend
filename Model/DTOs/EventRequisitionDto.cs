
namespace Project.Frontend.Model.DTOs
{
    public class CreateEventRequisitionDto
    {
        public required string Subject { get; set; } = string.Empty;
        public required string Body { get; set; } = string.Empty;
        public required DateTime RequestedDate { get; set; }
        public required decimal RequestedAmount { get; set; }
        public required Guid EventId { get; set; }
    }

    public class UpdateEventRequisitionDto
    {
        public required string Subject { get; set; } = string.Empty;
        public required string Body { get; set; } = string.Empty;
        public required DateTime RequestedDate { get; set; }
        public required decimal RequestedAmount { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateTime EventDate { get; set; }
        public required ICollection<EventRequirementDto> EventRequirements { get; set; }
    }


    // For pending requisition list
    public class RequisitionDetailsForChairperson
    {
        public Guid Id { get; set; }
        public DateTime RequestedDate { get; set; }
        public decimal RequestedAmount { get; set; }
        public required string Status { get; set; }
        public string? ReviewMessage { get; set; } // changed by jaosn on 6 march 11:31
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public required string ChairpersonName { get; set; }
        public required Event Event { get; set; }
    }

    public class RequisitionDetailsForSA : RequisitionDetailsForChairperson { }

    public class RequisitionDetailsForFinance
    {
        public Guid RequisitionId { get; set; }
        public string? SocietyName { get; set; }
        public string? EventName { get; set; }
        public DateTime EventDate { get; set; }
        public decimal AllocatedAmount { get; set; }
        public decimal BiitContribution { get; set; }
        public string? ChairpersonName { get; set; }
    }

    // For more details after selecting a specific requisition
    public class EventRequisitionDetailsDto
    {
        public Guid Id { get; set; }
        public required string Subject { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime RequestedDate { get; set; }
        public required string Body { get; set; }
        public required ICollection<EventRequirementDto> EventRequirements { get; set; }
        public required string ChairpersonName { get; set; }
        public required string SocietyName { get; set; }
    }

    public class EventRequisitionHistoryForCP
    {
        public Guid RequisitionId { get; set; }
        public Guid EventId { get; set; }
        public required string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public required string RequisitionStatus { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime AllocatedDate { get; set; }
        public decimal RequestedAmount { get; set; }
        public decimal AllocatedAmount { get; set; }
        public decimal BiitContribution { get; set; }
        public List<EventRequirement>? Requirements { get; set; }
    }

    public class EventRequisitionHistoryForSA : EventRequisitionHistoryForCP
    {
        public string? SocietyName { get; set; }
        public string? ReviewMessage { get; set; }
    }

    public class EventRequisitionHistoryForFinance : EventRequisitionHistoryForCP
    {
        public string? SocietyName { get; set; }
        public string? ChairpersonName { get; set; }
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
        public decimal BiitContribution { get; set; }
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
        public decimal AllotedBudget { get; set; }
        public decimal BiitContribution { get; set; }
    }

    public class ViewRequisitionDetailsForFinanceHistoryDto : ViewRequisitionDetailsForFinanceDto
    {
        public Guid EventId { get; set; }
        public required string Status { get; set; }
        
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
