using System.Text.Json.Serialization;

namespace Project.Frontend.Shared.Model
{
    public class EventRequirement
    {
        public int Id { get; set; }
        public required string Type { get; set; }
        public required string Name { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Guid EventId { get; set; }

        [JsonIgnore]
        public Event? _event { get; set; }

    }
}
