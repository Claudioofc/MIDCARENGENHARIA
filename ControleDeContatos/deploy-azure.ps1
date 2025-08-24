# Script para Deploy no Azure - MID'CAR
Write-Host "🚀 Iniciando deploy para Azure..." -ForegroundColor Green

# Verificar status do git
Write-Host "📋 Verificando status do repositório..." -ForegroundColor Yellow
git status

# Adicionar alterações
Write-Host "📦 Adicionando alterações..." -ForegroundColor Yellow
git add .

# Fazer commit
Write-Host "💾 Fazendo commit das alterações..." -ForegroundColor Yellow
git commit -m "Corrigir string de conexão para Azure - admin/123456"

# Fazer push
Write-Host "📤 Enviando para GitHub..." -ForegroundColor Yellow
git push origin master

Write-Host "✅ Deploy iniciado!" -ForegroundColor Green
Write-Host "⏳ Aguarde 2-3 minutos para o deploy automático no Azure..." -ForegroundColor Cyan
Write-Host "🌐 URL do site: https://midcar-engenharia-dnetdnd6axdzbsat.brazilsouth-01.azurewebsites.net" -ForegroundColor Cyan
Write-Host "🔐 Login: admin" -ForegroundColor Cyan
Write-Host "🔑 Senha: 123456" -ForegroundColor Cyan
