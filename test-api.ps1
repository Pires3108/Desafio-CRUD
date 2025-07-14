# Script de teste para a API de Funcionários e Equipes

Write-Host "🧪 Testando API de Funcionários e Equipes" -ForegroundColor Green
Write-Host "==========================================" -ForegroundColor Green

# Configurações
$baseUrl = "https://localhost:7001"
$headers = @{
    "Content-Type" = "application/json"
}

# Função para fazer requisições HTTP
function Invoke-APIRequest {
    param(
        [string]$Method,
        [string]$Url,
        [string]$Body = ""
    )
    
    try {
        if ($Body -ne "") {
            $response = Invoke-WebRequest -Uri $Url -Method $Method -Headers $headers -Body $Body -UseBasicParsing
        } else {
            $response = Invoke-WebRequest -Uri $Url -Method $Method -Headers $headers -UseBasicParsing
        }
        
        Write-Host "✅ $Method $Url - Status: $($response.StatusCode)" -ForegroundColor Green
        if ($response.Content) {
            Write-Host "Response: $($response.Content)" -ForegroundColor Gray
        }
        return $response
    }
    catch {
        Write-Host "❌ $Method $Url - Erro: $($_.Exception.Message)" -ForegroundColor Red
        return $null
    }
}

# Teste 1: Listar equipes (deve estar vazio)
Write-Host "`n📋 Teste 1: Listando equipes (deve estar vazio)" -ForegroundColor Yellow
Invoke-APIRequest -Method "GET" -Url "$baseUrl/api/equipes"

# Teste 2: Criar equipe "Land Tech"
Write-Host "`n📋 Teste 2: Criando equipe 'Land Tech'" -ForegroundColor Yellow
$landTechEquipe = '{"nome": "Land Tech"}'
Invoke-APIRequest -Method "POST" -Url "$baseUrl/api/equipes" -Body $landTechEquipe

# Teste 3: Criar equipe "Desenvolvimento"
Write-Host "`n📋 Teste 3: Criando equipe 'Desenvolvimento'" -ForegroundColor Yellow
$devEquipe = '{"nome": "Desenvolvimento"}'
Invoke-APIRequest -Method "POST" -Url "$baseUrl/api/equipes" -Body $devEquipe

# Teste 4: Listar equipes novamente
Write-Host "`n📋 Teste 4: Listando equipes (deve ter 2 equipes)" -ForegroundColor Yellow
Invoke-APIRequest -Method "GET" -Url "$baseUrl/api/equipes"

# Teste 5: Criar funcionário na equipe "Land Tech"
Write-Host "`n📋 Teste 5: Criando funcionário na equipe 'Land Tech'" -ForegroundColor Yellow
$funcionarioLandTech = '{
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
}'
Invoke-APIRequest -Method "POST" -Url "$baseUrl/api/clientes" -Body $funcionarioLandTech

# Teste 6: Criar funcionário em equipe existente
Write-Host "`n📋 Teste 6: Criando funcionário em equipe existente" -ForegroundColor Yellow
$funcionarioDev = '{
    "nome": "Maria Santos",
    "email": "maria@dev.com",
    "telefone": "(11) 88888-8888",
    "endereco": "Av. Paulista, 1000",
    "cidade": "São Paulo",
    "estado": "SP",
    "cep": "01310-100",
    "usuario": "maria.santos",
    "senha": "123456",
    "equipeId": 2
}'
Invoke-APIRequest -Method "POST" -Url "$baseUrl/api/clientes" -Body $funcionarioDev

# Teste 7: Listar funcionários
Write-Host "`n📋 Teste 7: Listando funcionários" -ForegroundColor Yellow
Invoke-APIRequest -Method "GET" -Url "$baseUrl/api/clientes"

# Teste 8: Login do funcionário Land Tech
Write-Host "`n📋 Teste 8: Login do funcionário Land Tech" -ForegroundColor Yellow
$loginData = '{"usuario": "joao.silva", "senha": "123456"}'
Invoke-APIRequest -Method "POST" -Url "$baseUrl/api/clientes/login" -Body $loginData

# Teste 9: Testar permissões do funcionário Land Tech
Write-Host "`n📋 Teste 9: Testando permissões do funcionário Land Tech" -ForegroundColor Yellow
Invoke-APIRequest -Method "GET" -Url "$baseUrl/api/clientes/com-permissoes/1"

Write-Host "`n🎉 Testes concluídos!" -ForegroundColor Green 