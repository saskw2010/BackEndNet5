using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BackEnd.Service.Service
{
  public class WizaredService: IWizaredService
  {
    #region createBackUp
    public Boolean createBackUp(string path, string fileName)
    {
      try
      {
        string sourceDirectory = path;
        string destinationDirectory = System.IO.Path.Combine(sourceDirectory, "BackUp");
        if (!System.IO.Directory.Exists(destinationDirectory))
          System.IO.Directory.CreateDirectory(destinationDirectory);
        string extension = ".model.xml";
        var fileNameWithDate = fileName + DateTime.Now.Ticks;
        var fileNameWIthExtenation = fileName + extension;
        var fileVersionName = fileNameWithDate + extension;
        File.Copy(Path.Combine(sourceDirectory, fileNameWIthExtenation), Path.Combine(destinationDirectory, fileVersionName));
        return true;
    }
      catch (Exception ex) {
        return false;
      }

}
    #endregion

    #region DeleteOldModel
    public bool DeleteOldFile(string path, string fileName)
    {
      try
      {
        if (File.Exists(Path.Combine(path, fileName)))
        {
          // If file found, delete it    
          File.Delete(Path.Combine(path, fileName));
          return true;
    }
        return false;
      }
      catch (Exception ex) {
        return false;
      }
     
    }
    #endregion

  }
}
