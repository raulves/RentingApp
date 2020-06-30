#pragma warning disable 1591
namespace WebApp.ViewModels.Mappers
{
    public abstract class BaseVMMapper<TLeftObject, TRightObject> : ee.itcollege.Raul.Vesinurm.DAL.Base.Mappers.BaseDALMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
    }
}