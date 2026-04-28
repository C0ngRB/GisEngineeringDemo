using System;

namespace SmallGis.Infrastructure.ArcObjects.Utilities
{
    public static class ArcObjectsExceptionMapper
    {
        public static ApplicationException ToApplicationException(string message, Exception exception)
        {
            return new ApplicationException(message, exception);
        }
    }
}
