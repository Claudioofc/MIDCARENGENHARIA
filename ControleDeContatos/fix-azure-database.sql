-- Script para corrigir banco de dados Azure MID'CAR
-- Execute este script no Editor de Consultas do Azure

-- 1. Verificar se a tabela Usuarios existe
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Usuarios' AND xtype='U')
BEGIN
    -- Criar tabela Usuarios
    CREATE TABLE Usuarios (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nome NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        Senha NVARCHAR(100) NOT NULL,
        Perfil NVARCHAR(50) NOT NULL,
        DataCriacao DATETIME NOT NULL,
        Ativo BIT NOT NULL DEFAULT 1
    );
    PRINT 'Tabela Usuarios criada com sucesso!';
END

-- 2. Limpar dados existentes
DELETE FROM Usuarios WHERE Email = 'admin@midcar.com';

-- 3. Inserir usuário administrador
INSERT INTO Usuarios (Nome, Email, Senha, Perfil, DataCriacao, Ativo)
VALUES ('Administrador', 'admin', '123456', 'Admin', GETDATE(), 1);

-- 4. Verificar se foi criado
SELECT * FROM Usuarios;

PRINT 'Usuário admin criado com sucesso!';
PRINT 'Login: admin';
PRINT 'Senha: 123456';
