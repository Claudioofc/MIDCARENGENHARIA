# 🚀 Deploy Imediato no Azure - MID'CAR Engenharia

## ⚡ Passo a Passo Rápido

### 1. 📋 Pré-requisitos
- ✅ Conta Azure criada
- ✅ Cartão de crédito configurado
- ✅ Projeto no GitHub (já feito)
- ✅ Projeto rodando localmente (já feito)

### 2. 🌐 Acessar Portal Azure
1. Abra: https://portal.azure.com
2. Faça login com sua conta
3. Clique em "Criar um recurso"

### 3. 🏗️ Criar App Service
1. **Pesquisar:** Digite "App Service"
2. **Selecionar:** "App Service" (não App Service Plan)
3. **Criar:** Clique em "Criar"

### 4. ⚙️ Configurar App Service
```
Nome do aplicativo: midcar-engenharia
Publicar: Código
Runtime stack: .NET 5 (LTS)
Sistema operacional: Windows
Região: Brazil South (São Paulo)
Plano do App Service: 
  - Criar novo
  - Nome: midcar-plan
  - Região: Brazil South
  - Plano de preços: F1 (Gratuito)
```

### 5. 🗄️ Criar Banco de Dados
1. **Voltar ao Portal**
2. **Criar recurso:** "SQL Database"
3. **Configurar:**
```
Nome do banco de dados: midcar-database
Servidor: Criar novo
  - Nome do servidor: midcar-server
  - Localização: Brazil South
  - Método de autenticação: SQL
  - Login do servidor: midcar_admin
  - Senha: [senha forte]
Plano de preços: Básico (5 DTUs)
```

### 6. 🔗 Conectar GitHub
1. **No App Service criado**
2. **Centro de implantação**
3. **Fonte:** GitHub
4. **Autorizar:** Conectar sua conta GitHub
5. **Repositório:** Claudioofc/MIDCARENGENHARIA
6. **Branch:** master
7. **Deploy automático:** Ativado

### 7. ⚙️ Configurar Variáveis
1. **App Service > Configurações > Configurações**
2. **Adicionar:**
```
WEBSITE_RUN_FROM_PACKAGE=1
DOTNET_ENVIRONMENT=Production
ConnectionStrings__DataBase=Server=midcar-server.database.windows.net;Database=midcar-database;User Id=midcar_admin;Password=[SUA_SENHA];TrustServerCertificate=true;
```

### 8. 🚀 Deploy Automático
- ✅ O deploy acontece automaticamente
- ✅ Cada push no GitHub = novo deploy
- ✅ URL: https://midcar-engenharia.azurewebsites.net

## 🔧 Comandos para Executar

### 1. Build do Projeto
```bash
dotnet publish --configuration Release --output ./publish
```

### 2. Verificar Build
```bash
dotnet build --configuration Release
```

### 3. Testar Localmente
```bash
dotnet run --environment Production
```

## 📊 Monitoramento

### 1. Logs em Tempo Real
- **App Service > Log stream**
- **Ver erros e informações**

### 2. Métricas
- **CPU, Memória, Disco**
- **Requests por segundo**

### 3. Banco de Dados
- **SQL Database > Query editor**
- **Executar migrations**

## 🎯 Resultado Final

### ✅ URL do Projeto:
**https://midcar-engenharia.azurewebsites.net**

### ✅ Funcionalidades:
- Sistema completo rodando na nuvem
- Banco de dados SQL Server
- Deploy automático do GitHub
- SSL/HTTPS gratuito
- Monitoramento básico

## 🚨 Troubleshooting

### Erro 500:
1. Verificar logs em "Log stream"
2. Verificar connection string
3. Executar migrations

### Deploy falhou:
1. Verificar build local
2. Verificar configurações do App Service
3. Verificar permissões do GitHub

### Banco não conecta:
1. Verificar firewall do SQL Server
2. Verificar connection string
3. Verificar credenciais

## 📞 Próximos Passos

1. **Testar o sistema** online
2. **Configurar domínio** personalizado (opcional)
3. **Configurar backup** (plano pago)
4. **Monitorar performance**
5. **Configurar alertas**
