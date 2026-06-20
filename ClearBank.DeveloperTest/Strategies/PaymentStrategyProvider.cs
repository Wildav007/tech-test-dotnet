namespace ClearBank.DeveloperTest.Strategies;

using System;
using System.Collections.Generic;
using System.Linq;
using Types;

/// <inheritdoc />
public class PaymentStrategyProvider : IPaymentStrategyProvider
{
    private readonly IEnumerable<IPaymentStrategy> _strategies;

    public PaymentStrategyProvider(IEnumerable<IPaymentStrategy> strategies)
    {
        _strategies = strategies;
    }

    /// <inheritdoc />
    public IPaymentStrategy GetStrategy(PaymentScheme scheme)
    {
        var strategy = _strategies.FirstOrDefault(s => s.Scheme == scheme);

        if (strategy == null)
        {
            throw new NotSupportedException($"Payment scheme {scheme} is not supported.");
        }

        return strategy;
    }
}
