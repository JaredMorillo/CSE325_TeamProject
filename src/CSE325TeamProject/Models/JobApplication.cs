using System.ComponentModel.DataAnnotations;

namespace CSE325TeamProject.Models;

public sealed class JobApplication
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Company { get; set; } = "";

    [Required]
    [StringLength(120)]
    public string JobTitle { get; set; } = "";

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Applied";

    public DateTime AppliedOn { get; set; } = DateTime.UtcNow.Date;

    [StringLength(2000)]
    public string? Notes { get; set; }

    [Required]
    public string UserId { get; set; } = "";
}
