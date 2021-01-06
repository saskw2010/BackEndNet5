using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Service.Service
{
  public class EsSrWorkshopRegionServices : IEsSrWorkshopRegionServices
  {
    
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public EsSrWorkshopRegionServices(IUnitOfWork unitOfWork, IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }
    #region checkAvailabelItem
    public Result checkAvailabelItem(long itemId,decimal lng,decimal lat)
    {
      try
      {
        var itemRegion = _unitOfWork.EsSrWorkshopRegionRepository.Get(filter:
          (reg => reg.EsSrTechnicals.Any(x => x.EsSrItemTechnicals.Any(it =>
          (it.ItemId == itemId) && (reg.IsActive == true) && (reg.IsDelete == false)))
          &&(reg.IsActive==true) && (reg.IsDelete == false)));
        var itemReginList = new List<EsSrWorkshopRegion>();
        var itemRegionVm = new List<EsSrWorkshopRegionViewModel>();
        foreach (var item in itemRegion)
        {
          if (item.MapLangitude != null && item.MapLatitude != null)
          {
            var dist = findDistanceBetweenTwoCoordinates(lng, lat, item.MapLangitude.Value, item.MapLatitude.Value)/1000;
            if (item.MapArea >= dist)
            {
              itemReginList.Add(item);
            }
            itemRegionVm = _mapper.Map<List<EsSrWorkshopRegionViewModel>>(itemReginList);
          }

        }

        return new Result
        {
          success = true,
          code = "200",
          data = itemRegionVm
        };
      }
      catch (Exception ex) {
        return new Result
        {
          success = false,
          code = "403",
          data = null,
          message=ex.Message
        };
      }
   
    }
    #endregion

    decimal findDistanceBetweenTwoCoordinates(decimal langFromRequest, decimal latFromRequest, decimal lanFromWrkShop, decimal latFromWorkShop) {
        var coord = new GeoCoordinate(Convert.ToDouble(langFromRequest), Convert.ToDouble(latFromRequest));
        var dist=coord.GetDistanceTo(new GeoCoordinate(Convert.ToDouble(lanFromWrkShop), Convert.ToDouble(latFromWorkShop)));
      return Convert.ToDecimal(dist);
    }

  }
}
