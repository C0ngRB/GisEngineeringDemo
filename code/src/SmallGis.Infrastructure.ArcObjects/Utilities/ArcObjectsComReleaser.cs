using System;
using System.Runtime.InteropServices;

namespace SmallGis.Infrastructure.ArcObjects.Utilities
{
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
