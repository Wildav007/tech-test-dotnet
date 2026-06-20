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
1. **Create Baseline Unit Tests:**
   * Establish a robust suite of unit tests using `AccountBuilder.New()` and `MakePaymentRequestBuilder.New()`.
   * Ensure full coverage of existing logic to prevent regression.

2. **Introduce Abstractions:**
   * Implement the `IAccountDataStore` interface to decouple business logic from the persistence layer.

3. **Dependency Injection:**
   * Refactor `PaymentService` to receive dependencies via the constructor.
   * Move the data store selection logic and configuration access out of the service methods.

4. **Payment Scheme Strategy Pattern:**
   * Create an `IPaymentSchemeStrategy` interface.
   * Implement concrete strategies (e.g., `BacsPaymentStrategy`, `FasterPaymentsStrategy`, `ChapsPaymentStrategy`) to encapsulate validation and execution logic specific to each `PaymentScheme`.

5. **Domain-Driven Design (DDD) Principles:**
   * Transition the `Account` entity from a passive DTO to a rich domain model.
   * Encapsulate state changes by adding methods such as `DecreaseBalance(decimal amount)` and `IncreaseBalance(decimal amount)` to the `Account` class to manage business invariants.

6. **Unit Testing:**
   * Create comprehensive test suites for each individual strategy.
   * Add unit tests specifically for the `Account` domain logic.

7. **Modernize Configuration:**
   * Replace `ConfigurationManager` usage with the modern Options pattern (`IOptions<T>`) for improved testability and flexibility.
