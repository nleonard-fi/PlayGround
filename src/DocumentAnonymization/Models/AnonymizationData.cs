namespace DocumentAnonymization.Models
{
    public class AnonymizationData
    {
        public string? AnonymizedHouseholdName { get; set; }

        public string? AnonymizedDocumentNotes { get; set; }

        public long LegacyId { get; set; }

        public long HouseholdId { get; set; }
    }
}
