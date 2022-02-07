namespace MvcCoreTest.Models
{
    public enum ResultStatus
    {
        Success = 1,
        Exception = -1,
        StringToDoubleConversionFailed = 2,
        EntityExists,
        RelationalEntitiesExist
    }
}
