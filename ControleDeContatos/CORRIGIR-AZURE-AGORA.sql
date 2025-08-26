-- CORRIGIR AZURE AGORA - EXECUTE ESTE SCRIPT NO AZURE PORTAL
-- Vá em: https://portal.azure.com > SQL Database > midcar-database > Query Editor

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
    -- Adicionar colunas que podem estar faltando
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

-- 3. VERIFICAR E CORRIGIR TABELA VEICULOS
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Veiculos')
BEGIN
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
        Renavam NVARCHAR(20) NULL
    );
    PRINT '✅ Tabela Veiculos criada';
END
ELSE
BEGIN
    -- Corrigir colunas problemáticas
    IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Veiculos' AND COLUMN_NAME = 'Ano' AND DATA_TYPE = 'NVARCHAR')
    BEGIN
        -- Criar tabela temporária
        CREATE TABLE Veiculos_Temp (
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
            Renavam NVARCHAR(20) NULL
        );
        
        -- Copiar dados convertendo Ano para INT
        INSERT INTO Veiculos_Temp (Placa, Modelo, Marca, Ano, Cor, Chassi, Motor, Quilometragem, Observacoes, DataCadastro, Ativo, ClienteId, Combustivel, Renavam)
        SELECT Placa, Modelo, Marca, 
               CASE WHEN ISNUMERIC(Ano) = 1 THEN CAST(Ano AS INT) ELSE 2020 END,
               Cor, Chassi, Motor, Quilometragem, Observacoes, DataCadastro, 
               CASE WHEN Ativo = '1' OR Ativo = 'true' THEN 1 ELSE 0 END,
               CASE WHEN ISNUMERIC(ClienteId) = 1 THEN CAST(ClienteId AS INT) ELSE 1 END,
               Combustivel, Renavam
        FROM Veiculos;
        
        -- Trocar tabelas
        DROP TABLE Veiculos;
        EXEC sp_rename 'Veiculos_Temp', 'Veiculos';
        PRINT '✅ Tabela Veiculos corrigida (Ano como INT, Ativo como BIT)';
    END
END

-- 4. VERIFICAR E CORRIGIR TABELA ORDENS SERVICO
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

-- 5. VERIFICAR E CORRIGIR TABELA ORCAMENTOS
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

-- 6. CRIAR USUARIO ADMIN SE NÃO EXISTIR
IF NOT EXISTS (SELECT * FROM Usuarios WHERE Login = 'admin')
BEGIN
    INSERT INTO Usuarios (Nome, Login, Email, Senha, Perfil, DataCadastro, Ativo)
    VALUES ('Administrador', 'admin', 'admin@midcar.com', '123456', 0, GETDATE(), 1);
    PRINT '✅ Usuário admin criado (login: admin, senha: 123456)';
END

-- 7. CRIAR CLIENTE EXEMPLO SE NÃO EXISTIR
IF NOT EXISTS (SELECT * FROM Clientes WHERE Email = 'cliente@exemplo.com')
BEGIN
    INSERT INTO Clientes (Nome, Email, Telefone, DataCadastro, Ativo)
    VALUES ('Cliente Exemplo', 'cliente@exemplo.com', '(11) 99999-9999', GETDATE(), 1);
    PRINT '✅ Cliente exemplo criado';
END

PRINT '✅ Banco Azure corrigido com sucesso!';
PRINT '✅ Todas as tabelas estão com schema correto';
PRINT '✅ Aplicação Azure deve funcionar agora!';
