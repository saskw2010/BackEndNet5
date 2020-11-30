using AutoMapper;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.MappingProfiles
{
  public class MappingProfiles : Profile {
    public MappingProfiles()
    {
      CreateMap<WorkSpace,WorkSpaceVm>();
      CreateMap<WorkSpaceVm, WorkSpace>();

      CreateMap<userVm, ApplicationUser>();
      CreateMap<ApplicationUser, ApplicationUser>();
      CreateMap<AspNetUsersTypes, AspNetUsersTypesViewModel>().ReverseMap();
    }
  }
}
