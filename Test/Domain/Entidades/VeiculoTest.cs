using MinimalApi.Dominio.Entidades;

namespace Test.Domain.Entidades
{
    [TestClass]
    public class VeiculoTest
    {
        [TestMethod]
        public void TestarGetSetPropriedades()
        {
            //Arrange
            var veiculo = new Veiculo();

            //Act
            veiculo.Id = 1;
            veiculo.Nome = "Corolla";
            veiculo.Marca = "Toyota";
            veiculo.Ano = 2023;

            //Assert
            Assert.AreEqual(1, veiculo.Id);
            Assert.AreEqual("Corolla", veiculo.Nome);
            Assert.AreEqual("Toyota", veiculo.Marca);
            Assert.AreEqual(2023, veiculo.Ano);
        }
    }
}