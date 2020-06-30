using BLL.App.DTO;

using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using ItemView = BLL.App.DTO.ItemView;
using SingleItemView = BLL.App.DTO.SingleItemView;

namespace BLL.App.Mappers
{
    public class ItemServiceMapper : BLLMapper<ItemDAL, ItemBLL>, IItemServiceMapper
    {
        public ItemView MapItemView(DAL.App.DTO.ItemView inObject)
        {
            return Mapper.Map<ItemView>(inObject);
        }

        public SingleItemView MapSingleItemView(DAL.App.DTO.SingleItemView inObject)
        {
            return Mapper.Map<SingleItemView>(inObject);
        }
    }
}