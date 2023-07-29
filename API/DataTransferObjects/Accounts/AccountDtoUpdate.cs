using API.Models;
using API.Utilities.Handlers;

namespace API.DataTransferObjects.Accounts;

public class AccountDtoUpdate
{
    public Guid Guid { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int? Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? ExpiredTime { get; set; }
    
    // implicit operator
    public static implicit operator Account(AccountDtoUpdate accountDtoUpdate)
    {
        return new Account
        {
            Guid = accountDtoUpdate.Guid,
            Password = HashingHandler.HashPassword(accountDtoUpdate.Password),
            IsDeleted = accountDtoUpdate.IsDeleted,
            Otp = accountDtoUpdate.Otp,
            IsUsed = accountDtoUpdate.IsUsed,
            ExpiredTime = accountDtoUpdate.ExpiredTime,
            ModifiedDate = DateTime.UtcNow
        };
    }
    
    // explicit operator
    public static explicit operator AccountDtoUpdate(Account account)
    {
        return new AccountDtoUpdate
        {
            Guid = account.Guid,
            Password = account.Password,
            IsDeleted = account.IsDeleted,
            Otp = account.Otp,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime
        };
    }
}
