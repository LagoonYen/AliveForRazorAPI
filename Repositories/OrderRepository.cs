
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;

namespace AliveStoreTemplate.Repositories
{
    public interface OrderRepository
    {
        public BaseQueryModel<int> UpsertAddress(AddressUpserConditionModel AddressUpserCondi);
    }
}
