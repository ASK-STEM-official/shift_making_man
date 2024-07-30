//
using shift_making_man.Models;
using System.Collections.Generic;

namespace shift_making_man.Data
{
    public interface IStoreDataAccess
    {
        List<Store> GetStores();
        Store GetStoreById(int storeId);
        //TimeSpan GetStoreOpenTime(int storeId);
        //TimeSpan GetStoreCloseTime(int storeId);
        //void AddStore(Store store);
        //void UpdateStore(Store store);
        //void DeleteStore(int storeId);
    }
}