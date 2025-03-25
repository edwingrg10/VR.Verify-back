using System.ComponentModel.DataAnnotations;

public class RoleUpdateDTO
{
    [Required(ErrorMessage = "El ID del rol es requerido")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del rol es requerido")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 255 caracteres")]
    public string Name { get; set; }
}