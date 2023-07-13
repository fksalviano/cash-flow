using Domain.Validation;
using Application.UseCases.SaveTransaction;
using Application.UseCases.SaveTransaction.Abstractions;
using Application.UseCases.SaveTransaction.Ports;
using AutoFixture;
using Moq;
using Moq.AutoMock;

namespace UnitTest.UseCases.SaveTransaction;

public class SaveTransactionUseCaseValidationTests
{
    private readonly Mock<ISaveTransactionUseCase> _useCase;
    private readonly Mock<ISaveTransactionOutputPort> _outputPort;

    private readonly ISaveTransactionUseCase _sut;
    private readonly Fixture _fixture = new();

    public SaveTransactionUseCaseValidationTests()
    {
        var mocker = new AutoMocker();
                
        _useCase = mocker.GetMock<ISaveTransactionUseCase>();
        _outputPort = mocker.GetMock<ISaveTransactionOutputPort>();

        _sut = mocker.CreateInstance<SaveTransactionUseCaseValidation>();
        _sut.SetOutputPort(_outputPort.Object);
    }

    [Fact]
    public async Task ShouldValidateSuccessfully()
    {
        //Arrange
        var input = _fixture.Build<SaveTransactionInput>()
            .With(input => input.Description, _fixture.Create<string>())
            .With(input => input.Type, new string[] { "C", "D" }[new Random().Next(1)])
            .With(input => input.Value, _fixture.Create<decimal>())
            .Create();

        //Act
        await _sut.ExecuteAsync(input, default);

        // Assert
        _useCase.Verify(useCase => useCase.ExecuteAsync(input, CancellationToken.None), 
            Times.Once);

        _outputPort.Verify(output => output.Invalid(It.IsAny<Result>()), 
            Times.Never);
    }

    [Theory]
    [InlineData("", "X", -100)]
    [InlineData("", "D", 100)]
    [InlineData("test", "X", 100)]    
    [InlineData("test", "C", 0)]    
    public async Task ShouldValidateReturnInvalid(string description, string type, decimal value)
    {
        //Arrange
        var input = _fixture.Build<SaveTransactionInput>()
            .With(input => input.Description, description)
            .With(input => input.Type, type)
            .With(input => input.Value, value)
            .Create();

        //Act
        await _sut.ExecuteAsync(input, default);

        // Assert
        _outputPort.Verify(output => output.Invalid(It.IsAny<Result>()), 
            Times.Once);

        _useCase.Verify(useCase => useCase.ExecuteAsync(input, CancellationToken.None), 
            Times.Never);        
    }
}
