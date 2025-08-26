-- ADICIONAR COLUNA MECANICO NA TABELA ORDENS SERVICO
-- Execute este script no Azure Portal Query Editor

PRINT '=== ADICIONANDO COLUNA MECANICO ===';

-- Verificar se a coluna já existe
IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'OrdensServico' 
    AND COLUMN_NAME = 'Mecanico'
)
BEGIN
    -- Adicionar a coluna Mecanico
    ALTER TABLE OrdensServico
    ADD Mecanico NVARCHAR(100) NULL DEFAULT 'Milton Diego';
    
    -- Atualizar registros existentes com o valor padrão
    UPDATE OrdensServico 
    SET Mecanico = 'Milton Diego' 
    WHERE Mecanico IS NULL;
    
    PRINT '✅ Coluna Mecanico adicionada com sucesso!';
    PRINT '✅ Todos os registros atualizados com Milton Diego como padrão';
END
ELSE
BEGIN
    PRINT '⚠️ Coluna Mecanico já existe na tabela OrdensServico';
END

-- Verificar a estrutura final
PRINT '=== VERIFICANDO ESTRUTURA FINAL ===';
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'OrdensServico'
ORDER BY ORDINAL_POSITION;

PRINT '=== SCRIPT CONCLUÍDO ===';
