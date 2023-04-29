using Application.UseCases.GetDailyBalance.Abstractions;
using Application.UseCases.GetDailyBalance.Ports;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Worker.Controllers.GetDailyBalance;

namespace UnitTest.Worker;

public class GetDailyBalanceControllerTests
{
    private readonly Mock<IGetDailyBalanceUseCase> _useCase;
    private readonly IGetDailyBalanceOutputPort _outputPort;

    private readonly GetDailyBalanceController _sut;
    private readonly Fixture _fixture = new();

    public GetDailyBalanceControllerTests()
    {
        var mocker = new AutoMocker();
        _useCase = mocker.GetMock<IGetDailyBalanceUseCase>();
        _sut = mocker.CreateInstance<GetDailyBalanceController>();
        _outputPort = (_sut as IGetDailyBalanceOutputPort);
    }

    [Fact]
    public async Task ShouldGetDailyBalanceSuccessfully()
    {
        //Arrange        
        var output = _fixture.Create<GetDailyBalanceOutput>();
        var expected = GetDailyBalanceResponse.Success(output);

        _useCase
            .Setup(useCase => useCase.ExecuteAsync(default))
            .Callback(() => _outputPort.Ok(output));

        //Act
        var result = await _sut.GetDailyBalance(default) as ObjectResult;

        //Assert
        result!.StatusCode.Should().Be(StatusCodes.Status200OK);
        result!.Value.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldGetDailyBalanceNotFound()
    {
        //Arrange        
        var errorMessage = "Not found Transactions";
        var expected = GetDailyBalanceResponse.Error(errorMessage);

        _useCase
            .Setup(useCase => useCase.ExecuteAsync(default))
            .Callback(() => _outputPort.NotFound());

        //Act
        var result = await _sut.GetDailyBalance(default) as ObjectResult;

        //Assert
        result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        result!.Value.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldGetDailyBalanceWithError()
    {
        //Arrange        
        var errorMessage = _fixture.Create<string>();
        var expected = GetDailyBalanceResponse.Error(errorMessage);

        _useCase
            .Setup(useCase => useCase.ExecuteAsync(default))
            .Callback(() => _outputPort.Error(errorMessage));

        //Act
        var result = await _sut.GetDailyBalance(default) as ObjectResult;

        //Assert
        result!.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        result!.Value.Should().BeEquivalentTo(expected);
    }
}
