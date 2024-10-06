![Logo](https://github.com/jasonamaral/mba.modulo1/blob/feature/modulo1/src/mba.modulo1.blog.mvc/wwwroot/images/logo%20-%20Copy.png?raw=true)

# **mba.modulo1 - Aplicação de Blog Simples com MVC e API RESTful**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **mba.modulo1**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Introdução ao Desenvolvimento ASP.NET Core**.
O objetivo principal desenvolver uma aplicação de blog que permite aos usuários criar, editar, visualizar e excluir posts e comentários, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful.
Descreva livremente mais detalhes do seu projeto aqui.

### **Autor(es)**
- **Jason Santos do Amaral**

## **2. Proposta do Projeto**

O projeto consiste em:

- **Aplicação MVC:** Interface web para interação com o blog.
- **API RESTful:** Exposição dos recursos do blog para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e usuários comuns.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:** SQL Server
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estilização básica
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:
- docs
  - [Especificação](https://github.com/jasonamaral/mba.modulo1/blob/feature/modulo1/docs/Projeto-Primeiro-Modulo-Mba-DevXpert.pdf)
  - [Tarefas para o Desenvolvimento](https://github.com/jasonamaral/mba.modulo1/blob/feature/modulo1/docs/Projeto-Primeiro-Modulo-Mba-DevXpert.pdf)
  - [Postman Environment](https://github.com/jasonamaral/mba.modulo1/blob/feature/modulo1/docs/MBA-Modulo1.postman_environment.json)
  - [Postman Collection](https://github.com/jasonamaral/mba.modulo1/raw/feature/modulo1/docs/MBA.Modulo1.Blog.API.postman_collection.json)

- src/
  - MBA.Modulo1.Blog.api/ - API RESTful 
  - MBA.Modulo1.Blog.data/ - Configuração do EF Core, Mapeamento de modelos
  - MBA.Modulo1.Blog.domain/ - Modelos de Dados
  - MBA.Modulo1.Blog.DTO - View models
  - MBA.Modulo1.Blog.IoC - Injeção de dependencia
  - MBA.Modulo1.Blog.mvc - Projeto MVC
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **CRUD para Posts e Comentários:** Permite criar, editar, visualizar e excluir posts e comentários.
- **Autenticação e Autorização:** Diferenciação entre usuários comuns e administradores.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.
- **Postman Collection:** Collection do Postman para validação da API.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- SQL Server
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/jasonamaral/mba.modulo1.git`
   - `cd mba.modulo1`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos

3. **Executar a Aplicação MVC:**
   - `cd src/MBA.Modulo1.Blog.Mvc/`
   - `dotnet run`
   - Acesse a aplicação em: http://localhost:5177

4. **Executar a API:**
   - `cd src/MBA.Modulo1.Blog.Api/`
   - `dotnet run`
   - Acesse a documentação da API em: https://localhost:7023/swagger
   - Opcionalmente, a API pode ser testada com o Postman https://www.postman.com/downloads/ usando o [Collection](https://github.com/jasonamaral/mba.modulo1/raw/feature/modulo1/docs/MBA.Modulo1.Blog.API.postman_collection.json) e [Environment](https://github.com/jasonamaral/mba.modulo1/blob/feature/modulo1/docs/MBA-Modulo1.postman_environment.json)

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.
  - No seed é criado o usuário "admin@example.com" com a senha "Teste@123"

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

https://localhost:7023/swagger

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.