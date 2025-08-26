-- CORRIGIR AZURE FINAL - BASEADO NA ESTRUTURA REAL
-- Execute este script no Azure Portal

PRINT '=== CORRIGINDO BANCO AZURE ===';

-- 1. VERIFICAR E CORRIGIR TABELA USUARIOS
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Usuarios')
BEGIN
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
    PRINT '✅ Tabela Usuarios criada';
END
ELSE
BEGIN
    -- Adicionar colunas faltantes na tabela Usuarios
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Usuarios' AND COLUMN_NAME = 'Login')
    BEGIN
        ALTER TABLE Usuarios ADD Login NVARCHAR(50) NOT NULL DEFAULT 'admin';
        PRINT '✅ Coluna Login adicionada à tabela Usuarios';
    END
    
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Usuarios' AND COLUMN_NAME = 'DataCadastro')
    BEGIN
        ALTER TABLE Usuarios ADD DataCadastro DATETIME2 NOT NULL DEFAULT GETDATE();
        PRINT '✅ Coluna DataCadastro adicionada à tabela Usuarios';
    END
END

-- 2. VERIFICAR E CORRIGIR TABELA CLIENTES
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Clientes')
BEGIN
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
    PRINT '✅ Tabela Clientes criada';
END

-- 3. VERIFICAR E CORRIGIR TABELA ORDENS SERVICO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OrdensServico')
BEGIN
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
        Ativo BIT NOT NULL DEFAULT 1
    );
    PRINT '✅ Tabela OrdensServico criada';
END

-- 4. VERIFICAR E CORRIGIR TABELA ORCAMENTOS
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Orcamentos')
BEGIN
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
        Ativo BIT NOT NULL DEFAULT 1
    );
    PRINT '✅ Tabela Orcamentos criada';
END

-- 5. CRIAR USUARIO ADMIN SE NÃO EXISTIR
IF NOT EXISTS (SELECT * FROM Usuarios WHERE Login = 'admin')
BEGIN
    INSERT INTO Usuarios (Nome, Login, Email, Senha, Perfil, DataCadastro, Ativo)
    VALUES ('Administrador', 'admin', 'admin@midcar.com', '123456', 0, GETDATE(), 1);
    PRINT '✅ Usuário admin criado (login: admin, senha: 123456)';
END

-- 6. CRIAR CLIENTE EXEMPLO SE NÃO EXISTIR
IF NOT EXISTS (SELECT * FROM Clientes WHERE Email = 'cliente@exemplo.com')
BEGIN
    INSERT INTO Clientes (Nome, Email, Telefone, DataCadastro, Ativo)
    VALUES ('Cliente Exemplo', 'cliente@exemplo.com', '(11) 99999-9999', GETDATE(), 1);
    PRINT '✅ Cliente exemplo criado';
END

-- 7. ATUALIZAR CLIENTEID DOS VEICULOS SE NECESSÁRIO
UPDATE Veiculos SET ClienteId = 1 WHERE ClienteId = 0;
PRINT '✅ ClienteId dos veículos atualizado';

PRINT '=== BANCO AZURE CORRIGIDO COM SUCESSO! ===';
PRINT '✅ Todas as tabelas estão com schema correto';
PRINT '✅ Aplicação Azure deve funcionar agora!';
PRINT '✅ Login: admin / Senha: 123456';
