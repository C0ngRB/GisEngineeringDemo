using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using SmallGis.Domain.Enums;
using SmallGis.Domain.Models;

namespace SmallGis.Presentation.WinForms.Forms
{
    /// <summary>
    /// Collects spatial query parameters for extent or selected-feature queries. / 收集当前范围或选中要素空间查询参数。
    /// </summary>
    public class SpatialQueryForm : Form
    {
        private readonly TextBox targetLayerTextBox;
        private readonly ComboBox relationComboBox;
        private readonly RadioButton extentRadioButton;
        private readonly RadioButton selectedFeatureRadioButton;
        private readonly TextBox xminTextBox;
        private readonly TextBox yminTextBox;
        private readonly TextBox xmaxTextBox;
        private readonly TextBox ymaxTextBox;
        private readonly TextBox sourceLayerTextBox;
        private readonly TextBox sourceObjectIdTextBox;

        public SpatialQueryForm(string defaultLayerName, IEnvelope currentExtent)
        {
            Text = "Spatial Query";
            Width = 460;
            Height = 360;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            TableLayoutPanel panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.ColumnCount = 2;
            panel.RowCount = 10;
            panel.Padding = new Padding(10);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            targetLayerTextBox = new TextBox();
            targetLayerTextBox.Text = defaultLayerName ?? string.Empty;
            relationComboBox = new ComboBox();
            relationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            relationComboBox.DataSource = Enum.GetValues(typeof(SpatialRelationType));

            FlowLayoutPanel modePanel = new FlowLayoutPanel();
            modePanel.Dock = DockStyle.Fill;
            extentRadioButton = new RadioButton();
            selectedFeatureRadioButton = new RadioButton();
            extentRadioButton.Text = "Current extent";
            selectedFeatureRadioButton.Text = "Selected feature";
            extentRadioButton.Checked = true;
            modePanel.Controls.Add(extentRadioButton);
            modePanel.Controls.Add(selectedFeatureRadioButton);

            xminTextBox = new TextBox();
            yminTextBox = new TextBox();
            xmaxTextBox = new TextBox();
            ymaxTextBox = new TextBox();
            sourceLayerTextBox = new TextBox();
            sourceLayerTextBox.Text = defaultLayerName ?? string.Empty;
            sourceObjectIdTextBox = new TextBox();

            if (currentExtent != null)
            {
                xminTextBox.Text = currentExtent.XMin.ToString(System.Globalization.CultureInfo.InvariantCulture);
                yminTextBox.Text = currentExtent.YMin.ToString(System.Globalization.CultureInfo.InvariantCulture);
                xmaxTextBox.Text = currentExtent.XMax.ToString(System.Globalization.CultureInfo.InvariantCulture);
                ymaxTextBox.Text = currentExtent.YMax.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }

            AddRow(panel, 0, "Target Layer", targetLayerTextBox);
            AddRow(panel, 1, "Relation", relationComboBox);
            AddRow(panel, 2, "Mode", modePanel);
            AddRow(panel, 3, "XMin", xminTextBox);
            AddRow(panel, 4, "YMin", yminTextBox);
            AddRow(panel, 5, "XMax", xmaxTextBox);
            AddRow(panel, 6, "YMax", ymaxTextBox);
            AddRow(panel, 7, "Source Layer", sourceLayerTextBox);
            AddRow(panel, 8, "Source ObjectID", sourceObjectIdTextBox);

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
            panel.Controls.Add(buttonPanel, 1, 9);

            Controls.Add(panel);
            AcceptButton = okButton;
            CancelButton = cancelButton;
        }

        public SpatialQueryCondition SpatialQueryCondition
        {
            get
            {
                SpatialQueryCondition condition = new SpatialQueryCondition
                {
                    TargetLayerName = targetLayerTextBox.Text.Trim(),
                    RelationType = (SpatialRelationType)relationComboBox.SelectedItem,
                    SourceLayerName = sourceLayerTextBox.Text.Trim(),
                    SourceFeatureObjectId = ParseOptionalInt(sourceObjectIdTextBox.Text)
                };

                if (extentRadioButton.Checked)
                {
                    condition.QueryExtent = new MapExtent
                    {
                        XMin = ParseDouble(xminTextBox.Text),
                        YMin = ParseDouble(yminTextBox.Text),
                        XMax = ParseDouble(xmaxTextBox.Text),
                        YMax = ParseDouble(ymaxTextBox.Text)
                    };
                }

                return condition;
            }
        }

        private static double ParseDouble(string text)
        {
            return double.Parse(text, System.Globalization.CultureInfo.InvariantCulture);
        }

        private static int ParseOptionalInt(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }

            return int.Parse(text, System.Globalization.CultureInfo.InvariantCulture);
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
