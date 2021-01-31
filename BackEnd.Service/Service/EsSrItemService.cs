using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.Service.IService;
using RealState.DAL.IBackEndContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
    public class EsSrItemService : IEsSrItemService
    {
        #region privateFilde
        private BAL.Interfaces.IUnitOfWork _unitOfWork;
        private AutoMapper.IMapper _mapper;
        private BakEndContext _db;
        #endregion

        #region EsSrItemService
        public EsSrItemService(BAL.Interfaces.IUnitOfWork unitOfWork, 
            AutoMapper.IMapper mapper,
            BakEndContext db)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _db = db;
        }
        #endregion

        #region GetAllItem
        public async Task<Result> GetAllItem()
        {
            try
            {
                List<object> list = new List<object>();
               list = _db.GetEsSr_Proc_GetActiveItems();
                return new Result
                {
                    success = true,
                    code = "200",
                    data = list,
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    success = false,
                    code = "400",
                    data = null,
                    message = ExtensionMethods.FullMessage(ex)
                };
            }
        }
        #endregion
    }
}
