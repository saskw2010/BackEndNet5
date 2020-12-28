using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.BAL.Repository;
using BackEnd.DAL.Context;
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
  public class EsSrTechnicalService : IEsSrTechnicalService
  {
    private IUnitOfWork _unitOfWork;
    private readonly BakEndContext _BakEndContext;
    private readonly UserManager<ApplicationUser> _userManager;
    public EsSrTechnicalService(IUnitOfWork unitOfWork,
      BakEndContext BakEndContext,
      UserManager<ApplicationUser> userManager)
    {
      _unitOfWork = unitOfWork;
      _BakEndContext = BakEndContext;
      _userManager = userManager;
    }
    #region createUserForTechnicalAsync
    public async Task<Result> createUserForTechnicalAsync()
    {
      try
      {
        List<EsSrTechnical> esSrClientList = _unitOfWork.EsSrTechnicalRepository.Get().ToList();
        foreach (var item in esSrClientList)
        {
          var User = FindByEmailCustome(item.Email);
          if (User == null)
          {
            await addUser(item.Email, item.Email, item.Phone, item.HashPassword);
          }
        }
        return new Result
        {
          success = true,
          code = "200",
          message = "User add Successfuly"
        };
      }
      catch (Exception ex)
      {
        return new Result
        {
          success = false,
          code = "403",
          message = "faild"
        };
      }
    }
    #endregion
    #region FindByEmailCustome
    public ApplicationUser FindByEmailCustome(string email)
    {
      return _BakEndContext.Users.FirstOrDefault(x => x.Email == email);
    }
    #endregion


    #region addUser
    public async Task addUser(string UserName, string Email, string PhoneNumber, string Password)
    {
      //int num = _random.Next(1000, 9999);
      // await _emailService.sendVerficationMobile(num, Email);

      var newUser = new ApplicationUser
      {
        Email = Email,
        UserName = UserName,
        PhoneNumber = PhoneNumber,
        //PasswordHash= Encrypt(Password,"xxx"),
        PasswordHash = Password,
        userTypeId = 4,
        confirmed = true,
        EmailConfirmed = true,
        IsApproved = true,
        PhoneNumberConfirmed = true,
        CreationDate = DateTime.Now,
        LastLoginDate = DateTime.Now,
        LastActivityDate = DateTime.Now,
        LastPasswordChangedDate = DateTime.Now,
        LastLockedOutDate = DateTime.Now,
        LastLockoutDate = DateTime.Now,
      };

      await _userManager.CreateAsync(newUser);
      //-----------------------------add Role to token------------------
      await _userManager.AddToRoleAsync(newUser, "Technical");
      //-----------------------------------------------------------------
    }
    #endregion

  }
}
