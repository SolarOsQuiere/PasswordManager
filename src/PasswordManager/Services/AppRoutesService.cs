using PasswordManager.Models;
using Microsoft.AspNetCore.Components;

namespace PasswordManager.Services;

public sealed class AppRoutesService
{
    private readonly NavigationManager navigationManager;

    private static readonly IReadOnlyDictionary<AppPage, string> RelativePagePaths = new Dictionary<
        AppPage,
        string
    >
    {
        [AppPage.Home] = string.Empty,
        [AppPage.Documentation] = "documentacion",
        [AppPage.NotFound] = "not-found",
    };

    public AppRoutesService(NavigationManager navigationManager)
    {
        this.navigationManager = navigationManager;
    }

    public string GetPath(AppPage page)
    {
        var targetUri = new Uri(new Uri(navigationManager.BaseUri), RelativePagePaths[page]);
        return targetUri.PathAndQuery + targetUri.Fragment;
    }

    public string GetGeneratorUri(GeneratorKind generatorKind)
    {
        var homeUri = new Uri(navigationManager.BaseUri);
        var builder = new UriBuilder(homeUri)
        {
            Query = $"tool={ToQueryValue(generatorKind)}",
        };

        return builder.Uri.PathAndQuery + builder.Uri.Fragment;
    }

    public static GeneratorKind ParseGenerator(string? tool) =>
        tool?.Trim().ToLowerInvariant() switch
        {
            "codigo" => GeneratorKind.NumericCode,
            "numeric-code" => GeneratorKind.NumericCode,
            _ => GeneratorKind.Password,
        };

    public static string ToQueryValue(GeneratorKind generatorKind) =>
        generatorKind switch
        {
            GeneratorKind.NumericCode => "codigo",
            _ => "clave",
        };
}
