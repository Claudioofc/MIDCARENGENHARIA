# Teste de Conexão com Azure SQL Database
Write-Host "🔍 Testando conexão com Azure SQL Database..." -ForegroundColor Yellow

$connectionString = "Server=midcar-server.database.windows.net;Database=midcar-database;User Id=midcar_admin;Password=Sistema2025!;TrustServerCertificate=true;MultipleActiveResultSets=true"

try {
    # Testar conexão básica
    Write-Host "📡 Tentando conectar ao banco de dados..." -ForegroundColor Cyan
    
    # Verificar se o SQL Server está acessível
    $server = "midcar-server.database.windows.net"
    $port = 1433
    
    $tcpClient = New-Object System.Net.Sockets.TcpClient
    $tcpClient.ConnectAsync($server, $port).Wait(5000)
    
    if ($tcpClient.Connected) {
        Write-Host "✅ Conexão TCP estabelecida com sucesso!" -ForegroundColor Green
        $tcpClient.Close()
    } else {
        Write-Host "❌ Falha na conexão TCP" -ForegroundColor Red
    }
    
    # Verificar configurações de firewall
    Write-Host "`n🔧 Verificando configurações de firewall..." -ForegroundColor Cyan
    Write-Host "⚠️  IMPORTANTE: Verifique no Portal Azure:" -ForegroundColor Yellow
    Write-Host "   1. SQL Server > Segurança > Rede" -ForegroundColor White
    Write-Host "   2. Adicione seu IP atual às regras de firewall" -ForegroundColor White
    Write-Host "   3. Ou habilite 'Permitir que serviços e recursos do Azure acessem este servidor'" -ForegroundColor White
    
} catch {
    Write-Host "❌ Erro ao testar conexão: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`n📋 Próximos passos:" -ForegroundColor Yellow
Write-Host "1. Acesse: https://portal.azure.com" -ForegroundColor White
Write-Host "2. Vá para: midcar-server (SQL Server)" -ForegroundColor White
Write-Host "3. Segurança > Rede" -ForegroundColor White
Write-Host "4. Adicione regra de firewall ou habilite acesso do Azure" -ForegroundColor White
