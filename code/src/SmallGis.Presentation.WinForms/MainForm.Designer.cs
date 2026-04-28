namespace SmallGis.Presentation.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.SplitContainer rightSplitContainer;
        private System.Windows.Forms.ListBox layerListBox;
        private System.Windows.Forms.DataGridView resultDataGridView;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl;
        private ESRI.ArcGIS.Controls.AxTOCControl axTocControl;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMxdMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addShapefileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attributeQueryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spatialQueryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAttributeTableMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCsvMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullExtentMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomInMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerManagerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ToolStripButton openMxdToolButton;
        private System.Windows.Forms.ToolStripButton addShapefileToolButton;
        private System.Windows.Forms.ToolStripButton attributeQueryToolButton;
        private System.Windows.Forms.ToolStripButton attributeTableToolButton;
        private System.Windows.Forms.ToolStripButton exportCsvToolButton;
        private System.Windows.Forms.ToolStripButton clearSelectionToolButton;
        private System.Windows.Forms.ToolStripButton fullExtentToolButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMxdMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addShapefileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attributeQueryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spatialQueryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAttributeTableMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCsvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullExtentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerManagerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.openMxdToolButton = new System.Windows.Forms.ToolStripButton();
            this.addShapefileToolButton = new System.Windows.Forms.ToolStripButton();
            this.attributeQueryToolButton = new System.Windows.Forms.ToolStripButton();
            this.attributeTableToolButton = new System.Windows.Forms.ToolStripButton();
            this.exportCsvToolButton = new System.Windows.Forms.ToolStripButton();
            this.clearSelectionToolButton = new System.Windows.Forms.ToolStripButton();
            this.fullExtentToolButton = new System.Windows.Forms.ToolStripButton();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.axTocControl = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.rightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.axToolbarControl = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            this.layerListBox = new System.Windows.Forms.ListBox();
            this.resultDataGridView = new System.Windows.Forms.DataGridView();
            this.mainMenuStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTocControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
            this.rightSplitContainer.Panel1.SuspendLayout();
            this.rightSplitContainer.Panel2.SuspendLayout();
            this.rightSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).BeginInit();
            this.SuspendLayout();
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.queryMenuItem,
            this.viewMenuItem,
            this.helpMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1064, 25);
            this.mainMenuStrip.TabIndex = 0;
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMxdMenuItem,
            this.addShapefileMenuItem,
            this.exitMenuItem});
            this.fileMenuItem.Text = "File";
            this.openMxdMenuItem.Text = "Open MXD";
            this.addShapefileMenuItem.Text = "Add Shapefile";
            this.exitMenuItem.Text = "Exit";
            this.queryMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attributeQueryMenuItem,
            this.spatialQueryMenuItem,
            this.showAttributeTableMenuItem,
            this.exportCsvMenuItem,
            this.clearSelectionMenuItem});
            this.queryMenuItem.Text = "Query";
            this.attributeQueryMenuItem.Text = "Attribute Query";
            this.spatialQueryMenuItem.Text = "Spatial Query";
            this.showAttributeTableMenuItem.Text = "Attribute Table";
            this.exportCsvMenuItem.Text = "Export CSV";
            this.clearSelectionMenuItem.Text = "Clear Selection";
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullExtentMenuItem,
            this.zoomInMenuItem,
            this.zoomOutMenuItem,
            this.panMenuItem,
            this.layerManagerMenuItem});
            this.viewMenuItem.Text = "View";
            this.fullExtentMenuItem.Text = "Full Extent";
            this.zoomInMenuItem.Text = "Zoom In";
            this.zoomOutMenuItem.Text = "Zoom Out";
            this.panMenuItem.Text = "Pan";
            this.layerManagerMenuItem.Text = "Layer Manager";
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
            this.helpMenuItem.Text = "Help";
            this.aboutMenuItem.Text = "About";
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMxdToolButton,
            this.addShapefileToolButton,
            this.attributeQueryToolButton,
            this.attributeTableToolButton,
            this.exportCsvToolButton,
            this.clearSelectionToolButton,
            this.fullExtentToolButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 25);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(1064, 25);
            this.mainToolStrip.TabIndex = 1;
            this.openMxdToolButton.Text = "Open";
            this.addShapefileToolButton.Text = "Add SHP";
            this.attributeQueryToolButton.Text = "Query";
            this.attributeTableToolButton.Text = "Table";
            this.exportCsvToolButton.Text = "CSV";
            this.clearSelectionToolButton.Text = "Clear";
            this.fullExtentToolButton.Text = "Full";
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 659);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(1064, 22);
            this.mainStatusStrip.TabIndex = 3;
            this.statusLabel.Text = "Ready";
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 50);
            this.mainSplitContainer.Name = "mainSplitContainer";
            this.mainSplitContainer.Panel1.Controls.Add(this.axTocControl);
            this.mainSplitContainer.Panel2.Controls.Add(this.rightSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(1064, 609);
            this.mainSplitContainer.SplitterDistance = 240;
            this.mainSplitContainer.TabIndex = 2;
            this.axTocControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTocControl.Location = new System.Drawing.Point(0, 0);
            this.axTocControl.Name = "axTocControl";
            this.axTocControl.Size = new System.Drawing.Size(240, 609);
            this.rightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.rightSplitContainer.Name = "rightSplitContainer";
            this.rightSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.rightSplitContainer.Panel1.Controls.Add(this.axMapControl);
            this.rightSplitContainer.Panel1.Controls.Add(this.axToolbarControl);
            this.rightSplitContainer.Panel2.Controls.Add(this.resultDataGridView);
            this.rightSplitContainer.Panel2.Controls.Add(this.layerListBox);
            this.rightSplitContainer.Size = new System.Drawing.Size(820, 609);
            this.rightSplitContainer.SplitterDistance = 420;
            this.axToolbarControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl.Location = new System.Drawing.Point(0, 0);
            this.axToolbarControl.Name = "axToolbarControl";
            this.axToolbarControl.Size = new System.Drawing.Size(820, 28);
            this.axMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl.Location = new System.Drawing.Point(0, 28);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.Size = new System.Drawing.Size(820, 392);
            this.layerListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.layerListBox.FormattingEnabled = true;
            this.layerListBox.ItemHeight = 12;
            this.layerListBox.Name = "layerListBox";
            this.layerListBox.Size = new System.Drawing.Size(220, 185);
            this.resultDataGridView.AllowUserToAddRows = false;
            this.resultDataGridView.AllowUserToDeleteRows = false;
            this.resultDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.resultDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultDataGridView.Location = new System.Drawing.Point(220, 0);
            this.resultDataGridView.Name = "resultDataGridView";
            this.resultDataGridView.ReadOnly = true;
            this.resultDataGridView.Size = new System.Drawing.Size(600, 185);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Small GIS";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTocControl)).EndInit();
            this.rightSplitContainer.Panel1.ResumeLayout(false);
            this.rightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
