using System.Collections.Generic;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public interface IStoreDataAccess
    {
        List<Store> GetStores();
        Store GetStoreById(int storeId);
        void AddStore(Store store);
        void UpdateStore(Store store);
        void DeleteStore(int storeId);
    }
}
