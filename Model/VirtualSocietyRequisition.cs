using System.Text.Json.Serialization;

namespace Project.Frontend.Model
{
    public class VirtualSocietyRequisition
    {
        public Guid Id { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public required string Status { get; set; }
        public string? ReviewMessage { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime AllocatedDate { get; set; }
        public decimal RequestAmount { get; set; }
        public decimal AllocatedAmount { get; set; }
        public decimal BiitContribution { get; set; }
        public Guid VirtualSocietyId { get; set; }
        [JsonIgnore]
        public VirtualSociety? VirtualSociety { get; set; }
    }
}
