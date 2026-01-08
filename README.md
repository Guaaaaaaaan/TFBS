# TFBS – Transport Fleet Booking System (Web API)

# 交通车队预约管理系统（Web API）

TFBS is an **enterprise-style backend system** built with **ASP.NET Core (.NET 10)** and **Entity Framework Core (Database-First)**.  
TFBS 是一个基于 **ASP.NET Core (.NET 10)** 与 **Entity Framework Core（Database-First）** 的企业级后端系统。

---

<details open>
<summary><b>🇺🇸 English Documentation (Click to Collapse)</b></summary>

<br>

<details>
<summary><b>System Overview</b></summary>

- **Primary Domain:** Institutional Fleet Management
- **Architecture Style:** Layered Web API (Controller → Service → Data)
- **Response Format:** JSON only

**Core Business Flows**

- Reservation → Trip lifecycle
- Maintenance Log → Maintenance Completion
- Transactional inventory deduction
- Read-optimized reporting

</details>

<details>
<summary><b>Architecture</b></summary>

- ASP.NET Core Web API
- Entity Framework Core (Database-First, Scaffolded)
- SQL Server (LocalDB / SQL Express)
- Service-layer business logic
- DTO-based API contracts
- Global exception handling middleware

**Principles**

- Thin controllers
- Business rules in Services
- Explicit workflows (not CRUD)

</details>

<details>
<summary><b>Key Features</b></summary>

**Reservations**

- Department & Faculty based booking
- Faculty–Department relationship enforcement

**Trips**

- One-to-one Reservation ↔ Trip
- Odometer validation on completion
- Fuel & credit card validation

**Maintenance**

- Two-step workflow (Create → Complete)
- Single transaction (all-or-nothing)
- Inventory deduction
- Role-based authorization

**Reports**

- Mileage by vehicle
- Mileage by department
- Revenue by department

</details>

<details>
<summary><b>Error Handling</b></summary>

- Domain-specific exceptions in Service layer
- Global middleware maps exceptions to HTTP responses
- No try/catch logic inside controllers

</details>

<details>
<summary><b>Transactional Guarantees</b></summary>

- Reservation → Trip creation is atomic
- Maintenance completion executes in a single transaction
- Inventory deduction is all-or-nothing
- Partial state persistence is prevented at service level

</details>

<details>
<summary><b>Security & Authorization</b></summary>

- Role-based access control (planned)
- Authorization enforced at service layer
- Separation of operational and reporting endpoints
- Designed for future authentication integration (JWT / SSO)

</details>

<details>
<summary><b>Project Structure</b></summary>

```text
TFBS/
├── Controllers/
├── Services/
├── Dtos/
├── Data/Entities/
├── database/
│   ├── seed.sql
│   └── reset.sql
├── docs/
│   └── screenshots/
```

</details>
<details>
<summary><b>Getting Started</b></summary>

- Run SQL scripts in database/
- Configure appsettings.json

```json
{
  "ConnectionStrings": {
    "TFBS": "Server=YOUR_SERVER\\SQLEXPRESS;Database=TFBS;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

- (Optional) Re-scaffold EF Core

```powershell
Scaffold-DbContext "YourConnectionString" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data/Entities -Force

```

- Run API and test via Swagger / Postman

</details>

<details>
<summary><b>Project Intent</b></summary>

TFBS is **not a UI demo**.
It focuses on backend workflow modeling, transactional safety,
and enterprise-style API design.

</details>

<details>
<summary><b>Future Enhancements</b></summary>

- Authentication & role management
- Audit logging for operational actions
- API versioning strategy
- Containerized deployment

</details>

</details>

---

<details>
<summary><b>🇨🇳 中文文档（点击展开 / 折叠）</b></summary>

<br>

<details>
<summary><b>系统概览</b></summary>

- **业务领域：** 机构车队管理
- **架构风格：** 分层式 Web API
- **返回格式：** 仅 JSON

**核心流程**

- 预约 → 行程
- 维修记录 → 维修完成
- 事务型库存扣减
- 报表查询接口

</details>

<details>
<summary><b>系统架构</b></summary>

- ASP.NET Core Web API
- EF Core（Database-First）
- SQL Server
- Service 层集中业务逻辑
- DTO 作为数据契约
- 全局异常处理中间件

</details>

<details>
<summary><b>核心功能</b></summary>

**预约**

- 按部门与教职员工预约
- 强制部门关系校验

**行程**

- Reservation ↔ Trip 一对一
- 行程完成里程校验
- 燃油与信用卡校验

**维修**

- 创建维修 → 完成维修
- 单事务执行
- 库存扣减
- 基于角色授权

**报表**

- 车辆里程
- 部门里程
- 部门收入

</details>

<details>
<summary><b>错误处理</b></summary>

- Service 层抛出业务异常
- 全局异常中间件统一映射 HTTP 响应
- Controller 中无 try/catch

</details>

<details>
<summary><b>事务一致性保证</b></summary>

- 预约与行程创建为原子操作
- 维修完成流程在单一事务中执行
- 库存扣减采用全有或全无策略
- 防止产生部分成功的业务状态

</details>

<details>
<summary><b>项目结构</b></summary>

```text
TFBS/
├── Controllers/
├── Services/
├── Dtos/
├── Data/Entities/
├── database/
├── docs/
│   └── screenshots/
```

</details>
<details>
<summary><b>快速开始</b></summary>

- 执行数据库脚本
- 配置 appsettings.json

```json
{
  "ConnectionStrings": {
    "TFBS": "Server=YOUR_SERVER\\SQLEXPRESS;Database=TFBS;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

- 启动 API 并测试

</details>

<details><summary><b>项目定位</b></summary>

- TFBS 并非展示型项目，
- 而是用于体现 企业级后端建模、事务控制与工作流设计能力。

</details>
</details>
