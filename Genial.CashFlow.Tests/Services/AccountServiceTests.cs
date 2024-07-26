using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Domain.Services;
using Genial.CashFlow.Infrastructure.Repositories;
using Genial.Framework.Exceptions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using System.Xml.Linq;

namespace Genial.CashFlow.Tests.Services;

public class AccountRepositoryTests
{
    [Fact]
    public async Task ExistsAsync_WhenDataIsValid_ReturnsOK()
    {
        // Arrange
        var parameter = new AccountIdentificationParameterDto()
        {
            CustomerDocument = "111.111.111-11",
            AgencyNumber = "1",
            AccountNumber = "1"
        };

        var mockAccountRepository = new Mock<IAccountRepository>();
        mockAccountRepository.Setup(x => x.ExistsAsync(It.IsAny<AccountIdentificationParameterDto>())).ReturnsAsync((true, true));


        var service = new AccountService(
            accountRepository: mockAccountRepository.Object
        );

        // Act
        var result = await service.ExistsAsync(parameter: parameter);

        // Assert
        Assert.True(result.CustomerExists);
        Assert.True(result.AccountExists);
        
    }

    [Fact]
    public async Task ExistsAsync_WhenCustomerNotFound_ReturnsFalse()
    {
        // Arrange
        var parameter = new AccountIdentificationParameterDto()
        {
            CustomerDocument = "111.111.111-12",
            AgencyNumber = "1",
            AccountNumber = "1"
        };

        var mockAccountRepository = new Mock<IAccountRepository>();
        mockAccountRepository.Setup(x => x.ExistsAsync(It.IsAny<AccountIdentificationParameterDto>())).ReturnsAsync((false, false));


        var service = new AccountService(
            accountRepository: mockAccountRepository.Object
        );

        // Act
        var result = await service.ExistsAsync(parameter: parameter);

        // Assert
        Assert.False(result.CustomerExists);
        Assert.False(result.AccountExists);

    }

    [Fact]
    public async Task ExistsAsync_WhenAccountNotFound_ReturnsFalse()
    {
        // Arrange
        var parameter = new AccountIdentificationParameterDto()
        {
            CustomerDocument = "111.111.111-1",
            AgencyNumber = "2",
            AccountNumber = "1"
        };

        var mockAccountRepository = new Mock<IAccountRepository>();
        mockAccountRepository.Setup(x => x.ExistsAsync(It.IsAny<AccountIdentificationParameterDto>())).ReturnsAsync((true, false));


        var service = new AccountService(
            accountRepository: mockAccountRepository.Object
        );

        // Act
        var result = await service.ExistsAsync(parameter: parameter);

        // Assert
        Assert.True(result.CustomerExists);
        Assert.False(result.AccountExists);
    }

    [Fact]
    public void ValidateIdentificationParameter_WhenDataIsValid_FormatValues()
    {
        // Arrange
        var parameter = new AccountIdentificationParameterDto()
        {
            CustomerDocument = "111.111.111-11",
            AgencyNumber = "1",
            AccountNumber = "1"
        };

        var mockAccountRepository = new Mock<IAccountRepository>();

        var service = new AccountService(
            accountRepository: mockAccountRepository.Object
        );

        // Act
        service.ValidateIdentificationParameter(parameter: parameter);

        // Assert
        Assert.Equal("11111111111", parameter.CustomerDocument);
        Assert.Equal("0001", parameter.AgencyNumber);
        Assert.Equal("000001", parameter.AccountNumber);
    }

    [Fact]
    public void ValidateIdentificationParameter_WhenDataIsInvalid_ThrowsException()
    {
        // Arrange
        var mockAccountRepository = new Mock<IAccountRepository>();

        var service = new AccountService(
            accountRepository: mockAccountRepository.Object
        );

        // Act
        var parameterIsNull = () => service.ValidateIdentificationParameter(parameter: null!);
        var customerIsNull = () => service.ValidateIdentificationParameter(parameter: new AccountIdentificationParameterDto() { });
        var agencyNumberIsNull = () => service.ValidateIdentificationParameter(parameter: new AccountIdentificationParameterDto() { CustomerDocument = "1" });
        var accountNumberIsNull = () => service.ValidateIdentificationParameter(parameter: new AccountIdentificationParameterDto() { CustomerDocument = "1", AccountNumber = "1" });

        // Assert
        Assert.Throws<BusinessException>(parameterIsNull);
        Assert.Throws<BusinessException>(customerIsNull);
        Assert.Throws<BusinessException>(agencyNumberIsNull);
        Assert.Throws<BusinessException>(accountNumberIsNull);
    }

    [Fact]
    public void ValidateIdentificationParameter_WhenHasCharacters_RemoveCharacters()
    {
        // Arrange
        var parameter = new AccountIdentificationParameterDto()
        {
            CustomerDocument = "1r11w1w1111r1-11",
            AgencyNumber = "assg1dfga",
            AccountNumber = "gdfg1dfgfd"
        };

        var mockAccountRepository = new Mock<IAccountRepository>();

        var service = new AccountService(
            accountRepository: mockAccountRepository.Object
        );

        // Act
        service.ValidateIdentificationParameter(parameter: parameter);

        // Assert
        Assert.Equal("11111111111", parameter.CustomerDocument);
        Assert.Equal("0001", parameter.AgencyNumber);
        Assert.Equal("000001", parameter.AccountNumber);
    }

    [Fact]
    public void ValidateIdentificationResult_WhenDataIsValid_ReturnsOK()
    {
        // Arrange
        var customer = new CustomerDto();
        var account = new AccountDto();
        var parameter = new AccountIdentificationResultDto()
        {
            Customer = customer,
            Account = account
        };

        var mockAccountRepository = new Mock<IAccountRepository>();

        var service = new AccountService(
            accountRepository: mockAccountRepository.Object
        );

        // Act
        service.ValidateIdentificationResult(result: parameter);

        // Assert
        Assert.Same(customer, parameter.Customer);
        Assert.Same(account, parameter.Account);
    }

    [Fact]
    public void ValidateIdentificationResult_WhenResultIsNull_ThrowsException()
    {
        // Arrange
        var customer = new CustomerDto();
        var account = new AccountDto();

        var mockAccountRepository = new Mock<IAccountRepository>();

        var service = new AccountService(
            accountRepository: mockAccountRepository.Object
        );

        // Act
        var resultIsNull = () => service.ValidateIdentificationResult(result: null!);
        var customerIsNull = () => service.ValidateIdentificationResult(result: new AccountIdentificationResultDto() { });
        var accountIsNull = () => service.ValidateIdentificationResult(result: new AccountIdentificationResultDto() { Customer = customer });

        // Assert
        Assert.Throws<NotFoundBusinessException>(resultIsNull);
        Assert.Throws<NotFoundBusinessException>(customerIsNull);
        Assert.Throws<NotFoundBusinessException>(accountIsNull);
    }

    [Fact]
    public void ValidateIdentificationResult_WhenResultIsFalse_ThrowsException()
    {
        // Arrange
        var mockAccountRepository = new Mock<IAccountRepository>();

        var service = new AccountService(
            accountRepository: mockAccountRepository.Object
        );

        // Act
        var customerIsFalse = () => service.ValidateIdentificationResult(result: (false, false));
        var accountIsFalse = () => service.ValidateIdentificationResult(result: (true, false));

        // Assert
        Assert.Throws<NotFoundBusinessException>(customerIsFalse);
        Assert.Throws<NotFoundBusinessException>(accountIsFalse);
    }
}
