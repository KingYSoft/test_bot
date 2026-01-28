# 部署指南 - 空运海运费用报价系统

## 环境要求

### 前端 (ClientApp)
- Node.js 18.x 或更高版本
- npm 或 yarn 包管理器

### 后端 (Server/FreightQuotation.API)
- .NET 8.0 SDK
- SQL Server 或兼容数据库

## 开发环境设置

### 1. 克隆项目
```bash
git clone https://github.com/KingYSoft/test_bot.git
cd test_bot
```

### 2. 设置后端

#### 2.1 安装 .NET 8.0 SDK
从 [.NET 下载页面](https://dotnet.microsoft.com/download/dotnet/8.0)安装 .NET 8.0 SDK

#### 2.2 配置数据库连接
在 `Server/FreightQuotation.API/appsettings.Development.json` 中配置数据库连接：

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FreightQuotationDB;Trusted_Connection=true;MultipleActiveResultSets=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "BaseUrl": "http://localhost:3000"
}
```

#### 2.3 安装 NuGet 包并运行后端
```bash
cd Server/FreightQuotation.API
dotnet restore
dotnet build
dotnet run
```

后端 API 将运行在 http://localhost:5000

### 3. 设置前端

#### 3.1 安装 Node.js 依赖
```bash
cd ClientApp
npm install
```

#### 3.2 运行前端开发服务器
```bash
npm run dev
```

前端将运行在 http://localhost:3000

## 生产环境部署

### 1. 构建前端
```bash
cd ClientApp
npm run build
```

这将在 `ClientApp/dist` 目录中生成生产就绪的静态文件。

### 2. 发布后端
```bash
cd Server/FreightQuotation.API
dotnet publish -c Release -o ./publish
```

### 3. 配置生产环境变量
在 `Server/FreightQuotation.API/appsettings.Production.json` 中配置生产环境设置：

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your-production-server;Database=FreightQuotationDB;User Id=your-username;Password=your-password;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "BaseUrl": "https://yourdomain.com"
}
```

## Docker 部署 (可选)

### 1. 创建 Dockerfile

#### 后端 Dockerfile (Server/FreightQuotation.API/Dockerfile)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Server/FreightQuotation.API/FreightQuotation.API.csproj", "Server/FreightQuotation.API/"]
RUN dotnet restore "Server/FreightQuotation.API/FreightQuotation.API.csproj"
COPY . .
WORKDIR "/src/Server/FreightQuotation.API"
RUN dotnet build "FreightQuotation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreightQuotation.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreightQuotation.API.dll"]
```

#### 前端 Dockerfile (ClientApp/Dockerfile)
```dockerfile
FROM node:18-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:alpine AS production
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

### 2. Docker Compose 配置 (docker-compose.yml)
```yaml
version: '3.8'

services:
  api:
    build:
      context: .
      dockerfile: Server/FreightQuotation.API/Dockerfile
    ports:
      - "5000:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Server=db;Database=FreightQuotationDB;User=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;
    depends_on:
      - db

  client:
    build:
      context: ./ClientApp
      dockerfile: Dockerfile
    ports:
      - "3000:80"
    depends_on:
      - api

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      SA_PASSWORD: "YourStrong@Passw0rd"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
    volumes:
      - sqlserver_data:/var/opt/mssql

volumes:
  sqlserver_data:
```

### 3. 运行 Docker Compose
```bash
docker-compose up -d
```

## 环境变量配置

### 前端环境变量 (.env)
在 `ClientApp/.env` 中设置：

```env
VITE_API_BASE_URL=http://localhost:5000/api
```

### 后端环境变量
在 `appsettings.json` 中设置：

- `ConnectionStrings:DefaultConnection` - 数据库连接字符串
- `BaseUrl` - 应用的基础URL（用于邮件中的确认链接）

## 数据库迁移

### 运行数据库迁移
```bash
cd Server/FreightQuotation.API
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 邮件服务配置

如果需要发送邮件功能，请配置邮件服务提供商（如SMTP、SendGrid等）并在 `Program.cs` 中注册相应的服务。

## 故障排除

### 常见问题

1. **前端无法连接到后端 API**
   - 检查 Vite 代理配置
   - 确保后端服务正在运行

2. **数据库连接失败**
   - 检查连接字符串配置
   - 确保数据库服务正在运行

3. **构建失败**
   - 检查 .NET 和 Node.js 版本
   - 清理并重新安装依赖项

### 日志位置
- 前端：浏览器控制台
- 后端：控制台输出或配置的日志文件