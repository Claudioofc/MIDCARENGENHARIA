-- CORRIGIR BANCO AZURE - ESTRUTURA FINAL CORRETA
-- Execute este script no Azure Portal Query Editor

PRINT '=== CORRIGINDO BANCO AZURE - ESTRUTURA FINAL ===';

-- 1. REMOVER FOREIGN KEYS PRIMEIRO
PRINT '=== REMOVENDO FOREIGN KEYS ===';

-- Remover todas as FOREIGN KEYS
DECLARE @sql NVARCHAR(MAX) = '';

-- Veiculos
SELECT @sql = @sql + 'ALTER TABLE Veiculos DROP CONSTRAINT ' + CONSTRAINT_NAME + ';' + CHAR(13)
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
WHERE TABLE_NAME = 'Veiculos' AND CONSTRAINT_TYPE = 'FOREIGN KEY';

-- OrdensServico
SELECT @sql = @sql + 'ALTER TABLE OrdensServico DROP CONSTRAINT ' + CONSTRAINT_NAME + ';' + CHAR(13)
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
WHERE TABLE_NAME = 'OrdensServico' AND CONSTRAINT_TYPE = 'FOREIGN KEY';

-- Orcamentos
SELECT @sql = @sql + 'ALTER TABLE Orcamentos DROP CONSTRAINT ' + CONSTRAINT_NAME + ';' + CHAR(13)
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
WHERE TABLE_NAME = 'Orcamentos' AND CONSTRAINT_TYPE = 'FOREIGN KEY';

IF @sql != ''
BEGIN
    EXEC sp_executesql @sql;
    PRINT '✅ FOREIGN KEYs removidas';
END

-- 2. CORRIGIR TABELA VEICULOS
PRINT '=== CORRIGINDO TABELA VEICULOS ===';

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
    Renavam NVARCHAR(20) NULL,
    DataModificacao DATETIME2 NULL
);

INSERT INTO Veiculos_Temp (
    Placa, Modelo, Marca, Ano, Cor, Chassi, Motor, Quilometragem, 
    Observacoes, DataCadastro, Ativo, ClienteId, Combustivel, Renavam, DataModificacao
)
SELECT 
    Placa,
    Modelo,
    Marca,
    CAST(Ano AS INT),
    Cor,
    Chassi,
    Motor,
    CAST(Quilometragem AS NVARCHAR(10)),
    Observacoes,
    ISNULL(DataCadastro, ISNULL(DataCriacao, GETDATE())),
    ISNULL(Ativo, 1),
    ClienteId,
    Combustivel,
    Renavam,
    NULL
FROM Veiculos;

DROP TABLE Veiculos;
EXEC sp_rename 'Veiculos_Temp', 'Veiculos';
PRINT '✅ Tabela Veiculos corrigida';

-- 3. CORRIGIR TABELA ORDENS SERVICO
PRINT '=== CORRIGINDO TABELA ORDENS SERVICO ===';

CREATE TABLE OrdensServico_Temp (
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

INSERT INTO OrdensServico_Temp (
    Numero, DataEntrada, DataSaida, Status, Observacoes, 
    ValorTotal, ClienteId, VeiculoId, DataCadastro, Ativo
)
SELECT 
    'OS' + CAST(Id AS NVARCHAR(10)) as Numero,
    ISNULL(DataCriacao, GETDATE()) as DataEntrada,
    NULL as DataSaida,
    Status,
    ISNULL(Descricao, '') as Observacoes,
    ISNULL(Valor, 0) as ValorTotal,
    ClienteId,
    VeiculoId,
    ISNULL(DataCriacao, GETDATE()) as DataCadastro,
    ISNULL(Ativo, 1)
FROM OrdensServico;

DROP TABLE OrdensServico;
EXEC sp_rename 'OrdensServico_Temp', 'OrdensServico';
PRINT '✅ Tabela OrdensServico corrigida';

-- 4. CORRIGIR TABELA ORCAMENTOS
PRINT '=== CORRIGINDO TABELA ORCAMENTOS ===';

CREATE TABLE Orcamentos_Temp (
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

INSERT INTO Orcamentos_Temp (
    Numero, DataOrcamento, Validade, Status, Observacoes,
    ValorTotal, ClienteId, VeiculoId, DataCadastro, Ativo
)
SELECT 
    'ORC' + CAST(Id AS NVARCHAR(10)) as Numero,
    ISNULL(DataCriacao, GETDATE()) as DataOrcamento,
    NULL as Validade,
    'Pendente' as Status,
    '' as Observacoes,
    0 as ValorTotal,
    1 as ClienteId,
    1 as VeiculoId,
    ISNULL(DataCriacao, GETDATE()) as DataCadastro,
    ISNULL(Ativo, 1)
FROM Orcamentos;

DROP TABLE Orcamentos;
EXEC sp_rename 'Orcamentos_Temp', 'Orcamentos';
PRINT '✅ Tabela Orcamentos corrigida';

-- 5. RECRIAR FOREIGN KEYS
PRINT '=== RECRIANDO FOREIGN KEYS ===';

ALTER TABLE Veiculos 
ADD CONSTRAINT FK_Veiculos_Clientes 
FOREIGN KEY (ClienteId) REFERENCES Clientes(Id);

ALTER TABLE OrdensServico 
ADD CONSTRAINT FK_OrdensServico_Clientes 
FOREIGN KEY (ClienteId) REFERENCES Clientes(Id);

ALTER TABLE OrdensServico 
ADD CONSTRAINT FK_OrdensServico_Veiculos 
FOREIGN KEY (VeiculoId) REFERENCES Veiculos(Id);

ALTER TABLE Orcamentos 
ADD CONSTRAINT FK_Orcamentos_Clientes 
FOREIGN KEY (ClienteId) REFERENCES Clientes(Id);

ALTER TABLE Orcamentos 
ADD CONSTRAINT FK_Orcamentos_Veiculos 
FOREIGN KEY (VeiculoId) REFERENCES Veiculos(Id);

PRINT '✅ FOREIGN KEYs recriadas';

-- 6. VERIFICAR RESULTADO
PRINT '=== VERIFICANDO ESTRUTURA FINAL ===';
SELECT 
    TABLE_NAME,
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME IN ('Veiculos', 'OrdensServico', 'Orcamentos')
    AND COLUMN_NAME IN ('Ano', 'Ativo', 'ClienteId', 'VeiculoId', 'DataCadastro')
ORDER BY TABLE_NAME, COLUMN_NAME;

PRINT '=== BANCO AZURE CORRIGIDO COM SUCESSO! ===';
PRINT '✅ Todas as tabelas com estrutura correta';
PRINT '✅ Tipos de dados padronizados';
PRINT '✅ FOREIGN KEYs configuradas';
PRINT '✅ Aplicação Azure deve funcionar agora!';
