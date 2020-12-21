using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
  public class FileManagerServices : IFileManagerServices
  {
    private readonly RoleManager<IdentityRole> _roleManager;
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public FileManagerServices(
      RoleManager<IdentityRole> roleManager,
      IUnitOfWork unitOfWork,
      IMapper mapper)
    {
      _roleManager = roleManager;
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }
    #region GetAllFileManagersByRolesName
    public async Task<Result> GetAllFileManagersByRolesName(List<string> RoleNames)
    {
      try
      {
        var fileManagersList = new List<FileManager>();
        foreach (var item in RoleNames)
        {
          string RoleId = await getRoleId(item);
          var result1 = _unitOfWork.FileManagerRoleRepository.Get(filter: (x => x.aspNetRoleFkId == RoleId)).ToList();
          foreach (var item2 in result1)
          {
            fileManagersList.Add(item2.FileManager);
          }
        }
        var FileManagerVmList = _mapper.Map<List<FileManagerViewModel>>(fileManagersList);
        return new Result
        {
          success = true,
          code = "200",
          data = FileManagerVmList
        };
      }
      catch (Exception ex) {
        return new Result
        {
          success = false,
          code = "403",
          data = null
        };
      }
    }
    #endregion

    private async Task<string> getRoleId(string RoleName) {
      var role=await _roleManager.FindByNameAsync(RoleName);
      return role.Id;
    }
  }
}
