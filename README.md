# Sistema de Gestão de Funcionários

Sistema completo para gerenciamento de funcionários e equipes com sistema de permissões avançado, desenvolvido em C# com ASP.NET Core Web API.

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas (Clean Architecture):

- **ClienteCRUD.Core**: Entidades, interfaces e regras de negócio
- **ClienteCRUD.Infrastructure**: Implementação do repositório e contexto do Entity Framework
- **ClienteCRUD.Application**: Serviços, DTOs e mapeamentos
- **ClienteCRUD.API**: Controllers e configuração da API

## 🚀 Funcionalidades

### ✅ CRUD Completo de Funcionários
- **Criar**: Cadastrar novos funcionários com vínculo obrigatório a equipe
- **Ler**: Listar funcionários com sistema de permissões
- **Atualizar**: Modificar dados dos funcionários
- **Excluir**: Remoção lógica (soft delete)

### ✅ CRUD Completo de Equipes
- **Criar**: Cadastrar novas equipes
- **Ler**: Listar todas as equipes
- **Atualizar**: Modificar dados das equipes
- **Excluir**: Remover equipes

### 🔐 Sistema de Permissões
- **Land Tech Admin**: Acesso total a todos os funcionários de todas as equipes
- **Equipe Admin**: Acesso aos funcionários da sua equipe
- **Funcionário Normal**: Acesso apenas aos seus próprios dados

### 🏢 Funcionalidade Land Tech
- Detecção automática da equipe "Land Tech" (qualquer formatação)
- Funcionários da Land Tech têm privilégios especiais
- Acesso total ao sistema

### 🔐 Segurança
- Criptografia de senhas com BCrypt
- Sistema de login com validação
- Validações de dados com Data Annotations
- Verificação de emails e usuários únicos

### 📊 Dados do Funcionário
- Nome completo
- Email (único)
- Telefone
- Endereço completo (rua, cidade, estado, CEP)
- Usuário (único)
- Senha (criptografada)
- Vínculo com equipe
- Permissões de admin
- Datas de cadastro e atualização
- Status ativo/inativo

## 🛠️ Tecnologias Utilizadas

- **.NET 8.0**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQLite**
- **AutoMapper**
- **BCrypt.Net-Next**
- **Swagger/OpenAPI**

## 📋 Pré-requisitos

- Visual Studio 2022 ou VS Code
- .NET 8.0 SDK

## 🔧 Configuração

1. **Clone o repositório**
   ```bash
   git clone [url-do-repositorio]
   cd ClienteCRUD
   ```

2. **Restaurar pacotes NuGet**
   ```bash
   dotnet restore
   ```

3. **Configurar banco de dados**
   - O sistema usa **SQLite** por padrão
   - O arquivo do banco será criado automaticamente na primeira execução

4. **Executar a aplicação**
   ```bash
   cd ClienteCRUD.API
   dotnet run
   ```

5. **Acessar a API**
   - URL: `https://localhost:7001`
   - Swagger UI: `https://localhost:7001/swagger`

## 📚 Endpoints da API

### Funcionários

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/clientes` | Listar todos os funcionários |
| GET | `/api/clientes/com-permissoes/{funcionarioId}` | Listar com permissões |
| GET | `/api/clientes/{id}` | Buscar funcionário por ID |
| POST | `/api/clientes` | Criar novo funcionário |
| PUT | `/api/clientes/{id}` | Atualizar funcionário |
| DELETE | `/api/clientes/{id}` | Excluir funcionário |
| POST | `/api/clientes/{id}/reativar` | Reativar funcionário inativo |
| POST | `/api/clientes/login` | Login de funcionário |

### Equipes

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/equipes` | Listar todas as equipes |
| GET | `/api/equipes/{id}` | Buscar equipe por ID |
| POST | `/api/equipes` | Criar nova equipe |
| PUT | `/api/equipes/{id}` | Atualizar equipe |
| DELETE | `/api/equipes/{id}` | Excluir equipe |

## 📝 Exemplos de Uso

### Criar Funcionário com Nova Equipe
```json
POST /api/clientes
{
  "nome": "João Silva",
  "email": "joao@landtech.com",
  "telefone": "(11) 99999-9999",
  "endereco": "Rua das Flores, 123",
  "cidade": "São Paulo",
  "estado": "SP",
  "cep": "01234-567",
  "usuario": "joao.silva",
  "senha": "123456",
  "novaEquipe": "Land Tech"
}
```

### Criar Funcionário em Equipe Existente
```json
POST /api/clientes
{
  "nome": "Maria Santos",
  "email": "maria@exemplo.com",
  "telefone": "(11) 88888-8888",
  "endereco": "Av. Paulista, 1000",
  "cidade": "São Paulo",
  "estado": "SP",
  "cep": "01310-100",
  "usuario": "maria.santos",
  "senha": "123456",
  "equipeId": 1
}
```

### Login
```json
POST /api/clientes/login
{
  "usuario": "joao.silva",
  "senha": "123456"
}
```

### Criar Equipe
```json
POST /api/equipes
{
  "nome": "Desenvolvimento"
}
```

### Listar Funcionários com Permissões
```json
GET /api/clientes/com-permissoes/1
```

## 🔒 Sistema de Permissões

### Land Tech Admin
- Funcionários da equipe "Land Tech" têm acesso total
- Podem ver todos os funcionários de todas as equipes
- Podem gerenciar todas as equipes
- Campo `IsLandTechAdmin = true`

### Equipe Admin
- Funcionários que criaram a equipe são admins
- Podem ver todos os funcionários da sua equipe
- Campo `IsEquipeAdmin = true`

### Funcionário Normal
- Veem apenas seus próprios dados
- Acesso limitado às funcionalidades básicas

## 🗄️ Banco de Dados

### Tabela Funcionarios (antiga Clientes)
```sql
CREATE TABLE Funcionarios (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL,
    Email TEXT NOT NULL UNIQUE,
    Telefone TEXT,
    Endereco TEXT,
    Cidade TEXT,
    Estado TEXT,
    Cep TEXT,
    Usuario TEXT NOT NULL UNIQUE,
    Senha TEXT NOT NULL,
    DataCadastro TEXT DEFAULT (CURRENT_TIMESTAMP),
    DataAtualizacao TEXT,
    Ativo INTEGER DEFAULT 1,
    EquipeId INTEGER,
    IsEquipeAdmin INTEGER DEFAULT 0,
    IsLandTechAdmin INTEGER DEFAULT 0,
    CriadoPor TEXT,
    AtualizadoPor TEXT,
    FOREIGN KEY (EquipeId) REFERENCES Equipes(Id)
)
```

### Tabela Equipes
```sql
CREATE TABLE Equipes (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL UNIQUE,
    DataCadastro TEXT DEFAULT (CURRENT_TIMESTAMP)
)
```

## 🧪 Testes

Para testar a API, você pode usar:

1. **Swagger UI**: Acesse `/swagger` na aplicação
2. **Postman**: Importe a coleção de testes
3. **cURL**: Use os comandos curl fornecidos

## 📦 Estrutura do Projeto

```
ClienteCRUD/
├── ClienteCRUD.sln
├── ClienteCRUD.Core/
│   ├── Entities/
│   │   ├── Cliente.cs (Funcionário)
│   │   └── Equipe.cs
│   └── Interfaces/
│       ├── IClienteRepository.cs
│       ├── IClienteService.cs
│       ├── IEquipeRepository.cs
│       └── IEquipeService.cs
├── ClienteCRUD.Infrastructure/
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   └── Repositories/
│       ├── ClienteRepository.cs
│       └── EquipeRepository.cs
├── ClienteCRUD.Application/
│   ├── DTOs/
│   │   └── ClienteDTO.cs
│   ├── Services/
│   │   ├── ClienteService.cs
│   │   └── EquipeService.cs
│   └── Mapping/
│       └── AutoMapperProfile.cs
└── ClienteCRUD.API/
    ├── Controllers/
    │   ├── ClientesController.cs
    │   └── EquipesController.cs
    ├── Program.cs
    └── appsettings.json
```

## 🤝 Contribuição

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 📞 Suporte

Para dúvidas ou suporte, entre em contato através dos canais disponíveis no repositório.

---

**Status:** Backend 100% funcional ✅ | Frontend em desenvolvimento 🔄 