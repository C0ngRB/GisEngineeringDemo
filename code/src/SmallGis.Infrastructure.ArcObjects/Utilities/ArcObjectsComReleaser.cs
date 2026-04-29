using System;
using System.Runtime.InteropServices;

namespace SmallGis.Infrastructure.ArcObjects.Utilities
{
    /// <summary>
    /// Centralizes COM release calls for ArcObjects cursors and workspaces. / 集中处理 ArcObjects 游标和工作空间的 COM 释放。
    /// </summary>
    public static class ArcObjectsComReleaser
    {
        public static void Release(object comObject)
        {
            if (comObject == null)
            {
                return;
            }

            if (Marshal.IsComObject(comObject))
            {
                Marshal.ReleaseComObject(comObject);
            }
        }

        public static void FinalRelease(object comObject)
        {
            if (comObject == null)
            {
                return;
            }

            if (Marshal.IsComObject(comObject))
            {
                Marshal.FinalReleaseComObject(comObject);
            }
        }
    }
}
