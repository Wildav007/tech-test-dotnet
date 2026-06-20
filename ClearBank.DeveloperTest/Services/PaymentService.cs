using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Strategies;

namespace ClearBank.DeveloperTest.Services;

/// <inheritdoc />
public class PaymentService : IPaymentService
{
    private readonly IAccountDataStore _accountDataStore;
    private readonly IPaymentStrategyProvider _strategyProvider;

    public PaymentService(IAccountDataStore accountDataStore, IPaymentStrategyProvider strategyProvider)
    {
        _accountDataStore = accountDataStore;
        _strategyProvider = strategyProvider;
    }

    /// <inheritdoc />
    public MakePaymentResult MakePayment(MakePaymentRequest request)
    {
        var account = _accountDataStore.GetAccount(request.DebtorAccountNumber);
        var strategy = _strategyProvider.GetStrategy(request.PaymentScheme);

        var result = new MakePaymentResult();

        if (!strategy.IsValid(account, request))
        {
            return result;
        }
        
        result.Success = true;
            
        // Note: This logic will be moved into the Account domain model
        account.Balance -= request.Amount;
            
        _accountDataStore.UpdateAccount(account);

        return result;
    }
}
