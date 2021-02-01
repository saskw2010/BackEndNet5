using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class dataControllerViewModel
  {
    public int Id { get; set; }
    public string dataController_name { get; set; }
    public string dataController_nativeSchema { get; set; }
    public string dataController_nativeTableName { get; set; }
    public string dataController_conflictDetection { get; set; }
    public string dataController_label { get; set; }
    public Nullable<int> xmlFkId { get; set; }
    public List<dataController_commandstableslistVM> dataController_commandstableslist { get; set; }
    public List<dataController_commandsViewModel> dataController_commands { get; set; }
    public List<dataController_viewsViewModel> dataController_views { get; set; }
    public List<dataController_fields_fieldViewModel> dataController_fields_field { get; set; }
  }
}
