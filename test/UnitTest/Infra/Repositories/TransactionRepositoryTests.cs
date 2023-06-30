
using System.Data;
using Domain.Models;
using AutoFixture;
using Dapper;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Moq.Dapper;
using Infra.Repositories.Abstractions;
using Infra.Repositories;

namespace UnitTest.Infra.Repositories;

public class TransactionRepositoryTests
{
    private readonly Mock<IDbConnection> _connection;
    private readonly ITransactionRepository _sut;
    private readonly Fixture _fixture = new();

    public TransactionRepositoryTests()
    {
        var mocker = new AutoMocker();
        _connection = mocker.GetMock<IDbConnection>();
        _sut = mocker.CreateInstance<TransactionRepository>();
    }

    [Fact]
    public async Task ShouldGetDailyBalanceSuccessfully()
    {
        //Arrange
        var expected = _fixture.CreateMany<DailyBalance>(0);

        _connection
            .SetupDapperAsync(conn => conn.QueryAsync(It.IsAny<CommandDefinition>()))
            .ReturnsAsync(expected);

        //Act
        var result = await _sut.GetDailyBalancesAsync(default);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldGetTransactionsSuccessfully()
    {
        //Arrange
        var expected = _fixture.CreateMany<Transaction>(0);

        _connection
            .SetupDapperAsync(conn => conn.QueryAsync(It.IsAny<CommandDefinition>()))
            .ReturnsAsync(expected);

        //Act
        var result = await _sut.GetTransactionsAsync(default);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldSaveSuccessfully()
    {
        //Arrange
        var transaction = _fixture.Create<Transaction>();

        _connection
            .SetupDapperAsync(conn => conn.ExecuteAsync(It.IsAny<CommandDefinition>()))
            .ReturnsAsync(1);
            
        //Act
        var result = await _sut.SaveTransactionAsync(transaction, default);

        //Assert
        result.Should().BeTrue();
    }    
}