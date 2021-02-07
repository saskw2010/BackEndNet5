using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class dataController_viewsViewModel
  {
    public long dataController_views_view { get; set; }
    public string dataController_name { get; set; }
    public string dataController_views_view_id { get; set; }
    public string dataController_views_view_type { get; set; }
    public string dataController_views_view_label { get; set; }
    public string dataController_views_view_commandId { get; set; }
    public string dataController_views_view_headerText { get; set; }
    public Nullable<int> datacontrollerFkId { get; set; }
    public List<dataController_dataFieldsGridViewModel> dataController_dataFields { get; set; }
    public virtual List<dataController_categoryCreateVm> dataController_categoryCreate { get; set; }
    public virtual List<dataController_categoryEditVm> dataController_categoryEdit { get; set; }
  }
}
