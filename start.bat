@echo off
echo ========================================
echo    CLIENTE CRUD - INICIALIZACAO
echo ========================================
echo.

echo [1/4] Verificando pre-requisitos...
where dotnet >nul 2>&1
if %errorlevel% neq 0 (
    echo ERRO: .NET 8.0 SDK nao encontrado!
    echo Instale o .NET 8.0 SDK em: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

where node >nul 2>&1
if %errorlevel% neq 0 (
    echo ERRO: Node.js nao encontrado!
    echo Instale o Node.js em: https://nodejs.org/
    pause
    exit /b 1
)

echo ✓ .NET 8.0 SDK encontrado
echo ✓ Node.js encontrado
echo.

echo [2/4] Restaurando dependencias do backend...
cd ClienteCRUD.API
dotnet restore
if %errorlevel% neq 0 (
    echo ERRO: Falha ao restaurar dependencias do backend
    pause
    exit /b 1
)
cd ..
echo ✓ Dependencias do backend restauradas
echo.

echo [3/4] Instalando dependencias do frontend...
cd frontend
npm install
if %errorlevel% neq 0 (
    echo ERRO: Falha ao instalar dependencias do frontend
    pause
    exit /b 1
)
cd ..
echo ✓ Dependencias do frontend instaladas
echo.

echo [4/4] Iniciando aplicacao...
echo.
echo ========================================
echo    INICIANDO BACKEND E FRONTEND
echo ========================================
echo.
echo Backend: http://localhost:7001
echo Frontend: http://localhost:3000
echo Swagger: http://localhost:7001/swagger
echo.
echo Pressione Ctrl+C para parar ambos os servicos
echo.

REM Iniciar backend em background
start "Backend - ClienteCRUD" cmd /k "cd ClienteCRUD.API && dotnet run"

REM Aguardar um pouco para o backend inicializar
timeout /t 3 /nobreak >nul

REM Iniciar frontend em background
start "Frontend - ClienteCRUD" cmd /k "cd frontend && npm start"

echo.
echo ✓ Aplicacao iniciada com sucesso!
echo.
echo Aguarde alguns segundos para os servicos carregarem...
echo.
pause 