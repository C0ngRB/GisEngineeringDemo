using System;

namespace SmallGis.Infrastructure.ArcObjects.Utilities
{
    /// <summary>
    /// Wraps ArcObjects exceptions before they cross the infrastructure boundary. / 在异常跨出基础设施层边界前进行包装。
    /// </summary>
    public static class ArcObjectsExceptionMapper
    {
        public static ApplicationException ToApplicationException(string message, Exception exception)
        {
            return new ApplicationException(message, exception);
        }
    }
}
