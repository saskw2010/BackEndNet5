// using System;
// using System.Data;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text.RegularExpressions;
// using System.Web;
// using System.Web.Security;
// using eZee.Data;
// using eZee.Models;

// namespace eZee.Rules
// {
// public partial class WorkspacelistBusinessRules : eZee.Rules.SharedBusinessRules
// {

// /// <summary>Rule "inserworkplace" implementation:
// /// This method will execute in any view after an action
// /// with a command name that matches "Insert|Update".
// /// </summary>
// [Rule("r100")]
// public void r100Implementation(WorkspacelistModel instance)
// {
// // This is the placeholder for method implementation.
// }
// }
// }
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
//using eZee.Data;
//using eZee.Models;
using Microsoft.Web.Administration;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using BackEnd.BAL.Models;
using Microsoft.Extensions.Configuration;
using BackEnd.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Service.Service
{
  public partial class WorkspacelistBusinessRules 
  {

    public IConfiguration Configuration { get; }

    private readonly BakEndContext Context;


    public WorkspacelistBusinessRules(IConfiguration iConfig, BakEndContext dbContext)
    {
      Configuration = iConfig;
      Context = dbContext;
    }

    /// <summary>
    ///         ''' Rule "workspaceafterinsert" implementation:
    ///         ''' This method will execute in any view after an action
    ///         ''' with a command name that matches "Insert".
    ///         ''' </summary>

    public Result r100Implementation(WorkSpaceVm workspace)
    {
      // This is the placeholder for method implementation.

      string MyApplicationPoolstring = "";
      MyApplicationPoolstring = workspace.WorkSpaceName;
      ServerManager server = new ServerManager();
      ApplicationPool myApplicationPool = null/* TODO Change to default(_) if this is not a reference type */;

      if (server.ApplicationPools != null && server.ApplicationPools.Count > 0)
      {
        if (server.ApplicationPools.Any(p => p.Name == MyApplicationPoolstring))
          myApplicationPool = server.ApplicationPools.First(p => p.Name == MyApplicationPoolstring);
        else
          myApplicationPool = server.ApplicationPools.Add(MyApplicationPoolstring);
      }
      else
        myApplicationPool = server.ApplicationPools.Add(MyApplicationPoolstring);

      if (myApplicationPool != null)
      {
        myApplicationPool.ProcessModel.IdentityType = ProcessModelIdentityType.LocalSystem;
        myApplicationPool.ManagedRuntimeVersion = "v4.0";
        server.CommitChanges();
      }






      if (server.Sites != null && server.Sites.Count > 0)
      {
        if (server.Sites.Any(s => s.Name == MyApplicationPoolstring) == false)
        {
          // 'create folders  and copy files
          string appDirectory = @"c:\MySiteFolder\";
          // 'HttpContext.Current.Server.MapPath("~")
          string CacheFolder = System.IO.Path.Combine(appDirectory, MyApplicationPoolstring);
          if (!System.IO.Directory.Exists(CacheFolder))
            System.IO.Directory.CreateDirectory(CacheFolder);
          // ---------------------------------------------------

          string sourceDirectory = @"c:\sourceDirectory";
          string targetDirectory = CacheFolder;
          Copy(sourceDirectory, targetDirectory);
          // Result.ShowMessage(CacheFolder + "  Copying")
          // WebConfigurationManager.ConnectionStrings("eZee").ToString()
          // If IsNothing(ConfigurationManager.AppSettings("imagepath")) Then
          var ServerConfiguration = Configuration
           .GetSection("ServerConfiguration")
           .Get<ServerConfiguration>();
          string masterservername = ".";
         
          if (ServerConfiguration != null)
          {
            masterservername = ServerConfiguration.masterserver;
          }
          else
           // masterservername = ConfigurationManager.AppSettings["masterserver"].ToString();
            masterservername= ".";
          // ---------------------------------------------
          string path = CacheFolder;
          string ip = "*";
          string port = "80";
          string hostName = MyApplicationPoolstring + "." + masterservername;
          string bindingInfo = string.Format("{0}:{1}:{2}", ip, port, hostName);
          Site site = server.Sites.Add(MyApplicationPoolstring, "http", bindingInfo, path);
          site.ApplicationDefaults.ApplicationPoolName = myApplicationPool.Name;
          server.CommitChanges();
      

          // Dim file1 As New FileInfo(server.MapPath(Url.Content("~/SqlScripts/CreateDB.sql"))
          // createDB(file)

          string sqlConnectionString = "Data Source=" + masterservername + ";Initial Catalog=" + MyApplicationPoolstring + ";Persist Security Info=True;User ID=sa;Password=mos@2017;"; 
          //commit
          string sqlfilepathscrpt = @"c:\sourceDirectory\SqlScripts\installsqlmembership.sql";
          // 'Dim script As String = file.OpenText().ReadToEnd()
          createDataBase(workspace.WorkSpaceName);
          //commit
          bool xpoli;
          xpoli = createfromsqlscript(sqlfilepathscrpt, sqlConnectionString);

          

          // Dim conn As New SqlConnection(sqlConnectionString)
          // Dim server1 As New Server(New ServerConnection(conn))
          // server1.ConnectionContext.ExecuteNonQuery(script)

          // for waiting javascript
          // Result.ExecuteOnClient()

          bool xpol;
          xpol = CreateTables(@"C:\sourceDirectory\SqlScripts\Tables", sqlConnectionString);



          bool xpol1;
          xpol1 = CreateTables(@"C:\sourceDirectory\SqlScripts\Indexes", sqlConnectionString);

          // 'Dim strSQL As String
          // 'Dim f As System.IO.File
          // 'Dim strR As System.IO.StreamReader
          // 'strR = f.OpenText("c:\sourceDirectory\installsqlmembership.sql")
          // 'strSQL = strR.ReadToEnd()
          // 'strSQL = strR.ReadToEnd.Replace("GO", ";")
          // 'Dim scriptArr As String()
          // 'scriptArr = strSQL.Split(";")
          // 'Dim sql1 As SqlText
          // 'Try
          // '    For i = 0 To scriptArr.Length - 1
          // '        sql1 = New SqlText(scriptArr.GetValue(i))
          // '        sql1.ExecuteNonQuery()
          // '    Next
          // 'Finally
          // 'End Try


          // 'Dim sServer As SQLDMO.SQLServer = New SQLDMO.SQLServer

          // 'sServer.Connect("ERALPER", "loginname", "password")

          // 'Dim oDataBase As SQLDMO.Database
          // ' sServer.Databases().Item("DBName").ExecuteImmediate(strSQL)

          // --------------------------------------------------
          string reportFileNamemossopath = targetDirectory + @"\";
          string reportFileNamemosso = "email.config";
          string myfilestring;
          myfilestring = "*email.config*";
          foreach (var file in new DirectoryInfo(reportFileNamemossopath).GetFiles(myfilestring))
            file.Delete();
          Encoding utfencoder = UTF8Encoding.GetEncoding("UTF-8", new EncoderReplacementFallback(""), new DecoderReplacementFallback(""));



          string newLine = Environment.NewLine;
          string s = "<connectionStrings> " + newLine + " <clear/> " + newLine + "" + " <remove name='LocalSqlServer' /> <add name='LocalSqlServer' connectionString='Data Source=" + masterservername + ";Initial Catalog=" + MyApplicationPoolstring + ";Persist Security Info=True;User ID=" + MyApplicationPoolstring + ";Password=" + MyApplicationPoolstring + "mos@2017;' providerName='System.Data.SqlClient' />" + " <add name='eZee' connectionString='Data Source=" + masterservername + ";Initial Catalog=" + MyApplicationPoolstring + ";Persist Security Info=True;User ID=" + MyApplicationPoolstring + ";Password=mos@2017;' providerName='System.Data.SqlClient' /> " + " <add name='FreeTrial' connectionString='Data Source=" + masterservername + ";Initial Catalog=" + MyApplicationPoolstring + ";Persist Security Info=True;User ID=" + MyApplicationPoolstring + ";Password=mos@2017;' providerName='System.Data.SqlClient'/>" + newLine + " </connectionStrings> ";


          XmlDocument doc = new XmlDocument();

          doc.LoadXml(s);
          // --Save the document to a file.  
          doc.Save(reportFileNamemossopath + reportFileNamemosso);
        }
      }
      return new Result {
        success=true,
        message="workspace added successfuly",
        code="200",
        data=null
      };
    }

    private bool createDataBase(string dbname)
    {
      try
      {
        object[] parameters =
                 { dbname };
        Context.Database.ExecuteSqlCommand("EXEC [dbo].[ValidateRequestPost1]  {0}", parameters);
        return true;
      }
      catch
      {
        return false;
      }

    }

    public bool createKeys(string sqlFilepath, string SqlConnectionString)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(SqlConnectionString))
        {
          SqlCommand command = new SqlCommand();
          command.Connection = connection;
          command.Connection.Open();
          var folderList = Directory.EnumerateFiles(sqlFilepath);
          bool xpoli;
          foreach (var curr in folderList)
          {
            FileInfo file = new FileInfo(curr);
            string sqlfilepathscrpt = file.FullName;
            xpoli = createfromsqlscript(sqlfilepathscrpt, SqlConnectionString);
          }

          command.Connection.Close();
        }

        return true;
      }
      catch
      {
        return false;
      }
    }
    public bool CreateTables(string sqlFilepath, string sqlConnectionString)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(sqlConnectionString))
        {
          SqlCommand command = new SqlCommand();
          command.Connection = connection;
          command.Connection.Open();
          var folderList = Directory.EnumerateFiles(sqlFilepath);
          bool xpoli;
          foreach (var curr in folderList)
          {
            FileInfo file = new FileInfo(curr);
            string sqlfilepathscrpt = file.FullName;
            xpoli = createfromsqlscript(sqlfilepathscrpt, sqlConnectionString);
          }

          command.Connection.Close();
        }

        return true;
      }
      catch
      {
        return false;
      }
    }


    public bool createfromsqlscript(string sqlFilepath, string SqlConnectionString)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(SqlConnectionString))
        {
          SqlCommand command = new SqlCommand();
          command.Connection = connection;
          command.Connection.Open();
          FileInfo file = new FileInfo(sqlFilepath);
          string script = file.OpenText().ReadToEnd();
          command.CommandText = script;
          command.ExecuteNonQuery();
          command.Connection.Close();
          return true;
        }
      }
      catch
      {
        return false;
      }
    }

 public static void Copy(string sourceDirectory, string targetDirectory)
    {
      DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
      DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);
      CopyAll(diSource, diTarget);
    }

    public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
      if (!System.IO.Directory.Exists(target.FullName))
        System.IO.Directory.CreateDirectory(target.FullName);



      foreach (FileInfo fi in source.GetFiles())
      {
        Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
        fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
      }

      foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
      {
        DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
        CopyAll(diSourceSubDir, nextTargetSubDir);
      }
    }
  }
  public class CustomXmlWriter : XmlTextWriter
  {
    public CustomXmlWriter(TextWriter writer) : base(writer)
    {
    }
    public CustomXmlWriter(Stream stream, Encoding encoding) : base(stream, encoding)
    {
    }
    public CustomXmlWriter(string file, Encoding encoding) : base(file, encoding)
    {
    }
    public override void WriteString(string text)
    {
      Encoding utfencoder = UTF8Encoding.GetEncoding("UTF-8", new EncoderReplacementFallback(""), new DecoderReplacementFallback(""));
      byte[] bytText = utfencoder.GetBytes(text);
      string strEncodedText = utfencoder.GetString(bytText);
      base.WriteString(strEncodedText);
    }

  
  }
}
