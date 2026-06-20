using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Strategies;

/// <inheritdoc />
public class FasterPaymentsPaymentStrategy : IPaymentStrategy
{
    /// <inheritdoc />
    public PaymentScheme Scheme => PaymentScheme.FasterPayments;

    /// <inheritdoc />
    public bool IsValid(Account account, MakePaymentRequest request)
    {
        return account != null && 
               account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) && 
               account.Balance >= request.Amount;
    }
}
