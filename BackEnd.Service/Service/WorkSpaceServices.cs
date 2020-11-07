using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.BAL.Repository;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
  public class WorkSpaceServices : IworkSapceServices
  {
    private UnitOfWork unitOfWork;
    private IMapper _mapper;
    public WorkSpaceServices(IUnitOfWork unitOfWork,
      BakEndContext context,
      IMapper mapper
      )
    {
      this.unitOfWork = new UnitOfWork(context);
      _mapper = mapper;
    }
    public async Task<Boolean> CreateWorkspace(WorkSpaceVm workspace)
    {
      string domainName = workspace.WorkSpaceName;
      string appPoolName = "Classic .NET AppPool";
      string webFiles = "F:\\asd";
      if (IsWebsiteExists(domainName) == false)
      {
        ServerManager iisManager = new ServerManager();
        iisManager.Sites.Add(domainName, "http", "*:8080:", webFiles);
        iisManager.ApplicationDefaults.ApplicationPoolName = appPoolName;
        iisManager.CommitChanges();
        return true;
      }
      else
      {
        return false;
      }
    }


    

    public static bool IsWebsiteExists(string strWebsitename)
    {
      ServerManager serverMgr = new ServerManager();
      Boolean flagset = false;
      SiteCollection sitecollection = serverMgr.Sites;
      flagset = sitecollection.Any(x => x.Name == strWebsitename);
      return flagset;
    }

    public async Task<bool> InsertWorkspace(WorkSpaceVm workSpaceVm)
    {
      //WorkSpace workspace = new WorkSpace {
      //  UserName = workSpaceVm.UserName,
      //  WorkSpaceName= workSpaceVm.WorkSpaceName,
      //  DatabaseName= workSpaceVm.DatabaseName
      //};
      WorkSpace workspace = _mapper.Map<WorkSpace>(workSpaceVm);
      unitOfWork.WorkSpaceRepository.Insert(workspace);
      await  unitOfWork.SaveAsync();
      return true;
    }
  }
}
