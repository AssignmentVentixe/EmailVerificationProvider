using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class SendBookingConfirmationRequest
{
    [Required]
    [EmailAddress]
    public string BookingEmail { get; set; } = null!;

    [Required]
    public string EventId { get; set; } = null!;

    [Required]
    public string EventName { get; set; } = null!;

    [Required]
    public string EventLocation { get; set; } = null!;

    [Required]
    public DateTime BookedDate { get; set; }

    [Required]
    public DateTime EventDate { get; set; }
}
