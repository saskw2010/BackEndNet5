using BackEnd.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
    public class EsSrItemController : ControllerBase
    {
        #region privateFilde
        private IEsSrItemService _EsSrItemService;
        #endregion

        #region EsSrItemController
        public EsSrItemController(IEsSrItemService EsSrItemService)
        {
            _EsSrItemService = EsSrItemService;
        }
        #endregion

        #region GetAllItem
        [HttpGet(BAL.ApiRoute.ApiRoute.ItemRoute.GetAllItem)]
        public async Task<BAL.Models.Result> GetAllItem()
        {
            return await _EsSrItemService.GetAllItem();
        }
        #endregion

    }
}
