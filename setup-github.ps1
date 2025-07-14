# Script para configurar repositório GitHub para ClienteCRUD
# Execute este script no diretório raiz do projeto

Write-Host "=== Configuração do Repositório GitHub ===" -ForegroundColor Green

# Verificar se o Git está instalado
if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    Write-Host "Git não está instalado. Por favor, instale o Git primeiro." -ForegroundColor Red
    exit 1
}

# Verificar se o GitHub CLI está instalado
if (-not (Get-Command gh -ErrorAction SilentlyContinue)) {
    Write-Host "GitHub CLI não está instalado. Instalando..." -ForegroundColor Yellow
    winget install GitHub.cli
    Write-Host "Por favor, reinicie o PowerShell e execute o script novamente." -ForegroundColor Yellow
    exit 1
}

# Inicializar repositório Git
Write-Host "Inicializando repositório Git..." -ForegroundColor Yellow
git init

# Adicionar todos os arquivos
Write-Host "Adicionando arquivos ao repositório..." -ForegroundColor Yellow
git add .

# Fazer commit inicial
Write-Host "Fazendo commit inicial..." -ForegroundColor Yellow
git commit -m "Initial commit: ClienteCRUD backend"

# Criar repositório no GitHub
Write-Host "Criando repositório no GitHub..." -ForegroundColor Yellow
$repoName = Read-Host "Digite o nome do repositório (ex: cliente-crud)"

# Criar repositório público no GitHub
gh repo create $repoName --public --source=. --remote=origin --push

Write-Host "Repositório criado com sucesso!" -ForegroundColor Green
Write-Host "URL do repositório: https://github.com/$(gh api user --jq .login)/$repoName" -ForegroundColor Cyan

# Configurar frontend como submodule
Write-Host "Configurando frontend como submodule..." -ForegroundColor Yellow
$frontendPath = "C:\Users\USER\cliente-crud-frontend"

if (Test-Path $frontendPath) {
    git submodule add $frontendPath frontend
    git commit -m "Add frontend as submodule"
    git push origin main
    Write-Host "Frontend configurado como submodule!" -ForegroundColor Green
} else {
    Write-Host "Caminho do frontend não encontrado: $frontendPath" -ForegroundColor Red
    Write-Host "Configure o frontend manualmente após criar o repositório." -ForegroundColor Yellow
}

Write-Host "=== Configuração concluída! ===" -ForegroundColor Green
Write-Host "Próximos passos:" -ForegroundColor Cyan
Write-Host "1. Acesse o repositório no GitHub" -ForegroundColor White
Write-Host "2. Configure as secrets necessárias" -ForegroundColor White
Write-Host "3. Configure CI/CD se necessário" -ForegroundColor White 