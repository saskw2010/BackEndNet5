using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.DAL.Entities
{
public  class dataController
    {
        [Key]
        public string dataController_name { get; set; }
        public string dataController_nativeSchema { get; set; }
        public string dataController_nativeTableName { get; set; }
        public string dataController_conflictDetection { get; set; }
        public string dataController_label { get; set; }
        [ForeignKey("dataControllerCollection")]
        public Nullable<int> dataControllerCollection { get; set; }
        public virtual ICollection<dataController_commands> dataController_commands { get; set; }
        public virtual ICollection<dataController_commandstableslist> dataController_commandstableslist { get; set; }
        public virtual ICollection<dataController_views> dataController_views { get; set; }
        public virtual dataControllerCollection dataControllerCollection1 { get; set; }
    }
}
