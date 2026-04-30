using System;
using System.Windows.Forms;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;

namespace SmallGis.Presentation.WinForms
{
    /// <summary>
    /// WinForms entry point. Initializes ArcGIS licensing before any Engine controls are used. / WinForms 入口，在使用 Engine 控件前初始化 ArcGIS 许可。
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            IAoInitialize aoInitialize = null;
            try
            {
                // Bind the ArcGIS runtime before creating AoInitialize or any Engine control. / 在创建 AoInitialize 或任何 Engine 控件前绑定 ArcGIS 运行时。
                if (!RuntimeManager.Bind(ProductCode.EngineOrDesktop))
                {
                    MessageBox.Show("ArcGIS Runtime 绑定失败，请确认 ArcGIS Engine 10.2 或 ArcGIS Desktop 已正确安装。", "Small GIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
            // Prefer Engine license, then fall back to Desktop Basic when available. / 优先使用 Engine 许可，不可用时回退到 Desktop Basic。
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
