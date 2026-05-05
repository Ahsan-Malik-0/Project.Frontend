using System.ComponentModel.DataAnnotations;
namespace Project.Frontend.Model.DTOs
{
    public class YearlyBudgetResponseDto
    {
        public Guid Id { get; set; }
        public string Session { get; set; } = string.Empty;
        public decimal RequestedAmount { get; set; }
        public decimal AllotedAmount { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime AllotedDate { get; set; }
        public decimal Credits { get; set; }
        public Guid SocietyId { get; set; }
        public List<YearlyEventResponseDto>? YearlyEvents { get; set; }
    }
    
    public class CreateYearlyBudgetDto
    {
        [Required]
        public required string Session { get; set; }
        [Required (ErrorMessage = "Requested amount is required.")]
        public required decimal RequestedAmount { get; set; }
        public DateTime RequestedDate { get; set; }
        public ICollection<CreateYearlyEventDto>? YearlyEvents { get; set; }
    }
}