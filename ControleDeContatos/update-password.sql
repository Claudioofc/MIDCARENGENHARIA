-- Script para atualizar a senha do usuário administrador
-- Execute este script no Query Editor do Azure SQL Database

-- Atualizar senha do usuário administrador
UPDATE Usuarios 
SET Senha = 'admin123' 
WHERE Email = 'admin@midcar.com';

-- Verificar se foi atualizado
SELECT Email, Senha FROM Usuarios WHERE Email = 'admin@midcar.com';

-- Verificar todos os usuários
SELECT * FROM Usuarios;

PRINT 'Senha atualizada com sucesso!';
PRINT 'Agora você pode fazer login com:';
PRINT 'Usuário: admin@midcar.com';
PRINT 'Senha: admin123';
