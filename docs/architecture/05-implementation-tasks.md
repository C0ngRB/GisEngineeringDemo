docs/architecture/05-implementation-tasks.md

# Codex 实现任务清单

## 总规则

Codex 必须分批实现，不允许一次性重写整个项目。

每一批任务完成后必须输出：

1. 修改文件列表；
2. 每个文件的修改目的；
3. 是否影响已有功能；
4. 如何编译；
5. 如何手动验证；
6. 如果有失败，说明失败原因。

如果架构文档与现有代码冲突，必须先报告冲突，不允许擅自大改。

---

## Batch A：建立解决方案和项目骨架

### 目标

建立 VS2012 可打开的解决方案结构。

### 需要创建

```text
SmallGis.sln

src/
  SmallGis.Domain/
  SmallGis.Application/
  SmallGis.Infrastructure.ArcObjects/
  SmallGis.Presentation.WinForms/
项目引用关系
SmallGis.Domain
  无项目引用

SmallGis.Application
  引用 SmallGis.Domain

SmallGis.Infrastructure.ArcObjects
  引用 SmallGis.Domain
  引用 SmallGis.Application
  引用 ESRI.ArcGIS.*

SmallGis.Presentation.WinForms
  引用 SmallGis.Domain
  引用 SmallGis.Application
  引用 SmallGis.Infrastructure.ArcObjects
  引用 ESRI.ArcGIS.Controls
禁止
不要实现复杂功能；
不要写查询逻辑；
不要引入第三方 DI 框架；
不要使用现代 C# 语法。
验收
VS2012 能打开 Solution；
所有项目能编译；
WinForms 主窗体能启动。
Batch B：实现 Domain 层
目标

实现纯 C# Domain 模型。

需要新增文件
src/SmallGis.Domain/
  Enums/
    LayerType.cs
    GeometryType.cs
    QueryOperator.cs
    SpatialRelationType.cs

  Models/
    LayerInfo.cs
    FieldInfo.cs
    FeatureRecord.cs
    QueryCondition.cs
    SpatialQueryCondition.cs
    QueryResult.cs
    MapExtent.cs
    MapDocumentInfo.cs
规则
不得引用 ESRI.ArcGIS.*；
不得引用 System.Windows.Forms；
不得包含 UI 逻辑；
所有类使用普通 get/set 属性；
使用 C# 5.0 兼容语法。
验收
Domain 项目能单独编译；
搜索 Domain 项目，不得出现 ESRI.ArcGIS。
Batch C：实现 Application Ports 和 UseCases
目标

实现应用层接口和用例编排。

需要新增文件
src/SmallGis.Application/
  Ports/
    IMapDocumentPort.cs
    ILayerCatalogPort.cs
    IFeatureQueryPort.cs
    ISpatialQueryPort.cs
    ISelectionPort.cs
    IMapNavigationPort.cs
    IQueryResultExportPort.cs
    ILoggerPort.cs

  UseCases/
    OpenMapDocumentUseCase.cs
    AddShapefileLayerUseCase.cs
    AddFileGdbLayerUseCase.cs
    ListLayersUseCase.cs
    QueryFeaturesByAttributeUseCase.cs
    QueryFeaturesBySpatialRelationUseCase.cs
    ClearSelectionUseCase.cs
    ExportQueryResultUseCase.cs
规则
Application 只能引用 Domain；
Application 不得引用 ESRI.ArcGIS.*；
UseCase 只做编排和校验；
不要在 UseCase 中写 ArcObjects 代码。
验收
Application 项目能编译；
搜索 Application 项目，不得出现 ESRI.ArcGIS；
每个 UseCase 都通过构造函数注入 Port。
Batch D：实现 Infrastructure.ArcObjects 基础适配器
目标

实现 ArcObjects 和 Clean Architecture 端口之间的适配。

需要新增文件
src/SmallGis.Infrastructure.ArcObjects/
  Adapters/
    ArcMapDocumentAdapter.cs
    ArcLayerCatalogAdapter.cs
    ArcFeatureQueryAdapter.cs
    ArcSpatialQueryAdapter.cs
    ArcSelectionAdapter.cs
    ArcMapNavigationAdapter.cs

  Mappers/
    ArcFeatureMapper.cs
    ArcGeometryMapper.cs
    ArcFieldMapper.cs
    ArcLayerMapper.cs

  Utilities/
    ArcObjectsComReleaser.cs
    ArcObjectsExceptionMapper.cs

  Export/
    CsvQueryResultExporter.cs

  Logging/
    FileLogger.cs
优先实现顺序
FileLogger；
ArcObjectsComReleaser；
ArcGeometryMapper；
ArcLayerMapper；
ArcLayerCatalogAdapter；
ArcMapDocumentAdapter；
ArcFeatureMapper；
ArcFeatureQueryAdapter；
ArcSelectionAdapter；
ArcSpatialQueryAdapter；
ArcMapNavigationAdapter；
CsvQueryResultExporter。
规则
ArcObjects 代码只能集中在 Infrastructure；
Cursor 用完必须释放；
不得在 Infrastructure 中弹 MessageBox；
不得直接依赖具体 Form；
不得把 IFeature 返回到 Application；
不得把 ILayer 返回到 Application；
必须转换成 Domain 模型。
验收
Infrastructure 项目能编译；
ArcObjects 引用集中在 Infrastructure 和 Presentation；
属性查询可返回 QueryResult；
图层列表可返回 IList<LayerInfo>。
Batch E：实现 Presentation.WinForms 主界面
目标

实现可运行、可演示的 WinForms 主界面。

需要新增或修改文件
src/SmallGis.Presentation.WinForms/
  Program.cs
  MainForm.cs
  MainForm.Designer.cs
  Controllers/
    MainFormController.cs
  Composition/
    AppCompositionRoot.cs
  Forms/
    AttributeQueryForm.cs
    SpatialQueryForm.cs
    AttributeTableForm.cs
    LayerManagerForm.cs
    AboutForm.cs
主窗体布局

MainForm 至少包含：

MenuStrip；
ToolStrip；
AxMapControl；
AxTOCControl；
AxToolbarControl；
StatusStrip；
DataGridView 或查询结果面板。
菜单功能

文件：

打开 MXD；
添加 Shapefile；
退出。

查询：

属性查询；
空间查询；
清除选择。

视图：

全图；
放大；
缩小；
平移。

帮助：

关于系统。
规则
MainForm 调用 MainFormController；
MainFormController 调用 UseCase；
MainForm 不直接创建 IQueryFilter；
MainForm 不直接遍历 IFeatureCursor；
MainForm 不直接处理 ArcObjects 查询；
UI 错误提示统一由 MainForm 处理。
验收
软件能启动；
能打开 MXD；
能加载 Shapefile；
能执行属性查询；
能显示查询结果；
能在地图上高亮查询结果；
能清除选择。
Batch F：实现空间查询和属性表查看
目标

增强查询功能。

功能
矩形范围查询；
与当前选择要素相交查询；
属性表查看；
查询结果导出 CSV。
规则
空间查询逻辑放在 ArcSpatialQueryAdapter；
属性表读取放在 ArcFeatureQueryAdapter；
CSV 导出放在 CsvQueryResultExporter；
UI 只收集参数和展示结果。
验收
空间查询可运行；
查询结果能显示；
导出 CSV 文件可打开；
无图层时给出提示；
空结果正常提示。
Batch G：质量检查和报告支撑
目标

补充课程报告需要的内容支撑。

需要补充
docs/
  user-guide.md
  test-plan.md
  quality-management.md
  supervision-plan.md
  progress-plan.md
test-plan.md 至少包含
地图打开测试；
图层加载测试；
属性查询测试；
空间查询测试；
查询结果展示测试；
错误路径测试；
空结果测试；
软件启动和关闭测试。
quality-management.md 至少包含
代码分层规范；
命名规范；
异常处理规范；
日志记录规范；
功能测试规范；
版本管理规范。
supervision-plan.md 至少包含
需求检查；
设计检查；
编码检查；
测试检查；
成果检查；
报告检查。
最终 Definition of Done

项目完成时必须满足：

Solution 能在 VS2012 打开；
项目能编译；
软件能启动；
ArcGIS Engine License 初始化成功；
能加载地图或图层；
能执行属性查询；
能执行空间查询；
能显示查询结果；
能高亮查询结果；
能清除选择；
代码遵守四层架构；
Domain 和 Application 不引用 ArcObjects；
Infrastructure 封装 ArcObjects；
Presentation 不写复杂查询逻辑；
有基本日志；
有基本测试说明；
有报告支撑文档。
```
