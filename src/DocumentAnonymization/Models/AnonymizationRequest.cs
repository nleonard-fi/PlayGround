using System.ComponentModel.DataAnnotations;

namespace DocumentAnonymization.Models
{
    public class AnonymizationRequest
    {
        [Range(1, long.MaxValue)]
        public long RequestId { get; set; }

        /// <summary>
        /// Something like the FIIMAC identity. e.g. nleonard for Nicholas Leonard.
        /// </summary>
        [Required]
        [MinLength(3)]
        public string? RequestorIdentity { get; set; }

        [Required]
        [MinLength(3)]
        public string? RequestorName { get; set; }

        public DateTime RequestApprovalDate { get; set; }

        [Required]
        [MinLength(3)]
        public string? RequestApproverName { get; set; }

        /// <summary>
        /// Something like the FIIMAC identity. e.g. nleonard for Nicholas Leonard.
        /// </summary>
        [Required]
        [MinLength(3)]
        public string? RequestApproverIdentiy {  get; set; }

        [Required]
        [MinLength(3)]
        public string? CaseId { get; set; }

        public DateTime CaseApprovalDate { get; set; }

        [Range(1, long.MaxValue)]
        public long LegacyId {  get; set; }

        [Range(1, long.MaxValue)]
        public long HouseholdId { get; set; }
    }
}
