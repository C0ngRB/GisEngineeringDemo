using System.Windows.Forms;

namespace SmallGis.Presentation.WinForms.Forms
{
    public class AboutForm : Form
    {
        public AboutForm()
        {
            Text = "About";
            Width = 360;
            Height = 180;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Label label = new Label();
            label.Dock = DockStyle.Fill;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Text = "Small GIS\r\nArcGIS Engine 10.2 / WinForms";
            Controls.Add(label);
        }
    }
}
