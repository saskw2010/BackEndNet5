using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.IService
{
    public interface IEsSrItemService
    {
        #region GetAllItem
        System.Threading.Tasks.Task<BAL.Models.Result> GetAllItem();
        #endregion
    }
}
