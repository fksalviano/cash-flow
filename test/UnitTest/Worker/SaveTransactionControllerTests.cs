using Application.Commons.Domain;
using Application.UseCases.SaveTransaction.Abstractions;
using Application.UseCases.SaveTransaction.Ports;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Worker.Controllers.SaveTransaction;

namespace UnitTest.Worker;

public class SaveTransactionControllerTests
{
    private readonly Mock<ISaveTransactionUseCase> _useCase;
    private readonly ISaveTransactionOutputPort _outputPort;

    private readonly SaveTransactionController _sut;
    private readonly Fixture _fixture = new();

    public SaveTransactionControllerTests()
    {
        var mocker = new AutoMocker();

        _useCase = mocker.GetMock<ISaveTransactionUseCase>();
        _sut = mocker.CreateInstance<SaveTransactionController>();
        _outputPort = (_sut as ISaveTransactionOutputPort);
    }

    [Fact]
    public async Task ShouldSaveTransactionSuccessfully()
    {
        //Arrange
        var input = _fixture.Create<SaveTransactionInput>();
        var output = _fixture.Create<SaveTransactionOutput>();
        var expected = SaveTransactionResponse.Success(output);

        _useCase
            .Setup(useCase => useCase.ExecuteAsync(It.IsAny<SaveTransactionInput>(), default))
            .Callback(() => _outputPort.Ok(output));

        //Act
        var result = await _sut.SaveTransaction(input, default) as ObjectResult;

        //Assert
        result!.StatusCode.Should().Be(StatusCodes.Status200OK);
        result!.Value.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldSaveTransactionInvalid()
    {
        //Arrange
        var input = _fixture.Create<SaveTransactionInput>();
        var error = Result.Error(_fixture.Create<string>());
        var expected = SaveTransactionResponse.Error(error.ErrorMessage);

        _useCase
            .Setup(useCase => useCase.ExecuteAsync(It.IsAny<SaveTransactionInput>(), default))
            .Callback(() => _outputPort.Invalid(error));

        //Act
        var result = await _sut.SaveTransaction(input, default) as ObjectResult;

        //Assert
        result!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        result!.Value.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldSaveTransactionWithError()
    {
        //Arrange
        var input = _fixture.Create<SaveTransactionInput>();
        var errorMessage = _fixture.Create<string>();
        var expected = SaveTransactionResponse.Error(errorMessage);

        _useCase
            .Setup(useCase => useCase.ExecuteAsync(It.IsAny<SaveTransactionInput>(), default))
            .Callback(() => _outputPort.Error(errorMessage));

        //Act
        var result = await _sut.SaveTransaction(input, default) as ObjectResult;

        //Assert
        result!.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        result!.Value.Should().BeEquivalentTo(expected);
    }
}
