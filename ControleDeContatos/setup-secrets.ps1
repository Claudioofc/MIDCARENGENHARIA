# Script para configurar User Secrets de forma segura
# Execute este script apenas em ambiente de desenvolvimento

Write-Host "🔐 Configurando User Secrets para MID'CAR Engenharia" -ForegroundColor Green
Write-Host ""

# Verificar se o User Secrets já está habilitado
$userSecretsId = dotnet user-secrets list --project . 2>$null
if ($LASTEXITCODE -ne 0) {
    Write-Host "⚠️  User Secrets não está habilitado. Habilitando..." -ForegroundColor Yellow
    dotnet user-secrets init --project .
    Write-Host "✅ User Secrets habilitado!" -ForegroundColor Green
}

Write-Host ""
Write-Host "📝 Configure suas credenciais de banco de dados:" -ForegroundColor Cyan
Write-Host ""

# Solicitar informações do usuário
$server = Read-Host "Servidor SQL (ex: localhost ou (localdb)\mssqllocaldb)"
$database = Read-Host "Nome do banco de dados (ex: DB_SistemaContatos)"
$username = Read-Host "Usuário SQL (deixe vazio para Windows Authentication)"
$password = ""

if ($username) {
    $password = Read-Host "Senha SQL" -AsSecureString
    $password = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))
}

# Construir connection string
if ($username) {
    $connectionString = "Server=$server;Database=$database;User Id=$username;Password=$password;TrustServerCertificate=true;MultipleActiveResultSets=true"
} else {
    $connectionString = "Server=$server;Database=$database;Trusted_Connection=true;MultipleActiveResultSets=true"
}

# Configurar User Secrets
Write-Host ""
Write-Host "🔧 Configurando User Secrets..." -ForegroundColor Yellow

dotnet user-secrets set "ConnectionStrings:DataBase" "$connectionString" --project .

# Configurar credenciais padrão de admin (opcional)
$adminUser = Read-Host "Usuário admin padrão (ex: admin)"
$adminPassword = Read-Host "Senha admin padrão" -AsSecureString
$adminPassword = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($adminPassword))

dotnet user-secrets set "Authentication:DefaultAdminUser" "$adminUser" --project .
dotnet user-secrets set "Authentication:DefaultAdminPassword" "$adminPassword" --project .

Write-Host ""
Write-Host "✅ User Secrets configurado com sucesso!" -ForegroundColor Green
Write-Host ""
Write-Host "📋 Próximos passos:" -ForegroundColor Cyan
Write-Host "1. Execute: dotnet ef database update"
Write-Host "2. Execute: dotnet run"
Write-Host ""
Write-Host "🔒 Suas credenciais estão seguras no User Secrets!" -ForegroundColor Green
Write-Host ""

# Mostrar configurações (sem senhas)
Write-Host "📊 Configurações salvas:" -ForegroundColor Cyan
Write-Host "   Servidor: $server"
Write-Host "   Banco: $database"
Write-Host "   Usuário SQL: $($username ? $username : 'Windows Authentication')"
Write-Host "   Usuário Admin: $adminUser"
Write-Host ""
