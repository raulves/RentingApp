using BLL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.BLL.Base.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IItemServiceMapper : IBaseBLLMapper<ItemDAL, ItemBLL>
    {
        BLLAppDTO.ItemView MapItemView(DALAppDTO.ItemView inObject);
        BLLAppDTO.SingleItemView MapSingleItemView(DALAppDTO.SingleItemView inObject);
    }
}