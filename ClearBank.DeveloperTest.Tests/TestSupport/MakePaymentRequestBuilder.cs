using ClearBank.DeveloperTest.Types;
#pragma warning disable CS0414 // Field is assigned but its value is never used

namespace ClearBank.DeveloperTest.Tests.TestSupport;

public class MakePaymentRequestBuilder
{
    private string? _creditorAccountNumber;
    private string _debtorAccountNumber = string.Empty;
    private decimal _amount;
    private DateTime _paymentDate;
    private PaymentScheme _paymentScheme;

    private MakePaymentRequestBuilder()
    {
        // Prevent using default constructor
    }

    public static MakePaymentRequestBuilder New() => new();
    public MakePaymentRequestBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }
    public MakePaymentRequestBuilder WithCreditorAccountNumber(string creditorAccountNumber)
    {
        _creditorAccountNumber = creditorAccountNumber;
        return this;
    }
    public MakePaymentRequestBuilder WithDebtorAccountNumber(string debtorAccountNumber)
    {
        _debtorAccountNumber = debtorAccountNumber;
        return this;
    }
    public MakePaymentRequestBuilder WithPaymentDate(DateTime paymentDate)
    {
        _paymentDate = paymentDate;
        return this;
    }
    public MakePaymentRequestBuilder WithPaymentScheme(PaymentScheme paymentScheme)
    {
        _paymentScheme = paymentScheme;
        return this;
    }
    
    public MakePaymentRequest Build() => new()
    {
        CreditorAccountNumber = _creditorAccountNumber,
        DebtorAccountNumber = _debtorAccountNumber,
        Amount = _amount,
        PaymentDate = _paymentDate,
        PaymentScheme = _paymentScheme
    };
}