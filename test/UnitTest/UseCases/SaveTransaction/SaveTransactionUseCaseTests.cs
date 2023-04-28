using Application.Domain;
using Application.UseCases.SaveTransaction;
using Application.UseCases.SaveTransaction.Abstractions;
using Application.UseCases.SaveTransaction.Ports;
using AutoFixture;
using Moq;
using Moq.AutoMock;

namespace UnitTest.UseCases.SaveTransaction;

public class SaveTransactionUseCaseTests
{
    private readonly Mock<ISaveTransactionRepository> _repository;
    private readonly Mock<ISaveTransactionOutputPort> _outputPort;

    private readonly ISaveTransactionUseCase _sut;
    private readonly Fixture _fixture = new();

    public SaveTransactionUseCaseTests()
    {
        var mocker = new AutoMocker();

        _repository = mocker.GetMock<ISaveTransactionRepository>();
        _outputPort = mocker.GetMock<ISaveTransactionOutputPort>();

        _sut = mocker.CreateInstance<SaveTransactionUseCase>();
        _sut.SetOutputPort(_outputPort.Object);
    }

    [Fact]
    public async Task ShouldExecuteSuccessfully()
    {
        //Arrange
        var input = _fixture.Create<SaveTransactionInput>();

        _repository
            .Setup(repo => repo.SaveAsync(It.IsAny<Transaction>(), default))
            .ReturnsAsync(true);

        //Act
        await _sut.ExecuteAsync(input, default);

        //Assert
        _outputPort.Verify(output => output.Ok(It.IsAny<SaveTransactionOutput>()), 
            Times.Once);

        _outputPort.Verify(output => output.Error(It.IsAny<string>()), 
            Times.Never);
    }

    [Fact]
    public async Task ShouldExecuteWithError()
    {
        //Arrange
        var input = _fixture.Create<SaveTransactionInput>();

        _repository
            .Setup(repo => repo.SaveAsync(It.IsAny<Transaction>(), default))
            .ReturnsAsync(false);

        //Act
        await _sut.ExecuteAsync(input, default);

        //Assert
        _outputPort.Verify(output => output.Error(It.IsAny<string>()), 
            Times.Once);

        _outputPort.Verify(output => output.Ok(It.IsAny<SaveTransactionOutput>()), 
            Times.Never);        
    }
}
