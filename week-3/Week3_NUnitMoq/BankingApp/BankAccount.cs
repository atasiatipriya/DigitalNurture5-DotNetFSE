namespace BankingApp
{
    // This is the class we will be testing
    public class BankAccount
    {
        // Properties
        public string AccountNumber { get; private set; }
        public string AccountHolder { get; private set; }
        public double Balance { get; private set; }

        // Constructor
        public BankAccount(string accountNumber, string accountHolder, double initialBalance)
        {
            AccountNumber = accountNumber;
            AccountHolder = accountHolder;
            Balance = initialBalance;
        }

        // Deposit money into account
        public void Deposit(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive.");

            Balance += amount;
        }

        // Withdraw money from account
        public void Withdraw(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.");

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds.");

            Balance -= amount;
        }

        // Get account summary
        public string GetSummary()
        {
            return $"Account: {AccountNumber} | Holder: {AccountHolder} | Balance: Rs.{Balance}";
        }
    }
}