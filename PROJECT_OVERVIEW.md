# 空运海运费用报价系统 - 项目概述

## 项目结构

```
FreightQuotationSystem/
├── ClientApp/                 # Vue3前端应用
│   ├── package.json          # 前端依赖配置
│   ├── vite.config.js        # Vite构建配置
│   ├── src/
│   │   ├── App.vue           # 应用根组件
│   │   ├── main.js           # 应用入口文件
│   │   ├── composables/
│   │   │   └── useApi.js     # API调用组合函数
│   │   ├── router/
│   │   │   └── index.js      # 路由配置
│   │   ├── stores/
│   │   │   └── counter.js    # Pinia状态管理
│   │   ├── styles/
│   │   │   └── main.css      # 全局样式
│   │   ├── views/
│   │   │   ├── HomeView.vue  # 首页视图
│   │   │   ├── SuppliersView.vue # 供应商管理视图
│   │   │   └── ReportsView.vue # 报表视图
│   │   ├── pages/
│   │   │   └── QuotationForm.vue # 报价单创建页面
│   │   ├── components/
│   │   │   ├── SupplierSelector.vue # 供应商选择组件
│   │   │   └── QuotationItem.vue # 报价明细组件
│   │   └── layouts/
├── Server/
│   └── FreightQuotation.API/ # .NET 8.0后端API
│       ├── FreightQuotation.API.csproj # 项目文件
│       ├── Program.cs         # 应用启动配置
│       ├── Properties/
│       │   └── launchSettings.json # 启动配置
│       ├── Controllers/
│       │   ├── SuppliersController.cs # 供应商控制器
│       │   └── QuotationsController.cs # 报价单控制器
│       ├── Data/
│       │   ├── ApplicationDbContext.cs # 数据库上下文
│       │   └── Configurations/ # 实体配置
│       ├── Models/
│       │   ├── Supplier.cs    # 供应商模型
│       │   ├── Quotation.cs   # 报价单模型
│       │   ├── QuotationDetail.cs # 报价明细模型
│       │   └── ConfirmationToken.cs # 确认令牌模型
│       ├── Services/
│       │   ├── Interfaces/
│       │   │   └── IEmailService.cs # 邮件服务接口
│       │   └── EmailService.cs # 邮件服务实现
│       └── DTOs/
│           └── QuotationCreateDto.cs # 报价单DTO
```

## 功能模块

### 1. 供应商管理
- 供应商信息维护
- 供应商查询和筛选

### 2. 报价单管理
- 报价单创建（空运/海运）
- 报价明细录入
- 总价自动计算
- 报价单状态跟踪

### 3. 邮件通知系统
- 自动发送报价请求邮件
- 供应商确认/拒绝链接
- 邮件模板定制

### 4. 报表统计
- 报价历史统计
- 供应商对比分析
- 费用趋势图表

## 技术栈

### 前端 (ClientApp)
- **框架**: Vue 3 with Composition API
- **UI库**: Vuetify 3 (Material Design)
- **路由**: Vue Router 4
- **状态管理**: Pinia
- **HTTP客户端**: Axios
- **构建工具**: Vite
- **图标**: Material Design Icons

### 后端 (Server/FreightQuotation.API)
- **框架**: .NET 8.0
- **Web API**: ASP.NET Core
- **ORM**: Entity Framework Core
- **数据库**: SQL Server (可适配其他数据库)
- **API文档**: Swagger/OpenAPI

## 环境配置

### 前端开发环境
```bash
cd ClientApp
npm install
npm run dev
```
前端将运行在 http://localhost:3000

### 后端开发环境
```bash
cd Server/FreightQuotation.API
dotnet run
```
后端API将运行在 http://localhost:5000

### API代理配置
前端已配置代理，将 `/api` 请求转发到后端API。

## 部署说明

### 生产构建
```bash
# 构建前端
cd ClientApp
npm run build

# 发布后端
cd Server/FreightQuotation.API
dotnet publish -c Release -o ./publish
```

## 数据库配置

在 `appsettings.json` 中配置数据库连接字符串：
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FreightQuotationDB;Trusted_Connection=true;MultipleActiveResultSets=true;"
  }
}
```