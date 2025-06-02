using WebApi.Models;

namespace WebApi.Interfaces;

public interface IBookingEmailService
{
    Task<ResponseResult> SendBookingConfirmationAsync(SendBookingConfirmationRequest request);
}
