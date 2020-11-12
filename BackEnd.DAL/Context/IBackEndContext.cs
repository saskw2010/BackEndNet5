using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using BackEnd.DAL.Entities;

namespace RealState.DAL.Context
{
  public interface IBackEndContext
  {
    DbSet<WorkSpace> WorkSpaces { get; set; }
    DbSet<UserType> UserType { get; set; }
  }
}
