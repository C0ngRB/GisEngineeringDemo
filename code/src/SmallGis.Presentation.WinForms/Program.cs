using System;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace SmallGis.Presentation.WinForms
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            IAoInitialize aoInitialize = null;
            try
            {
                aoInitialize = new AoInitializeClass();
                InitializeLicense(aoInitialize);

                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                System.Windows.Forms.Application.Run(new MainForm());
            }
            finally
            {
                if (aoInitialize != null)
                {
                    aoInitialize.Shutdown();
                }
            }
        }

        private static void InitializeLicense(IAoInitialize aoInitialize)
        {
            esriLicenseStatus status = aoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngine);
            if (status == esriLicenseStatus.esriLicenseCheckedOut)
            {
                return;
            }

            status = aoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeBasic);
            if (status != esriLicenseStatus.esriLicenseCheckedOut)
            {
                MessageBox.Show("ArcGIS Engine license initialization failed.", "Small GIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }
    }
}
