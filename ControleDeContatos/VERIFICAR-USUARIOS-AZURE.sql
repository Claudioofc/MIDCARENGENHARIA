-- VERIFICAR ESTRUTURA DA TABELA USUARIOS NO AZURE
-- Execute este script primeiro

PRINT '=== VERIFICANDO TABELA USUARIOS ===';

-- Verificar se a tabela existe
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Usuarios')
BEGIN
    PRINT '✅ Tabela Usuarios existe';
    
    -- Listar TODAS as colunas da tabela Usuarios
    SELECT 
        COLUMN_NAME as 'Coluna',
        DATA_TYPE as 'Tipo',
        IS_NULLABLE as 'Permite_Null',
        COLUMN_DEFAULT as 'Valor_Padrao'
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Usuarios'
    ORDER BY ORDINAL_POSITION;
    
    -- Mostrar dados existentes
    PRINT '=== DADOS EXISTENTES ===';
    SELECT TOP 5 * FROM Usuarios;
    
END
ELSE
BEGIN
    PRINT '❌ Tabela Usuarios NÃO existe';
END

PRINT '=== FIM DA VERIFICAÇÃO ===';
