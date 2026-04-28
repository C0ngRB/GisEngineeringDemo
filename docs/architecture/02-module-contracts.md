`docs/architecture/02-module-contracts.md`

````md
# 模块契约说明

## 1. Domain 层模块契约

### 1.1 LayerInfo

#### 职责

表示地图图层的基础信息。

#### 字段

- string Id
- string Name
- LayerType LayerType
- GeometryType GeometryType
- bool Visible
- string DataSourcePath
- int FeatureCount

#### 禁止

- 不得保存 ILayer；
- 不得保存 IFeatureLayer；
- 不得引用 ESRI.ArcGIS.\*。

---

### 1.2 FieldInfo

#### 职责

表示图层字段信息。

#### 字段

- string Name
- string AliasName
- string FieldType
- bool IsNullable
- bool IsEditable

#### 禁止

- 不得保存 IField。

---

### 1.3 FeatureRecord

#### 职责

表示查询得到的一个要素记录。

#### 字段

- int ObjectId
- string LayerName
- GeometryType GeometryType
- Dictionary<string, object> Attributes

#### 可选字段

- MapExtent Extent

#### 禁止

- 不得保存 IFeature；
- 不得保存 IGeometry。

---

### 1.4 QueryCondition

#### 职责

表示属性查询条件。

#### 字段

- string LayerName
- string FieldName
- QueryOperator Operator
- string Value
- string RawWhereClause

#### 规则

如果 RawWhereClause 不为空，则优先使用 RawWhereClause。

如果 RawWhereClause 为空，则根据 FieldName、Operator、Value 拼接查询表达式。

---

### 1.5 SpatialQueryCondition

#### 职责

表示空间查询条件。

#### 字段

- string TargetLayerName
- SpatialRelationType RelationType
- MapExtent QueryExtent
- double BufferDistance
- string SourceLayerName
- int SourceFeatureObjectId

#### 规则

初始版本至少支持：

- Intersects；
- Contains；
- Within；
- EnvelopeIntersects。

缓冲区查询可以作为第二阶段功能。

---

### 1.6 QueryResult

#### 职责

表示查询结果集合。

#### 字段

- string LayerName
- int TotalCount
- List<FeatureRecord> Records
- string Message

#### 规则

查询无结果时，Records 为空列表，TotalCount 为 0，Message 为“未查询到符合条件的要素”。

---

## 2. Application 层模块契约

### 2.1 IMapDocumentPort

#### 职责

抽象地图文档加载能力。

#### 方法

```csharp
MapDocumentInfo OpenMapDocument(string mxdPath);
void SaveMapDocument(string mxdPath);
bool CanOpen(string mxdPath);
实现位置

由 Infrastructure.ArcObjects 中的 ArcMapDocumentAdapter 实现。

禁止

接口中不得出现 IMapDocument、IMap、AxMapControl。

2.2 ILayerCatalogPort
职责

抽象图层管理能力。

方法
IList<LayerInfo> GetLayers();
LayerInfo AddShapefile(string folderPath, string fileName);
LayerInfo AddFileGdbFeatureClass(string gdbPath, string featureClassName);
void RemoveLayer(string layerName);
void SetLayerVisible(string layerName, bool visible);
IList<FieldInfo> GetFields(string layerName);
实现位置

由 Infrastructure.ArcObjects 中的 ArcLayerCatalogAdapter 实现。

2.3 IFeatureQueryPort
职责

抽象属性查询能力。

方法
QueryResult QueryByAttribute(QueryCondition condition);
IList<FeatureRecord> GetAllFeatures(string layerName, int maxCount);
实现位置

由 Infrastructure.ArcObjects 中的 ArcFeatureQueryAdapter 实现。

规则
查询条件转换为 IQueryFilter 的逻辑放在 Infrastructure；
Application 只负责调用；
Domain 只保存 QueryCondition。
2.4 ISpatialQueryPort
职责

抽象空间查询能力。

方法
QueryResult QueryByExtent(SpatialQueryCondition condition);
QueryResult QueryBySelectedFeature(SpatialQueryCondition condition);
实现位置

由 Infrastructure.ArcObjects 中的 ArcSpatialQueryAdapter 实现。

2.5 ISelectionPort
职责

抽象地图选择和高亮能力。

方法
void SelectFeatures(string layerName, IList<int> objectIds);
void ClearSelection();
void FlashFeatures(string layerName, IList<int> objectIds);
实现位置

由 Infrastructure.ArcObjects 中的 ArcSelectionAdapter 实现。

2.6 IMapNavigationPort
职责

抽象地图浏览能力。

方法
void ZoomIn();
void ZoomOut();
void Pan();
void FullExtent();
void ZoomToLayer(string layerName);
void ZoomToSelectedFeatures();
实现位置

由 Infrastructure.ArcObjects 中的 ArcMapNavigationAdapter 实现。

2.7 IQueryResultExportPort
职责

抽象查询结果导出能力。

方法
void ExportToCsv(QueryResult result, string outputPath);
实现位置

由 Infrastructure.ArcObjects 或 Infrastructure.FileSystem 中的 CsvQueryResultExporter 实现。

2.8 ILoggerPort
职责

抽象日志记录能力。

方法
void Info(string message);
void Warn(string message);
void Error(string message);
void Error(string message, Exception ex);
实现位置

由 FileLogger 实现。

3. UseCase 契约
3.1 OpenMapDocumentUseCase
输入
string mxdPath
输出
MapDocumentInfo
调用
IMapDocumentPort.OpenMapDocument
ILayerCatalogPort.GetLayers
ILoggerPort.Info
禁止
不得直接调用 AxMapControl；
不得直接使用 IMapDocument。
3.2 AddShapefileLayerUseCase
输入
string folderPath
string fileName
输出
LayerInfo
调用
ILayerCatalogPort.AddShapefile
ILoggerPort.Info
3.3 QueryFeaturesByAttributeUseCase
输入
QueryCondition
输出
QueryResult
调用
IFeatureQueryPort.QueryByAttribute
ISelectionPort.SelectFeatures
ILoggerPort.Info
规则

查询成功后，应将查询结果同步到地图选择集。

禁止
不得在 UI 中直接执行查询；
不得将 IFeature 暴露给 UI。
3.4 QueryFeaturesBySpatialRelationUseCase
输入
SpatialQueryCondition
输出
QueryResult
调用
ISpatialQueryPort.QueryByExtent 或 QueryBySelectedFeature
ISelectionPort.SelectFeatures
ILoggerPort.Info
3.5 ClearSelectionUseCase
输入

无。

输出

无。

调用
ISelectionPort.ClearSelection
3.6 ExportQueryResultUseCase
输入
QueryResult
string outputPath
输出

无。

调用
IQueryResultExportPort.ExportToCsv
4. Infrastructure 层模块契约
4.1 ArcMapDocumentAdapter
职责

封装 MXD 打开、保存、地图文档检查。

依赖
AxMapControl 或 IMapControl3；
IMapDocument；
ESRI.ArcGIS.Carto；
ESRI.ArcGIS.Controls。
实现接口
IMapDocumentPort。
禁止
不得弹出 WinForms MessageBox；
不得处理 UI 展示；
错误通过异常或 Result 返回给 Application。
4.2 ArcLayerCatalogAdapter
职责

封装图层加载、删除、可见性控制、字段读取。

依赖
IMap；
ILayer；
IFeatureLayer；
IFeatureClass；
IWorkspaceFactory。
实现接口
ILayerCatalogPort。
输出

必须转换为 Domain 中的 LayerInfo 和 FieldInfo。

4.3 ArcFeatureQueryAdapter
职责

封装属性查询。

依赖
IFeatureLayer；
IFeatureClass；
IQueryFilter；
IFeatureCursor。
实现接口
IFeatureQueryPort。
输出

必须转换为 QueryResult。

COM 规则

IFeatureCursor 使用结束后必须释放。

4.4 ArcSpatialQueryAdapter
职责

封装空间查询。

依赖
ISpatialFilter；
IGeometry；
IEnvelope；
IFeatureCursor。
实现接口
ISpatialQueryPort。
规则

空间关系类型由 SpatialRelationType 映射到 ArcObjects esriSpatialRelEnum。

4.5 ArcSelectionAdapter
职责

封装地图选择、高亮、清除选择。

依赖
IMap；
IFeatureSelection；
ISelectionSet。
实现接口
ISelectionPort。
4.6 ArcGeometryMapper
职责

在 ArcObjects 几何对象和 Domain 几何描述之间做转换。

允许
IEnvelope -> MapExtent；
esriGeometryType -> GeometryType。
禁止
不得将 IGeometry 返回给 Domain 或 Application。
4.7 ArcFeatureMapper
职责

将 IFeature 转换为 FeatureRecord。

输入
IFeature；
string layerName。
输出
FeatureRecord。
规则

Attributes 字典应排除 Shape 字段，保留 OBJECTID 字段。

5. Presentation 层模块契约
5.1 MainForm
职责

主界面。

包含控件
AxMapControl；
AxTOCControl；
AxToolbarControl；
MenuStrip；
ToolStrip；
StatusStrip；
SplitContainer；
DataGridView。
允许
调用 UseCase；
展示返回结果；
显示错误提示；
将用户输入转换为 Request 或 Condition。
禁止
不得直接创建 IQueryFilter；
不得直接创建 ISpatialFilter；
不得直接遍历 IFeatureCursor；
不得直接构造 QueryResult；
不得写复杂业务逻辑。
5.2 AttributeQueryForm
职责

收集属性查询条件。

输出
QueryCondition。
禁止
不得执行查询；
不得访问 ArcObjects。
5.3 SpatialQueryForm
职责

收集空间查询条件。

输出
SpatialQueryCondition。
禁止
不得执行查询；
不得访问 ArcObjects。
5.4 AttributeTableForm
职责

显示属性表或查询结果。

输入
QueryResult 或 IList<FeatureRecord>。
禁止
不得访问 IFeatureClass；
不得访问 IFeatureCursor。
5.5 AppCompositionRoot
职责

装配系统依赖。

规则

AppCompositionRoot 可以持有 AxMapControl，并将其传给 Infrastructure Adapter。

示例职责
创建 FileLogger；
创建 ArcMapDocumentAdapter；
创建 ArcLayerCatalogAdapter；
创建 ArcFeatureQueryAdapter；
创建 ArcSpatialQueryAdapter；
创建 ArcSelectionAdapter；
创建 UseCase；
注入 MainFormController。
6. 错误处理契约
6.1 用户可恢复错误

例如：

文件不存在；
图层不存在；
字段不存在；
查询条件为空；
查询结果为空。

处理方式：

Application 返回空结果或抛出业务异常；
Presentation 捕获后用 MessageBox 展示；
Logger 记录 Warn。
6.2 系统错误

例如：

ArcObjects COM 异常；
Workspace 打开失败；
MXD 无法加载；
游标读取异常。

处理方式：

Infrastructure 捕获后包装为 ApplicationException 或 GisOperationException；
Application 记录日志；
Presentation 展示错误提示。
7. 日志契约

日志至少记录：

软件启动；
打开地图文档；
添加图层；
属性查询条件；
属性查询结果数量；
空间查询条件；
空间查询结果数量；
导出结果路径；
异常信息。

日志文件建议保存到：

logs/
  smallgis_yyyyMMdd.log
8. 最小验收功能契约

Codex 必须优先实现以下功能：

主窗口启动；
ArcGIS Engine License 初始化；
AxMapControl 正常显示；
打开 MXD；
添加 Shapefile；
图层列表读取；
属性查询；
查询结果 DataGridView 展示；
查询结果地图高亮；
清除选择。

其余功能可以作为第二阶段。
```
````
