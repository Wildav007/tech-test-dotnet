using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Strategies;

/// <summary>
/// Defines a strategy for validating payment requests against specific account rules and schemes.
/// </summary>
public interface IPaymentStrategy
{
    /// <summary>
    /// Gets the payment scheme supported by this strategy.
    /// </summary>
    PaymentScheme Scheme { get; }

    /// <summary>
    /// Validates whether a payment request is valid for the given account.
    /// </summary>
    /// <param name="account">The account to validate against.</param>
    /// <param name="request">The payment request details.</param>
    /// <returns>True if the payment is valid; otherwise, false.</returns>
    bool IsValid(Account account, MakePaymentRequest request);
}
