using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos
{
    public class AdministradorServico : IAdministradorServico
    {
        private readonly DbContexto _contexto;

        public AdministradorServico(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public Administrador? Login(LoginDTO loginDTO)
        {
            var temAdministrador = _contexto.Administradores.Where(a => 
                a.Email == loginDTO.Email &&
                a.Senha == loginDTO.Senha
            ).FirstOrDefault();

            return temAdministrador;
        }
    }
}