using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class SelectClauseDictionary : SortedDictionary<string, string>
  {
    public new bool ContainsKey(string name)
    {
      return base.ContainsKey(name.ToLower());
    }

    public new void Add(string key, string value)
    {
      base.Add(key.ToLower(), value);
    }
  }
}
