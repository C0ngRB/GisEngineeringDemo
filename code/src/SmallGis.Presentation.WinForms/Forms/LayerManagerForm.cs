using System.Collections.Generic;
using System.Windows.Forms;
using SmallGis.Domain.Models;

namespace SmallGis.Presentation.WinForms.Forms
{
    public class LayerManagerForm : Form
    {
        public LayerManagerForm(IList<LayerInfo> layers)
        {
            Text = "Layer Manager";
            Width = 640;
            Height = 360;
            StartPosition = FormStartPosition.CenterParent;

            DataGridView gridView = new DataGridView();
            gridView.Dock = DockStyle.Fill;
            gridView.ReadOnly = true;
            gridView.AllowUserToAddRows = false;
            gridView.AllowUserToDeleteRows = false;
            gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            gridView.Columns.Add("Name", "Name");
            gridView.Columns.Add("LayerType", "LayerType");
            gridView.Columns.Add("GeometryType", "GeometryType");
            gridView.Columns.Add("Visible", "Visible");
            gridView.Columns.Add("FeatureCount", "FeatureCount");
            gridView.Columns.Add("DataSourcePath", "DataSourcePath");

            if (layers != null)
            {
                for (int i = 0; i < layers.Count; i++)
                {
                    LayerInfo layer = layers[i];
                    gridView.Rows.Add(layer.Name, layer.LayerType, layer.GeometryType, layer.Visible, layer.FeatureCount, layer.DataSourcePath);
                }
            }

            Controls.Add(gridView);
        }
    }
}
