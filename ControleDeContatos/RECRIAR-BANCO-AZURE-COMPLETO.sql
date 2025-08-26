-- RECRIAR BANCO AZURE COMPLETO - COMO NO LOCAL
-- Execute este script no Azure Portal

PRINT '=== APAGANDO TODAS AS TABELAS ===';

-- 1. APAGAR TODAS AS TABELAS EXISTENTES
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Orcamentos')
    DROP TABLE Orcamentos;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OrdensServico')
    DROP TABLE OrdensServico;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Veiculos')
    DROP TABLE Veiculos;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Clientes')
    DROP TABLE Clientes;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Usuarios')
    DROP TABLE Usuarios;

PRINT '✅ Todas as tabelas apagadas';

-- 2. CRIAR TABELA USUARIOS
PRINT '=== CRIANDO TABELA USUARIOS ===';
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Login NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NULL,
    Senha NVARCHAR(50) NOT NULL,
    Perfil INT NOT NULL DEFAULT 0,
    DataCadastro DATETIME2 NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1
);

-- 3. CRIAR TABELA CLIENTES
PRINT '=== CRIANDO TABELA CLIENTES ===';
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NULL,
    Telefone NVARCHAR(20) NULL,
    Celular NVARCHAR(20) NULL,
    CpfCnpj NVARCHAR(20) NULL,
    Endereco NVARCHAR(200) NULL,
    Cidade NVARCHAR(50) NULL,
    Estado NVARCHAR(2) NULL,
    Cep NVARCHAR(10) NULL,
    Observacoes NVARCHAR(500) NULL,
    DataCadastro DATETIME2 NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1
);

-- 4. CRIAR TABELA VEICULOS
PRINT '=== CRIANDO TABELA VEICULOS ===';
CREATE TABLE Veiculos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Placa NVARCHAR(10) NOT NULL,
    Modelo NVARCHAR(50) NOT NULL,
    Marca NVARCHAR(50) NOT NULL,
    Ano INT NOT NULL,
    Cor NVARCHAR(20) NULL,
    Chassi NVARCHAR(20) NULL,
    Motor NVARCHAR(20) NULL,
    Quilometragem NVARCHAR(10) NULL,
    Observacoes NVARCHAR(500) NULL,
    DataCadastro DATETIME2 NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    ClienteId INT NOT NULL,
    Combustivel NVARCHAR(20) NULL,
    Renavam NVARCHAR(20) NULL,
    DataModificacao DATETIME2 NULL,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

-- 5. CRIAR TABELA ORDENS SERVICO
PRINT '=== CRIANDO TABELA ORDENS SERVICO ===';
CREATE TABLE OrdensServico (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Numero NVARCHAR(20) NOT NULL,
    DataEntrada DATETIME2 NOT NULL,
    DataSaida DATETIME2 NULL,
    Status NVARCHAR(20) NOT NULL,
    Observacoes NVARCHAR(500) NULL,
    ValorTotal DECIMAL(10,2) NULL,
    ClienteId INT NOT NULL,
    VeiculoId INT NOT NULL,
    DataCadastro DATETIME2 NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    FOREIGN KEY (VeiculoId) REFERENCES Veiculos(Id)
);

-- 6. CRIAR TABELA ORCAMENTOS
PRINT '=== CRIANDO TABELA ORCAMENTOS ===';
CREATE TABLE Orcamentos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Numero NVARCHAR(20) NOT NULL,
    DataOrcamento DATETIME2 NOT NULL,
    Validade DATETIME2 NULL,
    Status NVARCHAR(20) NOT NULL,
    Observacoes NVARCHAR(500) NULL,
    ValorTotal DECIMAL(10,2) NULL,
    ClienteId INT NOT NULL,
    VeiculoId INT NOT NULL,
    DataCadastro DATETIME2 NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    FOREIGN KEY (VeiculoId) REFERENCES Veiculos(Id)
);

-- 7. INSERIR DADOS INICIAIS
PRINT '=== INSERINDO DADOS INICIAIS ===';

-- Usuário admin
INSERT INTO Usuarios (Nome, Login, Email, Senha, Perfil, DataCadastro, Ativo)
VALUES ('Administrador', 'admin', 'admin@midcar.com', '123456', 0, GETDATE(), 1);

-- Cliente exemplo
INSERT INTO Clientes (Nome, Email, Telefone, DataCadastro, Ativo)
VALUES ('Cliente Exemplo', 'cliente@exemplo.com', '(11) 99999-9999', GETDATE(), 1);

-- Veículo exemplo
INSERT INTO Veiculos (Placa, Modelo, Marca, Ano, Cor, Combustivel, DataCadastro, Ativo, ClienteId)
VALUES ('DDD-4104', 'Astra', 'Chevrolet', 2010, 'Prata', 'Flex', GETDATE(), 1, 1);

PRINT '✅ Dados iniciais inseridos';

-- 8. VERIFICAR RESULTADO
PRINT '=== VERIFICANDO RESULTADO ===';
SELECT 'Usuarios' as Tabela, COUNT(*) as Registros FROM Usuarios
UNION ALL
SELECT 'Clientes', COUNT(*) FROM Clientes
UNION ALL
SELECT 'Veiculos', COUNT(*) FROM Veiculos
UNION ALL
SELECT 'OrdensServico', COUNT(*) FROM OrdensServico
UNION ALL
SELECT 'Orcamentos', COUNT(*) FROM Orcamentos;

PRINT '=== BANCO AZURE RECRIADO COM SUCESSO! ===';
PRINT '✅ Todas as tabelas criadas com schema correto';
PRINT '✅ Foreign Keys configuradas';
PRINT '✅ Dados iniciais inseridos';
PRINT '✅ Login: admin / Senha: 123456';
PRINT '✅ Aplicação Azure deve funcionar agora!';
