using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Data;

public interface IAccountDataStore
{
    /// <summary>
    /// Retrieves the account details from the data store for the specified account number.
    /// </summary>
    /// <param name="accountNumber">The unique identifier for the account to be retrieved.</param>
    /// <returns>An <see cref="Account"/> object if the account exists; otherwise, null.</returns>
    Account GetAccount(string accountNumber);
    
    /// <summary>
    /// Persists the updated account information, such as balance or status changes, back to the data store.
    /// </summary>
    /// <param name="account">The <see cref="Account"/> instance containing the updated data to be saved.</param>
    void UpdateAccount(Account account);
}