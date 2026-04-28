using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using SmallGis.Domain.Enums;
using SmallGis.Domain.Models;

namespace SmallGis.Presentation.WinForms.Forms
{
    public class SpatialQueryForm : Form
    {
        private readonly TextBox targetLayerTextBox;
        private readonly ComboBox relationComboBox;
        private readonly TextBox xminTextBox;
        private readonly TextBox yminTextBox;
        private readonly TextBox xmaxTextBox;
        private readonly TextBox ymaxTextBox;

        public SpatialQueryForm(string defaultLayerName, IEnvelope currentExtent)
        {
            Text = "Spatial Query";
            Width = 420;
            Height = 270;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            TableLayoutPanel panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.ColumnCount = 2;
            panel.RowCount = 7;
            panel.Padding = new Padding(10);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            targetLayerTextBox = new TextBox();
            targetLayerTextBox.Text = defaultLayerName ?? string.Empty;
            relationComboBox = new ComboBox();
            relationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            relationComboBox.DataSource = Enum.GetValues(typeof(SpatialRelationType));
            xminTextBox = new TextBox();
            yminTextBox = new TextBox();
            xmaxTextBox = new TextBox();
            ymaxTextBox = new TextBox();

            if (currentExtent != null)
            {
                xminTextBox.Text = currentExtent.XMin.ToString(System.Globalization.CultureInfo.InvariantCulture);
                yminTextBox.Text = currentExtent.YMin.ToString(System.Globalization.CultureInfo.InvariantCulture);
                xmaxTextBox.Text = currentExtent.XMax.ToString(System.Globalization.CultureInfo.InvariantCulture);
                ymaxTextBox.Text = currentExtent.YMax.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }

            AddRow(panel, 0, "Target Layer", targetLayerTextBox);
            AddRow(panel, 1, "Relation", relationComboBox);
            AddRow(panel, 2, "XMin", xminTextBox);
            AddRow(panel, 3, "YMin", yminTextBox);
            AddRow(panel, 4, "XMax", xmaxTextBox);
            AddRow(panel, 5, "YMax", ymaxTextBox);

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Dock = DockStyle.Fill;
            Button okButton = new Button();
            Button cancelButton = new Button();
            okButton.Text = "OK";
            cancelButton.Text = "Cancel";
            okButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;
            buttonPanel.Controls.Add(cancelButton);
            buttonPanel.Controls.Add(okButton);
            panel.Controls.Add(buttonPanel, 1, 6);

            Controls.Add(panel);
            AcceptButton = okButton;
            CancelButton = cancelButton;
        }

        public SpatialQueryCondition SpatialQueryCondition
        {
            get
            {
                return new SpatialQueryCondition
                {
                    TargetLayerName = targetLayerTextBox.Text.Trim(),
                    RelationType = (SpatialRelationType)relationComboBox.SelectedItem,
                    QueryExtent = new MapExtent
                    {
                        XMin = ParseDouble(xminTextBox.Text),
                        YMin = ParseDouble(yminTextBox.Text),
                        XMax = ParseDouble(xmaxTextBox.Text),
                        YMax = ParseDouble(ymaxTextBox.Text)
                    }
                };
            }
        }

        private static double ParseDouble(string text)
        {
            return double.Parse(text, System.Globalization.CultureInfo.InvariantCulture);
        }

        private static void AddRow(TableLayoutPanel panel, int row, string label, Control control)
        {
            Label labelControl = new Label();
            labelControl.Text = label;
            labelControl.Dock = DockStyle.Fill;
            labelControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            control.Dock = DockStyle.Fill;
            panel.Controls.Add(labelControl, 0, row);
            panel.Controls.Add(control, 1, row);
        }
    }
}
