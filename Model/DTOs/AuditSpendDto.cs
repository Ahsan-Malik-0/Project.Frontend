namespace Project.Frontend.Model.DTOs
{
    public class CreateAuditSpendDto
    {
        public required string VenderName { get; set; }
        public string? ItemDescription { get; set; }
        public required decimal Amount { get; set; }
        public string? ReceiptPicture { get; set; }
    }

    public class UpdateAuditSpendDto : CreateAuditSpendDto { }
}
