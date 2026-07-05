using NUnit.Framework;
using Moq;
using BankingApp;

namespace BankingApp.Tests
{
    // ============================================
    // First we need an interface to mock
    // ============================================

    // This interface represents an email service
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
        bool IsEmailValid(string email);
    }

    // This interface represents a database service
    public interface IAccountRepository
    {
        void Save(BankAccount account);
        BankAccount GetById(string accountNumber);
    }

    // ============================================
    // This is the service we are testing
    // It depends on IEmailService and IAccountRepository
    // We will MOCK those dependencies
    // ============================================
    public class AccountService
    {
        private readonly IEmailService _emailService;
        private readonly IAccountRepository _repository;

        public AccountService(IEmailService emailService, IAccountRepository repository)
        {
            _emailService = emailService;
            _repository = repository;
        }

        public void CreateAccount(BankAccount account, string email)
        {
            // Save the account
            _repository.Save(account);

            // Send welcome email
            _emailService.SendEmail(
                email,
                "Welcome to BankingApp!",
                $"Hello {account.AccountHolder}, your account {account.AccountNumber} is created."
            );
        }

        public double GetBalance(string accountNumber)
        {
            var account = _repository.GetById(accountNumber);
            return account.Balance;
        }
    }

    // ============================================
    // MOQ TESTS
    // ============================================
    [TestFixture]
    public class Exercise4_MoqTests
    {
        private Mock<IEmailService> _mockEmailService;
        private Mock<IAccountRepository> _mockRepository;
        private AccountService _accountService;
        private BankAccount _testAccount;

        [SetUp]
        public void Setup()
        {
            // Create mock objects - fake implementations
            _mockEmailService = new Mock<IEmailService>();
            _mockRepository = new Mock<IAccountRepository>();

            // Create the service with mocked dependencies
            _accountService = new AccountService(
                _mockEmailService.Object,
                _mockRepository.Object
            );

            _testAccount = new BankAccount("ACC001", "Atasi Priya", 10000);
        }

        // ============================================
        // TEST 1: Verify Save was called once
        // ============================================
        [Test]
        public void CreateAccount_ShouldSaveAccount_Once()
        {
            // Act
            _accountService.CreateAccount(_testAccount, "atasi@email.com");

            // Assert - verify Save was called exactly once
            _mockRepository.Verify(r => r.Save(_testAccount), Times.Once);
        }

        // ============================================
        // TEST 2: Verify email was sent
        // ============================================
        [Test]
        public void CreateAccount_ShouldSendWelcomeEmail()
        {
            // Act
            _accountService.CreateAccount(_testAccount, "atasi@email.com");

            // Assert - verify SendEmail was called once
            _mockEmailService.Verify(
                e => e.SendEmail(
                    "atasi@email.com",
                    "Welcome to BankingApp!",
                    It.IsAny<string>() // any string for body
                ),
                Times.Once
            );
        }

        // ============================================
        // TEST 3: Mock return value
        // ============================================
        [Test]
        public void GetBalance_ShouldReturnCorrectBalance()
        {
            // Setup mock to return our test account when asked
            _mockRepository
                .Setup(r => r.GetById("ACC001"))
                .Returns(_testAccount);

            // Act
            double balance = _accountService.GetBalance("ACC001");

            // Assert
            Assert.That(balance, Is.EqualTo(10000));
        }

        // ============================================
        // TEST 4: Verify no email sent if account not created
        // ============================================
        [Test]
        public void NoInteraction_EmailNotSent()
        {
            // We never call CreateAccount here

            // Assert - email service should never have been called
            _mockEmailService.Verify(
                e => e.SendEmail(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                ),
                Times.Never
            );
        }

        [TearDown]
        public void TearDown()
        {
            _testAccount = null;
        }
    }
}