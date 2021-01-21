
using System;
using System.Collections.Generic;
namespace  BackEnd.DAL.Entities

{
  public  class dataController_commandstableslist
  {
    public int dataController_commands_commandtableslist { get; set; }
    public string dataController_name { get; set; }
    public string dataController_commands_command_tableslist { get; set; }
    public string dataController_commands_command_fieldslist { get; set; }
    public string dataController_commands_command_slist { get; set; }

    public virtual dataController dataController { get; set; }
  }
}
