# DIO - Desafio de Projeto: API de Registro de Veículos com Minimal APIs

🔗 [www.dio.me](www.dio.me)

## Desafio de Projeto

> Este projeto foi desenvolvido como parte de um desafio da plataforma Digital Innovation One (DIO), com o objetivo de criar uma API utilizando a técnica de Minimal APIs para o registro de veículos.

## Contexto

> O sistema desenvolvido visa gerenciar o registro de veículos, oferecendo funcionalidades para **CRUD (Create, Read, Update, Delete)** de veículos e gerenciamento de administradores com autenticação **JWT**.
>
> Para explorar o funcionamento da API, foi utilizada a ferramenta **Swagger**, facilitando a interação e teste das requisições. Além disso, **testes unitários** foram aplicados para garantir a robustez e a confiabilidade do sistema.

## Funcionalidades

1. **Registro de Veículos**: Operações de CRUD para veículos com informações como Nome, Marca, e Ano.

2. **Gerenciamento de Administradores**: Controle de acesso e autenticação de administradores utilizando JWT.

3. **Autenticação JWT**: Implementação de autenticação com tokens JWT para proteger as rotas da API e garantir que apenas administradores autorizados possam acessar certos recursos.

4. **Documentação via Swagger**: Exploração da API com uma interface visual amigável oferecida pelo Swagger.

5. **Testes Unitários**: Testes aplicados para validar as funcionalidades principais da aplicação e garantir a qualidade do código.

## Regras e Validações

1. **CRUD de Veículos**: As operações de criação, leitura, atualização e exclusão de veículos são controladas e protegidas por autenticação JWT.

2. **JWT para Administradores**: Apenas administradores autenticados podem realizar certas ações, garantindo um controle seguro do sistema.

3. **Validações de Dados**: Todas as informações fornecidas para o registro de veículos são validadas, como o formato da placa e o ano do veículo.
   
## Tecnologias Utilizadas

- .NET Core com Minimal APIs
- Autenticação JWT
- Swagger para documentação da API
- Testes unitários com MSTest
- Entity Framework Core para manipulação de dados
- SQL Server como banco de dados

## Solução

> O projeto foi desenvolvido a partir do zero, utilizando as práticas modernas de desenvolvimento de APIs com .NET Core. O uso de **Minimal APIs** permitiu uma abordagem simplificada e eficiente, focada no desempenho e na facilidade de implementação.
>
> Além disso, a segurança foi reforçada com o uso de JWT, e a documentação automática via Swagger permitiu a fácil exploração das funcionalidades da API durante o desenvolvimento.

---

## Como Utilizar o Projeto - Passo a Passo

> Siga os passos abaixo para executar o projeto localmente:

### Pré-requisitos

> Antes de começar, certifique-se de que você tenha instalado o seguinte:

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download) (Utilizei o [.NET SDK 7.0.404](https://dotnet.microsoft.com/pt-br/download/dotnet/thank-you/sdk-7.0.404-windows-x64-installer) no projeto).

- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (ou outra base de dados configurável).

- [Visual Studio Code](https://code.visualstudio.com/) ou [Visual Studio 2022](https://visualstudio.microsoft.com/).

- [Postman](https://www.postman.com/) ou outra ferramenta para testar requisições HTTP (opcional, além do Swagger).

### 1. Clone o repositório

> Clone o projeto para o seu ambiente local:

```bash
git clone https://github.com/seu-usuario/minimal-api.git
cd minimal-api
```

### 2. Configure o Banco de Dados

> Abra os arquivos `appsettings.json` dentro das pastas `Api/` e `Test/` e configure a string de conexão com seu banco de dados SQL Server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=NomeDoBanco;Uid=seu_usuario;Pwd=sua_senha;"
}
```

### 3. Restaurar Dependências

> Navegue até o diretório do projeto e execute o comando para restaurar os pacotes NuGet necessários:

```bash
dotnet restore
```

### 4. Executar Migrações do Entity Framework

> Aplique as migrações do banco de dados para criar as tabelas necessárias:

```bash
dotnet ef database update
```

### 5. Executar o Projeto

> Execute a aplicação para inicializar a API:

```bash
dotnet run --project Api
```

> A API estará disponível em `https://localhost:5001` ou `http://localhost:5000`.

### 6. Explorar a API via Swagger

> Abra o navegador e acesse o Swagger para interagir com a API:

```
https://localhost:5001/swagger
```

> No Swagger, você poderá visualizar e testar as rotas da API diretamente.

### 7. Testes

> Para rodar os testes unitários e garantir que a aplicação está funcionando corretamente:

```bash
dotnet test
```

---

> Agora você tem o projeto configurado e funcionando no seu ambiente local. Sinta-se à vontade para modificar e expandir conforme necessário!
