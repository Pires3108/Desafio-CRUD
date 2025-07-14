@echo off
echo ========================================
echo    Sistema CRUD de Clientes
echo ========================================
echo.

echo Restaurando pacotes NuGet...
dotnet restore

echo.
echo Compilando projeto...
dotnet build

echo.
echo Iniciando a aplicacao...
echo.
echo A API estara disponivel em:
echo - HTTPS: https://localhost:7001
echo - HTTP:  http://localhost:5001
echo.
echo Swagger UI: https://localhost:7001/swagger
echo.
echo Pressione Ctrl+C para parar a aplicacao
echo.

cd ClienteCRUD.API
dotnet run 