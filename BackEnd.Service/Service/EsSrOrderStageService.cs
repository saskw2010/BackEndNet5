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
  class EsSrOrderStageService : IEsSrOrderStageService
  {
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public EsSrOrderStageService(
      IUnitOfWork unitOfWork,
      IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }
    public async Task<Result> UpdateEsSrOrderStage(EsSrOrderStageViewModel esSrOrderViewModel)
    {
      try
      {
        EsSrOrderStage esSrOrderStage = new EsSrOrderStage();
        var obje = _mapper.Map(esSrOrderViewModel, esSrOrderStage);
        obje.IsDelete = false;
        obje.IsActive = true;
        _unitOfWork.EsSrOrderStageRepository.Update(obje);
        var result1 = await _unitOfWork.SaveAsync();
        if (result1 == 200)
        {
          return new Result
          {
            success = true,
            code = "200",
            message = "Update Client Faild",
            data = null
          };
        }
        else
        {
          return new Result
          {
            success = false,
            code = "403",
            message = "Update Client Faild",
            data = null
          };
        }
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
  }
}
