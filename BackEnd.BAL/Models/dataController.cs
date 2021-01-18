using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
namespace BackEnd.BAL.Models
{
  // using System.Xml.Serialization;
  // XmlSerializer serializer = new XmlSerializer(typeof(DataController));
  // using (StringReader reader = new StringReader(xml))
  // {
  //    var test = (DataController)serializer.Deserialize(reader);
  // }

  [XmlRoot(ElementName = "command")]
  public class Command
  {
    [XmlIgnore]
    public string text { get; set; }
    [XmlElement(ElementName = "text")]
    [JsonIgnore]
    public System.Xml.XmlCDataSection textCdata
    {
      get
      {
        return new System.Xml.XmlDocument().CreateCDataSection(text);
      }
      set
      {
        text = value.Value;
      }
    }

    //[XmlElement(ElementName = "text")]
    //public string Text { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }

    [XmlElement(ElementName = "output")]
    public Output Output { get; set; }

    [XmlAttribute(AttributeName = "event")]
    public string Event { get; set; }
  }

  [XmlRoot(ElementName = "fieldOutput")]
  public class FieldOutput
  {

    [XmlAttribute(AttributeName = "fieldName")]
    public string FieldName { get; set; }
  }

  [XmlRoot(ElementName = "output")]
  public class Output
  {

    [XmlElement(ElementName = "fieldOutput")]
    public List<FieldOutput> FieldOutput { get; set; }
  }

  [XmlRoot(ElementName = "commands")]
  public class Commands
  {

    [XmlElement(ElementName = "command")]
    public List<Command> Command { get; set; }
  }

  [XmlRoot(ElementName = "field")]
  public class Field
  {

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "allowNulls")]
    public string AllowNulls { get; set; }
    

    [XmlAttribute(AttributeName = "isPrimaryKey")]
    public string IsPrimaryKey { get; set; }
    [XmlAttribute(AttributeName = "default")]
    public string Default { get; set; }
    [XmlAttribute(AttributeName = "dataFormatString")]
    public string DataFormatString { get; set; }
    [XmlAttribute(AttributeName = "readOnly")]
    public string ReadOnly { get; set; }
    [XmlAttribute(AttributeName = "label")]
    public string Label { get; set; }

    [XmlElement(ElementName = "items")]
    public Items Items { get; set; }

    [XmlAttribute(AttributeName = "length")]
    public string Length { get; set; }
    [XmlAttribute(AttributeName = "showInSummary")]
    public string ShowInSummary { get; set; }

  }

  [XmlRoot(ElementName = "items")]
  public class Items
  {

    [XmlAttribute(AttributeName = "style")]
    public string Style { get; set; }

    [XmlAttribute(AttributeName = "dataController")]
    public string DataController { get; set; }

    [XmlAttribute(AttributeName = "newDataView")]
    public string NewDataView { get; set; }

    [XmlAttribute(AttributeName = "dataValueField")]
    public string DataValueField { get; set; }

    [XmlAttribute(AttributeName = "dataTextField")]
    public string DataTextField { get; set; }
  }

  [XmlRoot(ElementName = "fields")]
  public class Fields
  {

    [XmlElement(ElementName = "field")]
    public List<Field> Field { get; set; }
  }

  [XmlRoot(ElementName = "dataField")]
  public class DataField
  {

    [XmlAttribute(AttributeName = "fieldName")]
    public string FieldName { get; set; }

    [XmlAttribute(AttributeName = "aliasFieldName")]
    public string AliasFieldName { get; set; }

    [XmlAttribute(AttributeName = "columns")]
    public string Columns { get; set; }
  }

  [XmlRoot(ElementName = "dataFields")]
  public class DataFields
  {

    [XmlElement(ElementName = "dataField")]
    public List<DataField> DataField { get; set; }
  }

  [XmlRoot(ElementName = "view")]
  public class View
  {

    [XmlElement(ElementName = "headerText")]
    public string HeaderText { get; set; }

    [XmlElement(ElementName = "dataFields")]
    public DataFields DataFields { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "commandId")]
    public string CommandId { get; set; }

    [XmlAttribute(AttributeName = "label")]
    public string Label { get; set; }

    [XmlText]
    public string Text { get; set; }

    [XmlElement(ElementName = "categories")]
    public Categories Categories { get; set; }
  }

  [XmlRoot(ElementName = "category")]
  public class Category
  {
    [XmlIgnore]
    public string description { get; set; }
    [XmlElement(ElementName = "description")]
    [JsonIgnore]
    public System.Xml.XmlCDataSection descriptionCdata
    {
      get
      {
        return new System.Xml.XmlDocument().CreateCDataSection(description);
      }
      set
      {
        description = value.Value;
      }
    }

    [XmlElement(ElementName = "dataFields")]
    public DataFields DataFields { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "headerText")]
    public string HeaderText { get; set; }

    [XmlAttribute(AttributeName = "flow")]
    public string Flow { get; set; }

    [XmlText]
    public string Text { get; set; }
  }

  [XmlRoot(ElementName = "categories")]
  public class Categories
  {

    [XmlElement(ElementName = "category")]
    public Category Category { get; set; }
  }

  [XmlRoot(ElementName = "views")]
  public class Views
  {

    [XmlElement(ElementName = "view")]
    public List<View> View { get; set; }
  }

  [XmlRoot(ElementName = "action")]
  public class Action
  {

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }
    [XmlAttribute(AttributeName = "whenLastCommandName")]
    public string WhenLastCommandName { get; set; }


    [XmlAttribute(AttributeName = "whenLastCommandArgument")]
    public string WhenLastCommandArgument { get; set; }


 

    [XmlAttribute(AttributeName = "commandName")]
    public string CommandName { get; set; }

    [XmlAttribute(AttributeName = "commandArgument")]
    public string CommandArgument { get; set; }

    [XmlAttribute(AttributeName = "whenClientScript")]
    public string WhenClientScript { get; set; }
    [XmlAttribute(AttributeName = "whenKeySelected")]
 
    public string WhenKeySelected { get; set; }
    [XmlAttribute(AttributeName = "cssClass")]
    public string CssClass { get; set; }
    [XmlAttribute(AttributeName = "whenView")]
    public string WhenView { get; set; }
   

  }

  [XmlRoot(ElementName = "actionGroup")]
  public class ActionGroup
  {

    [XmlElement(ElementName = "action")]
    public List<Action> Action { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "scope")]
    public string Scope { get; set; }

    [XmlAttribute(AttributeName = "headerText")]
    public string HeaderText { get; set; }

    [XmlAttribute(AttributeName = "flat")]
    public string Flat { get; set; }
  }

  [XmlRoot(ElementName = "actions")]
  public class Actions
  {

    [XmlElement(ElementName = "actionGroup")]
    public List<ActionGroup> ActionGroup { get; set; }
  }

  [XmlRoot(ElementName = "dataController")]
  public class DataController
  {

    [XmlElement(ElementName = "commands")]
    public Commands Commands { get; set; }

    [XmlElement(ElementName = "fields")]
    public Fields Fields { get; set; }

    [XmlElement(ElementName = "views")]
    public Views Views { get; set; }

    [XmlElement(ElementName = "actions")]
    public Actions Actions { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "conflictDetection")]
    public string ConflictDetection { get; set; }

    [XmlAttribute(AttributeName = "label")]
    public string Label { get; set; }

    [XmlAttribute(Namespace = "urn:schemas-codeontime-com:data-aquarium")]

    public string Xmlns { get; set; }

    [XmlText]
    public string Text { get; set; }
  }
  public class SaveDataController
  {
    public string controllerName { get; set; }
    public DataController dataController { get; set; }
  }

}
