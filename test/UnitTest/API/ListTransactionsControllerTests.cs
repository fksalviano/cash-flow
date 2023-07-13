using Application.UseCases.ListTransactions.Abstractions;
using Application.UseCases.ListTransactions.Ports;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using API.Controllers.ListTransactions;

namespace UnitTest.API;

public class ListTransactionsControllerTests
{
    private readonly Mock<IListTransactionsUseCase> _useCase;
    private readonly IListTransactionsOutputPort _outputPort;

    private readonly ListTransactionsController _sut;
    private readonly Fixture _fixture = new();

    public ListTransactionsControllerTests()
    {
        var mocker = new AutoMocker();
        _useCase = mocker.GetMock<IListTransactionsUseCase>();
        _sut = mocker.CreateInstance<ListTransactionsController>();
        _outputPort = (_sut as IListTransactionsOutputPort);
    }

    [Fact]
    public async Task ShouldGetTransactionsSuccessfully()
    {
        //Arrange        
        var output = _fixture.Create<ListTransactionsOutput>();
        var expected = ListTransactionsResponse.Success(output);

        _useCase
            .Setup(useCase => useCase.ExecuteAsync(default))
            .Callback(() => _outputPort.Ok(output));

        //Act
        var result = await _sut.GetTransactions(default) as ObjectResult;

        //Assert
        result!.StatusCode.Should().Be(StatusCodes.Status200OK);
        result!.Value.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldGetTransactionsNotFound()
    {
        //Arrange        
        var errorMessage = "Not found Transactions";
        var expected = ListTransactionsResponse.Error(errorMessage);

        _useCase
            .Setup(useCase => useCase.ExecuteAsync(default))
            .Callback(() => _outputPort.NotFound());

        //Act
        var result = await _sut.GetTransactions(default) as ObjectResult;

        //Assert
        result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        result!.Value.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldSaveTransactionWithError()
    {
        //Arrange        
        var errorMessage = _fixture.Create<string>();
        var expected = ListTransactionsResponse.Error(errorMessage);

        _useCase
            .Setup(useCase => useCase.ExecuteAsync(default))
            .Callback(() => _outputPort.Error(errorMessage));

        //Act
        var result = await _sut.GetTransactions(default) as ObjectResult;

        //Assert
        result!.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        result!.Value.Should().BeEquivalentTo(expected);
    }
}
