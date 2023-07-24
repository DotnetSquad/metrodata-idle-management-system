namespace API.DataTransferObjects.Accounts;

public class AccountDtoChangePassword
{
    public string Email { get; set; }
    public int Otp { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
