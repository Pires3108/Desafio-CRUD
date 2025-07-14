# 🚀 Início Rápido - ClienteCRUD

Este guia irá ajudá-lo a executar o projeto rapidamente após clonar do GitHub.

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- **.NET 8.0 SDK**: [Download aqui](https://dotnet.microsoft.com/download)
- **Node.js**: [Download aqui](https://nodejs.org/)

## ⚡ Execução Rápida

### Opção 1: Script Automático (Recomendado)

**Windows (PowerShell/CMD):**
```bash
# Script batch
start.bat

# Ou script PowerShell
powershell -ExecutionPolicy Bypass -File start.ps1
```

**Git Bash / Linux / Mac:**
```bash
# Tornar o script executável (apenas na primeira vez)
chmod +x start.sh

# Executar o script
./start.sh
```

**WSL (Windows Subsystem for Linux):**
```bash
# Executar o script bash
bash start.sh
```

### Opção 2: Execução Manual

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/Pires3108/Desafio-CRUD.git
   cd Desafio-CRUD
   ```

2. **Configure o backend:**
   ```bash
   cd ClienteCRUD.API
   dotnet restore
   dotnet run
   ```

3. **Em outro terminal, configure o frontend:**
   ```bash
   cd frontend
   npm install
   npm start
   ```

## 🌐 URLs de Acesso

Após a execução, acesse:

- **Frontend**: http://localhost:3000
- **Backend API**: http://localhost:7001
- **Swagger UI**: http://localhost:7001/swagger

## 🔧 Solução de Problemas

### Erro: .NET não encontrado
```bash
# Verifique a instalação
dotnet --version

# Se não estiver instalado, baixe em:
# https://dotnet.microsoft.com/download
```

### Erro: Node.js não encontrado
```bash
# Verifique a instalação
node --version
npm --version

# Se não estiver instalado, baixe em:
# https://nodejs.org/
```

### Erro: Porta já em uso
```bash
# Verifique processos nas portas
netstat -ano | findstr :7001
netstat -ano | findstr :3000

# Encerre os processos se necessário
taskkill /PID [PID_NUMBER] /F
```

### Erro: Dependências não instaladas
```bash
# Backend
cd ClienteCRUD.API
dotnet restore
dotnet build

# Frontend
cd frontend
npm install
```

### Erro: Permissão negada no Git Bash
```bash
# Torne o script executável
chmod +x start.sh

# Execute novamente
./start.sh
```

## 📝 Primeiro Acesso

1. Acesse http://localhost:3000
2. Crie uma conta de funcionário
3. Faça login no sistema
4. Explore as funcionalidades!

## 🆘 Precisa de Ajuda?

- Verifique os logs no terminal
- Consulte o README.md principal
- Abra uma issue no GitHub

---

**🎉 Pronto! Seu sistema está rodando!** 