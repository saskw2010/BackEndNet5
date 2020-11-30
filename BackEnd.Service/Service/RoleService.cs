using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
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
  }
}
