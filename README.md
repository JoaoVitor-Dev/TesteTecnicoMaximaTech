# Teste Técnico - MaximaTech

Este projeto foi desenvolvido como parte de um teste técnico para a empresa **MaximaTech**, com o objetivo de demonstrar habilidades em desenvolvimento **fullstack**, utilizando **Angular** no frontend e **.NET Core** no backend.

##  Sumário

- [ Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [ Como Executar Localmente](#-como-executar-localmente)
- [ API - Endpoints](#-api---endpoints)
- [ Funcionalidades Implementadas](#-funcionalidades-implementadas)

---

##  Tecnologias Utilizadas

### Backend - .NET Core
- ASP.NET Core Web API
- Dapper
- MySQL
- Swagger 
- CORS Policy

### Frontend - Angular
- Angular 18
- Reactive Forms
- HttpClient
- TypeScript

---

## Como Executar Localmente

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js LTS](https://nodejs.org/)
- Angular CLI 18+:  
  ```bash
  npm install -g @angular/cli
  ```

### 1. Clonar o repositório

```bash
git clone https://github.com/JoaoVitor-Dev/TesteTecnicoMaximaTech.git
cd TesteTecnicoMaximaTech
```

### 2. Backend - API (.NET)

- Recomendado abrir e rodar o projeto em uma IDE (Visual Studio/Rider) em debug
- Acessar o Projeto MaximaTechProductAPI.Application e configurar o "DefaultConnection" no appsettings.json com: Endereço do servidor; Porta; User; Passoword;
- Ao executar o projeto Application, ele criara o Banco de Dados, Tabelas, Colunas e dados mocados vis script SQL e exibirá o Swagger no Browser.

### 3. Frontend - Angular

```bash
cd MaximaTechProductWebApp
npm install
ng serve
```

- A aplicação estará disponível em: `http://localhost:4200`
- (ATENÇÃO): A api possívelmente pode rodar em uma porta diferente da 7235, se necessário altere a porta em: MaximaTechProductWebApp/src/app/services/api.service.ts na variável port.

---

##  API - Endpoints

Exemplos principais (documentados via Swagger):

- `GET /produto` - Listar produtos
- `GET /produto/{id}` - Obter produto por ID
- `POST /produto` - Cadastrar produto
- `PUT /produto/{id}` - Atualizar produto
- `DELETE /produto/{id}` - Inativar produto

---

##  Funcionalidades Implementadas

### Frontend:
- Listagem de produtos
- Cadastro e edição de produto
- Inativação de Produto
- Interface responsiva

### Backend:
- API RESTful com .NET Core
- CRUD de Produto e Departamento
- Persistência com Dapper em MySQL
- Injeção de dependência e arquitetura em camadas
- Documentação com Swagger

---
Desenvolvido por [João Vitor Sousa](https://github.com/JoaoVitor-Dev)