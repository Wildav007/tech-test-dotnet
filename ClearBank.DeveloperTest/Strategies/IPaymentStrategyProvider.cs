using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Strategies;

/// <summary>
/// Provides access to the appropriate <see cref="IPaymentStrategy"/> based on a <see cref="PaymentScheme"/>.
/// </summary>
public interface IPaymentStrategyProvider
{
    /// <summary>
    /// Retrieves the strategy associated with the specified payment scheme.
    /// </summary>
    /// <param name="scheme">The payment scheme to look up.</param>
    /// <returns>An implementation of <see cref="IPaymentStrategy"/>.</returns>
    /// <exception cref="System.NotSupportedException">Thrown when no strategy is found for the scheme.</exception>
    IPaymentStrategy GetStrategy(PaymentScheme scheme);
}
