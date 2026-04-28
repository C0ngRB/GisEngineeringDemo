# GisEngineeringDemo

基于 ArcGIS Engine 10.2、C# 和 WinForms 的桌面端小型 GIS 工程设计课程项目。项目目标是实现一个可在 Windows 桌面运行的基础 GIS 查询软件，支持地图加载、图层管理、属性查询、空间查询、查询结果展示、高亮选择和 CSV 导出。

这个仓库适合用于 GIS 工程设计、ArcGIS Engine 桌面开发、WinForms GIS 软件架构实践和课程实习报告支撑。

## 项目能做什么

当前版本已经实现以下核心能力：

- 打开 ArcMap 地图文档 `*.mxd`
- 添加 Shapefile 图层
- 显示地图和 TOC 图层目录
- 查看图层列表
- 执行属性查询
- 执行当前地图范围空间查询
- 执行基于当前选中要素的空间查询
- 查询结果同步到地图选择集
- 查询结果在表格中展示
- 查看图层属性表
- 导出查询结果为 CSV
- 清除选择
- 地图全图、放大、缩小、平移
- 基础文件日志记录

## 技术栈

- ArcGIS Desktop / ArcGIS Engine
- ArcObjects SDK for .NET
- Visual Studio 2012
- C#
- WinForms
- .NET Framework 4.0
- Windows 桌面应用

项目不使用 Web、REST API、Electron、ArcGIS Pro SDK、Entity Framework 或大型第三方框架。

## 架构设计

项目采用 Clean Architecture 适配版桌面 GIS 架构，分为四层：

```text
Presentation.WinForms  ->  Application  ->  Domain
Infrastructure.ArcObjects  ->  Application / Domain
```

各层职责：

- `SmallGis.Domain`：核心领域模型，不引用 ArcObjects，不引用 WinForms。
- `SmallGis.Application`：端口接口和 UseCase 编排，不直接调用 ArcObjects。
- `SmallGis.Infrastructure.ArcObjects`：封装 ArcObjects，实现 Application 端口。
- `SmallGis.Presentation.WinForms`：WinForms 界面、ArcGIS Engine 控件和用户交互。

核心约束：

- Domain 层不得引用 `ESRI.ArcGIS.*`
- Application 层不得引用 `ESRI.ArcGIS.*`
- ArcObjects 代码集中在 `Infrastructure.ArcObjects` 和 `Presentation.WinForms`
- MainForm 不直接创建 `IQueryFilter`、`ISpatialFilter`，不直接遍历 `IFeatureCursor`
- UseCase 只做流程编排，不写 ArcObjects 查询细节

## 目录结构

```text
workspace/
  code/
    SmallGis.sln
    src/
      SmallGis.Domain/
      SmallGis.Application/
      SmallGis.Infrastructure.ArcObjects/
      SmallGis.Presentation.WinForms/

  docs/
    architecture/
      01-overview.md
      02-module-contracts.md
      03-class-diagram.puml
      04-sequence-diagrams.puml
      05-implementation-tasks.md
      06-codex-master-prompt.md
    internship-report.md
```

## 已完成进度

- Batch A：建立解决方案和项目骨架
- Batch B：实现 Domain 层模型
- Batch C：实现 Application Ports 和 UseCases
- Batch D：实现 Infrastructure.ArcObjects 基础适配器
- Batch E：实现 WinForms 主界面、Controller 和 CompositionRoot
- Batch F：实现空间查询、属性表查看和 CSV 导出

主要 Git 提交：

```text
3b6527b Initial SmallGis architecture skeleton
17f8de6 Implement ArcObjects adapters and WinForms shell
b6d3ea5 Implement spatial query table export features
```

## 如何构建

推荐环境：

- Windows
- Visual Studio 2012
- .NET Framework 4.0 Targeting Pack
- ArcGIS Desktop / Engine 10.2
- ArcObjects SDK for .NET

用 VS2012 打开：

```text
code/SmallGis.sln
```

命令行构建示例：

```powershell
C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe code\SmallGis.sln /t:Build /p:Configuration=Debug /p:Platform="Any CPU"
```

ArcGIS Engine 桌面程序通常建议使用 `x86` 平台。在 VS2012 中可通过“生成 -> 配置管理器”将解决方案平台改为 `x86`。

## 如何运行

1. 安装 ArcGIS Desktop / ArcGIS Engine 和 ArcObjects SDK。
2. 用 Visual Studio 2012 打开 `code/SmallGis.sln`。
3. 将 `SmallGis.Presentation.WinForms` 设置为启动项目。
4. 编译并运行。
5. 在主界面中打开 MXD 或添加 Shapefile。
6. 选择图层后执行属性查询或空间查询。

## 手动验证建议

可以按以下流程验收：

1. 启动软件，确认 ArcGIS Engine License 初始化成功。
2. 打开一个 MXD 文件，确认地图显示和 TOC 刷新。
3. 添加一个 Shapefile，确认图层列表出现图层名称。
4. 选择图层，执行属性查询，确认结果表有记录。
5. 执行当前地图范围空间查询，确认结果可显示。
6. 选择地图要素后执行选中要素空间查询。
7. 查看属性表。
8. 导出 CSV，并用表格软件打开检查内容。
9. 点击清除选择，确认地图高亮选择被清除。

## 文档

架构文档位于：

```text
docs/architecture/
```

实习报告位于：

```text
docs/internship-report.md
```

报告内容包括实习内容、进度规划、质量管理办法、监理办法、实习成果描述和实习总结。

## 当前注意事项

- 当前开发机器构建时可能提示缺少 `.NET Framework 4.0 Targeting Pack`，正式环境建议安装 VS2012 或对应 .NET 4.0 开发组件。
- ArcGIS Engine 10.2 常见部署环境为 32 位，建议最终将项目平台配置为 `x86`。
- 本项目为课程实习项目，重点是桌面 GIS 功能演示和 Clean Architecture 分层实践。
