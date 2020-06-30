using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.BLL.Base.Mappers;

namespace Contracts.BLL.App.Mappers
{
    public interface ILangStrTranslationServiceMapper: IBaseBLLMapper<DALAppDTO.LangStrTranslation, BLLAppDTO.LangStrTranslation>
    {
        
    }
}