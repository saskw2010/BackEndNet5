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
    DbSet<AspNetUsersTypes> AspNetUsersTypes { get; set; }
    DbSet<AspNetusertypjoin> AspNetusertypjoin { get; set; }
    DbSet<AspNetUsersTypes_roles> AspNetUsersTypes_roles { get; set; }
    DbSet<EsSrClient> EsSrClient { get; set; }
    DbSet<EsSrTechnical> EsSrTechnical { get; set; }
  }
}
