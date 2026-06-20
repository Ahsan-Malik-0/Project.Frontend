namespace Project.Frontend.Model
{
    public class VirtualSociety
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Session {  get; set; }
    }
}
