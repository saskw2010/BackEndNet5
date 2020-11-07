using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class paginationMetadata
  {
    public int totalCount { get; set; }
    public int pageSize { get; set; }
    public int currentPage { get; set; }
    public string nextPage { get; set; }
    public string previousPage { get; set; }
    public object data { get; set; }

  }
}
