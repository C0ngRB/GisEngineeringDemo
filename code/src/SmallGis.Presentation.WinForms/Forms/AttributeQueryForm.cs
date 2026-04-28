using System;
using System.Windows.Forms;
using SmallGis.Domain.Enums;
using SmallGis.Domain.Models;

namespace SmallGis.Presentation.WinForms.Forms
{
    public class AttributeQueryForm : Form
    {
        private readonly TextBox layerTextBox;
        private readonly TextBox fieldTextBox;
        private readonly ComboBox operatorComboBox;
        private readonly TextBox valueTextBox;
        private readonly TextBox rawWhereTextBox;
        private readonly Button okButton;
        private readonly Button cancelButton;

        public AttributeQueryForm(string defaultLayerName)
        {
            Text = "Attribute Query";
            Width = 440;
            Height = 260;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            TableLayoutPanel panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.ColumnCount = 2;
            panel.RowCount = 6;
            panel.Padding = new Padding(10);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            layerTextBox = new TextBox();
            layerTextBox.Text = defaultLayerName ?? string.Empty;
            fieldTextBox = new TextBox();
            operatorComboBox = new ComboBox();
            operatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            operatorComboBox.DataSource = Enum.GetValues(typeof(QueryOperator));
            valueTextBox = new TextBox();
            rawWhereTextBox = new TextBox();
            okButton = new Button();
            cancelButton = new Button();
            okButton.Text = "OK";
            cancelButton.Text = "Cancel";
            okButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;

            AddRow(panel, 0, "Layer", layerTextBox);
            AddRow(panel, 1, "Field", fieldTextBox);
            AddRow(panel, 2, "Operator", operatorComboBox);
            AddRow(panel, 3, "Value", valueTextBox);
            AddRow(panel, 4, "Where", rawWhereTextBox);

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.Controls.Add(cancelButton);
            buttonPanel.Controls.Add(okButton);
            panel.Controls.Add(buttonPanel, 1, 5);

            Controls.Add(panel);
            AcceptButton = okButton;
            CancelButton = cancelButton;
        }

        public QueryCondition QueryCondition
        {
            get
            {
                return new QueryCondition
                {
                    LayerName = layerTextBox.Text.Trim(),
                    FieldName = fieldTextBox.Text.Trim(),
                    Operator = (QueryOperator)operatorComboBox.SelectedItem,
                    Value = valueTextBox.Text.Trim(),
                    RawWhereClause = rawWhereTextBox.Text.Trim()
                };
            }
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
