# 给 Codex 的总控提示词

你现在负责实现一个 GIS 工程设计课程项目。

项目要求：

- 技术栈：ArcGIS Desktop 10.2、ArcGIS Engine 10.2、Visual Studio 2012、C#、WinForms；
- 软件类型：Windows 桌面端小型 GIS 软件；
- 最低功能：基础地理空间信息查询；
- 架构原则：Clean Architecture 适配版；
- 实现方式：按文档分批实现，不允许重写整个项目。

请先阅读以下架构文件：

1. docs/architecture/01-overview.md
2. docs/architecture/02-module-contracts.md
3. docs/architecture/03-class-diagram.puml
4. docs/architecture/04-sequence-diagrams.puml
5. docs/architecture/05-implementation-tasks.md

## 强制规则

1. 严格遵守架构分层。
2. Domain 层不得引用 ESRI.ArcGIS.\*。
3. Application 层不得引用 ESRI.ArcGIS.\*。
4. ArcObjects 代码只能放在 Infrastructure.ArcObjects 和 Presentation.WinForms。
5. MainForm 不得直接实现属性查询和空间查询细节。
6. UseCase 只负责编排，不直接调用 ArcObjects。
7. Infrastructure Adapter 负责把 ArcObjects 对象转换成 Domain 模型。
8. 不允许引入 Web、REST API、Electron、ArcGIS Pro SDK。
9. 不允许引入大型第三方框架。
10. 不允许使用 VS2012 不支持的现代 C# 语法。
11. 每次只完成一个 Batch。
12. 每次修改前先列出计划修改文件。
13. 每次修改后输出 git diff 摘要。
14. 如果现有代码与架构文档冲突，先报告冲突，不要自行大改。
15. 不要删除已有可运行功能。
16. 不要为了架构纯净性破坏 ArcGIS Engine 控件正常工作。
17. 所有代码必须能够在 VS2012 中编译。

## 当前任务

先执行 Batch A：建立解决方案和项目骨架。

只做以下事情：

1. 创建 SmallGis.sln；
2. 创建四个项目：
   - SmallGis.Domain
   - SmallGis.Application
   - SmallGis.Infrastructure.ArcObjects
   - SmallGis.Presentation.WinForms
3. 设置项目引用关系；
4. 创建空目录结构；
5. 创建 WinForms 主窗体；
6. 确保项目能够编译；
7. 不实现复杂 GIS 功能。

## 完成后必须输出

1. 修改文件列表；
2. 每个文件的作用；
3. 项目引用关系；
4. 编译方式；
5. 手动验证方式；
6. 是否偏离架构文档；
7. 下一批建议任务。
