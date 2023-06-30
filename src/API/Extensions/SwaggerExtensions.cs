using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Application.Commons.Extensions;

[ExcludeFromCodeCoverage]
public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, 
        string name, string title, string description)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options => 
            {
                options.SwaggerDoc(name, new OpenApiInfo
                {
                    Title = title,
                    Description = description
                });
                
                options.TagActionsByRelativePath();
                
                var xmlFilename = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

        return services;
    }

    public static void TagActionsByRelativePath(this SwaggerGenOptions options)
    {
        options.TagActionsBy(api => new[] { api.RelativePath.ToTitleCase() });
        options.DocInclusionPredicate((name, api) => true);
    }

    private static string ToTitleCase(this string? text) =>
        CultureInfo.InstalledUICulture.TextInfo.ToTitleCase(text?.ToLower() ?? string.Empty);
}
