using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Serialization;

namespace BackEnd.BAL.Models
{
  [XmlRoot(ElementName = "foreignKeyColumn")]
  public class ForeignKeyColumn
  {

    [XmlAttribute(AttributeName = "columnName")]
    public string ColumnName { get; set; }

    [XmlAttribute(AttributeName = "parentColumnName")]
    public string ParentColumnName { get; set; }
  }

  [XmlRoot(ElementName = "foreignKey")]
  public class ForeignKey
  {

    [XmlElement(ElementName = "foreignKeyColumn")]
    public ForeignKeyColumn ForeignKeyColumn { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "parentTableSchema")]
    public string ParentTableSchema { get; set; }

    [XmlAttribute(AttributeName = "parentTableName")]
    public string ParentTableName { get; set; }
  }

  [XmlRoot(ElementName = "foreignKeys")]
  public class ForeignKeys
  {

    [XmlElement(ElementName = "foreignKey")]
    public ForeignKey ForeignKey { get; set; }
  }


  [XmlRoot(ElementName = "column")]
  public class Column
  {

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "fieldName")]
    public string FieldName { get; set; }

    [XmlAttribute(AttributeName = "label")]
    public string Label { get; set; }

    [XmlAttribute(AttributeName = "aliasColumnName")]
    public string AliasColumnName { get; set; }

    [XmlAttribute(AttributeName = "aliasForeignKey")]
    public string AliasForeignKey { get; set; }
    [XmlAttribute(AttributeName = "format")]
    public string Format { get; set; }
    [XmlAttribute(AttributeName = "foreignKey")]
    public string ForeignKey { get; set; }
  }


  [XmlRoot(ElementName = "columns")]
  public class Columns
  {

    [XmlElement(ElementName = "column")]
    public List<Column> Column { get; set; }

  }

  [Serializable]

  [XmlRoot(ElementName = "dataModel")]

  public class DataModel
  {

    [XmlElement(ElementName = "foreignKeys")]
    public ForeignKeys ForeignKeys { get; set; }

    [XmlElement(ElementName = "columns")]
    public Columns Columns { get; set; }

    [XmlAttribute(AttributeName = "baseSchema")]
    public string BaseSchema { get; set; }

    [XmlAttribute(AttributeName = "baseTable")]
    public string BaseTable { get; set; }

    [XmlAttribute(AttributeName = "alias")]
    public string Alias { get; set; }

    [XmlAttribute(AttributeName = "created")]
    public string Created { get; set; }

    [XmlAttribute(AttributeName = "modified")]
    public string Modified { get; set; }
    //[XmlAttribute(AttributeName = "xmlns")]
    [XmlAttribute(Namespace = "urn:schemas-codeontime-com:data-aquarium")]

    public string Xmlns { get; set; }
  }

  
}
