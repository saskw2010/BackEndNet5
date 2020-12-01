using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
  public class RoleService : IRoleService
  {
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
      this._unitOfWork = unitOfWork;

      _mapper = mapper;
    }
    #region AddspNetUsersTypes_roles
    public async Task<Result> AddspNetUsersTypes_roles(AspNetUsersTypes_rolesInsertViewModel aspNetUsersTypes_rolesInsertViewModel)
    {
      aspNetUsersTypes_rolesInsertViewModel.CreatedOn = DateTime.Now;
      AspNetUsersTypes_roles aspNetUsersTypes_roles = new AspNetUsersTypes_roles();
     var obje= _mapper.Map(aspNetUsersTypes_rolesInsertViewModel, aspNetUsersTypes_roles);
      _unitOfWork.AspNetUsersTypes_rolesRepository.Insert(obje);
      var result1 = await _unitOfWork.SaveAsync();
      if (result1 == 200)
      {
        return new Result
        {

          success = true,
          code = "200",
          message = "row added successfuly"
        };
      }
      else {
        return new Result
        {

          success = true,
          code = "400",
          message = "row added faild"
        };
      }
    }

    public async Task<Result> DeleteAspNetUsersTypesRoles(string idAspNetRoles, long usrTypID)
    {
      var Entity=_unitOfWork.AspNetUsersTypes_rolesRepository.GetEntity(filter: (x =>
      (x.IdAspNetRoles == idAspNetRoles) && (x.UsrTypID == usrTypID)));
      _unitOfWork.AspNetUsersTypes_rolesRepository.Delete(Entity);
      var result1 = await _unitOfWork.SaveAsync();
      if (result1 == 200)
      {
        return new Result
        {

          success = true,
          code = "200",
          message = "row Deleted successfuly"
        };
      }
      else
      {
        return new Result
        {

          success = true,
          code = "400",
          message = "row Deleted faild"
        };
      }
    }

    public async Task<Result> FilterAspNetUsersTypes_roles(string SerachWord, int pageNumber = 1, int pageSize = 2)
    {
      // Get's No of Rows Count 
      var count = _unitOfWork.AspNetUsersTypes_rolesRepository.Get(filter:x=>(
      SerachWord!=null?(x.AspNetUsersTypes.UsrTypNm==SerachWord)|| (x.IdentityRole.Name == SerachWord) : true))
        .Count();

     
      // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1
      int CurrentPage = pageNumber;

      // Parameter is passed from Query string if it is null then it default Value will be pageSize:20
      int PageSize = pageSize;

      // Display TotalCount to Records to User
      int TotalCount = count;

      // Calculating Totalpage by Dividing (No of Records / Pagesize)
      int TotalPages = (int)Math.Ceiling(count / (double)PageSize);


      // Returns List of Customer after applying Paging 
      var items = _unitOfWork.AspNetUsersTypes_rolesRepository.Get(filter: x => (
       SerachWord != null ? (x.AspNetUsersTypes.UsrTypNm == SerachWord) || (x.IdentityRole.Name == SerachWord) : true),
       page: pageNumber, Take: pageSize);

      // if CurrentPage is greater than 1 means it has previousPage
      var previousPage = CurrentPage > 1 ? "Yes" : "No";

      // if TotalPages is greater than CurrentPage means it has nextPage
      var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
      // map 
      var aspNetUsersTypes_rolesViewModel = _mapper.Map<List<AspNetUsersTypes_rolesViewModel>>(items);
      // Object which we are going to send in header 
      paginationMetadata paginationMetadata = new paginationMetadata
      {
        totalCount = TotalCount,
        pageSize = PageSize,
        currentPage = CurrentPage,
        nextPage = nextPage,
        previousPage = previousPage,
        data = aspNetUsersTypes_rolesViewModel
      };
      return new Result
      {
        success = true,
        code = "200",
        data = paginationMetadata
      };
    }
    #endregion

    #region GetAllAspNetUsersTypes_roles
    public Result GetAllAspNetUsersTypes_roles(int pageNumber = 1, int pageSize = 2)
    {
      // Get's No of Rows Count 
      int count = _unitOfWork.AspNetUsersTypes_rolesRepository.Count();

      // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1
      int CurrentPage = pageNumber;

      // Parameter is passed from Query string if it is null then it default Value will be pageSize:20
      int PageSize = pageSize;

      // Display TotalCount to Records to User
      int TotalCount = count;

      // Calculating Totalpage by Dividing (No of Records / Pagesize)
      int TotalPages = (int)Math.Ceiling(count / (double)PageSize);


      // Returns List of Customer after applying Paging 
      var items = _unitOfWork.AspNetUsersTypes_rolesRepository.Get(page: pageNumber, Take: pageSize);

      // if CurrentPage is greater than 1 means it has previousPage
      var previousPage = CurrentPage > 1 ? "Yes" : "No";

      // if TotalPages is greater than CurrentPage means it has nextPage
      var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
      // map 
      var aspNetUsersTypes_rolesViewModel = _mapper.Map<List<AspNetUsersTypes_rolesViewModel>>(items);
      // Object which we are going to send in header 
      paginationMetadata paginationMetadata = new paginationMetadata
      {
        totalCount = TotalCount,
        pageSize = PageSize,
        currentPage = CurrentPage,
        nextPage = nextPage,
        previousPage = previousPage,
        data = aspNetUsersTypes_rolesViewModel
      };
      return new Result
      {
        success = true,
        code = "200",
        data = paginationMetadata
      };
    }
    #endregion
    #region getAllAspNetUserType
    public Result getAllAspNetUserType()
    {
      var aspNetType=_unitOfWork.AspNetUsersTypesRepository.Get();
     var aspNetTypeViewModel= _mapper.Map<List<AspNetUsersTypesViewModel>>(aspNetType);
      return new Result {
        success=true,
        code="200",
        data= aspNetTypeViewModel
      };
    }

    public Task<Result> UpdatespNetUsersTypes_roles(AspNetUsersTypes_rolesInsertViewModel aspNetUsersTypes_rolesViewModel)
    {
      throw new NotImplementedException();
    }

    #endregion

    //#region UpdatespNetUsersTypes_roles
    //public async Task<Result> UpdatespNetUsersTypes_roles(AspNetUsersTypes_rolesInsertViewModel aspNetUsersTypes_rolesViewModel)
    //{
    //  AspNetUsersTypes_roles aspNetUsersTypes_roles = new AspNetUsersTypes_roles();
    //  aspNetUsersTypes_rolesViewModel.ModifiedOn = DateTime.Now;
    //  var Dto=_mapper.Map(aspNetUsersTypes_rolesViewModel, aspNetUsersTypes_roles);
    //  _unitOfWork.AspNetUsersTypes_rolesRepository.Update(Dto);
    //  var result1 =await _unitOfWork.SaveAsync();
    //  if (result1 == 200)
    //  {
    //    return new Result
    //    {

    //      success = true,
    //      code = "200",
    //      message = "row added successfuly"
    //    };
    //  }
    //  else
    //  {
    //    return new Result
    //    {

    //      success = true,
    //      code = "400",
    //      message = "row added faild"
    //    };
    //  }

    //}
    //#endregion
  }
}
