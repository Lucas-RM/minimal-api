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
    public class VeiculoServicoTest
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
        public void TestandoSalvarVeiculo()
        {
            // Arrange: Cria o contexto de teste e limpa a tabela Veiculos
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            // Criação de um novo objeto 'Veiculo' com valores de teste.
            var veiculo = new Veiculo();
            veiculo.Nome = "Civic";
            veiculo.Marca = "Honda";
            veiculo.Ano = 2022;

            // Instancia a classe de serviço responsável por operações de CRUD com o contexto de banco de dados.
            var veiculoServico = new VeiculoServico(context);

            // Act: Insere o veículo no banco de dados.
            veiculoServico.Incluir(veiculo);

            // Assert: Verifica se o número de veículos cadastrados é igual a 1.
            Assert.AreEqual(1, veiculoServico.Todos(1).Count(), "O número de veículos cadastrados está incorreto.");
        }

        [TestMethod]
        public void TestandoBuscaPorId()
        {
            // Arrange: Cria o contexto de teste e limpa a tabela Veiculos
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            // Criação de um novo objeto 'Veiculo' com valores de teste.
            var veiculo = new Veiculo();
            veiculo.Nome = "Civic";
            veiculo.Marca = "Honda";
            veiculo.Ano = 2022;

            // Instancia a classe de serviço responsável por operações de CRUD com o contexto de banco de dados.
            var veiculoServico = new VeiculoServico(context);

            // Act: Inclui o veículo no banco e busca pelo ID
            veiculoServico.Incluir(veiculo);
            var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);

            // Assert: Verifica se o ID do veículo retornado é igual a 1
            Assert.AreEqual(1, veiculoDoBanco?.Id);
        }

        [TestMethod]
        public void TestandoBuscarTodosOsVeiculos()
        {
            // Arrange: Cria o contexto de teste e limpa a tabela Veiculos
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            // Instancia a classe de serviço responsável por operações de CRUD com o contexto de banco de dados.
            var veiculoServico = new VeiculoServico(context);

            // Cria uma lista de veiculos para teste
            var listaVeiculos = new List<Veiculo>
            {
                new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2022 },
                new Veiculo { Nome = "Corolla", Marca = "Toyota", Ano = 2023 },
                new Veiculo { Nome = "Mustang", Marca = "Ford", Ano = 2021 }
            };

            // Insere todos os veiculos no contexto
            foreach (var veiculo in listaVeiculos)
            {
                veiculoServico.Incluir(veiculo);
            }

            // Act: Busca todos os veiculos cadastrados
            var veiculosDoBanco = veiculoServico.Todos(1);

            // Assert: Verifica se a quantidade de veículos retornados é igual à quantidade inserida
            Assert.AreEqual(
                listaVeiculos.Count,
                veiculosDoBanco.Count(),
                "O número de veículos retornados está incorreto."
            );
        }

        [TestMethod]
        public void TestandoAtualizarVeiculo()
        {
            // Arrange: Cria o contexto de teste e limpa a tabela Veiculos
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            // Criação de um novo objeto 'Veiculo' com valores de teste
            var veiculo = new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2022 };

            // Instancia a classe de serviço responsável por operações de CRUD com o contexto de banco de dados
            var veiculoServico = new VeiculoServico(context);

            // Inclui o veículo no banco de dados
            veiculoServico.Incluir(veiculo);

            // Atualiza os dados do veículo
            veiculo.Nome = "Civic Atualizado";
            veiculo.Ano = 2023;

            // Act: Atualiza o veículo no banco de dados
            veiculoServico.Atualizar(veiculo);

            // Busca o veículo atualizado do banco
            var veiculoAtualizado = veiculoServico.BuscaPorId(veiculo.Id);

            // Assert: Verifica se as alterações foram persistidas corretamente
            Assert.AreEqual("Civic Atualizado", veiculoAtualizado?.Nome, "O nome do veículo não foi atualizado corretamente.");
            Assert.AreEqual(2023, veiculoAtualizado?.Ano, "O ano do veículo não foi atualizado corretamente.");
        }

        [TestMethod]
        public void TestandoApagarVeiculo()
        {
            // Arrange: Cria o contexto de teste e limpa a tabela Veiculos
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            // Instancia a classe de serviço responsável por operações de CRUD com o contexto de banco de dados
            var veiculoServico = new VeiculoServico(context);

            // Cria uma lista de veículos para teste
            var listaVeiculos = new List<Veiculo>
            {
                new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2022 },
                new Veiculo { Nome = "Corolla", Marca = "Toyota", Ano = 2023 },
                new Veiculo { Nome = "Mustang", Marca = "Ford", Ano = 2021 }
            };

            // Insere todos os veículos no contexto
            foreach (var veiculo in listaVeiculos)
            {
                veiculoServico.Incluir(veiculo);
            }

            // Act: Remove apenas um veículo do banco de dados (Civic)
            var veiculoParaRemover = listaVeiculos.First(v => v.Nome == "Civic");
            veiculoServico.Apagar(veiculoParaRemover);

            // Busca todos os veículos após a remoção
            var veiculosDoBanco = veiculoServico.Todos(1).ToList();

            // Assert: Verifica se apenas o veículo "Civic" foi removido
            Assert.AreEqual(2, veiculosDoBanco.Count, "O número de veículos retornados está incorreto após a remoção.");
            Assert.IsFalse(veiculosDoBanco.Any(v => v.Nome == "Civic"), "O veículo 'Civic' não foi removido corretamente.");
            Assert.IsTrue(veiculosDoBanco.Any(v => v.Nome == "Corolla"), "O veículo 'Corolla' deveria estar presente.");
            Assert.IsTrue(veiculosDoBanco.Any(v => v.Nome == "Mustang"), "O veículo 'Mustang' deveria estar presente.");
        }
    }
}