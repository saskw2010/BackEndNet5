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
  public class EsSrAttacheService : IEsSrAttacheService
  {
   
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public EsSrAttacheService(IUnitOfWork unitOfWork, IMapper mapper
      )
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }
    public async Task<Result> saveAttatche(List<string> FileName, long OrderId,string UserName, long Length)
    {
      try {
        List<EsSrAttache> ListesSrAttache = new List<EsSrAttache>();
        foreach (var item in FileName) {
          EsSrAttache esSrAttache = new EsSrAttache()
          {
            FileName = item,
            OrderId = OrderId,
            IsDelete = false,
            IsActive = true,
            CreatedBy=UserName,
            CreatedOn = DateTime.Now,
            Length=Length
          };
          ListesSrAttache.Add(esSrAttache);
        }
       
        _unitOfWork.EsSrAttacheRepository.AddRange(ListesSrAttache);
        var res = await _unitOfWork.SaveAsync();


        if (res == 200)
        {
          return new Result
          {
            success = true,
            code = "200",
            message = "attache added successfuly"
          };
        }
        else
        {
          return new Result
          {
            success = false,
            code = "403",
            message = "attache added faild"
          };
        }
      }
      catch (Exception ex) {
        return new Result
        {
          success = false,
          code = "403",
          message = ex.Message
        };
      }
    
    }

    public async Task<Result> UpdateAttatch(long essrAttatchId, string FileName, string UserName, long Length)
    {
      try
      {
       
        EsSrAttache EsSrAttacheObj =await _unitOfWork.EsSrAttacheRepository.GetAsyncByID(essrAttatchId);
        string folderName = "wwwroot/UploadFiles";
        var Oldfile = System.IO.Path.Combine(folderName, EsSrAttacheObj.FileName);
        try
        {
          System.IO.File.Delete(Oldfile);
        }
        catch { }
        EsSrAttacheObj.FileName = FileName;
        EsSrAttacheObj.ModifiedBy = UserName;
        EsSrAttacheObj.Length = Length;
        EsSrAttacheObj.ModifiedOn = DateTime.Now;
        _unitOfWork.EsSrAttacheRepository.Update(EsSrAttacheObj);
        var res = await _unitOfWork.SaveAsync();
       
        if (res == 200)
        {
          return new Result
          {
            success = true,
            code = "200",
            message = "attache added successfuly"
          };
        }
        else
        {
          return new Result
          {
            success = false,
            code = "403",
            message = "attache added faild"
          };
        }
      }
      catch (Exception ex)
      {
        return new Result
        {
          success = false,
          code = "403",
          message = ex.Message
        };
      }
    }
  }
}
