using System.Collections.Generic;
using System.Windows.Forms;
using SmallGis.Domain.Models;

namespace SmallGis.Presentation.WinForms.Forms
{
    /// <summary>
    /// Displays query records in a read-only table. / 以只读表格显示查询记录。
    /// </summary>
    public class AttributeTableForm : Form
    {
        private readonly DataGridView gridView;

        public AttributeTableForm(QueryResult result)
        {
            Text = "Attribute Table";
            Width = 760;
            Height = 420;
            StartPosition = FormStartPosition.CenterParent;

            gridView = new DataGridView();
            gridView.Dock = DockStyle.Fill;
            gridView.ReadOnly = true;
            gridView.AllowUserToAddRows = false;
            gridView.AllowUserToDeleteRows = false;
            gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            Controls.Add(gridView);

            LoadResult(result);
        }

        private void LoadResult(QueryResult result)
        {
            gridView.Columns.Clear();
            gridView.Rows.Clear();

            if (result == null || result.Records == null)
            {
                return;
            }

            gridView.Columns.Add("ObjectId", "ObjectId");
            gridView.Columns.Add("LayerName", "LayerName");
            gridView.Columns.Add("GeometryType", "GeometryType");

            IList<string> fields = CollectAttributeFields(result);
            for (int i = 0; i < fields.Count; i++)
            {
                gridView.Columns.Add(fields[i], fields[i]);
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

                gridView.Rows.Add(values);
            }
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
