using System.Data;
using Application.UseCases.GetDailyBalance.Abstractions;
using Application.UseCases.GetDailyBalance.Domain;
using Application.UseCases.GetDailyBalance.Repositories;
using AutoFixture;
using Dapper;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Moq.Dapper;

namespace UnitTest.UseCases.GetDailyBalance;

public class GetDailyBalanceRepositoryTests
{
    private readonly Mock<IDbConnection> _connection;
    private readonly IGetDailyBalanceRepository _sut;
    private readonly Fixture _fixture = new();

    public GetDailyBalanceRepositoryTests()
    {
        var mocker = new AutoMocker();
        _connection = mocker.GetMock<IDbConnection>();
        _sut = mocker.CreateInstance<GetDailyBalanceRepository>();
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
}
