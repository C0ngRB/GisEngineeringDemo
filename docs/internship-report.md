# GIS 工程设计实习报告

## 一、实习内容

本次实习围绕一个基于 ArcGIS Engine 的桌面端小型 GIS 软件展开，目标是在 Visual Studio 2012、C#、WinForms 和 ArcGIS Engine 10.2 技术环境下，完成一个具备基础地理空间信息查询能力的课程项目。项目根目录为 `F:\GisDevelop\workspace`，代码统一放置在 `code` 目录，文档统一放置在 `docs` 目录。

实习的主要内容包括：

1. 阅读并理解 GIS 软件的架构设计文档，明确项目技术栈、功能范围、分层原则和实现批次。
2. 搭建 Visual Studio 2012 可打开的解决方案和项目骨架，建立 Domain、Application、Infrastructure、Presentation 四层结构。
3. 实现 Domain 层模型，包括图层信息、字段信息、查询条件、空间查询条件、查询结果、地图范围等核心数据结构。
4. 实现 Application 层端口接口和用例类，使业务流程通过 UseCase 编排，不直接依赖 ArcObjects。
5. 实现 Infrastructure.ArcObjects 层适配器，将 ArcObjects 能力封装为 Application 层端口，包括地图文档加载、图层管理、属性查询、空间查询、选择集操作、地图浏览、CSV 导出和日志记录。
6. 实现 Presentation.WinForms 主界面，集成 ArcGIS Engine 地图控件、TOC 控件、工具条、菜单栏、状态栏、图层列表和查询结果表格。
7. 增强查询功能，实现矩形范围空间查询、基于当前选择要素的空间查询、属性表查看、查询结果展示和 CSV 导出。
8. 使用 Git 和 GitHub 管理项目版本，按批次提交和推送代码。

项目严格遵循 Clean Architecture 适配版分层规则。Domain 层和 Application 层不引用 `ESRI.ArcGIS.*`，ArcObjects 调用集中在 Infrastructure.ArcObjects 和 Presentation.WinForms 层，MainForm 只负责界面交互和结果展示，不直接实现属性查询或空间查询细节。

## 二、进度规划

本项目采用分批实现的方式推进，每一批只完成明确范围内的任务，避免一次性重写或生成过大的不可维护代码。

### 1. Batch A：解决方案和项目骨架

目标是建立 `SmallGis.sln` 和四个项目：

- `SmallGis.Domain`
- `SmallGis.Application`
- `SmallGis.Infrastructure.ArcObjects`
- `SmallGis.Presentation.WinForms`

本阶段完成了项目引用关系、空目录结构、WinForms 主窗体骨架和基础编译验证。

### 2. Batch B：Domain 层模型

目标是实现纯 C# Domain 模型和枚举。已完成：

- `LayerInfo`
- `FieldInfo`
- `FeatureRecord`
- `QueryCondition`
- `SpatialQueryCondition`
- `QueryResult`
- `MapExtent`
- `MapDocumentInfo`
- `LayerType`
- `GeometryType`
- `QueryOperator`
- `SpatialRelationType`

该阶段确保 Domain 层不包含 ArcObjects、WinForms 或 UI 逻辑。

### 3. Batch C：Application Ports 和 UseCases

目标是实现 Application 层端口接口和用例编排。已完成地图文档、图层目录、属性查询、空间查询、选择集、地图浏览、查询结果导出和日志等端口接口，并实现打开地图、添加 Shapefile、查询、清除选择、导出等 UseCase。

该阶段重点是保证 Application 层只依赖 Domain 层，通过接口抽象外部能力。

### 4. Batch D：Infrastructure.ArcObjects 基础适配器

目标是实现 ArcObjects 与 Application Port 之间的适配。已完成：

- 地图文档适配器
- 图层目录适配器
- 属性查询适配器
- 空间查询适配器
- 选择集适配器
- 地图浏览适配器
- ArcObjects 到 Domain 模型的 Mapper
- COM 对象释放工具
- CSV 导出器
- 文件日志器

该阶段将 ArcObjects 代码集中在 Infrastructure 层，避免向 Domain 或 Application 层泄漏。

### 5. Batch E：Presentation.WinForms 主界面

目标是实现可运行的 WinForms 主界面，并通过 Controller 和 CompositionRoot 装配系统依赖。已完成：

- `MainForm`
- `MainFormController`
- `AppCompositionRoot`
- 属性查询窗体
- 空间查询窗体
- 属性表窗体
- 图层管理窗体
- 关于窗体

主界面包含菜单栏、工具栏、地图控件、TOC 控件、图层列表、查询结果表格和状态栏。

### 6. Batch F：空间查询和属性表查看增强

目标是增强查询功能。已完成：

- 当前地图范围空间查询
- 当前选中要素空间查询
- 属性表查看
- 查询结果 CSV 导出
- 无图层、未选图层、空结果等提示处理

该阶段进一步完善了课堂演示所需的核心查询流程。

### 7. 后续 Batch G：质量检查和报告支撑

后续可继续补充用户手册、测试计划、质量管理文档、监理计划、进度计划等材料，使项目成果更适合课程验收和报告归档。

## 三、质量管理办法

为保证项目质量，本实习项目从架构、编码、编译、验证和版本管理几个方面进行控制。

### 1. 架构质量控制

项目采用四层结构：

- Domain 层负责核心模型。
- Application 层负责端口和用例编排。
- Infrastructure.ArcObjects 层负责 ArcObjects 适配。
- Presentation.WinForms 层负责界面交互。

质量控制要求如下：

1. Domain 层不得引用 `ESRI.ArcGIS.*` 或 `System.Windows.Forms`。
2. Application 层不得引用 ArcObjects 或 WinForms 控件。
3. ArcObjects 查询、图层访问、空间过滤和 COM 释放逻辑集中在 Infrastructure.ArcObjects。
4. MainForm 不直接创建 `IQueryFilter`、`ISpatialFilter`，不直接遍历 `IFeatureCursor`。
5. UseCase 只负责编排和基础校验，不直接调用 ArcObjects。

### 2. 编码质量控制

编码时遵循 Visual Studio 2012 和 C# 5.0 兼容要求，避免使用现代 C# 语法，例如 record、nullable reference types、pattern matching、target-typed new、async Main 等。

代码命名尽量保持职责清晰：

- 端口接口以 `I...Port` 命名。
- 用例类以 `...UseCase` 命名。
- ArcObjects 适配器以 `Arc...Adapter` 命名。
- 转换类以 `...Mapper` 命名。
- UI 窗体以 `...Form` 命名。

### 3. 编译质量控制

每个批次完成后都执行解决方案编译，确认无编译错误。当前项目可通过以下命令编译：

```powershell
C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe F:\GisDevelop\workspace\code\SmallGis.sln /t:Build /p:Configuration=Debug /p:Platform="Any CPU"
```

当前构建存在环境相关警告，包括缺少 `.NET Framework 4.0 Targeting Pack` 和 AnyCPU/x86 架构提示。它们不属于代码错误，但在正式验收环境中建议安装 VS2012/.NET 4.0 开发组件，并将 ArcGIS Engine 程序平台调整为 x86。

### 4. 功能质量控制

功能验证重点包括：

1. 软件能正常启动。
2. ArcGIS Engine License 能初始化。
3. 地图控件能显示。
4. 能打开 MXD。
5. 能添加 Shapefile。
6. 能读取图层列表。
7. 能执行属性查询。
8. 能执行空间查询。
9. 查询结果能在表格中展示。
10. 查询结果能同步到地图选择集。
11. 能清除选择。
12. 能导出 CSV。

### 5. 版本质量控制

项目使用 Git 管理版本，并推送到 GitHub 仓库：

```text
https://github.com/C0ngRB/GisEngineeringDemo.git
```

目前主要提交包括：

- `3b6527b Initial SmallGis architecture skeleton`
- `17f8de6 Implement ArcObjects adapters and WinForms shell`
- `b6d3ea5 Implement spatial query table export features`

每个阶段提交前均进行编译验证，提交信息描述对应批次的核心功能。

## 四、监理办法

本项目的监理办法主要用于保证实习任务按计划、按架构、按质量要求推进。

### 1. 需求监理

需求监理重点检查项目是否围绕课程要求展开，包括：

1. 是否使用 ArcGIS Engine 10.2、C#、WinForms 和 Visual Studio 2012 技术栈。
2. 是否定位为 Windows 桌面端 GIS 软件。
3. 是否实现基础地理空间信息查询功能。
4. 是否避免引入 Web、REST API、Electron、ArcGIS Pro SDK 或大型第三方框架。

### 2. 架构监理

架构监理重点检查分层边界：

1. Domain 层是否保持纯模型。
2. Application 层是否只通过端口接口访问外部能力。
3. Infrastructure 层是否集中封装 ArcObjects。
4. Presentation 层是否只负责界面交互和结果展示。
5. MainForm 是否避免直接实现复杂查询逻辑。

### 3. 编码监理

编码监理重点检查：

1. 是否使用 VS2012 支持的语法。
2. 文件是否放在正确目录。
3. 类名、接口名、方法名是否表达清楚。
4. 是否存在无关重构或破坏已有功能的修改。
5. 是否按批次逐步实现，而不是一次性大改。

### 4. 编译监理

每个批次完成后执行编译检查。编译结果中如果出现错误，必须先修复后再进入下一批。对环境警告进行记录和说明，不将其误判为代码逻辑错误。

### 5. 功能监理

功能监理采用手动验证方式，围绕课堂演示流程逐项检查：

1. 启动软件。
2. 打开地图文档或添加图层。
3. 查看图层列表。
4. 执行属性查询。
5. 执行空间查询。
6. 查看查询结果。
7. 清除地图选择。
8. 导出查询结果。

### 6. 成果监理

成果监理检查最终交付是否包括：

1. 可打开的 VS2012 Solution。
2. 完整代码目录。
3. 架构设计文档。
4. 实习报告。
5. Git 版本记录。
6. GitHub 远程仓库。

## 五、实习成果描述

本次实习已形成一个具有 Clean Architecture 分层结构的小型 GIS 桌面软件原型。项目当前具备以下成果：

### 1. 工程结构成果

已建立 `SmallGis.sln`，包含四个项目：

```text
SmallGis.Domain
SmallGis.Application
SmallGis.Infrastructure.ArcObjects
SmallGis.Presentation.WinForms
```

项目引用方向符合架构要求：

```text
Presentation.WinForms -> Application -> Domain
Infrastructure.ArcObjects -> Application / Domain
```

### 2. Domain 层成果

Domain 层定义了图层、字段、要素记录、查询条件、空间查询条件、查询结果、地图范围和地图文档等模型。这些模型不依赖 ArcObjects，可作为系统核心数据结构。

### 3. Application 层成果

Application 层定义了地图文档、图层目录、属性查询、空间查询、选择集、地图浏览、查询结果导出和日志等端口接口，并通过 UseCase 编排业务流程。

### 4. Infrastructure 层成果

Infrastructure.ArcObjects 层实现了 ArcObjects 适配器，能够将 ArcGIS Engine 的地图、图层、字段、要素和几何对象转换为 Domain 模型，并支持查询、选择、地图浏览、CSV 导出和日志记录。

### 5. Presentation 层成果

Presentation.WinForms 层实现了主界面和多个交互窗体，界面包括：

- 地图显示区域
- TOC 图层目录
- ArcGIS 工具条
- 图层列表
- 查询结果表格
- 菜单栏
- 工具栏
- 状态栏

支持的主要功能包括：

1. 打开 MXD。
2. 添加 Shapefile。
3. 查看图层列表。
4. 属性查询。
5. 当前范围空间查询。
6. 当前选中要素空间查询。
7. 属性表查看。
8. 查询结果展示。
9. 查询结果 CSV 导出。
10. 地图高亮选择。
11. 清除选择。
12. 地图全图、放大、缩小、平移。

### 6. 版本管理成果

项目已关联 GitHub 仓库并完成多次提交。代码按批次推进，便于回溯每一阶段的实现内容。

## 六、实习总结

通过本次 GIS 工程设计实习，我完成了从架构阅读、项目搭建、分层实现、ArcObjects 适配、WinForms 界面开发到版本管理的完整实践过程。

本次实习的重点不只是实现功能，还包括如何组织一个可维护的 GIS 桌面软件。传统 ArcGIS Engine WinForms 项目容易把地图加载、图层管理、属性查询、空间查询和 UI 事件全部写在 MainForm 中，导致窗体代码越来越复杂。本项目通过 Domain、Application、Infrastructure、Presentation 四层结构，将核心模型、业务编排、ArcObjects 调用和界面交互分离，使代码职责更加清晰。

在实现过程中，Domain 和 Application 层保持了对 ArcObjects 的隔离，Infrastructure 层负责处理 ArcObjects 对象、游标、过滤器和 COM 释放，Presentation 层只负责用户输入、调用 Controller 和展示结果。这种方式有利于后续维护，也方便在课程报告中说明系统架构和模块职责。

本次实习也暴露出一些实际工程问题。例如 ArcGIS Engine 依赖 32 位 COM 环境，项目在命令行构建时会出现 AnyCPU 和 x86 架构提示；当前机器缺少 .NET Framework 4.0 Targeting Pack，也会产生构建警告。这些问题说明 GIS 桌面软件开发不仅要关注代码逻辑，也要关注运行环境、SDK 安装、目标框架和平台配置。

总体来看，本项目已经完成基础 GIS 查询软件的主要框架和核心功能，能够支撑课堂演示和后续报告撰写。后续可以继续完善测试计划、用户手册、质量管理文档和监理计划，并在真实 ArcGIS Engine 10.2 + VS2012 环境中进行完整手动验收。
