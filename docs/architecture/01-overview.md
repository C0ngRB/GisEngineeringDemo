docs/architecture/01-overview.md

# GIS 工程设计课程项目架构总览

## 1. 项目目标

本项目是一个基于 ArcGIS Engine 10.2、Visual Studio 2012、C#、WinForms 的小型桌面 GIS 软件。
最终软件至少应实现基础地理空间信息查询功能，包括：

1. 地图文档加载；
2. Shapefile 或 File Geodatabase 图层加载；
3. 图层显示控制；
4. 地图浏览；
5. 属性查询；
6. 空间查询；
7. 查询结果列表展示；
8. 查询结果在地图上高亮显示；
9. 清除选择；
10. 基础属性表查看。
    本项目不是 WebGIS，不是前后端分离系统，不是微服务系统，不是 ArcGIS Pro 插件。

## 2. 技术栈约束

必须使用：

- ArcGIS Desktop 10.2；
- ArcGIS Engine 10.2；
- ArcObjects SDK for .NET；
- Visual Studio 2012；
- C#；
- WinForms；
- .NET Framework 项目；
- Windows 桌面应用。
  禁止引入：
- ASP.NET；
- REST API 服务；
- Web 前端；
- Electron；
- ArcGIS Pro SDK；
- QGIS；
- Python GIS 后端；
- 微服务；
- SOA；
- Entity Framework；
- 大型第三方依赖框架。

## 3. 架构风格

本系统采用 Clean Architecture 适配版桌面 GIS 架构。
系统分为四层：

1. Domain 层；
2. Application 层；
3. Infrastructure 层；
4. Presentation 层。
   依赖方向必须向内：

```text
Presentation.WinForms  →  Application  →  Domain
Infrastructure.ArcObjects  →  Application / Domain
Domain 层不能依赖任何外部框架。
Application 层不能依赖 WinForms，也不能依赖 ArcGIS Engine 控件。
Infrastructure 层允许依赖 ArcObjects。
Presentation 层允许依赖 WinForms 和 ArcGIS Engine 控件，但不得写复杂 GIS 业务逻辑。

4. 分层职责
4.1 Domain 层
Domain 层负责表达系统核心概念，不负责调用 ArcGIS Engine。
Domain 层包含：
LayerInfo；
FieldInfo；
FeatureRecord；
QueryCondition；
QueryResult；
SpatialQueryCondition；
MapExtent；
GeometryType；
LayerType；
QueryOperator；
SpatialRelationType。

Domain 层不得包含：
AxMapControl；
IMap；
ILayer；
IFeature；
IFeatureClass；
IWorkspace；
IGeometry；
IEnvelope；
ISpatialFilter；
IQueryFilter；
ESRI.ArcGIS 命名空间。

4.2 Application 层
Application 层负责编排用例。
Application 层包含：
OpenMapDocumentUseCase；
AddShapefileLayerUseCase；
AddFileGdbLayerUseCase；
ListLayersUseCase；
QueryFeaturesByAttributeUseCase；
QueryFeaturesBySpatialRelationUseCase；
IdentifyFeatureUseCase；
ShowAttributeTableUseCase；
ClearSelectionUseCase；
ZoomToSelectedFeaturesUseCase；
ExportQueryResultUseCase。

Application 层定义端口接口：
IMapDocumentPort；
ILayerCatalogPort；
IFeatureQueryPort；
ISpatialQueryPort；
ISelectionPort；
IMapNavigationPort；
IQueryResultExportPort；
ILoggerPort。

Application 层不直接依赖 ArcObjects。

4.3 Infrastructure 层
Infrastructure 层负责实现 Application 层定义的端口接口。
Infrastructure 层可以依赖 ArcObjects。
Infrastructure 层包含：
ArcMapDocumentAdapter；
ArcLayerCatalogAdapter；
ArcFeatureQueryAdapter；
ArcSpatialQueryAdapter；
ArcSelectionAdapter；
ArcMapNavigationAdapter；
CsvQueryResultExporter；
FileLogger；
ArcObjectsComReleaser；
ArcGeometryMapper；
ArcFeatureMapper。

Infrastructure 层职责：

加载 MXD；
加载 Shapefile；
加载 File Geodatabase 图层；
使用 IQueryFilter 执行属性查询；
使用 ISpatialFilter 执行空间查询；
将 IFeature 转换为 FeatureRecord；
将 IEnvelope 转换为 MapExtent；
将查询结果同步到地图选择集；
释放 ArcObjects COM 对象；
导出查询结果 CSV。
4.4 Presentation 层

Presentation 层负责用户界面和交互。

Presentation 层包含：

MainForm；
AttributeQueryForm；
SpatialQueryForm；
AttributeTableForm；
LayerManagerForm；
AboutForm；
MainFormPresenter 或 MainFormController；
AppCompositionRoot。

Presentation 层可以使用：

AxMapControl；
AxTOCControl；
AxToolbarControl；
AxLicenseControl；
WinForms 控件。

Presentation 层不得直接实现：

属性查询算法；
空间查询算法；
ArcObjects 数据访问细节；
CSV 导出逻辑；
COM 对象释放策略。
5. 依赖规则
允许
Presentation 调用 Application UseCase；
Application 调用 Domain Model；
Application 调用 Port 接口；
Infrastructure 实现 Application Port；
Infrastructure 调用 ArcObjects；
Presentation 通过 CompositionRoot 装配 Infrastructure 实现。
禁止
Domain 引用 ESRI.ArcGIS.*；
Domain 引用 System.Windows.Forms；
Application 引用 AxMapControl；
Application 引用 IFeature、ILayer、IMap；
UseCase 中直接写 ArcObjects 查询代码；
MainForm 中直接拼接大量 ArcObjects 查询逻辑；
Infrastructure 反向调用 WinForms；
查询窗口直接访问 ArcObjects；
为了一个按钮点击事件重写整个系统。
6. 架构目标

本架构的目标是：

降低 MainForm.cs 的复杂度；
将 ArcObjects 调用集中到 Infrastructure 层；
让查询、图层、地图浏览等功能以 UseCase 组织；
便于课程报告撰写系统架构、功能模块、质量管理办法；
便于 Codex 按模块逐步实现；
避免一次性生成不可维护的大型 MainForm 代码。
7. 非功能要求
7.1 可维护性
每个类只承担一个主要职责；
MainForm 不得超过合理长度；
查询逻辑不得散落在多个按钮事件中；
公共转换逻辑放入 Mapper 或 Helper。
7.2 可演示性

软件必须能够在课堂验收时完成以下演示流程：

启动软件；
打开地图文档或加载图层；
查看图层列表；
执行属性查询；
执行空间查询；
查看查询结果表；
地图高亮查询结果；
清除查询结果；
放大、缩小、平移、全图显示。
7.3 稳定性
打开不存在的数据路径时应给出错误提示；
查询字段不存在时应给出错误提示；
查询条件为空时应给出错误提示；
无图层时不得执行查询；
空查询结果应正常显示“未查询到结果”；
ArcObjects COM 游标使用后应释放。
7.4 兼容性

代码必须适配 Visual Studio 2012 和 ArcGIS Engine 10.2。

不得使用现代 C# 语法特性，例如：

record；
nullable reference types；
async Main；
pattern matching；
target-typed new；
expression-bodied members 的复杂写法；
.NET 6 或更高版本 API。
8. 推荐解决方案结构

建议使用一个 VS2012 Solution，包含四个项目：

SmallGis.sln

src/
  SmallGis.Domain/
    SmallGis.Domain.csproj

  SmallGis.Application/
    SmallGis.Application.csproj

  SmallGis.Infrastructure.ArcObjects/
    SmallGis.Infrastructure.ArcObjects.csproj

  SmallGis.Presentation.WinForms/
    SmallGis.Presentation.WinForms.csproj
```
