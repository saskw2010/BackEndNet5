using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
  public static class ExtensionMethods
  {
    public static string FullMessage(this Exception ex)
    {
      if (ex is AggregateException) return (ex as AggregateException).InnerExceptions.Aggregate("[ ", (total, next) => $"{total}[{next.FullMessage()}] ") + "]";
      var msg = ex.Message.Replace(", see inner exception.", "").Trim();
      var innerMsg = ex.InnerException?.FullMessage();
      if (innerMsg is object && innerMsg != msg) msg = $"{msg} [ {innerMsg} ]";
      return msg;
    }
  }
}
