using AutoMapper;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Service.MappingProfiles
{
  public class MappingProfiles : Profile {
    public MappingProfiles()
    {
      CreateMap<WorkSpace,WorkSpaceVm>();
      CreateMap<WorkSpaceVm, WorkSpace>();

      CreateMap<userVm, ApplicationUser>();
    
      CreateMap<AspNetUsersTypes, AspNetUsersTypesViewModel>().ReverseMap();
      CreateMap<AspNetUsersTypes_roles, AspNetUsersTypes_rolesViewModel>()
        .ForMember(dest =>
            dest.AspNetUsersTypes,
            opt => opt.MapFrom(src => src.AspNetUsersTypes.UsrTypNm))
          .ForMember(dest =>
            dest.IdentityRole,
            opt => opt.MapFrom(src => src.IdentityRole.Name))
        .ReverseMap();

      CreateMap<AspNetUsersTypes_roles, AspNetUsersTypes_rolesInsertViewModel>().ReverseMap();
      CreateMap<EsSrClient, EsSrClientViewModel>().ReverseMap();
      CreateMap<EsSrTechnical, EsSrTechnicalViewModel>().ReverseMap();
      CreateMap<FileManager, FileManagerViewModel>()
        .ForMember(dest =>
             dest.filemanagerName,
             opt => opt.MapFrom(src => src.filemanagerName))
             .ReverseMap();

      CreateMap<EsSrItemTechnical, EsSrItemTechnicalViewModel>().ReverseMap();
      CreateMap<EsSrWorkshopRegion, EsSrWorkshopRegionViewModel>().ReverseMap();
      CreateMap<EsSrOrder, EsSrOrderViewModel>().ReverseMap();
      CreateMap<EsSrOrderStage, EsSrOrderStageBase>().ReverseMap();


    }
  }
}
