using Application.Domain;
using Application.UseCases.ListTransactions;
using Application.UseCases.ListTransactions.Abstractions;
using Application.UseCases.ListTransactions.Ports;
using AutoFixture;
using Moq;
using Moq.AutoMock;

namespace UnitTest.UseCases.ListTransactions;

public class ListTransactionsUseCaseTests
{
    private readonly Mock<IListTransactionsRepository> _repository;
    private readonly Mock<IListTransactionsOutputPort> _outputPort;

    private readonly IListTransactionsUseCase _sut;
    private readonly Fixture _fixture = new();

    public ListTransactionsUseCaseTests()
    {
        var mocker = new AutoMocker();

        _repository = mocker.GetMock<IListTransactionsRepository>();
        _outputPort = mocker.GetMock<IListTransactionsOutputPort>();

        _sut = mocker.CreateInstance<ListTransactionsUseCase>();
        _sut.SetOutputPort(_outputPort.Object);
    }

    [Fact]
    public async Task ShouldExecuteSuccessfully()
    {
        //Arrange
        var transactions = _fixture.CreateMany<Transaction>(count: 5);

        _repository
            .Setup(repo => repo.GetTransactionsAsync(default))
            .ReturnsAsync(transactions);

        //Act
        await _sut.ExecuteAsync(default);

        //Assert
        _outputPort.Verify(output => output.Ok(It.IsAny<ListTransactionsOutput>()),
            Times.Once);

        _outputPort.Verify(output => output.NotFound(),
            Times.Never);

        _outputPort.Verify(output => output.Error(It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task ShouldExecuteReturnNotFound()
    {
        //Arrange
        var transactions = _fixture.CreateMany<Transaction>(count: 0);

        _repository
            .Setup(repo => repo.GetTransactionsAsync(default))
            .ReturnsAsync(transactions);

        //Act
        await _sut.ExecuteAsync(default);

        //Assert
        _outputPort.Verify(output => output.NotFound(),
            Times.Once);

        _outputPort.Verify(output => output.Ok(It.IsAny<ListTransactionsOutput>()),
            Times.Never);        

        _outputPort.Verify(output => output.Error(It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task ShouldExecuteWithError()
    {
        //Arrange
        _repository
            .Setup(repo => repo.GetTransactionsAsync(default))
            .ReturnsAsync((IEnumerable<Transaction>?)null);

        //Act
        await _sut.ExecuteAsync(default);

        //Assert
        _outputPort.Verify(output => output.Error(It.IsAny<string>()),
            Times.Once);

        _outputPort.Verify(output => output.Ok(It.IsAny<ListTransactionsOutput>()),
            Times.Never);

        _outputPort.Verify(output => output.NotFound(),
            Times.Never);        
    }
}
