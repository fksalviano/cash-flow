namespace IntegratedTest.API;

public class ListTransactionsTests : IClassFixture<ApplicationFactory>
{
    private readonly ApplicationFactory _factory;

    public ListTransactionsTests(ApplicationFactory factory) =>
        _factory = factory;    

    [Fact]
    public async Task ShouldListTransactionsSuccessfully()
    {
        //Arrange                        
        var client = _factory.GetClient();        

        //Act
        var response = await client.GetAsync(Routes.Transaction);

        //Assert
        response.EnsureSuccessStatusCode();
    }
}
