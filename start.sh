#!/bin/bash

echo "========================================"
echo "    CLIENTE CRUD - INICIALIZACAO"
echo "========================================"
echo

echo "[1/4] Verificando pre-requisitos..."

# Verificar .NET
if ! command -v dotnet &> /dev/null; then
    echo "ERRO: .NET 8.0 SDK nao encontrado!"
    echo "Instale o .NET 8.0 SDK em: https://dotnet.microsoft.com/download"
    read -p "Pressione Enter para sair"
    exit 1
else
    echo "✓ .NET encontrado: $(dotnet --version)"
fi

# Verificar Node.js
if ! command -v node &> /dev/null; then
    echo "ERRO: Node.js nao encontrado!"
    echo "Instale o Node.js em: https://nodejs.org/"
    read -p "Pressione Enter para sair"
    exit 1
else
    echo "✓ Node.js encontrado: $(node --version)"
fi

echo

echo "[2/4] Restaurando dependencias do backend..."
cd ClienteCRUD.API
if ! dotnet restore; then
    echo "ERRO: Falha ao restaurar dependencias do backend"
    read -p "Pressione Enter para sair"
    exit 1
fi
cd ..
echo "✓ Dependencias do backend restauradas"
echo

echo "[3/4] Instalando dependencias do frontend..."
cd frontend
if ! npm install; then
    echo "ERRO: Falha ao instalar dependencias do frontend"
    read -p "Pressione Enter para sair"
    exit 1
fi
cd ..
echo "✓ Dependencias do frontend instaladas"
echo

echo "[4/4] Iniciando aplicacao..."
echo
echo "========================================"
echo "    INICIANDO BACKEND E FRONTEND"
echo "========================================"
echo
echo "Backend: http://localhost:7001"
echo "Frontend: http://localhost:3000"
echo "Swagger: http://localhost:7001/swagger"
echo
echo "Pressione Ctrl+C para parar ambos os servicos"
echo

# Função para limpar processos ao sair
cleanup() {
    echo
    echo "Encerrando servicos..."
    pkill -f "dotnet run"
    pkill -f "npm start"
    exit 0
}

# Capturar Ctrl+C
trap cleanup SIGINT

# Iniciar backend em background
echo "Iniciando backend..."
cd ClienteCRUD.API
dotnet run &
BACKEND_PID=$!
cd ..

# Aguardar um pouco para o backend inicializar
sleep 3

# Iniciar frontend em background
echo "Iniciando frontend..."
cd frontend
npm start &
FRONTEND_PID=$!
cd ..

echo
echo "✓ Aplicacao iniciada com sucesso!"
echo
echo "Aguarde alguns segundos para os servicos carregarem..."
echo
echo "Pressione Ctrl+C para parar os servicos"
echo

# Aguardar indefinidamente
wait 