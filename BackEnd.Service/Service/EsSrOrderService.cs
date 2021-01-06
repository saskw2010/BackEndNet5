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
  public class EsSrOrderService : IEsSrOrderService
  {
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public EsSrOrderService(IUnitOfWork unitOfWork,
      IMapper mapper
      )
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }
    #region saveOrder
    public async Task<Result> saveOrder(EsSrOrderViewModel esSrOrderVm)
    {
      try
      {
        EsSrOrder esSrOrder = new EsSrOrder();
        var obje = _mapper.Map(esSrOrderVm, esSrOrder);
        _unitOfWork.EsSrOrderRepository.Insert(obje);
        var res1 = await _unitOfWork.SaveAsync();
        //---------getOrderStageByCategoryId
        var orderStageBase=getOrderStageByCategoryId(esSrOrderVm.CatgeoryId.Value);
        foreach (var item in orderStageBase) {
          var res2 = addOrderStage(item, obje.OrderId, obje.CreatedBy);
        }
        //---------------update orderStageId in order
        long orderStageId= GetFirstElementOfOrderStageByOrderId(obje.OrderId);
        updateOrderStage(obje.OrderId, orderStageId);



        if (res1 == 200)
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
            success = false,
            code = "403",
            message = "row added Falid"
          };
        }
        //--------end getOrderStageByCategoryId
       
    }
      catch (Exception ex)
      {
        return new Result {
          success = false,
          code = "400",
          data = null,
          message = ExtensionMethods.FullMessage(ex)
  };
}
    }
    #endregion

    #region getOrderStageByCategoryId
    public List<EsSrOrderStageBase> getOrderStageByCategoryId(long CatgeoryId) {
     var res= _unitOfWork.EsSrOrderStageBaseCatgeoryRepository.Get(filter: (x=>x.CatgeoryId== CatgeoryId)).ToList();
      List<EsSrOrderStageBase> esSrOrderStageBaseList = new List<EsSrOrderStageBase>();
      foreach (var item in res) {
        EsSrOrderStageBase esSrOrderStageBase = new EsSrOrderStageBase();
        esSrOrderStageBase = item.EsSrOrderStageBase;
        esSrOrderStageBaseList.Add(esSrOrderStageBase);
      }
      return esSrOrderStageBaseList;
    }
    #endregion

    #region addOrderStage
    private Boolean addOrderStage(EsSrOrderStageBase esSrOrderStageBase,long OrderId,string createdBy) {
      try {
        EsSrOrderStage esSrOrderStage = new EsSrOrderStage();
        var obje = _mapper.Map(esSrOrderStageBase, esSrOrderStage);
        obje.OrderId = OrderId;
        obje.CreatedBy = createdBy;
        obje.CreatedOn = DateTime.Now;
        obje.ModifiedOn = null;
        obje.ModifiedBy = null;
        obje.ProgressValue = 0;
        _unitOfWork.EsSrOrderStageRepository.Insert(obje);
        _unitOfWork.Save();
        return true;
      }
      catch (Exception ex) {
        return false;
      }
     
    }
    #endregion

    #region GetOrderStage of orderStageBy orderId
    long GetFirstElementOfOrderStageByOrderId(long orderID) {
     var res= _unitOfWork.EsSrOrderStageRepository.Get(filter: (x => x.OrderId == orderID)).OrderBy(y=>y.ShowOrder);
      return res.FirstOrDefault().OrderStageId;
    }

    #endregion

    #region deleteOrderById
    private void RoleBackByOrderId(long orderId) {
      _unitOfWork.EsSrOrderRepository.Delete(orderId);
    }
    #endregion

    #region updateOrderStage
    Boolean updateOrderStage(long orderID,long orderStageId) {
      var order=_unitOfWork.EsSrOrderRepository.GetEntity(x => x.OrderId == orderID);
      order.OrderStageId = orderStageId;
      _unitOfWork.EsSrOrderRepository.Update(order);
      _unitOfWork.Save();
      return true;
    }
    #endregion

    #region UpdateOrder
    public async Task<Result> UpdateOrder(EsSrOrderViewModel esSrOrderVm)
    {
      try
      {
        var orderStageId= await AddOrderStageUpdateOrder(esSrOrderVm);
        if (orderStageId > 0)
        {
          EsSrOrder esSrOrder = new EsSrOrder();
          var obje = _mapper.Map(esSrOrderVm, esSrOrder);
          obje.OrderStageId = orderStageId;
          _unitOfWork.EsSrOrderRepository.Update(obje);
          await _unitOfWork.SaveAsync();
          return new Result
          {
            success = true,
            code = "200",
            message = "row updated successfuly"
          };
        }
        else {
          return new Result
          {
            success = false,
            code = "403",
            message = "row updated Falid"
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
    #endregion

    #region AddOrderStage
    public async Task<long> AddOrderStageUpdateOrder(EsSrOrderViewModel esSrOrderVm) {
      try
      {
        EsSrOrderStage esSrOrderStage = new EsSrOrderStage
        {
          ShowOrder = esSrOrderVm.OrderStageShowOrder,
          OrderId = esSrOrderVm.OrderId,
          DescriptionAr =  " تم تعديل الطلب رقم "+ esSrOrderVm.OrderId,
          DescriptionEn = "order number " + esSrOrderVm.OrderId + " is updated .",
          IsActive = true,
          CreatedBy = esSrOrderVm.CreatedBy,
          CreatedOn = DateTime.Now,
          IsDelete = false
        };
        _unitOfWork.EsSrOrderStageRepository.Insert(esSrOrderStage);
        await _unitOfWork.SaveAsync();
        return esSrOrderStage.OrderStageId;

    }
      catch (Exception ex) {
        return 0;
      }

}
    #endregion

  }
}
