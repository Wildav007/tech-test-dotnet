using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.TestSupport;

public class AccountBuilder
{
    private string _accountNumber = string.Empty;
    private AllowedPaymentSchemes _allowedPaymentSchemes;
    private decimal _balance;
    private AccountStatus _accountStatus;

    private AccountBuilder()
    {
        // Prevent using default constructor
    }

    public static AccountBuilder New() => new();
    
    public AccountBuilder WithAccountNumber(string accountNumber)
    {
        _accountNumber = accountNumber;
        return this;
    }
    public AccountBuilder WithAllowedPaymentSchemes(AllowedPaymentSchemes allowedPaymentSchemes)
    {
        _allowedPaymentSchemes = allowedPaymentSchemes;
        return this;
    }
    public AccountBuilder WithBalance(decimal balance)
    {
        _balance = balance;
        return this;
    }
    public AccountBuilder WithAccountStatus(AccountStatus accountStatus)
    {
        _accountStatus = accountStatus;
        return this;
    }

    public Account Build() => new()
    {
        AccountNumber = _accountNumber,
        AllowedPaymentSchemes = _allowedPaymentSchemes,
        Balance = _balance,
        Status = _accountStatus
    };
}