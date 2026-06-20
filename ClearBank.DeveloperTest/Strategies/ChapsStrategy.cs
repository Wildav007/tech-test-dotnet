using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Strategies;

/// <inheritdoc />
public class ChapsPaymentStrategy : IPaymentStrategy
{
    /// <inheritdoc />
    public PaymentScheme Scheme => PaymentScheme.Chaps;

    /// <inheritdoc />
    public bool IsValid(Account account, MakePaymentRequest request)
    {
        return account != null && 
               account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) && 
               account.Status == AccountStatus.Live;
    }
}
