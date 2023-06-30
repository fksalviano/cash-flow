var builder = WebApplication.CreateBuilder(args);

builder.Services
    .InstallServices()
    .AddControllers();

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddSwagger("v1",
    "CashFlow-API", 
    ".Net Core API using Clean Architecture and Vertical Slice");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
