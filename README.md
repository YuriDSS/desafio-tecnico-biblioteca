# Desafio Técnico — Biblioteca API

API para gerenciamento de biblioteca, com cadastro de usuários, livros e empréstimos, incluindo controle de devoluções, cálculo de multas e regras de negócio.

## Tecnologias utilizadas

* .NET 8
* ASP.NET Core Web API
* Dapper
* SQLite
* Swagger / OpenAPI
* xUnit

## Arquitetura do projeto

O projeto segue uma abordagem **monolítica em camadas**, mantendo a estrutura original da solução:

* **Application**: Controllers e modelos de resposta da API
* **Domain**: Entidades e regras de negócio
* **Infrastructure**: Acesso a dados, configuração e repositórios
* **UseCases**: Casos de uso da aplicação

## Como executar o projeto

### Pré-requisitos

- .NET SDK 8.0 ou superior
- Visual Studio 2022 / VS Code / Rider (opcional)

### Passos

1. Clone o repositório:

```bash
git clone https://github.com/YuriDSS/desafio-tecnico-biblioteca.git
cd desafio-tecnico-biblioteca
```

2. Restaure as dependências:

```bash
dotnet restore
```

3. Execute o projeto:

```bash
dotnet run --project BibliotecaApi.csproj
```

4. Acesse o Swagger:

* `https://localhost:<porta>/swagger`
* ou `http://localhost:<porta>/swagger`

> A porta pode variar conforme a configuração local.

## Banco de dados

O projeto utiliza **SQLite** com o arquivo `Biblioteca.db` incluído na solução.

A connection string está configurada no arquivo `appsettings.json`.

## Endpoints disponíveis

### Usuário

* `POST /Usuario/Cadastrar` — cadastra um novo usuário

### Livro

* `POST /Livro/Cadastrar` — cadastra um novo livro
* `GET /Livro/Listar` — lista todos os livros *(implementado na solução final)*

### Empréstimo

* `POST /Emprestimo/Cadastrar` — realiza um novo empréstimo
* `POST /Emprestimo/Devolver` — registra a devolução de um empréstimo

## Autenticação

Os endpoints sensíveis podem ser protegidos por **token de API** via header `Authorization`.

Exemplo:

```http
Authorization: Bearer SEU_TOKEN_AQUI
```

> O token utilizado deve ser configurado no arquivo `appsettings.json`.

## Desafios implementados

> Esta seção será atualizada conforme a implementação das etapas.

* [ ] 1. Validar CPF do usuário
* [ ] 2. Validar ISBN do livro
* [ ] 3. Corrigir cálculo de multa no prazo
* [ ] 4. Impedir empréstimo de livro já emprestado
* [ ] 5. Criar endpoint `GET /Livro/Listar`
* [ ] 6. Impedir novo empréstimo para usuário com atraso
* [ ] 7. Ajustar nova regra de multa
* [ ] 8. Implementar autenticação

## Melhorias extras

> Esta seção será atualizada conforme as melhorias adicionais forem concluídas.

* [ ] Aplicação de injeção de dependência real
* [ ] Correção do fluxo de devolução para evitar duplicidade de empréstimo
* [ ] Correção de mapeamentos de repositório
* [ ] Padronização de mensagens e ajustes de Swagger
* [ ] Adição de testes unitários

## Testes

> Os testes automatizados serão adicionados em um projeto separado de testes (`BibliotecaApi.Tests`).

Para executar os testes (após implementação):

```bash
dotnet test
```

## Observações técnicas

Durante a implementação, a proposta foi **manter a arquitetura original do projeto**, aplicando melhorias pontuais para:

* corrigir bugs
* reforçar regras de negócio
* melhorar a organização interna
* facilitar manutenção e testes

Sem alterar desnecessariamente a estrutura da solução.

## Próximos passos

* Concluir todos os desafios propostos
* Finalizar testes unitários
* Atualizar esta documentação com o checklist final
* Revisar a solução completa antes da entrega
