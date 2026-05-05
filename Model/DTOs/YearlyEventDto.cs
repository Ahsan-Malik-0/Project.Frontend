using System.ComponentModel.DataAnnotations;

namespace Project.Frontend.Model.DTOs
{
    public class YearlyEventResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? EstimateAmount { get; set; }
        public string EstimateMonth { get; set; } = string.Empty;
        public List<YearlyEventRequirementResponseDto>? YearlyEventRequirements { get; set; }
    }
    
    public class CreateYearlyEventDto
    {
        [Required (ErrorMessage = "Event name is required.")]
        public required string Name { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Estimate amount must be a positive number.")]
        public decimal? EstimateAmount { get; set; }
        [Required (ErrorMessage = "Estimate month is required.")]
        public required string EstimateMonth { get; set; }
        public ICollection<CreateYearlyEventRequirementDto>? YearlyEventRequirements { get; set; }
    }
}