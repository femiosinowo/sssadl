namespace Ektron.Cms.PageBuilder
{
    using System.Collections.Generic;
    using Ektron.Newtonsoft.Json;
    using Ektron.Newtonsoft.Json.Converters;

    public enum UXDropZoneActionType
    {
        SetMasterZone,
        UnsetMasterZone,
        RemoveColumn,
        AddColumn,
        ResizeColumn,
        MoveColumn
    }

    public class UXDropZoneData
    {
        public string ID { get; set; }
        public string MarkupID { get; set; }
        public bool IsDropZoneEditable { get; set; }
        public bool IsMasterZone { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class UXColumnData
    {
        public UXColumnData()
        {
            this.Actions = new List<UXDropZoneAction>();
            this.Index = 0;
        }
        public string ID { get; set; }
        public string ColumnGUID { get; set; }
        public string UnitName { get; set; }
        public string CssClass { get; set; }
        public string Width { get; set; }
        public string CssFramework { get; set; }
        public bool Visible { get; set; }
        public int Index { get; set; }
        public List<UXDropZoneAction> Actions { get; set; }
    }

    public class UXDropZoneAction
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public UXDropZoneActionType Action { get; set; }
        public string ConfirmationMessage { get; set; }
        public string Href { get; set; }
        public string Callback { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}