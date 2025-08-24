# Script para inicializar repositório Git com configurações seguras
# Execute este script antes de fazer o primeiro commit

Write-Host "🚀 Inicializando repositório Git para MID'CAR Engenharia" -ForegroundColor Green
Write-Host ""

# Verificar se Git está instalado
try {
    git --version | Out-Null
    Write-Host "✅ Git encontrado!" -ForegroundColor Green
} catch {
    Write-Host "❌ Git não encontrado. Instale o Git primeiro." -ForegroundColor Red
    exit 1
}

# Inicializar repositório Git
if (!(Test-Path ".git")) {
    Write-Host "📁 Inicializando repositório Git..." -ForegroundColor Yellow
    git init
    Write-Host "✅ Repositório Git inicializado!" -ForegroundColor Green
} else {
    Write-Host "✅ Repositório Git já existe!" -ForegroundColor Green
}

# Configurar .gitignore
if (Test-Path ".gitignore") {
    Write-Host "✅ .gitignore já existe!" -ForegroundColor Green
} else {
    Write-Host "❌ .gitignore não encontrado. Crie o arquivo primeiro." -ForegroundColor Red
    exit 1
}

# Adicionar arquivos ao staging
Write-Host ""
Write-Host "📦 Adicionando arquivos ao staging..." -ForegroundColor Yellow
git add .

# Verificar status
Write-Host ""
Write-Host "📊 Status do repositório:" -ForegroundColor Cyan
git status

# Fazer primeiro commit
Write-Host ""
$commitMessage = Read-Host "Digite a mensagem do primeiro commit (ou pressione Enter para usar padrão)"
if ([string]::IsNullOrWhiteSpace($commitMessage)) {
    $commitMessage = "Initial commit: MID'CAR Engenharia - Sistema de Gestão Automotiva"
}

Write-Host "💾 Fazendo primeiro commit..." -ForegroundColor Yellow
git commit -m $commitMessage

Write-Host ""
Write-Host "✅ Repositório Git configurado com sucesso!" -ForegroundColor Green
Write-Host ""
Write-Host "📋 Próximos passos:" -ForegroundColor Cyan
Write-Host "1. Crie um repositório no GitHub"
Write-Host "2. Execute: git remote add origin https://github.com/seu-usuario/midcar-engenharia.git"
Write-Host "3. Execute: git branch -M main"
Write-Host "4. Execute: git push -u origin main"
Write-Host ""
Write-Host "🔒 Arquivos sensíveis protegidos pelo .gitignore:" -ForegroundColor Green
Write-Host "   - appsettings.json"
Write-Host "   - appsettings.Development.json"
Write-Host "   - bin/"
Write-Host "   - obj/"
Write-Host "   - .vs/"
Write-Host ""
Write-Host "📝 Lembre-se de configurar suas credenciais usando:" -ForegroundColor Yellow
Write-Host "   .\setup-secrets.ps1"
Write-Host ""
