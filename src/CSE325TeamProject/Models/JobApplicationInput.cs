using System.ComponentModel.DataAnnotations;

namespace CSE325TeamProject.Models;

public sealed class JobApplicationInput
{
    [Required]
    [StringLength(120)]
    public string Company { get; set; } = "";

    [Required]
    [StringLength(120)]
    public string JobTitle { get; set; } = "";

    [Required]
    [RegularExpression("^(Applied|Interviewing|Offer|Rejected|Withdrawn)$")]
    public string Status { get; set; } = "Applied";

    [Required]
    public DateTime AppliedOn { get; set; } = DateTime.Today;

    [StringLength(2000)]
    public string? Notes { get; set; }
}
