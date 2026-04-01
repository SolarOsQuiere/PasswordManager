using PassManager.Models;

namespace PassManager.Services;

public sealed class AppRoutesService
{
    private static readonly IReadOnlyDictionary<AppPage, string> PagePaths = new Dictionary<
        AppPage,
        string
    >
    {
        [AppPage.Home] = "/",
        [AppPage.Documentation] = "/documentacion",
        [AppPage.NotFound] = "/not-found",
    };

    public static string GetPath(AppPage page) => PagePaths[page];

    public static string GetGeneratorUri(GeneratorKind generatorKind) =>
        $"{GetPath(AppPage.Home)}?tool={ToQueryValue(generatorKind)}";

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
