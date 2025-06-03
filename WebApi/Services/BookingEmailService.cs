using Azure;
using Azure.Communication.Email;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services;


public class BookingEmailService(IConfiguration configuration, EmailClient emailClient) : IBookingEmailService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly EmailClient _emailClient = emailClient;

    public async Task<ResponseResult> SendBookingConfirmationAsync(SendBookingConfirmationRequest request)
    {
        try
        {
            var subject = $"Booking Confirmation";

            var plainTextContent = $@"
                    Hello,

                    Thank you for your booking. Here are your booking details:

                    Event: {request.EventName}
                    Event Location: {request.EventLocation}
                    Event Date:{request.EventDate}

                    Date of booking: {request.BookedDate:dd MMMM, yyyy, HH mm}


                    We look forward to seeing you there.

                    Best regards,
                    Ventixe

                    © 2025 Ventixe. All rights reserved.
                    ";

            var htmlContent = $@"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <title>Booking Confirmation</title>
                </head>
                <body style=""font-family: Arial, sans-serif; background-color:#f4f4f4; margin:0; padding:0;"">
                    <div style=""max-width:600px; margin:40px auto; background:#ffffff; border-radius:8px; overflow:hidden;"">
                        <div style=""background-color:#f26cf9; color:#ffffff; padding:20px; text-align:center;"">
                            <h1 style=""margin:0; font-size:24px;"">Booking Confirmation</h1>
                        </div>
                        <div style=""padding:20px; color:#333333;"">
                            <p>Hello,</p>
                            <p>Thank you for your booking! Here are your booking details:</p>
                            <ul>
                                <li><strong>Event:</strong> {request.EventName}</li>
                                <li><strong>Event Location:</strong>{request.EventLocation}</li>
                                <li><strong>Event Date:</strong>Event Date:{request.EventDate}</li>
                                <br>
                                <li><strong>Date of booking: {request.BookedDate:dd MMMM, yyyy, HH mm}</li>
                            </ul>
                            <p>We look forward to seeing you there.</p>
                            <p>Best regards,<br>Ventixe</p>
                        </div>
                        <div style=""background-color:#f4f4f4; text-align:center; padding:10px; font-size:12px; color:#777777;"">
                            © 2025 Ventixe. All rights reserved.
                        </div>
                    </div>
                </body>
                </html>";

            var senderAddress = _configuration["ACS:SenderAddress"]!;

            var emailMessage = new EmailMessage(
                senderAddress: senderAddress,
                recipients: new EmailRecipients([new EmailAddress(request.BookingEmail)]),
                content: new EmailContent(subject)
                {
                    PlainText = plainTextContent,
                    Html = htmlContent
                });

            var emailSendOperation = await _emailClient.SendAsync(WaitUntil.Started, emailMessage);

            return new ResponseResult
            {
                Succeeded = true,
                Message = "Booking confirmation email sent successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseResult
            {
                Succeeded = false,
                Error = $"Failed to send booking confirmation email: {ex.Message}"
            };
        }
    }


}
