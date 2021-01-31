using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BackEnd.BAL.Models
{
  public class DataControllerBase
  {
    public static Regex SqlSelectRegex1 = new Regex("/\\*<select>\\*/(?\'Select\'[\\S\\s]*)?/\\*</select>\\*[\\S\\s]*?/\\*<from>\\*/(?\'From\'[\\S\\s]" +
              "*)?/\\*</from>\\*[\\S\\s]*?/\\*(<order-by>\\*/(?\'OrderBy\'[\\S\\s]*)?/\\*</order-by>\\*/)?", RegexOptions.IgnoreCase);
    public static Regex SqlSelectRegex2 = new Regex(@"\s*select\s*(?'Select'[\S\s]*)?\sfrom\s*(?'From'[\S\s]*)?\swhere\s*(?'Where'[\S\s]*)?\sorder\s+by\s*(?'OrderBy'[\S\s]*)?|\s*select\s*(?'Select'[\S\s]*)?\sfrom\s*(?'From'[\S\s]*)?\swhere\s*(?'Where'[\S\s]*)?|\s*select\s*(?'Select'[\S\s]*)?\sfrom\s*(?'From'[\S\s]*)?\sorder\s+by\s*(?'OrderBy'[\S\s]*)?|\s*select\s*(?'Select'[\S\s]*)?\sfrom\s*(?'From'[\S\s]*)?", RegexOptions.IgnoreCase);

    public static Regex SelectExpressionRegex = new Regex("\\s*(?\'Expression\'[\\S\\s]*?(\\([\\s\\S]*?\\)|(\\.((\"|\'|\\[|`)(?\'FieldName\'[\\S\\s]*?)(\"|\'|\\" +
                 "]|`))|(\"|\'|\\[|`|)(?\'FieldName\'[\\w\\s]*?)(\"|\'|\\]|)|)))((\\s+as\\s+|\\s+)(\"|\'|\\[|`|)(?" +
                 "\'Alias\'[\\S\\s]*?)|)(\"|\'|\\]|`|)\\s*(,|$)", RegexOptions.IgnoreCase);
  }
}
