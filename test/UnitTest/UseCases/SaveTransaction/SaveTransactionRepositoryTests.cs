using System.Data;
using AutoFixture;
using Application.UseCases.SaveTransaction.Abstractions;
using Application.UseCases.SaveTransaction.Repositories;
using Moq;
using Moq.AutoMock;
using Application.Domain;
using FluentAssertions;
using Dapper;
using Moq.Dapper;

namespace UnitTest.UseCases.SaveTransaction;

public class SaveTransactionRepositoryTests
{
    private readonly Mock<IDbConnection> _connection;
    private readonly ISaveTransactionRepository _sut;

    private readonly Fixture _fixture = new();    

    public SaveTransactionRepositoryTests()
    {
        var mocker = new AutoMocker();

        _connection = mocker.GetMock<IDbConnection>();
        _sut = mocker.CreateInstance<SaveTransactionRepository>();
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
        var result = await _sut.SaveAsync(transaction, default);

        //Assert
        result.Should().BeTrue();
    }
}
