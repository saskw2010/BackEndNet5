using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
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
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }
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
    #endregion

  }
}
