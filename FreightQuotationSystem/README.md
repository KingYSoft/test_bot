# 空运海运费用报价系统

这是一个基于 Vue 3 + Vuetify 3 + Vite + .NET 8.0 的空运海运费用报价系统。

## 功能特性

- 供应商管理
- 报价单创建与管理
- 邮件通知供应商
- 供应商在线确认/拒绝报价
- 报价单状态跟踪

## 技术栈

- **前端**: Vue 3, TypeScript, Vuetify 3, Vue Router, Pinia
- **后端**: .NET 8.0 Web API, Entity Framework Core
- **数据库**: SQL Server
- **构建工具**: Vite

## 项目结构

```
FreightQuotationSystem/
├── ClientApp/                 # Vue3前端应用
│   ├── src/
│   │   ├── components/       # Vue组件
│   │   ├── composables/      # 组合式API逻辑
│   │   ├── layouts/          # 页面布局
│   │   ├── pages/            # 页面组件
│   │   ├── router/           # 路由配置
│   │   └── stores/           # Pinia状态管理
│   ├── package.json          # 前端依赖
│   └── vite.config.js        # Vite构建配置
├── Server/
│   ├── FreightQuotation.API/ # .NET 8.0后端API
│   │   ├── Controllers/      # API控制器
│   │   ├── Data/             # 数据访问层
│   │   ├── Models/           # 数据模型
│   │   ├── Services/         # 业务逻辑服务
│   │   └── DTOs/             # 数据传输对象
│   └── FreightQuotation.Shared/ # 共享模型
```

## 快速开始

### 环境要求

- Node.js 18+
- .NET 8.0 SDK
- SQL Server

### 开发设置

1. 克隆项目
2. 安装前后端依赖
3. 配置数据库连接字符串
4. 启动前后端服务

### 前端启动

```bash
cd ClientApp
npm install
npm run dev
```

### 后端启动

```bash
cd Server/FreightQuotation.API
dotnet run
```

## 系统架构

- RESTful API 设计
- JWT 身份验证
- 数据库迁移
- 邮件服务集成
- 响应式UI设计