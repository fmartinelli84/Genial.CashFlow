using Castle.Core.Resource;
using Duende.IdentityServer.Models;
using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using Genial.CashFlow.Application.Dtos.Queries;
using Genial.CashFlow.Domain.Services;
using Genial.CashFlow.Infrastructure.Repositories;
using Genial.Framework.Exceptions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using System.Xml.Linq;

namespace Genial.CashFlow.Tests.Services;

public class TransactionServiceTests
{
    [Fact]
    public async Task GetStatementAsync_WhenDataIsValid_ReturnsOK()
    {
        // Arrange
        var request = new GetStatementQuery();
        this.FillAccountIdentificationParameter(request);

        var expectedResult = new GetStatementQueryResult()
        {
            Transactions = new List<TransactionDto>() 
            {
                new TransactionDto()
                { 
                    Id = 1,
                    Date = new DateTime(2024, 07, 26, 19, 1, 20),
                    Type = TransactionType.Credit,
                    Description = "Crédito Inicial",
                    Value = 10m,
                    BalanceValue = 10m
                }
            }
        };
        this.FillAccountIdentificationResult(request, expectedResult);

        var mockTransactionRepository = new Mock<ITransactionRepository>();
        mockTransactionRepository.Setup(x => x.GetStatementAsync(It.IsAny<GetStatementQuery>())).ReturnsAsync(expectedResult);

        var mockAccountService = new Mock<IAccountService>();

        var service = new TransactionService(
            transactionRepository: mockTransactionRepository.Object,
            accountService: mockAccountService.Object
        );

        // Act
        var result = await service.GetStatementAsync(request: request);

        // Assert
        this.AssertAccountIdentificationResult(expectedResult, result);

        Assert.NotNull(result.Transactions);
        Assert.Collection(result.Transactions, transaction =>
        {
            var expectedTransaction = expectedResult.Transactions.First();

            Assert.NotNull(transaction);
            Assert.Equal(expectedTransaction.Id, transaction.Id);
            Assert.Equal(expectedTransaction.Date, transaction.Date);
            Assert.Equal(expectedTransaction.Type, transaction.Type);
            Assert.Equal(expectedTransaction.Description, transaction.Description);
            Assert.Equal(expectedTransaction.Value, transaction.Value);
            Assert.Equal(expectedTransaction.BalanceValue, transaction.BalanceValue);
        });
        
    }

    [Fact]
    public async Task GetStatementAsync_WhenPeriodIsValid_FormatValues()
    {
        // Arrange
        var request = new GetStatementQuery();
        this.FillAccountIdentificationParameter(request);

        var startDate = new DateTime(2024, 7, 25, 15, 20, 38);
        var endDate = new DateTime(2024, 7, 26, 18, 10, 20);
        request.StartDate = startDate;
        request.EndDate = endDate;

        var mockTransactionRepository = new Mock<ITransactionRepository>();

        var mockAccountService = new Mock<IAccountService>();

        var service = new TransactionService(
            transactionRepository: mockTransactionRepository.Object,
            accountService: mockAccountService.Object
        );

        // Act
        var result = await service.GetStatementAsync(request: request);

        // Assert
        Assert.Equal(startDate.Date, request.StartDate);
        Assert.Equal(endDate.Date.Add(new TimeSpan(23, 59, 59)), request.EndDate);
    }

    [Fact]
    public async Task GetStatementAsync_WhenPeriodIsInvalid_ThrowsException()
    {
        // Arrange
        var request = new GetStatementQuery();
        this.FillAccountIdentificationParameter(request);

        request.StartDate = new DateTime(2024, 7, 27);
        request.EndDate = new DateTime(2024, 7, 26);

        var mockTransactionRepository = new Mock<ITransactionRepository>();

        var mockAccountService = new Mock<IAccountService>();

        var service = new TransactionService(
            transactionRepository: mockTransactionRepository.Object,
            accountService: mockAccountService.Object
        );

        // Act
        var periodIsInvalid = async () => await service.GetStatementAsync(request: request);

        // Assert
        await Assert.ThrowsAsync<BusinessException>(periodIsInvalid);
    }

    [Fact]
    public async Task GetStatementAsync_WhenTransactionsNotFound_ReturnEmpty()
    {
        // Arrange
        var request = new GetStatementQuery();
        this.FillAccountIdentificationParameter(request);

        var expectedResult = new GetStatementQueryResult()
        {
            Transactions = new List<TransactionDto>()
        };
        this.FillAccountIdentificationResult(request, expectedResult);

        var mockTransactionRepository = new Mock<ITransactionRepository>();
        mockTransactionRepository.Setup(x => x.GetStatementAsync(It.IsAny<GetStatementQuery>())).ReturnsAsync(expectedResult);

        var mockAccountService = new Mock<IAccountService>();

        var service = new TransactionService(
            transactionRepository: mockTransactionRepository.Object,
            accountService: mockAccountService.Object
        );

        // Act
        var result = await service.GetStatementAsync(request: request);

        // Assert
        this.AssertAccountIdentificationResult(expectedResult, result);

        Assert.NotNull(result.Transactions);
        Assert.False(result.Transactions.Any());
    }


    [Fact]
    public async Task GetBalanceAsync_WhenDataIsValid_ReturnsOK()
    {
        // Arrange
        var request = new GetBalanceQuery();
        this.FillAccountIdentificationParameter(request);

        var expectedResult = new GetBalanceQueryResult()
        {
            BalanceValue = 10m
        };
        this.FillAccountIdentificationResult(request, expectedResult);

        var mockTransactionRepository = new Mock<ITransactionRepository>();
        mockTransactionRepository.Setup(x => x.GetBalanceAsync(It.IsAny<GetBalanceQuery>())).ReturnsAsync(expectedResult);

        var mockAccountService = new Mock<IAccountService>();

        var service = new TransactionService(
            transactionRepository: mockTransactionRepository.Object,
            accountService: mockAccountService.Object
        );

        // Act
        var result = await service.GetBalanceAsync(request: request);

        // Assert
        this.AssertAccountIdentificationResult(expectedResult, result);

        Assert.Equal(expectedResult.BalanceValue, result.BalanceValue);
    }

    [Fact]
    public async Task GetBalanceAsync_WhenTransactionsNotFound_ReturnZero()
    {
        // Arrange
        var request = new GetBalanceQuery();
        this.FillAccountIdentificationParameter(request);

        var expectedResult = new GetBalanceQueryResult()
        {
            BalanceValue = 0
        };
        this.FillAccountIdentificationResult(request, expectedResult);

        var mockTransactionRepository = new Mock<ITransactionRepository>();
        mockTransactionRepository.Setup(x => x.GetBalanceAsync(It.IsAny<GetBalanceQuery>())).ReturnsAsync(expectedResult);

        var mockAccountService = new Mock<IAccountService>();

        var service = new TransactionService(
            transactionRepository: mockTransactionRepository.Object,
            accountService: mockAccountService.Object
        );

        // Act
        var result = await service.GetBalanceAsync(request: request);

        // Assert
        this.AssertAccountIdentificationResult(expectedResult, result);

        Assert.Equal(expectedResult.BalanceValue, result.BalanceValue);
    }


    [Fact]
    public async Task CreateAsync_WhenDataIsValid_ReturnsOK()
    {
        // Arrange
        var request = new CreateTransactionCommand();
        this.FillAccountIdentificationParameter(request);
        
        request.Type = TransactionType.Credit;
        request.Description = "Crédito Inicial";
        request.Value = 10m;

        var expectedResult = new TransactionDto()
        {
            Id = 1,
            Date = new DateTime(2024, 07, 26, 19, 1, 20),
            Type = request.Type,
            Description = request.Description,
            Value = request.Value,
            BalanceValue = request.Value
        };
        

        var mockTransactionRepository = new Mock<ITransactionRepository>();
        mockTransactionRepository.Setup(x => x.CreateAsync(It.IsAny<CreateTransactionCommand>())).ReturnsAsync(expectedResult);

        var mockAccountService = new Mock<IAccountService>();

        var service = new TransactionService(
            transactionRepository: mockTransactionRepository.Object,
            accountService: mockAccountService.Object
        );

        // Act
        var result = await service.CreateAsync(request: request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResult.Id, result.Id);
        Assert.Equal(expectedResult.Date, result.Date);
        Assert.Equal(expectedResult.Type, result.Type);
        Assert.Equal(expectedResult.Description, result.Description);
        Assert.Equal(expectedResult.Value, result.Value);
        Assert.Equal(expectedResult.BalanceValue, result.BalanceValue);
    }

    [Fact]
    public async Task CreateAsync_WhenDataIsInvalid_ThrowsException()
    {
        // Arrange
        var request = new CreateTransactionCommand();
        this.FillAccountIdentificationParameter(request);

        request.Type = TransactionType.Credit;
        request.Description = "Crédito Inicial";
        request.Value = 10m;

        var mockTransactionRepository = new Mock<ITransactionRepository>();

        var mockAccountService = new Mock<IAccountService>();

        var service = new TransactionService(
            transactionRepository: mockTransactionRepository.Object,
            accountService: mockAccountService.Object
        );

        // Act
        request.Type = 0;
        request.Description = "Crédito Inicial";
        request.Value = 10m;
        var typeIsInvalid = async () => await service.CreateAsync(request: request);

        request.Type = TransactionType.Credit;
        request.Description = string.Empty;
        request.Value = 10m;
        var descriptionIsInvalid = async () => await service.CreateAsync(request: request);

        request.Type = TransactionType.Credit;
        request.Description = "Crédito Inicial";
        request.Value = 0m;
        var valueIsInvalid = async () => await service.CreateAsync(request: request);

        // Assert
        await Assert.ThrowsAsync<BusinessException>(typeIsInvalid);
        await Assert.ThrowsAsync<BusinessException>(descriptionIsInvalid);
        await Assert.ThrowsAsync<BusinessException>(valueIsInvalid);
    }


    private void FillAccountIdentificationParameter(AccountIdentificationParameterDto parameter)
    {
        parameter.CustomerDocument = "111.111.111-11";
        parameter.AgencyNumber = "1";
        parameter.AccountNumber = "1";
    }

    private void FillAccountIdentificationResult(AccountIdentificationParameterDto parameter, AccountIdentificationResultDto result)
    {
        result.Customer = new CustomerDto()
        {
            Id = 1,
            Name = "Test",
            Document = parameter.CustomerDocument,
        };
        result.Account = new AccountDto()
        {
            Id = 1,
            AgencyNumber = parameter.AgencyNumber,
            Number = parameter.AccountNumber
        };
    }

    private void AssertAccountIdentificationResult(AccountIdentificationResultDto expectedResult, AccountIdentificationResultDto result)
    {
        Assert.NotNull(result);

        Assert.NotNull(result.Customer);
        Assert.Equal(expectedResult.Customer.Id, result.Customer.Id);
        Assert.Equal(expectedResult.Customer.Name, result.Customer.Name);
        Assert.Equal(expectedResult.Customer.Document, result.Customer.Document);

        Assert.NotNull(result.Account);
        Assert.Equal(expectedResult.Account.Id, result.Account.Id);
        Assert.Equal(expectedResult.Account.AgencyNumber, result.Account.AgencyNumber);
        Assert.Equal(expectedResult.Account.Number, result.Account.Number);
    }
}
