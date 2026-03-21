# Desafio Técnico — Biblioteca API

API desenvolvida em **.NET 8** para gerenciamento de biblioteca, com cadastro de usuários, livros e empréstimos, incluindo controle de devoluções, cálculo de multas e aplicação de regras de negócio.

A solução foi construída com foco em:

- clareza das regras de negócio
- separação de responsabilidades
- baixo acoplamento
- facilidade de manutenção
- execução local e via Docker

---

## Tecnologias utilizadas

- **.NET 8**
- **ASP.NET Core Web API**
- **Dapper**
- **SQLite**
- **FluentValidation**
- **Swagger / OpenAPI**
- **xUnit**
- **Moq**
- **Docker**

---

## Estrutura do projeto

A solução foi organizada em uma arquitetura **monolítica em camadas**, mantendo a estrutura base do desafio e evoluindo com foco em legibilidade e manutenção.

### Estrutura de diretórios

```text
.
├── .dockerignore
├── .gitattributes
├── .gitignore
├── BibliotecaApi.slnx
├── docker-compose.yml
├── Dockerfile
├── README.md
├── Script_para_adicionar_campo_possui_atraso_ativo_tabela_usuarios.sql
├── src
│   └── BibliotecaApi
│       ├── Application
│       ├── Domain
│       ├── Infrastructure
│       ├── UseCases
│       ├── Biblioteca.db
│       ├── appsettings.json
│       └── BibliotecaApi.csproj
└── tests
    └── BibliotecaApi.Tests
        └── BibliotecaApi.Tests.csproj
```

### Camadas principais

- **Application**
  - Controllers
  - Filtros de autorização
  - Atributos
  - Respostas padronizadas da API
  - Validators

- **Domain**
  - Entidades
  - Regras de negócio centrais

- **Infrastructure**
  - Repositórios
  - Acesso ao banco de dados
  - Injeção de dependência / IoC

- **UseCases**
  - Casos de uso da aplicação
  - Orquestração das regras de negócio

- **Tests**
  - Testes automatizados com xUnit e Moq

---

## Como executar localmente

### Pré-requisitos

- **.NET SDK 8.0** ou superior
- Visual Studio 2022 / VS Code / Rider (opcional)

### 1. Restaurar dependências

```bash
dotnet restore
```

### 2. Compilar a solução

```bash
dotnet build
```

### 3. Executar os testes

```bash
dotnet test
```

### 4. Executar a API

```bash
dotnet run --project ./src/BibliotecaApi/BibliotecaApi.csproj
```

### 5. Acessar o Swagger

Em ambiente local, a porta pode variar conforme o `launchSettings.json` do projeto.

Após subir a API, acesse:

- `http://localhost:<porta>/swagger`
- ou
- `https://localhost:<porta>/swagger`

> **Observação:** o Swagger está habilitado apenas em ambiente **Development**.

---

## Como executar com Docker

O projeto já está preparado para execução em container, incluindo o arquivo do banco SQLite no build da imagem.

### 1. Build da imagem

```bash
docker build -t biblioteca-api .
```

### 2. Executar o container

```bash
docker run -d -p 8080:8080 --name biblioteca-api biblioteca-api
```

### 3. Acessar a aplicação

- API: `http://localhost:8080`
- Swagger: `http://localhost:8080/swagger`

> O container sobe em ambiente **Development**, permitindo acesso ao Swagger.

---

## Como executar com Docker Compose

Também é possível executar a API via `docker-compose`.

### Subir o container

```bash
docker compose up --build
```

### Acessar a aplicação

- API: `http://localhost:8080`
- Swagger: `http://localhost:8080/swagger`

---

## Banco de dados

O projeto utiliza **SQLite** com o arquivo de banco já incluído no repositório:

- `src/BibliotecaApi/Biblioteca.db`

A connection string está configurada em:

- `src/BibliotecaApi/appsettings.json`

```json
"ConnectionStrings": {
  "Database": "Data Source = Biblioteca.db"
}
```

### Observação importante

Como a connection string utiliza um caminho relativo (`Biblioteca.db`), o `Dockerfile` copia o banco para o diretório `/app` dentro do container, garantindo que a aplicação encontre o arquivo corretamente durante a execução.

---

## Autenticação

Os endpoints sensíveis foram protegidos por **API Token** via header `Authorization`.

### Header esperado

```http
Authorization: Bearer biblioteca-api-token-seguro
```

O token está configurado em:

- `src/BibliotecaApi/appsettings.json`

```json
"ApiTokenSettings": {
  "Token": "biblioteca-api-token-seguro"
}
```

### Endpoints protegidos por token

- `POST /Livro/Cadastrar`
- `POST /Emprestimo/Cadastrar`
- `POST /Emprestimo/Devolver`

---

## Endpoints principais

### Usuário

- `POST /Usuario/Cadastrar`

### Livro

- `POST /Livro/Cadastrar` _(protegido por token)_
- `GET /Livro/Listar`

### Empréstimo

- `POST /Emprestimo/Cadastrar` _(protegido por token)_
- `POST /Emprestimo/Devolver` _(protegido por token)_

---

## Desafios implementados

### 1. Validar CPF do usuário

- Impede o cadastro de usuário com CPF já existente.
- Retorna erro apropriado quando o CPF já está cadastrado.

### 2. Validar ISBN do livro

- O ISBN foi validado para conter **exatamente 13 dígitos numéricos**.
- A validação foi aplicada com **FluentValidation**.

### 3. Corrigir geração de multa quando a devolução ocorre no prazo

- A multa só é aplicada quando a devolução ocorre **após** a data prevista de devolução.
- Corrigido o bug em que a multa era gerada indevidamente.

### 4. Impedir empréstimo de livro já emprestado

- Antes de registrar um novo empréstimo, a API verifica se o livro já está emprestado e ainda não foi devolvido.
- Se estiver, o novo empréstimo é bloqueado.

### 5. Criar endpoint `GET /Livro/Listar`

- Implementado o endpoint `GET /Livro/Listar`.
- Retorna a lista completa de livros cadastrados.

### 6. Impedir novo empréstimo se o usuário tiver atraso

- Antes de permitir novo empréstimo, a API verifica se o usuário possui empréstimo em atraso.
- Caso exista atraso ativo, o novo empréstimo é bloqueado.

#### Alteração estrutural do banco

Para suportar essa regra, foi incluído o script SQL:

- `Script_para_adicionar_campo_possui_atraso_ativo_tabela_usuarios.sql`

Esse script adiciona a marcação de atraso ativo na tabela de usuários, conforme solicitado no desafio.

### 7. Ajustar regra de multa

Nova regra implementada:

- Até **3 dias** de atraso → **R$ 2,00 por dia**
- A partir do **4º dia** → **R$ 3,50 por dia**
- Limite máximo de multa → **R$ 50,00**

Exemplos da regra:

- 2 dias → **R$ 4,00**
- 5 dias → **R$ 13,00**
- 20 dias → **R$ 50,00** (limitado)

### 8. Implementar autenticação (Bearer / API Token)

- Endpoints sensíveis protegidos por **API Token**
- Retorno `401 Unauthorized` quando o token é inválido ou ausente

---

## Testes automatizados

O projeto possui uma suíte de testes em:

- `tests/BibliotecaApi.Tests`

### Ferramentas utilizadas

- **xUnit**
- **Moq**

### Executar os testes

```bash
dotnet test
```

---

## Melhorias adicionais realizadas

Além da implementação dos desafios, também foram aplicadas melhorias complementares para elevar a qualidade da entrega:

- Correção de **warnings de nulabilidade** (`Nullable Reference Types`)
- Ajustes de robustez em fluxos de devolução e autorização
- Organização dos commits por responsabilidade
- Padronização de respostas da API
- Suporte completo à execução via **Docker**
- Atualização da documentação para facilitar avaliação

---

## Observações técnicas

- O projeto foi mantido no formato original em camadas, evitando refatorações estruturais desnecessárias.
- O foco foi preservar a proposta do desafio e evoluir a solução com melhorias de qualidade.
- A aplicação foi validada com:
  - `dotnet build`
  - `dotnet test`
  - execução local
  - execução via Docker
  - acesso ao Swagger no container

---

## Autor

Desenvolvido por **Yuri D S Santos** como parte de desafio técnico.
