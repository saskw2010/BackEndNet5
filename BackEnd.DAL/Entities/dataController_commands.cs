using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.DAL.Entities
{
  public  class dataController_commands
  {
        [Key]
        public int dataController_commands_command { get; set; }
  
        public string dataController_name { get; set; }
        public string dataController_commands_command_type { get; set; }
        public string dataController_commands_command_id { get; set; }
        public string dataController_commands_command_text { get; set; }
        public string dataController_commands_command_event { get; set; }
        public string dataController_commands_command_output_fieldOutput_fieldName { get; set; }
        [ForeignKey("dataController")]
        public Nullable<int> datacontrollerFkId { get; set; }
        public virtual dataController dataController { get; set; }

    }
}
