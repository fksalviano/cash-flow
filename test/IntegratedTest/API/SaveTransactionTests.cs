using System.Net.Http.Json;
using Application.UseCases.SaveTransaction.Ports;

namespace IntegratedTest.API;

public class SaveTransactionTests : IClassFixture<ApplicationFactory>
{
    private readonly ApplicationFactory _factory;

    public SaveTransactionTests(ApplicationFactory factory) =>
        _factory = factory;    

    [Fact]
    public async Task ShouldSaveTransactionSuccessfully()
    {
        //Arrange                        
        var client = _factory.GetClient();
        var input = new SaveTransactionInput(DateTime.Now, "Test", "C", 100);

        //Act
        var response = await client.PostAsJsonAsync(Routes.Transaction, input);

        //Assert
        response.EnsureSuccessStatusCode();
    }
}
