using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models;

public sealed class PasswordGenerationRequest : KeyMaterialInput
{
    [Required(ErrorMessage = "Debes indicar la longitud de la clave.")]
    [Range(15, 128, ErrorMessage = "La longitud debe estar entre 15 y 128 caracteres.")]
    public int? Length { get; set; } = 20;
}
