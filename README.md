### Test Description
In the 'PaymentService.cs' file you will find a method for making a payment. At a high level the steps for making a payment are:

 - Lookup the account the payment is being made from
 - Check the account is in a valid state to make the payment
 - Deduct the payment amount from the account's balance and update the account in the database
 
What we’d like you to do is refactor the code with the following things in mind:  
 - Adherence to SOLID principals
 - Testability  
 - Readability 

We’d also like you to add some unit tests to the ClearBank.DeveloperTest.Tests project to show how you would test the code that you’ve produced. The only specific ‘rules’ are:  

 - The solution should build.
 - The tests should all pass.
 - You should not change the method signature of the MakePayment method.

You are free to use any frameworks/NuGet packages that you see fit.  
 
You should plan to spend around 1 to 3 hours to complete the exercise.


Observations:
1. **Tight Coupling:** The `PaymentService` is tightly coupled to specific data store implementations (`AccountDataStore` and `BackupAccountDataStore`) and the static `ConfigurationManager`. This makes unit testing impossible without external dependencies.
2. **Violation of Single Responsibility Principle (SRP):** The `MakePayment` method is doing too much:
    - Deciding which data store to use based on configuration.
    - Retrieving data.
    - Validating different payment schemes.
    - Updating the account balance.
    - Persisting changes.

3. **Violation of Open/Closed Principle (OCP):** If a new payment scheme (e.g., "International") or a new data store type is added, the `PaymentService` must be modified.
4. **Logic Duplication:** The logic to determine the data store type is repeated twice (once for fetching and once for saving).
5. **Lack of Abstraction:** There is no common interface for the data stores, even though they share the same method signatures.


### Refactoring Plan
1. **Create Baseline Unit Tests:** Create unit tests to prevent any regression during refactoring.
2. **Introduce Abstractions:** Create an `IAccountDataStore` interface to unify the data access layer.
3. **Dependency Injection:** Move data store selection and configuration access out of the service and inject the required dependencies via the constructor.
4. **Validation Strategy Pattern:** Move the validation logic for each into its own class (e.g., `BacsValidator`, `FasterPaymentsValidator`, etc.) implementing a shared interface. `PaymentScheme`
5. **Unit Testing:** Create a comprehensive test suite covering all logic paths for Bacs, Chaps, and FasterPayments.
6. **Modernise Configuration:** Replace `ConfigurationManager` with a more modern options pattern (though I will keep the logic compatible with the current requirements).