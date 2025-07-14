# Sistema de Gestão de Funcionários e Equipes (Monorepo)

Sistema completo para gerenciamento de funcionários e equipes com sistema de permissões avançado, desenvolvido em C# com ASP.NET Core Web API (backend) e React (frontend).

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas (Clean Architecture) e está organizado como monorepo:

- **ClienteCRUD.Core**: Entidades, interfaces e regras de negócio
- **ClienteCRUD.Infrastructure**: Implementação do repositório e contexto do Entity Framework
- **ClienteCRUD.Application**: Serviços, DTOs e mapeamentos
- **ClienteCRUD.API**: Controllers e configuração da API
- **frontend**: Aplicação React para interface do usuário

---

## 🌐 Frontend (React)

O frontend está localizado na pasta `frontend/` e oferece uma interface moderna e responsiva para o sistema.

### Principais Funcionalidades
- Login e cadastro com painel moderno
- Listagem de equipes e funcionários com permissões (Land Tech Admin, Equipe Admin, Funcionário)
- Edição inline dos dados do funcionário
- Consumo de endpoints protegidos por permissão
- Visual inspirado no site da Land Tech

### Tecnologias Utilizadas
- React
- Axios
- CSS customizado

### Instalação e Execução do Frontend

1. Acesse a pasta do frontend:
   ```bash
   cd frontend
   ```
2. Instale as dependências:
   ```bash
   npm install
   ```
3. Inicie o frontend:
   ```bash
   npm start
   ```
   O app estará disponível em `http://localhost:3000`.

4. Certifique-se de que o backend C# (.NET) está rodando em `http://localhost:5000` (ou ajuste o endereço no frontend se necessário).

### Permissões e Endpoints Consumidos
- **Listagem de funcionários:**
  - `GET /api/clientes/com-permissoes/{funcionarioId}`
- **Atualização de funcionário:**
  - `PUT /api/clientes/{id}/com-permissoes?funcionarioLogadoId={funcionarioId}`
- **Exclusão de funcionário:**
  - `DELETE /api/clientes/{id}/com-permissoes?funcionarioLogadoId={funcionarioId}`
- O frontend sempre passa o ID do funcionário logado para garantir as regras de permissão.

### Visual
- Painéis centrais grandes e modernos para login e cadastro
- Logo Land Tech destacada
- Cards de equipe e funcionários com edição inline
- Responsivo e com visual inspirado no site da Land Tech

---

## 🚀 Funcionalidades Backend

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

## 🛠️ Tecnologias Utilizadas (Backend)

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
- Node.js e npm (para o frontend)

## 🔧 Configuração Backend

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
├── ClienteCRUD.API/
│   ├── Controllers/
│   │   ├── ClientesController.cs
│   │   └── EquipesController.cs
│   ├── Program.cs
│   └── appsettings.json
└── frontend/
    ├── public/
    ├── src/
    ├── package.json
    └── ...
```

---

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

**Status:** Backend 100% funcional ✅ | Frontend presente e integrado ✅ 