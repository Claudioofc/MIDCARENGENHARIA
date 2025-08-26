-- CORRIGIR APENAS TABELA VEICULOS NO AZURE
-- Execute este script no Azure Portal

PRINT '=== CORRIGINDO TABELA VEICULOS ===';

-- 1. VERIFICAR ESTRUTURA ATUAL
PRINT '=== ESTRUTURA ATUAL ===';
SELECT 
    COLUMN_NAME as 'Coluna',
    DATA_TYPE as 'Tipo',
    IS_NULLABLE as 'Permite_Null'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Veiculos' 
AND COLUMN_NAME IN ('Ano', 'Ativo', 'ClienteId')
ORDER BY COLUMN_NAME;

-- 2. CRIAR TABELA TEMPORÁRIA COM DADOS
PRINT '=== CRIANDO TABELA TEMPORÁRIA ===';
SELECT * INTO #VeiculosTemp FROM Veiculos;

-- 3. APAGAR TABELA ORIGINAL
PRINT '=== APAGANDO TABELA ORIGINAL ===';
DROP TABLE Veiculos;

-- 4. CRIAR TABELA VEICULOS CORRETA
PRINT '=== CRIANDO TABELA CORRETA ===';
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
    DataModificacao DATETIME2 NULL
);

-- 5. INSERIR DADOS CORRIGIDOS
PRINT '=== INSERINDO DADOS CORRIGIDOS ===';
INSERT INTO Veiculos (
    Placa, Modelo, Marca, Ano, Cor, Chassi, Motor, 
    Quilometragem, Observacoes, DataCadastro, Ativo, 
    ClienteId, Combustivel, Renavam, DataModificacao
)
SELECT 
    Placa,
    Modelo,
    Marca,
    CAST(Ano AS INT) as Ano,
    Cor,
    Chassi,
    Motor,
    Quilometragem,
    Observacoes,
    DataCadastro,
    CAST(Ativo AS BIT) as Ativo,
    CAST(ClienteId AS INT) as ClienteId,
    Combustivel,
    Renavam,
    DataModificacao
FROM #VeiculosTemp;

-- 6. VERIFICAR RESULTADO
PRINT '=== VERIFICANDO RESULTADO ===';
SELECT TOP 3 Id, Placa, Ano, Ativo, ClienteId FROM Veiculos;

-- 7. LIMPAR TABELA TEMPORÁRIA
DROP TABLE #VeiculosTemp;

PRINT '=== TABELA VEICULOS CORRIGIDA COM SUCESSO! ===';
PRINT '✅ Ano: INT';
PRINT '✅ Ativo: BIT';
PRINT '✅ ClienteId: INT';
PRINT '✅ Aplicação Azure deve funcionar agora!';
