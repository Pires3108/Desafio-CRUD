# 🚀 Configuração do Repositório GitHub

Este guia irá ajudá-lo a configurar o repositório GitHub para o projeto ClienteCRUD.

## 📋 Pré-requisitos

1. **Git instalado** no seu computador
2. **GitHub CLI** instalado (opcional, mas recomendado)
3. **Conta no GitHub**

## 🔧 Passo a Passo

### 1. Instalar GitHub CLI (Recomendado)

```powershell
# Instalar via winget
winget install GitHub.cli

# Ou baixar manualmente em: https://cli.github.com/
```

### 2. Autenticar no GitHub

```powershell
gh auth login
```

### 3. Executar o Script Automatizado

```powershell
# No diretório raiz do projeto (C:\ClienteCRUD)
.\setup-github.ps1
```

### 4. Configuração Manual (Alternativa)

Se preferir fazer manualmente:

```powershell
# 1. Inicializar Git
git init

# 2. Adicionar arquivos
git add .

# 3. Commit inicial
git commit -m "Initial commit: ClienteCRUD backend"

# 4. Criar repositório no GitHub
gh repo create cliente-crud --public --source=. --remote=origin --push
```

## 📁 Estrutura do Repositório

O repositório será organizado da seguinte forma:

```
cliente-crud/
├── backend/           # Código C# (.NET)
│   ├── ClienteCRUD.API/
│   ├── ClienteCRUD.Core/
│   ├── ClienteCRUD.Application/
│   ├── ClienteCRUD.Infrastructure/
│   └── ...
├── frontend/          # Frontend (como submodule)
│   └── [código do frontend]
├── docs/             # Documentação
├── scripts/          # Scripts de automação
└── README.md
```

## 🔗 Configurando o Frontend

### Opção 1: Como Submodule (Recomendado)

```powershell
# Adicionar frontend como submodule
git submodule add C:\Users\USER\cliente-crud-frontend frontend
git commit -m "Add frontend as submodule"
git push origin main
```

### Opção 2: Repositório Separado

1. Crie um repositório separado para o frontend
2. Configure as URLs no README.md
3. Use GitHub Actions para deploy

## 📝 Arquivos Importantes

- `.gitignore`: Configurado para projetos .NET
- `README.md`: Documentação completa do projeto
- `setup-github.ps1`: Script de automação
- `launchSettings.json`: Configurações de execução

## 🔐 Configurações de Segurança

### Secrets do GitHub (Recomendado)

Configure as seguintes secrets no repositório:

1. **ConnectionStrings**: String de conexão do banco
2. **JWT_SECRET**: Chave para JWT tokens
3. **API_KEYS**: Chaves de APIs externas

### Como configurar secrets:

1. Vá para Settings > Secrets and variables > Actions
2. Clique em "New repository secret"
3. Adicione as variáveis necessárias

## 🚀 Deploy e CI/CD

### GitHub Actions (Opcional)

Crie um arquivo `.github/workflows/deploy.yml`:

```yaml
name: Deploy ClienteCRUD

on:
  push:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 8.0.x
        
    - name: Restore dependencies
      run: dotnet restore
      
    - name: Build
      run: dotnet build --no-restore
      
    - name: Test
      run: dotnet test --no-build --verbosity normal
```

## 📊 Monitoramento

### GitHub Insights

- **Traffic**: Visualize o tráfego do repositório
- **Contributors**: Veja quem contribuiu
- **Commits**: Histórico de commits
- **Releases**: Versões do projeto

## 🔄 Comandos Úteis

```powershell
# Atualizar submodules
git submodule update --remote

# Clonar com submodules
git clone --recursive [url-do-repositorio]

# Fazer push de alterações
git add .
git commit -m "Descrição das alterações"
git push origin main

# Verificar status
git status
git log --oneline
```

## 📞 Suporte

Se encontrar problemas:

1. Verifique se o Git está instalado: `git --version`
2. Verifique se o GitHub CLI está instalado: `gh --version`
3. Verifique se está autenticado: `gh auth status`
4. Consulte a documentação do GitHub CLI: `gh help`

## 🎯 Próximos Passos

Após configurar o repositório:

1. ✅ Configurar CI/CD (GitHub Actions)
2. ✅ Configurar secrets do repositório
3. ✅ Configurar branch protection rules
4. ✅ Configurar deploy automático
5. ✅ Configurar monitoramento
6. ✅ Configurar documentação automática

---

**🎉 Parabéns! Seu repositório está configurado e pronto para uso!** 