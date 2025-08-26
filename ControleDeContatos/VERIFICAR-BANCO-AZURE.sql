-- VERIFICAR ESTRUTURA DO BANCO AZURE
-- Execute este script primeiro para ver o que existe

PRINT '=== VERIFICANDO TABELAS EXISTENTES ===';

-- Listar todas as tabelas
SELECT TABLE_NAME as 'Tabela'
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;

PRINT '=== VERIFICANDO ESTRUTURA DA TABELA USUARIOS ===';

-- Verificar se a tabela Usuarios existe
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Usuarios')
BEGIN
    PRINT '✅ Tabela Usuarios existe';
    
    -- Listar colunas da tabela Usuarios
    SELECT COLUMN_NAME as 'Coluna', DATA_TYPE as 'Tipo', IS_NULLABLE as 'Permite_Null'
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Usuarios'
    ORDER BY ORDINAL_POSITION;
END
ELSE
BEGIN
    PRINT '❌ Tabela Usuarios NÃO existe';
END

PRINT '=== VERIFICANDO ESTRUTURA DA TABELA VEICULOS ===';

-- Verificar se a tabela Veiculos existe
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Veiculos')
BEGIN
    PRINT '✅ Tabela Veiculos existe';
    
    -- Listar colunas da tabela Veiculos
    SELECT COLUMN_NAME as 'Coluna', DATA_TYPE as 'Tipo', IS_NULLABLE as 'Permite_Null'
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Veiculos'
    ORDER BY ORDINAL_POSITION;
END
ELSE
BEGIN
    PRINT '❌ Tabela Veiculos NÃO existe';
END

PRINT '=== VERIFICANDO DADOS EXISTENTES ===';

-- Verificar dados nas tabelas
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Usuarios')
BEGIN
    SELECT COUNT(*) as 'Total_Usuarios' FROM Usuarios;
    SELECT TOP 3 * FROM Usuarios;
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Veiculos')
BEGIN
    SELECT COUNT(*) as 'Total_Veiculos' FROM Veiculos;
    SELECT TOP 3 * FROM Veiculos;
END

PRINT '=== FIM DA VERIFICAÇÃO ===';
