using WebApi.Models;

namespace WebApi.Interfaces;

public interface IVerificationService
{
    Task<ResponseResult> SendVerificationCodeAsync(SendVerificationCodeRequest request);
    void SaveVerificationCode(SaveVerificationCodeRequest request);
    ResponseResult VerifyVerificationCode(VerifyVerificationCodeRequest request);

}