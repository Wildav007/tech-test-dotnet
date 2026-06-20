using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Strategies;

/// <inheritdoc />
public class BacsPaymentStrategy : IPaymentStrategy
{
    /// <inheritdoc />
    public PaymentScheme Scheme => PaymentScheme.Bacs;

    /// <inheritdoc />
    public bool IsValid(Account account, MakePaymentRequest request)
    {
        return account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
    }
}
