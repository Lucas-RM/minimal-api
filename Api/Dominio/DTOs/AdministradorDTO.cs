using MinimalApi.Dominio.Enums;

namespace MinimalApi.DTOs
{
    public class AdministradorDTO
    {
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
        public EPerfil? Perfil { get; set; } = default!;
    }
}