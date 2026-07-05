using NUnit.Framework;
using BankingApp;

namespace BankingApp.Tests
{
    // [TestFixture] marks this class as containing tests
    [TestFixture]
    public class BankAccountTests
    {
        // This variable will hold our test bank account
        private BankAccount _account;

        // [SetUp] runs BEFORE every single test
        // Creates a fresh account for each test
        [SetUp]
        public void Setup()
        {
            _account = new BankAccount("ACC001", "Atasi Priya", 10000);
        }

        // ============================================
        // TEST 1: Check initial balance is correct
        // ============================================
        [Test]
        public void Deposit_ValidAmount_IncreasesBalance()
        {
            // Arrange - set up what we need
            double depositAmount = 5000;

            // Act - do the action we're testing
            _account.Deposit(depositAmount);

            // Assert - check the result is what we expect
            Assert.That(_account.Balance, Is.EqualTo(15000));
        }

        // ============================================
        // TEST 2: Withdraw reduces balance correctly
        // ============================================
        [Test]
        public void Withdraw_ValidAmount_DecreasesBalance()
        {
            // Arrange
            double withdrawAmount = 3000;

            // Act
            _account.Withdraw(withdrawAmount);

            // Assert
            Assert.That(_account.Balance, Is.EqualTo(7000));
        }

        // ============================================
        // TEST 3: Withdrawing more than balance throws exception
        // ============================================
        [Test]
        public void Withdraw_InsufficientFunds_ThrowsException()
        {
            // Assert - we expect this to throw InvalidOperationException
            Assert.Throws<InvalidOperationException>(() =>
            {
                _account.Withdraw(99999);
            });
        }

        // ============================================
        // TEST 4: Depositing negative amount throws exception
        // ============================================
        [Test]
        public void Deposit_NegativeAmount_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _account.Deposit(-500);
            });
        }

        // ============================================
        // TEST 5: Parameterized test - test multiple
        // deposit amounts with one test method
        // ============================================
        [Test]
        [TestCase(1000, 11000)]
        [TestCase(5000, 15000)]
        [TestCase(10000, 20000)]
        public void Deposit_MultipleAmounts_CorrectBalance(double deposit, double expectedBalance)
        {
            _account.Deposit(deposit);
            Assert.That(_account.Balance, Is.EqualTo(expectedBalance));
        }

        // ============================================
        // TEST 6: GetSummary returns correct string
        // ============================================
        [Test]
        public void GetSummary_ReturnsCorrectFormat()
        {
            string summary = _account.GetSummary();
            Assert.That(summary, Does.Contain("ACC001"));
            Assert.That(summary, Does.Contain("Atasi Priya"));
            Assert.That(summary, Does.Contain("10000"));
        }

        // [TearDown] runs AFTER every single test
        [TearDown]
        public void TearDown()
        {
            // Clean up after each test if needed
            _account = null;
        }
    }
}