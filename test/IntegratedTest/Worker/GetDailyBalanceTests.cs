namespace IntegratedTest.Worker;

public class GetDailyBalanceTests : IClassFixture<ApplicationFactory>
{
    private readonly ApplicationFactory _factory;

    public GetDailyBalanceTests(ApplicationFactory factory) =>
        _factory = factory;    

    [Fact]
    public async Task ShouldGetDailyBalanceSuccessfully()
    {
        //Arrange                        
        var client = _factory.GetClient();        

        //Act
        var response = await client.GetAsync(Routes.Balance);

        //Assert
        response.EnsureSuccessStatusCode();
    }
}
