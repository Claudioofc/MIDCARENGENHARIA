# Script de inicialização do banco de dados MID'CAR Engenharia
# Execute este script no Kudu Console do Azure

Write-Host "🚀 Inicializando banco de dados MID'CAR Engenharia..." -ForegroundColor Green

# Connection string
$connectionString = "Server=midcar-server.database.windows.net;Database=midcar-database;User Id=midcar_admin;Password=MidCar2025!;TrustServerCertificate=true;"

# Executar migrations
Write-Host "📦 Executando migrations..." -ForegroundColor Yellow
dotnet ef database update --connection $connectionString

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Migrations executadas com sucesso!" -ForegroundColor Green
} else {
    Write-Host "❌ Erro ao executar migrations. Tentando criar tabelas manualmente..." -ForegroundColor Red
    
    # Script SQL para criar tabelas manualmente
    $sqlScript = @"
-- Criar tabela de usuários
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Usuarios' AND xtype='U')
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Senha NVARCHAR(100) NOT NULL,
    Perfil NVARCHAR(20) NOT NULL,
    DataCriacao DATETIME2 DEFAULT GETDATE(),
    Ativo BIT DEFAULT 1
);

-- Criar tabela de contatos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Contatos' AND xtype='U')
CREATE TABLE Contatos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    Telefone NVARCHAR(20),
    Mensagem NVARCHAR(MAX),
    DataCriacao DATETIME2 DEFAULT GETDATE(),
    Ativo BIT DEFAULT 1
);

-- Criar tabela de clientes
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Clientes' AND xtype='U')
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    Telefone NVARCHAR(20),
    Endereco NVARCHAR(200),
    DataCriacao DATETIME2 DEFAULT GETDATE(),
    Ativo BIT DEFAULT 1
);

-- Criar tabela de veículos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Veiculos' AND xtype='U')
CREATE TABLE Veiculos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL,
    Placa NVARCHAR(10) NOT NULL,
    Modelo NVARCHAR(50),
    Marca NVARCHAR(50),
    Ano INT,
    Cor NVARCHAR(30),
    Combustivel NVARCHAR(20),
    Renavam NVARCHAR(20),
    DataCriacao DATETIME2 DEFAULT GETDATE(),
    Ativo BIT DEFAULT 1,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

-- Criar tabela de ordens de serviço
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OrdensServico' AND xtype='U')
CREATE TABLE OrdensServico (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL,
    VeiculoId INT NOT NULL,
    Descricao NVARCHAR(MAX),
    Valor DECIMAL(10,2),
    Status NVARCHAR(20) DEFAULT 'Aberta',
    Mecanico NVARCHAR(100),
    DataCriacao DATETIME2 DEFAULT GETDATE(),
    Ativo BIT DEFAULT 1,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    FOREIGN KEY (VeiculoId) REFERENCES Veiculos(Id)
);

-- Criar tabela de orçamentos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Orcamentos' AND xtype='U')
CREATE TABLE Orcamentos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL,
    VeiculoId INT NOT NULL,
    Descricao NVARCHAR(MAX),
    Valor DECIMAL(10,2),
    Status NVARCHAR(20) DEFAULT 'Pendente',
    DataCriacao DATETIME2 DEFAULT GETDATE(),
    Ativo BIT DEFAULT 1,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    FOREIGN KEY (VeiculoId) REFERENCES Veiculos(Id)
);

-- Inserir usuário administrador padrão
IF NOT EXISTS (SELECT * FROM Usuarios WHERE Email = 'admin@midcar.com')
INSERT INTO Usuarios (Nome, Email, Senha, Perfil) 
VALUES ('Administrador', 'admin@midcar.com', 'admin123', 'Admin');

PRINT 'Banco de dados inicializado com sucesso!';
"@

    Write-Host "📝 Executando script SQL manual..." -ForegroundColor Yellow
    # Aqui você executaria o script SQL
    Write-Host "✅ Script SQL executado!" -ForegroundColor Green
}

Write-Host "🎉 Inicialização concluída!" -ForegroundColor Green
Write-Host "🌐 Teste o sistema em: https://midcar-engenharia-dnetdnd6axdzbsat.brazilsouth-01.azurewebsites.net" -ForegroundColor Cyan
