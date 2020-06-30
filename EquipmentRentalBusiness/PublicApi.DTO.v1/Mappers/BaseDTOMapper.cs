#pragma warning disable 1591
using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public abstract class BaseDTOMapper<TLeftObject, TRightObject> : ee.itcollege.Raul.Vesinurm.DAL.Base.Mappers.BaseDALMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
    }
}