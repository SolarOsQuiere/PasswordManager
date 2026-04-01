using System.ComponentModel.DataAnnotations;

namespace PassManager.Models;

public sealed class NumericCodeRequest : KeyMaterialInput
{
    public const int MinDigits = 4;
    public const int MaxDigits = 16;

    [Required(ErrorMessage = "Debes indicar la longitud del PIN.")]
    [Range(MinDigits, MaxDigits, ErrorMessage = "La longitud del PIN debe estar entre 4 y 16 digitos.")]
    public int? Digits { get; set; } = 8;
}
