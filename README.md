# 🔐 API de Autenticação com Hash de Senha (.NET)

API simples desenvolvida em **ASP.NET** para cadastro e autenticação de usuários utilizando **hash seguro de senha com BCrypt**.

Este projeto foi criado com objetivo **educacional**, para praticar conceitos importantes de backend como segurança de senha, Entity Framework e APIs REST.

---

## 🚀 Funcionalidades

- Cadastro de usuários
- Hash seguro de senha com **BCrypt**
- Login com verificação de senha
- Banco de dados **SQLite**
- Entity Framework Core (Code First)
- Testes de endpoints com **Swagger**

---

## 🧰 Tecnologias utilizadas

- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQLite**
- **BCrypt.Net**
- **Swagger / OpenAPI**

---

## 📦 Estrutura do projeto


Controllers/
UsuariosController.cs

Models/
Usuario.cs

DTOs/
UsuarioCriacaoDto.cs
UsuarioExibicaoDto.cs
LoginDto.cs

Data/
AppDbContext.cs

Program.cs


---

## ⚙️ Instalação

Clone o repositório:


Entre na pasta do projeto:


cd seu-repositorio


Restaure as dependências:


dotnet restore


Execute o projeto:


dotnet run


---

## 🗄 Banco de dados

O projeto utiliza **SQLite**, um banco de dados baseado em arquivo.

O arquivo será criado automaticamente:


usuarios.db


Caso utilize migrations:


dotnet ef migrations add InitialCreate
dotnet ef database update


---

## 📡 Endpoints da API

### 📌 Cadastro de usuário

POST `/api/usuarios`

Exemplo de requisição:

```json
{
  "nome": "string",
  "email": "email@email.com",
  "senha": "123456"
}

A senha será transformada em hash antes de ser salva no banco.

📌 Listar usuários

GET /api/usuarios

Retorna todos os usuários cadastrados.

⚠️ Neste projeto o hash da senha é exibido apenas para fins educacionais.

📌 Buscar usuário por ID

GET /api/usuarios/{id}

📌 Login

POST /api/usuarios/login

Exemplo:

{
  "email": "email@email.com",
  "senha": "123456"
}

O sistema valida a senha utilizando:

BCrypt.Verify()
🔒 Segurança de senha

As senhas não são armazenadas em texto puro.

Durante o cadastro é utilizado:

BCrypt.Net.BCrypt.HashPassword()

Durante o login:

BCrypt.Net.BCrypt.Verify()

Isso garante que mesmo que o banco de dados seja exposto, as senhas originais não possam ser recuperadas.

📚 Conceitos praticados

Este projeto aborda diversos conceitos importantes de backend:

APIs REST

Hash de senha

Autenticação básica

Entity Framework Core

SQLite

DTO (Data Transfer Object)

Injeção de dependência

Async / Await

⚠️ Aviso

Este projeto retorna o hash da senha em algumas respostas apenas para fins educacionais.

Em aplicações reais, hashes de senha não devem ser retornados por endpoints da API.

📄 Licença

Projeto desenvolvido para fins de estudo.
