namespace SmallGis.Domain.Enums
{
    /// <summary>
    /// Supported operators for attribute query conditions. / 属性查询条件支持的操作符。
    /// </summary>
    public enum QueryOperator
    {
        Equal = 0,
        NotEqual = 1,
        GreaterThan = 2,
        GreaterOrEqual = 3,
        LessThan = 4,
        LessOrEqual = 5,
        Like = 6
    }
}
