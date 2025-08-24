# 🚀 Deploy no Azure - MID'CAR Engenharia

## 📋 Pré-requisitos

### 1. Conta Azure
- Acesse: https://azure.microsoft.com/pt-br/free/
- Crie conta gratuita (12 meses + $200 crédito)
- Configure cartão de crédito (não será cobrado no plano gratuito)

### 2. Ferramentas
- Visual Studio 2022 ou VS Code
- Azure CLI (opcional)
- Git configurado

## 🎯 Passo a Passo

### Passo 1: Criar App Service

1. **Acesse o Portal Azure**
   - https://portal.azure.com

2. **Criar Recurso**
   - Clique em "Criar um recurso"
   - Procure por "App Service"
   - Clique em "Criar"

3. **Configurar App Service**
   ```
   Nome: midcar-engenharia
   Publicar: Código
   Runtime stack: .NET 5 (LTS)
   Sistema operacional: Windows
   Região: Brazil South (São Paulo)
   Plano do App Service: F1 (Gratuito)
   ```

### Passo 2: Criar Banco de Dados

1. **Criar SQL Database**
   - Procure por "SQL Database"
   - Configure:
   ```
   Nome: midcar-database
   Servidor: midcar-server
   Autenticação: SQL
   Usuário: midcar_admin
   Senha: [senha forte]
   ```

2. **Configurar Firewall**
   - Permitir acesso do Azure
   - Adicionar IP do seu computador

### Passo 3: Configurar Connection String

1. **No App Service**
   - Vá em "Configurações" > "Configurações"
   - Adicione:
   ```
   Nome: ConnectionStrings__DataBase
   Valor: Server=midcar-server.database.windows.net;Database=midcar-database;User Id=midcar_admin;Password=[sua-senha];TrustServerCertificate=true;
   ```

### Passo 4: Deploy do GitHub

1. **Conectar GitHub**
   - No App Service, vá em "Centro de implantação"
   - Escolha "GitHub"
   - Autorize o Azure

2. **Configurar Deploy**
   - Repositório: Claudioofc/MIDCARENGENHARIA
   - Branch: master
   - Deploy automático: Ativado

3. **Configurar Build**
   - Runtime: .NET 5
   - Build Command: `dotnet build --configuration Release`
   - Output Directory: `bin/Release/net5.0/publish`

### Passo 5: Configurar Variáveis de Ambiente

No App Service, adicione:

```
WEBSITE_RUN_FROM_PACKAGE=1
DOTNET_ENVIRONMENT=Production
```

## 🔧 Configurações Específicas

### 1. Migrations
```bash
# No Azure CLI ou Kudu Console
dotnet ef database update
```

### 2. Logs
- Configure Application Insights
- Monitore erros e performance

### 3. SSL
- Certificado gratuito automático
- HTTPS obrigatório

## 📊 Monitoramento

### 1. Métricas
- CPU, Memória, Disco
- Requests por segundo
- Tempo de resposta

### 2. Logs
- Application logs
- Web server logs
- Failed request logs

## 💰 Custos (Plano Gratuito)

### F1 App Service (Gratuito)
- 1 GB RAM
- 1 CPU compartilhado
- 1 GB de disco
- 60 minutos de CPU/dia

### SQL Database (Gratuito)
- 32 MB de armazenamento
- 5 DTUs
- Ideal para desenvolvimento

## 🚨 Limitações do Plano Gratuito

### App Service F1
- ❌ Sem suporte a SSL personalizado
- ❌ Sem backup automático
- ❌ Sem staging slots
- ❌ Sem autoscaling

### SQL Database
- ❌ Limite de 32 MB
- ❌ Sem backup automático
- ❌ Sem geo-replicação

## 🔄 Upgrade para Plano Pago

### Quando considerar:
- Mais de 100 usuários
- Necessidade de backup
- SSL personalizado
- Melhor performance

### Planos recomendados:
- **B1 (Básico):** $13/mês
- **S1 (Standard):** $73/mês
- **P1V2 (Premium):** $147/mês

## 📞 Suporte

### Documentação Oficial
- https://docs.microsoft.com/azure/app-service/
- https://docs.microsoft.com/azure/sql-database/

### Comunidade
- Stack Overflow: tag `azure`
- GitHub Issues
- Microsoft Q&A

## 🎉 Resultado Final

Após o deploy, seu projeto estará disponível em:
**https://midcar-engenharia.azurewebsites.net**

### Funcionalidades Disponíveis:
- ✅ Sistema completo rodando na nuvem
- ✅ Banco de dados SQL Server
- ✅ Deploy automático do GitHub
- ✅ SSL gratuito
- ✅ Monitoramento básico
