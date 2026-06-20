using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Strategies;
using ClearBank.DeveloperTest.Tests.TestSupport;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services;

public class PaymentServiceTests
{
    private readonly Mock<IAccountDataStore> _mockDataStore;

    private readonly PaymentService _service;

    public PaymentServiceTests()
    {
        _mockDataStore = new Mock<IAccountDataStore>();
        _service = new PaymentService(_mockDataStore.Object, new PaymentStrategyProvider(new List<IPaymentStrategy>
        {
            new BacsPaymentStrategy(),
            new FasterPaymentsPaymentStrategy(),
            new ChapsPaymentStrategy()
        }));
    }

    #region Bacs Payment Scheme Tests

    [Fact]
    public void MakePayment_Bacs_AccountIsNull_ShouldReturnFailure()
    {
        // Arrange
        const string debtorAccountNumber = "ACC123";
        Account account = null!;
        
        var request = MakePaymentRequestBuilder
            .New()
            .WithPaymentScheme(PaymentScheme.Bacs)
            .WithDebtorAccountNumber(debtorAccountNumber)
            .Build();

        _mockDataStore.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(account);

        // Act
        var result = _service.MakePayment(request);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void MakePayment_Bacs_InvalidSchemeFlag_ShouldReturnFailure()
    {
        // Arrange
        const string debtorAccountNumber = "ACC123";
        
        var request = MakePaymentRequestBuilder
            .New()
            .WithPaymentScheme(PaymentScheme.Bacs)
            .WithDebtorAccountNumber(debtorAccountNumber)
            .Build();

        var account = AccountBuilder
            .New()
            .WithAllowedPaymentSchemes(AllowedPaymentSchemes.Chaps)
            .Build();

        _mockDataStore.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(account);

        // Act
        var result = _service.MakePayment(request);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void MakePayment_Bacs_ValidRequest_ShouldReturnSuccess()
    {
        // Arrange
        const string debtorAccountNumber = "ACC123";
        const decimal amount = 100m;
        const decimal balance = 500m;
        const decimal expectedBalance = 400m;

        var request = MakePaymentRequestBuilder
            .New()
            .WithPaymentScheme(PaymentScheme.Bacs)
            .WithDebtorAccountNumber(debtorAccountNumber)
            .WithAmount(amount)
            .Build();

        var account = AccountBuilder
            .New()
            .WithAllowedPaymentSchemes(AllowedPaymentSchemes.Bacs)
            .WithBalance(balance)
            .Build();

        _mockDataStore.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(account);

        // Act
        var result = _service.MakePayment(request);

        // Assert
        result.Success.Should().BeTrue();
        account.Balance.Should().Be(expectedBalance);
        _mockDataStore.Verify(x => x.UpdateAccount(account), Times.Once);
    }

    #endregion

    #region FasterPayments Payment Scheme Tests

    [Fact]
    public void MakePayment_FasterPayments_AccountIsNull_ShouldReturnFailure()
    {
        // Arrange
        const string debtorAccountNumber = "ACC456";
        Account account = null!;

        var request = MakePaymentRequestBuilder
            .New()
            .WithPaymentScheme(PaymentScheme.FasterPayments)
            .WithDebtorAccountNumber(debtorAccountNumber)
            .Build();

        _mockDataStore.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(account);

        // Act
        var result = _service.MakePayment(request);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void MakePayment_FasterPayments_InvalidSchemeFlag_ShouldReturnFailure()
    {
        // Arrange
        const string debtorAccountNumber = "ACC123";
        
        var request = MakePaymentRequestBuilder
            .New()
            .WithPaymentScheme(PaymentScheme.FasterPayments)
            .WithDebtorAccountNumber(debtorAccountNumber)
            .Build();

        var account = AccountBuilder
            .New()
            .WithAllowedPaymentSchemes(AllowedPaymentSchemes.Chaps)
            .Build();

        _mockDataStore.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(account);

        // Act
        var result = _service.MakePayment(request);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void MakePayment_FasterPayments_InsufficientBalance_ShouldReturnFailure()
    {
        // Arrange
        const string debtorAccountNumber = "ACC456";
        const decimal amount = 200m;
        const decimal balance = 100m;

        var request = MakePaymentRequestBuilder
            .New()
            .WithPaymentScheme(PaymentScheme.FasterPayments)
            .WithDebtorAccountNumber(debtorAccountNumber)
            .WithAmount(amount)
            .Build();

        var account = AccountBuilder.New()
            .WithAllowedPaymentSchemes(AllowedPaymentSchemes.FasterPayments)
            .WithBalance(balance)
            .Build();

        _mockDataStore.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(account);

        // Act
        var result = _service.MakePayment(request);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void MakePayment_FasterPayments_ValidRequest_ShouldReturnSuccess()
    {
        // Arrange
        const string debtorAccountNumber = "ACC456";
        const decimal amount = 100m;
        const decimal balance = 300m;
        const decimal expectedBalance = 200m;

        var request = MakePaymentRequestBuilder
            .New()
            .WithPaymentScheme(PaymentScheme.FasterPayments)
            .WithDebtorAccountNumber(debtorAccountNumber)
            .WithAmount(amount)
            .Build();

        var account = AccountBuilder.New()
            .WithAllowedPaymentSchemes(AllowedPaymentSchemes.FasterPayments)
            .WithBalance(balance)
            .Build();

        _mockDataStore.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(account);

        // Act
        var result = _service.MakePayment(request);

        // Assert
        result.Success.Should().BeTrue();
        account.Balance.Should().Be(expectedBalance);
        _mockDataStore.Verify(x => x.UpdateAccount(account), Times.Once);
    }

    #endregion

    #region Chaps Payment Scheme Tests

    [Fact]
    public void MakePayment_Chaps_AccountIsNull_ShouldReturnFailure()
    {
        // Arrange
        const string debtorAccountNumber = "ACC456";
        Account account = null!;

        var request = MakePaymentRequestBuilder
            .New()
            .WithPaymentScheme(PaymentScheme.Chaps)
            .WithDebtorAccountNumber(debtorAccountNumber)
            .Build();

        _mockDataStore.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(account);

        // Act
        var result = _service.MakePayment(request);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void MakePayment_Chaps_InvalidSchemeFlag_ShouldReturnFailure()
    {
        // Arrange
        const string debtorAccountNumber = "ACC123";
        
        var request = MakePaymentRequestBuilder
            .New()
            .WithPaymentScheme(PaymentScheme.Chaps)
            .WithDebtorAccountNumber(debtorAccountNumber)
            .Build();

        var account = AccountBuilder
            .New()
            .WithAllowedPaymentSchemes(AllowedPaymentSchemes.FasterPayments)
            .Build();

        _mockDataStore.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(account);

        // Act
        var result = _service.MakePayment(request);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void MakePayment_Chaps_AccountNotLive_ShouldReturnFailure()
    {
        // Arrange
        const string debtorAccountNumber = "ACC789";

        var request = MakePaymentRequestBuilder
            .New()
            .WithPaymentScheme(PaymentScheme.Chaps)
            .WithDebtorAccountNumber(debtorAccountNumber)
            .Build();

        var account = AccountBuilder
            .New()
            .WithAllowedPaymentSchemes(AllowedPaymentSchemes.Chaps)
            .WithAccountStatus(AccountStatus.Disabled)
            .Build();

        _mockDataStore.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(account);

        // Act
        var result = _service.MakePayment(request);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void MakePayment_Chaps_ValidRequest_ShouldReturnSuccess()
    {
        // Arrange
        const string debtorAccountNumber = "ACC789";
        const decimal amount = 500m;
        const decimal balance = 2000m;
        const decimal expectedBalance = 1500m;

        var request = MakePaymentRequestBuilder
            .New()
            .WithPaymentScheme(PaymentScheme.Chaps)
            .WithDebtorAccountNumber(debtorAccountNumber)
            .WithAmount(amount)
            .Build();

        var account = AccountBuilder
            .New()
            .WithAllowedPaymentSchemes(AllowedPaymentSchemes.Chaps)
            .WithAccountStatus(AccountStatus.Live)
            .WithBalance(balance)
            .Build();

        _mockDataStore.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(account);

        // Act
        var result = _service.MakePayment(request);

        // Assert
        result.Success.Should().BeTrue();
        account.Balance.Should().Be(expectedBalance);
        _mockDataStore.Verify(x => x.UpdateAccount(account), Times.Once);
    }

    #endregion

    #region Edge Case Tests

    [Fact]
    public void MakePayment_InvalidScheme_ShouldThrowException()
    {
        // Arrange
        var request = new MakePaymentRequest
        {
            PaymentScheme = (PaymentScheme)999 // An unsupported scheme
        };


        // Act & Assert
        Assert.Throws<NotSupportedException>(() => _service.MakePayment(request));
    }

    #endregion
}