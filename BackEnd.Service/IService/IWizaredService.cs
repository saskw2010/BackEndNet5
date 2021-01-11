using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.IService
{
  public interface IWizaredService
  {
    Boolean createBackUp(string path, string fileName);
    Boolean DeleteOldFile(string path, string fileName);
  }
}
