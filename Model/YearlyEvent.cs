using Project.Frontend.Users.Chairperson.Pages;

namespace Project.Frontend.Model
{
    public class YearlyEvent
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public decimal? EstimateAmount { get; set; }
        public required string EstimateMonth { get; set; }

        public Guid YearlyBudgetId { get; set; }
        public HandleYearlyBudget? YearlyBudget { get; set; }

        public ICollection<YearlyEventRequirement>? YearlyEventRequirements { get; set; }
    }
}
