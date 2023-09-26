namespace DocumentAnonymization.Models
{
    public class AnonymizationRequestDto
    {
        public Guid RequestId { get; set; }

        public string? RequestorName {  get; set; }

        public DateTime RequestApprovalDate { get; set; }

        public string? RequestApproverIdentity { get; set; }

        public string? RequestApproverName { get; set; }

        public string? CaseId { get; set; }

        public DateTime CaseApprovalDate { get; set; }

        public long LegacyId { get; set; }

        public long HousehouldId { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
