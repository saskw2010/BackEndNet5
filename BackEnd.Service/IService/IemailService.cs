using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IemailService
  {
    Task<Boolean> sendVerfication(int verficationCode,string Email);
    Task<Boolean> sendVerficationMobile(int verficationCode,string Email);
  }
}
