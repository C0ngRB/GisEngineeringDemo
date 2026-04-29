using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmallGis.Domain.Models;
using SmallGis.Presentation.WinForms.Composition;
using SmallGis.Presentation.WinForms.Controllers;
using SmallGis.Presentation.WinForms.Forms;

namespace SmallGis.Presentation.WinForms
{
    /// <summary>
    /// Main shell form. It handles UI events and delegates GIS workflows to MainFormController. / 主界面窗体，处理 UI 事件并将 GIS 流程委托给 MainFormController。
    /// </summary>
    public partial class MainForm : Form
    {
        private MainFormController controller;
        private QueryResult lastQueryResult;

        public MainForm()
        {
            InitializeComponent();
            WireEvents();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                axTocControl.SetBuddyControl(axMapControl);
                axToolbarControl.SetBuddyControl(axMapControl);
                // Dependency composition is isolated from the form constructor so controls are initialized first. / 依赖装配从窗体构造函数中分离，确保控件先完成初始化。
                controller = AppCompositionRoot.Create(axMapControl);
                SetStatus("Ready");
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void WireEvents()
        {
            openMxdMenuItem.Click += OpenMxdMenuItemClick;
            openMxdToolButton.Click += OpenMxdMenuItemClick;
            addShapefileMenuItem.Click += AddShapefileMenuItemClick;
            addShapefileToolButton.Click += AddShapefileMenuItemClick;
            attributeQueryMenuItem.Click += AttributeQueryMenuItemClick;
            attributeQueryToolButton.Click += AttributeQueryMenuItemClick;
            spatialQueryMenuItem.Click += SpatialQueryMenuItemClick;
            showAttributeTableMenuItem.Click += ShowAttributeTableMenuItemClick;
            attributeTableToolButton.Click += ShowAttributeTableMenuItemClick;
            exportCsvMenuItem.Click += ExportCsvMenuItemClick;
            exportCsvToolButton.Click += ExportCsvMenuItemClick;
            clearSelectionMenuItem.Click += ClearSelectionMenuItemClick;
            clearSelectionToolButton.Click += ClearSelectionMenuItemClick;
            fullExtentMenuItem.Click += FullExtentMenuItemClick;
            fullExtentToolButton.Click += FullExtentMenuItemClick;
            zoomInMenuItem.Click += ZoomInMenuItemClick;
            zoomOutMenuItem.Click += ZoomOutMenuItemClick;
            panMenuItem.Click += PanMenuItemClick;
            layerManagerMenuItem.Click += LayerManagerMenuItemClick;
            aboutMenuItem.Click += AboutMenuItemClick;
            exitMenuItem.Click += delegate { Close(); };
        }

        private void OpenMxdMenuItemClick(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "ArcMap Document (*.mxd)|*.mxd";
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                ExecuteUiAction(delegate
                {
                    MapDocumentInfo info = controller.OpenMapDocument(dialog.FileName);
                    RefreshLayerList(info == null ? controller.ListLayers() : info.Layers);
                    SetStatus("Opened MXD: " + dialog.FileName);
                });
            }
        }

        private void AddShapefileMenuItemClick(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Shapefile (*.shp)|*.shp";
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                ExecuteUiAction(delegate
                {
                    controller.AddShapefile(dialog.FileName);
                    RefreshLayerList(controller.ListLayers());
                    SetStatus("Added shapefile: " + dialog.FileName);
                });
            }
        }

        private void AttributeQueryMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                EnsureLayerSelected();
            }
            catch (Exception ex)
            {
                ShowError(ex);
                return;
            }

            using (AttributeQueryForm form = new AttributeQueryForm(GetSelectedLayerName()))
            {
                if (form.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                ExecuteUiAction(delegate
                {
                    // MainForm displays results only; the query itself is handled by the Application use case. / MainForm 只展示结果，查询本身由 Application 用例处理。
                    QueryResult result = controller.QueryByAttribute(form.QueryCondition);
                    DisplayQueryResult(result);
                    lastQueryResult = result;
                    if (result != null && result.TotalCount > 0)
                    {
                        ShowAttributeTable(result);
                    }
                });
            }
        }

        private void SpatialQueryMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                EnsureLayerSelected();
            }
            catch (Exception ex)
            {
                ShowError(ex);
                return;
            }

            using (SpatialQueryForm form = new SpatialQueryForm(GetSelectedLayerName(), axMapControl.Extent))
            {
                if (form.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                ExecuteUiAction(delegate
                {
                    QueryResult result = controller.QueryBySpatialRelation(form.SpatialQueryCondition);
                    DisplayQueryResult(result);
                    lastQueryResult = result;
                    if (result != null && result.TotalCount > 0)
                    {
                        ShowAttributeTable(result);
                    }
                });
            }
        }

        private void ShowAttributeTableMenuItemClick(object sender, EventArgs e)
        {
            ExecuteUiAction(delegate
            {
                EnsureLayerSelected();
                QueryResult result = controller.ShowAttributeTable(GetSelectedLayerName(), 1000);
                DisplayQueryResult(result);
                lastQueryResult = result;
                ShowAttributeTable(result);
            });
        }

        private void ExportCsvMenuItemClick(object sender, EventArgs e)
        {
            ExecuteUiAction(delegate
            {
                if (lastQueryResult == null || lastQueryResult.Records == null || lastQueryResult.Records.Count == 0)
                {
                    throw new InvalidOperationException("No query result is available to export.");
                }

                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "CSV file (*.csv)|*.csv";
                    dialog.FileName = "query_result.csv";
                    if (dialog.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }

                    controller.ExportQueryResult(lastQueryResult, dialog.FileName);
                    SetStatus("Exported CSV: " + dialog.FileName);
                }
            });
        }

        private void ClearSelectionMenuItemClick(object sender, EventArgs e)
        {
            ExecuteUiAction(delegate
            {
                controller.ClearSelection();
                SetStatus("Selection cleared.");
            });
        }

        private void FullExtentMenuItemClick(object sender, EventArgs e)
        {
            ExecuteUiAction(delegate { controller.FullExtent(); });
        }

        private void ZoomInMenuItemClick(object sender, EventArgs e)
        {
            ExecuteUiAction(delegate { controller.ZoomIn(); });
        }

        private void ZoomOutMenuItemClick(object sender, EventArgs e)
        {
            ExecuteUiAction(delegate { controller.ZoomOut(); });
        }

        private void PanMenuItemClick(object sender, EventArgs e)
        {
            ExecuteUiAction(delegate { controller.Pan(); });
        }

        private void LayerManagerMenuItemClick(object sender, EventArgs e)
        {
            ExecuteUiAction(delegate
            {
                using (LayerManagerForm form = new LayerManagerForm(controller.ListLayers()))
                {
                    form.ShowDialog(this);
                }
            });
        }

        private void AboutMenuItemClick(object sender, EventArgs e)
        {
            using (AboutForm form = new AboutForm())
            {
                form.ShowDialog(this);
            }
        }

        private void DisplayQueryResult(QueryResult result)
        {
            resultDataGridView.Columns.Clear();
            resultDataGridView.Rows.Clear();

            if (result == null || result.Records == null || result.Records.Count == 0)
            {
                SetStatus(result == null ? "No result." : result.Message);
                MessageBox.Show(this, result == null ? "No result." : result.Message, "Small GIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            resultDataGridView.Columns.Add("ObjectId", "ObjectId");
            resultDataGridView.Columns.Add("LayerName", "LayerName");
            resultDataGridView.Columns.Add("GeometryType", "GeometryType");

            IList<string> fields = CollectAttributeFields(result);
            for (int i = 0; i < fields.Count; i++)
            {
                resultDataGridView.Columns.Add(fields[i], fields[i]);
            }

            for (int i = 0; i < result.Records.Count; i++)
            {
                FeatureRecord record = result.Records[i];
                object[] values = new object[3 + fields.Count];
                values[0] = record.ObjectId;
                values[1] = record.LayerName;
                values[2] = record.GeometryType;
                for (int j = 0; j < fields.Count; j++)
                {
                    object value = null;
                    record.Attributes.TryGetValue(fields[j], out value);
                    values[3 + j] = value;
                }

                resultDataGridView.Rows.Add(values);
            }

            SetStatus("Query result count: " + result.TotalCount);
        }

        private void ShowAttributeTable(QueryResult result)
        {
            using (AttributeTableForm form = new AttributeTableForm(result))
            {
                form.ShowDialog(this);
            }
        }

        private void RefreshLayerList(IList<LayerInfo> layers)
        {
            layerListBox.Items.Clear();
            if (layers == null)
            {
                return;
            }

            for (int i = 0; i < layers.Count; i++)
            {
                layerListBox.Items.Add(layers[i].Name);
            }

            if (layerListBox.Items.Count > 0)
            {
                layerListBox.SelectedIndex = 0;
            }
        }

        private string GetSelectedLayerName()
        {
            return layerListBox.SelectedItem == null ? string.Empty : layerListBox.SelectedItem.ToString();
        }

        private void EnsureLayerSelected()
        {
            if (layerListBox.Items.Count == 0)
            {
                throw new InvalidOperationException("No layer is available.");
            }

            if (layerListBox.SelectedItem == null)
            {
                throw new InvalidOperationException("Please select a layer first.");
            }
        }

        private void ExecuteUiAction(MethodInvoker action)
        {
            try
            {
                if (controller == null)
                {
                    throw new InvalidOperationException("Application controller is not initialized.");
                }

                action();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Small GIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            SetStatus("Error: " + ex.Message);
        }

        private void SetStatus(string message)
        {
            statusLabel.Text = message;
        }

        private static IList<string> CollectAttributeFields(QueryResult result)
        {
            IList<string> fields = new List<string>();
            for (int i = 0; i < result.Records.Count; i++)
            {
                foreach (string key in result.Records[i].Attributes.Keys)
                {
                    if (!fields.Contains(key))
                    {
                        fields.Add(key);
                    }
                }
            }

            return fields;
        }
    }
}
