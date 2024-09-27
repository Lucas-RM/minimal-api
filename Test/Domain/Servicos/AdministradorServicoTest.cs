using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

namespace Test.Domain.Entidades
{

    [TestClass]
    public class AdministradorServicoTest
    {
        private DbContexto CriarContextoDeTeste()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

            var builder = new ConfigurationBuilder()
                .SetBasePath(path ?? Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            return new DbContexto(configuration);
        }


        [TestMethod]
        public void TestandoSalvarAdministrador()
        {
            // Arrange: Cria o contexto de teste e limpa a tabela Administradores
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

            // Criação de um novo objeto 'Administrador' com valores de teste.
            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";

            // Instancia a classe de serviço responsável por operações de CRUD com o contexto de banco de dados.
            var administradorServico = new AdministradorServico(context);

            // Act: Insere o administrador no banco de dados.
            administradorServico.Incluir(adm);

            // Assert: Verifica se o número de administradores cadastrados é igual a 1.
            Assert.AreEqual(1, administradorServico.Todos(1).Count(), "O número de administradores cadastrados está incorreto.");
        }

        [TestMethod]
        public void TestandoBuscaPorId()
        {
            // Arrange: Cria o contexto de teste e limpa a tabela Administradores
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

            // Criação de um novo objeto 'Administrador' com valores de teste.
            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";

            // Instancia a classe de serviço responsável por operações de CRUD com o contexto de banco de dados.
            var administradorServico = new AdministradorServico(context);

            // Act: Inclui o administrador no banco e busca pelo ID
            administradorServico.Incluir(adm);
            var admDoBanco = administradorServico.BuscaPorId(adm.Id);

            // Assert: Verifica se o ID do administrador retornado é igual a 1
            Assert.AreEqual(1, admDoBanco?.Id);
        }

        [TestMethod]
        public void TestandoBuscarTodosOsAdministradores()
        {
            // Arrange: Cria o contexto de teste e limpa a tabela Administradores
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

            // Instancia a classe de serviço responsável por operações de CRUD com o contexto de banco de dados.
            var administradorServico = new AdministradorServico(context);

            // Cria uma lista de administradores para teste
            var listaAdministradores = new List<Administrador>
            {
                new Administrador { Email = "admin1@teste.com", Senha = "senha1", Perfil = "Adm" },
                new Administrador { Email = "admin2@teste.com", Senha = "senha2", Perfil = "Editor" },
                new Administrador { Email = "admin3@teste.com", Senha = "senha3", Perfil = "Adm" }
            };

            // Insere todos os administradores no contexto
            foreach (var adm in listaAdministradores)
            {
                administradorServico.Incluir(adm);
            }

            // Act: Busca todos os administradores cadastrados
            var administradoresDoBanco = administradorServico.Todos(1);

            // Assert: Verifica se a quantidade de administradores retornados é igual à quantidade inserida
            Assert.AreEqual(
                listaAdministradores.Count,
                administradoresDoBanco.Count(),
                "O número de administradores retornados está incorreto."
            );
        }

        [TestMethod]
        public void TestandoLoginAdministrador()
        {
            // Arrange: Criação do contexto de teste e limpeza da tabela 'Administradores'
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

            // Criação de um novo objeto 'Administrador' com dados de teste
            var adm = new Administrador { Email = "login@teste.com", Senha = "123456", Perfil = "Adm" };

            // Instancia o serviço de administrador e insere o administrador de teste no banco de dados
            var administradorServico = new AdministradorServico(context);
            administradorServico.Incluir(adm);

            // Criação do objeto LoginDTO com as mesmas credenciais para simular o login
            var loginDTO = new LoginDTO { Email = "login@teste.com", Senha = "123456" };

            // Act: Chama o método Login() com o objeto LoginDTO criado
            var resultadoLogin = administradorServico.Login(loginDTO);

            // Assert: Verifica se o administrador retornado não é nulo e se o e-mail corresponde ao esperado
            Assert.IsNotNull(resultadoLogin, "O login falhou, mas deveria ter sucesso.");
            Assert.AreEqual(adm.Email, resultadoLogin?.Email, "O e-mail do administrador retornado não corresponde ao esperado.");
            Assert.AreEqual(adm.Senha, resultadoLogin?.Senha, "A senha do administrador retornado não corresponde ao esperado.");
        }

        [TestMethod]
        public void TestandoLoginAdministradorComCredenciaisInvalidas()
        {
            // Arrange: Criação do contexto de teste e limpeza da tabela 'Administradores'
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

            // Criação de um novo objeto 'Administrador' com dados de teste
            var adm = new Administrador { Email = "login@teste.com", Senha = "123456", Perfil = "Adm" };

            // Instancia o serviço de administrador e insere o administrador de teste no banco de dados
            var administradorServico = new AdministradorServico(context);
            administradorServico.Incluir(adm);

            // Criação do objeto LoginDTO com credenciais incorretas para simular o login
            var loginDTO = new LoginDTO { Email = "login@teste.com", Senha = "senhaIncorreta" };

            // Act: Chama o método Login() com credenciais incorretas
            var resultadoLogin = administradorServico.Login(loginDTO);

            // Assert: Verifica se o retorno é nulo, já que as credenciais são inválidas
            Assert.IsNull(resultadoLogin, "O login deveria ter falhado com credenciais incorretas, mas não falhou.");
        }
    }
}