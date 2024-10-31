using System.ComponentModel.DataAnnotations;

namespace ERPSystem.API.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "El número de teléfono no tiene un formato válido.")]
        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}