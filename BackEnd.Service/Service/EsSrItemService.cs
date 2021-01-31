using BackEnd.BAL.Models;
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
        private IBackEndContext _db;
        #endregion

        #region EsSrItemService
        public EsSrItemService(BAL.Interfaces.IUnitOfWork unitOfWork, 
            AutoMapper.IMapper mapper,
            IBackEndContext db)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region GetAllItem
        public async Task<Result> GetAllItem()
        {
            try
            {
                List<object> list = new List<object>();
                using (var cmd = _db.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "EsSr_Proc_GetActiveItems";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    _db.Database.OpenConnection();
                    using (var result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            list.Add(new 
                            {
                                ItemID = result["ItemID"]?.ToString(),
                                CatgeoryID = result["CatgeoryID"]?.ToString(),
                                CatgeoryNameAr = result["CatgeoryNameAr"]?.ToString(),
                                CatgeoryNameEn = result["CatgeoryNameEn"]?.ToString(),
                                CurrencyID = result["CurrencyID"]?.ToString(),
                                CurrencyNameAr = result["CurrencyNameAr"]?.ToString(),
                                CurrencyNameEn = result["CurrencyNameEn"]?.ToString(),
                                CatgeoryPicStockId = result["CatgeoryPicStockId"]?.ToString(),
                                CatgeoryId1 = result["CatgeoryId1"]?.ToString(),
                                CatgeoryParentId = result["CatgeoryParentId"]?.ToString(),
                                CatgeoryHasSubCatgeory = result["CatgeoryHasSubCatgeory"]?.ToString(),
                                CatgeoryDescriptionEn = result["CatgeoryDescriptionEn"]?.ToString(),
                                CatgeoryDescriptionAr = result["CatgeoryDescriptionAr"]?.ToString(),
                                CatgeoryShowOrder = result["CatgeoryShowOrder"]?.ToString(),
                                CatgeoryDiscount = result["CatgeoryDiscount"]?.ToString(),
                                CatgeoryIsActive = result["CatgeoryIsActive"]?.ToString(),
                                CatgeoryIsDelete = result["CatgeoryIsDelete"]?.ToString(),
                                CatgeoryPicStockControllername = result["CatgeoryPicStockControllername"]?.ToString(),
                                CatgeoryPicStockPictureFileName = result["CatgeoryPicStockPictureFileName"]?.ToString(),
                                CatgeoryImageUrl = result["CatgeoryImageUrl"]?.ToString(),
                                PicStockID = result["PicStockID"]?.ToString(),
                                PicStockControllername = result["PicStockControllername"]?.ToString(),
                                PicStockPictureFileName = result["PicStockPictureFileName"]?.ToString(),
                                OrganisationID = result["OrganisationID"]?.ToString(),
                                OrganisationNameAr = result["OrganisationNameAr"]?.ToString(),
                                OrganisationNameEn = result["OrganisationNameEn"]?.ToString(),
                                NameAr = result["NameAr"]?.ToString(),
                                NameEn = result["NameEn"]?.ToString(),
                                DescriptionEn = result["DescriptionEn"]?.ToString(),
                                DescriptionAr = result["DescriptionAr"]?.ToString(),
                                ShowOrder = result["ShowOrder"]?.ToString(),
                                Discount = result["Discount"]?.ToString(),
                                CreatedHour = result["CreatedHour"]?.ToString(),
                                EditHour = result["EditHour"]?.ToString(),
                                IsActive = result["IsActive"]?.ToString(),
                                Notes = result["Notes"]?.ToString(),
                                CreatedBy = result["CreatedBy"]?.ToString(),
                                CreatedOn = result["CreatedOn"]?.ToString(),
                                ModifiedBy = result["ModifiedBy"]?.ToString(),
                                ModifiedOn = result["ModifiedOn"]?.ToString(),
                                IsDelete = result["IsDelete"]?.ToString(),
                                Price = result["Price"]?.ToString(),
                                IsPreview = result["IsPreview"]?.ToString(),
                            });
                        }
                    }
                }
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
