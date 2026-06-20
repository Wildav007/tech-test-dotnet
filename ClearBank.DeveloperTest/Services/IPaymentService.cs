using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services;

/// <summary>
/// Defines the contract for processing payments.
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Executes a payment based on the provided request details.
    /// </summary>
    /// <param name="request">The details of the payment to be made.</param>
    /// <returns>A <see cref="MakePaymentResult"/> indicating success or failure.</returns>
    MakePaymentResult MakePayment(MakePaymentRequest request);
}