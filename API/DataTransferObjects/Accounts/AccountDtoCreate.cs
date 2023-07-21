﻿namespace API.DataTransferObjects.Accounts;

public class AccountDtoCreate
{
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }
}