using Application.UseCases.GetDailyBalance;
using Application.UseCases.GetDailyBalance.Abstractions;
using Application.UseCases.GetDailyBalance.Domain;
using Application.UseCases.GetDailyBalance.Ports;
using AutoFixture;
using Moq;
using Moq.AutoMock;

namespace UnitTest.UseCases.GetDailyBalance;

public class GetDailyBalanceUseCaseTests
{
    private readonly Mock<IGetDailyBalanceRepository> _repository;
    private readonly Mock<IGetDailyBalanceOutputPort> _outputPort;

    private readonly IGetDailyBalanceUseCase _sut;
    private readonly Fixture _fixture = new();

    public GetDailyBalanceUseCaseTests()
    {
        var mocker = new AutoMocker();

        _repository = mocker.GetMock<IGetDailyBalanceRepository>();
        _outputPort = mocker.GetMock<IGetDailyBalanceOutputPort>();

        _sut = mocker.CreateInstance<GetDailyBalanceUseCase>();
        _sut.SetOutputPort(_outputPort.Object);
    }

    [Fact]
    public async Task ShouldExecuteSuccessfully()
    {
        //Arrange
        var balances = _fixture.CreateMany<DailyBalance>(count: 5);

        _repository
            .Setup(repo => repo.GetDailyBalancesAsync(default))
            .ReturnsAsync(balances);

        //Act
        await _sut.ExecuteAsync(default);

        //Assert
        _outputPort.Verify(output => output.Ok(It.IsAny<GetDailyBalanceOutput>()),
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
        var balances = _fixture.CreateMany<DailyBalance>(count: 0);

        _repository
            .Setup(repo => repo.GetDailyBalancesAsync(default))
            .ReturnsAsync(balances);

        //Act
        await _sut.ExecuteAsync(default);

        //Assert
        _outputPort.Verify(output => output.NotFound(),
            Times.Once);

        _outputPort.Verify(output => output.Ok(It.IsAny<GetDailyBalanceOutput>()),
            Times.Never);        

        _outputPort.Verify(output => output.Error(It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task ShouldExecuteWithError()
    {
        //Arrange
        _repository
            .Setup(repo => repo.GetDailyBalancesAsync(default))
            .ReturnsAsync((IEnumerable<DailyBalance>?)null);

        //Act
        await _sut.ExecuteAsync(default);

        //Assert
        _outputPort.Verify(output => output.Error(It.IsAny<string>()),
            Times.Once);

        _outputPort.Verify(output => output.Ok(It.IsAny<GetDailyBalanceOutput>()),
            Times.Never);

        _outputPort.Verify(output => output.NotFound(),
            Times.Never);        
    }
}
