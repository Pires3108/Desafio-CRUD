# ClienteCRUD - Script de Inicialização
# Execute este script após clonar o repositório

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "    CLIENTE CRUD - INICIALIZACAO" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Verificar pré-requisitos
Write-Host "[1/4] Verificando pre-requisitos..." -ForegroundColor Yellow

# Verificar .NET
try {
    $dotnetVersion = dotnet --version
    Write-Host "✓ .NET encontrado: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "ERRO: .NET 8.0 SDK nao encontrado!" -ForegroundColor Red
    Write-Host "Instale o .NET 8.0 SDK em: https://dotnet.microsoft.com/download" -ForegroundColor Yellow
    Read-Host "Pressione Enter para sair"
    exit 1
}

# Verificar Node.js
try {
    $nodeVersion = node --version
    Write-Host "✓ Node.js encontrado: $nodeVersion" -ForegroundColor Green
} catch {
    Write-Host "ERRO: Node.js nao encontrado!" -ForegroundColor Red
    Write-Host "Instale o Node.js em: https://nodejs.org/" -ForegroundColor Yellow
    Read-Host "Pressione Enter para sair"
    exit 1
}

Write-Host ""

# Restaurar dependências do backend
Write-Host "[2/4] Restaurando dependencias do backend..." -ForegroundColor Yellow
Set-Location "ClienteCRUD.API"
try {
    dotnet restore
    Write-Host "✓ Dependencias do backend restauradas" -ForegroundColor Green
} catch {
    Write-Host "ERRO: Falha ao restaurar dependencias do backend" -ForegroundColor Red
    Read-Host "Pressione Enter para sair"
    exit 1
}
Set-Location ".."
Write-Host ""

# Instalar dependências do frontend
Write-Host "[3/4] Instalando dependencias do frontend..." -ForegroundColor Yellow
Set-Location "frontend"
try {
    npm install
    Write-Host "✓ Dependencias do frontend instaladas" -ForegroundColor Green
} catch {
    Write-Host "ERRO: Falha ao instalar dependencias do frontend" -ForegroundColor Red
    Read-Host "Pressione Enter para sair"
    exit 1
}
Set-Location ".."
Write-Host ""

# Iniciar aplicação
Write-Host "[4/4] Iniciando aplicacao..." -ForegroundColor Yellow
Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "    INICIANDO BACKEND E FRONTEND" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Backend: http://localhost:7001" -ForegroundColor White
Write-Host "Frontend: http://localhost:3000" -ForegroundColor White
Write-Host "Swagger: http://localhost:7001/swagger" -ForegroundColor White
Write-Host ""
Write-Host "Pressione Ctrl+C para parar ambos os servicos" -ForegroundColor Yellow
Write-Host ""

# Iniciar backend em background
Start-Process -FilePath "cmd" -ArgumentList "/k", "cd ClienteCRUD.API && dotnet run" -WindowStyle Normal

# Aguardar um pouco para o backend inicializar
Start-Sleep -Seconds 3

# Iniciar frontend em background
Start-Process -FilePath "cmd" -ArgumentList "/k", "cd frontend && npm start" -WindowStyle Normal

Write-Host ""
Write-Host "✓ Aplicacao iniciada com sucesso!" -ForegroundColor Green
Write-Host ""
Write-Host "Aguarde alguns segundos para os servicos carregarem..." -ForegroundColor Yellow
Write-Host ""
Read-Host "Pressione Enter para sair" 