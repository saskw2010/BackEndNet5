//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.DAL.Entities
{
  public partial class dataControllerCollection
    {
        [Key]
        public int dataControllerCollectionId { get; set; }
        public string dataControllerCollection_xmlns { get; set; }
        public string dataControllerCollection_snapshot { get; set; }
        public string dataControllerCollection_Name { get; set; }
        public string dataControllerCollection_Version { get; set; }
        public virtual ICollection<dataController> dataControllers { get; set; }
    }
}
