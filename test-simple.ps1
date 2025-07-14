# Script de teste simples para a API

Write-Host "Testando API de Funcionarios e Equipes" -ForegroundColor Green
Write-Host "=====================================" -ForegroundColor Green

$baseUrl = "https://localhost:7001"

# Teste 1: Listar equipes
Write-Host "Teste 1: Listando equipes" -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "$baseUrl/api/equipes" -UseBasicParsing
    Write-Host "SUCCESS: $($response.StatusCode)" -ForegroundColor Green
    Write-Host "Response: $($response.Content)" -ForegroundColor Gray
}
catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}

# Teste 2: Criar equipe Land Tech
Write-Host "Teste 2: Criando equipe Land Tech" -ForegroundColor Yellow
try {
    $body = '{"nome": "Land Tech"}'
    $response = Invoke-WebRequest -Uri "$baseUrl/api/equipes" -Method POST -Body $body -ContentType "application/json" -UseBasicParsing
    Write-Host "SUCCESS: $($response.StatusCode)" -ForegroundColor Green
    Write-Host "Response: $($response.Content)" -ForegroundColor Gray
}
catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}

# Teste 3: Criar funcionario
Write-Host "Teste 3: Criando funcionario" -ForegroundColor Yellow
try {
    $body = '{
        "nome": "Joao Silva",
        "email": "joao@landtech.com",
        "telefone": "(11) 99999-9999",
        "endereco": "Rua das Flores, 123",
        "cidade": "Sao Paulo",
        "estado": "SP",
        "cep": "01234-567",
        "usuario": "joao.silva",
        "senha": "123456",
        "novaEquipe": "Land Tech"
    }'
    $response = Invoke-WebRequest -Uri "$baseUrl/api/clientes" -Method POST -Body $body -ContentType "application/json" -UseBasicParsing
    Write-Host "SUCCESS: $($response.StatusCode)" -ForegroundColor Green
    Write-Host "Response: $($response.Content)" -ForegroundColor Gray
}
catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "Testes concluidos!" -ForegroundColor Green 