using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class dataController_commandsViewModel
  {
    public int dataController_commands_command { get; set; }
    public string dataController_name { get; set; }
    public string dataController_commands_command_type { get; set; }
    public string dataController_commands_command_id { get; set; }
    public string dataController_commands_command_text { get; set; }
    public string dataController_commands_command_event { get; set; }
    public string dataController_commands_command_output_fieldOutput_fieldName { get; set; }
    public Nullable<int> datacontrollerFkId { get; set; }
  }
}
