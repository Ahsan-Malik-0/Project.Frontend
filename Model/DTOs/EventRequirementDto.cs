using System.ComponentModel.DataAnnotations;

namespace Project.Frontend.Model.DTOs
{
    public class EventRequirementDto
    {
        public required string Type { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
