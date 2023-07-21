using API.Models;

namespace API.DataTransferObjects.Accounts;

public class AccountDtoCreate
{
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int? Otp { get; set; }
    public bool? IsUsed { get; set; }
    public DateTime? ExpiredTime { get; set; }
    
    // implicit operator
    public static implicit operator Account(AccountDtoCreate accountDtoCreate)
    {
        return new Account
        {
            Password = accountDtoCreate.Password,
            IsDeleted = accountDtoCreate.IsDeleted,
            Otp = accountDtoCreate.Otp,
            IsUsed = accountDtoCreate.IsUsed,
            ExpiredTime = accountDtoCreate.ExpiredTime
        };
    }
    
    // explicit operator
    public static explicit operator AccountDtoCreate(Account account)
    {
        return new AccountDtoCreate
        {
            Password = account.Password,
            IsDeleted = account.IsDeleted,
            Otp = account.Otp,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime
        };
    }
}
