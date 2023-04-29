using System.Data;
using Application.Domain;
using Application.UseCases.ListTransactions.Abstractions;
using Application.UseCases.ListTransactions.Repositories;
using AutoFixture;
using Dapper;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Moq.Dapper;

namespace UnitTest.UseCases.ListTransactions;

public class ListTransactionsRepositoryTests
{
    private readonly Mock<IDbConnection> _connection;
    private readonly IListTransactionsRepository _sut;
    private readonly Fixture _fixture = new();

    public ListTransactionsRepositoryTests()
    {
        var mocker = new AutoMocker();
        _connection = mocker.GetMock<IDbConnection>();
        _sut = mocker.CreateInstance<ListTransactionsRepository>();
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
}
