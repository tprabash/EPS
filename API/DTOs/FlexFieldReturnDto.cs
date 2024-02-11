namespace API.DTOs
{
    public class FlexFieldReturnDto
    {
        public int AutoId {get;set;}
        public int CategoryId {get;set;}
        public string Category {get;set;}
        public int ProdTypeId {get;set;}
        public string ProdTypeName {get;set;}
        public byte ModuleId {get;set;}
        public string FlexFieldName {get;set;}
        public string FlexFieldCode {get;set;}
        public string DataType {get;set;}
        public bool ValueList {get;set;}
        public bool Mandatory {get;set;}
        public bool isActive {get;set;}
    }
}