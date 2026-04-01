using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models;

public abstract class KeyMaterialInput : IValidatableObject
{
    [Required(ErrorMessage = "La clave privada es obligatoria.")]
    [StringLength(
        200,
        MinimumLength = 3,
        ErrorMessage = "La clave privada debe tener entre 3 y 200 caracteres."
    )]
    public string PrivateKey { get; set; } = string.Empty;

    [Required(ErrorMessage = "La clave publica es obligatoria.")]
    [StringLength(
        200,
        MinimumLength = 3,
        ErrorMessage = "La clave publica debe tener entre 3 y 200 caracteres."
    )]
    public string PublicKey { get; set; } = string.Empty;

    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(PrivateKey))
        {
            yield return new ValidationResult(
                "La clave privada no puede estar vacia.",
                [nameof(PrivateKey)]
            );
        }

        if (string.IsNullOrWhiteSpace(PublicKey))
        {
            yield return new ValidationResult(
                "La clave publica no puede estar vacia.",
                [nameof(PublicKey)]
            );
        }
    }
}
