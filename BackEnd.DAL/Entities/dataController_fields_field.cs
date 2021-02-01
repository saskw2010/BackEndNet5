using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.DAL.Entities
{ 
    public partial class dataController_fields_field
    {
        [Key]
        public int dataController_fields_fieldId { get; set; }
        public string dataController_name { get; set; }
        public string dataController_fields_field_isPrimaryKey { get; set; }
        public string dataController_fields_field_name { get; set; }
        public string dataController_fields_field_label { get; set; }
        public string dataController_fields_field_allowNulls { get; set; }
        public string dataController_fields_field_readOnly { get; set; }
        public string dataController_fields_field_type { get; set; }
        public string dataController_fields_field_showInSummary { get; set; }
        public string dataController_fields_field_dataFormatString { get; set; }
        public string dataController_fields_field_default { get; set; }
        public Nullable<int> dataController_fields_field_length { get; set; }
        public string dataController_fields_field_items_style { get; set; }
        public string dataController_fields_field_items_dataTextField { get; set; }
        public string dataController_fields_field_items_newDataView { get; set; }
        public string dataController_fields_field_items_dataValueField { get; set; }
        public string dataController_fields_field_items_dataController { get; set; }
        public string dataController_fields_field_items_searchOnStart { get; set; }
        public string dataController_fields_field_items_copy { get; set; }
        [ForeignKey("dataController")]
        public Nullable<int> datacontrollerFkId { get; set; }
        public virtual dataController dataController { get; set; }
  }
}
