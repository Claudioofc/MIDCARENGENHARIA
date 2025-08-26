-- VERIFICAR TODAS AS TABELAS PROBLEMÁTICAS NO AZURE
-- Execute este script para identificar onde está o erro

PRINT '=== VERIFICANDO TODAS AS TABELAS ===';

-- 1. VERIFICAR TABELA VEICULOS
PRINT '=== TABELA VEICULOS ===';
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Veiculos')
BEGIN
    PRINT '✅ Tabela Veiculos existe';
    
    -- Verificar colunas problemáticas
    SELECT 
        COLUMN_NAME as 'Coluna',
        DATA_TYPE as 'Tipo',
        IS_NULLABLE as 'Permite_Null'
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Veiculos' 
    AND COLUMN_NAME IN ('Ano', 'Ativo', 'ClienteId')
    ORDER BY COLUMN_NAME;
    
    -- Mostrar exemplo de dados
    PRINT '=== EXEMPLO DE DADOS VEICULOS ===';
    SELECT TOP 3 Id, Placa, Ano, Ativo, ClienteId FROM Veiculos;
END
ELSE
BEGIN
    PRINT '❌ Tabela Veiculos NÃO existe';
END

-- 2. VERIFICAR TABELA ORDENS SERVICO
PRINT '=== TABELA ORDENS SERVICO ===';
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OrdensServico')
BEGIN
    PRINT '✅ Tabela OrdensServico existe';
    
    -- Verificar colunas problemáticas
    SELECT 
        COLUMN_NAME as 'Coluna',
        DATA_TYPE as 'Tipo',
        IS_NULLABLE as 'Permite_Null'
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'OrdensServico' 
    AND COLUMN_NAME IN ('ClienteId', 'VeiculoId', 'Ativo')
    ORDER BY COLUMN_NAME;
    
    -- Mostrar exemplo de dados
    PRINT '=== EXEMPLO DE DADOS ORDENS SERVICO ===';
    SELECT TOP 3 Id, ClienteId, VeiculoId, Ativo FROM OrdensServico;
END
ELSE
BEGIN
    PRINT '❌ Tabela OrdensServico NÃO existe';
END

-- 3. VERIFICAR TABELA ORCAMENTOS
PRINT '=== TABELA ORCAMENTOS ===';
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Orcamentos')
BEGIN
    PRINT '✅ Tabela Orcamentos existe';
    
    -- Verificar colunas problemáticas
    SELECT 
        COLUMN_NAME as 'Coluna',
        DATA_TYPE as 'Tipo',
        IS_NULLABLE as 'Permite_Null'
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Orcamentos' 
    AND COLUMN_NAME IN ('ClienteId', 'VeiculoId', 'Ativo')
    ORDER BY COLUMN_NAME;
    
    -- Mostrar exemplo de dados
    PRINT '=== EXEMPLO DE DADOS ORCAMENTOS ===';
    SELECT TOP 3 Id, ClienteId, VeiculoId, Ativo FROM Orcamentos;
END
ELSE
BEGIN
    PRINT '❌ Tabela Orcamentos NÃO existe';
END

PRINT '=== FIM DA VERIFICAÇÃO ===';
