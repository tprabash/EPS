using System;

namespace API.DTOs
{
    public class FlexFieldDto
    {
        public string FlexFieldName {get;set;}
        public string FlexFieldCode {get;set;}
        public string DataType {get;set;}
        public bool ValueList {get;set;}
        public int FlexFieldId {get;set;}      
        public bool bFlexFieldValue {get;set;}
        public DateTime dFlexFieldValue {get;set;}
        public int iFlexFeildValue {get;set;}
        public double fFlexFeildValue {get;set;}
        public string cFlexFeildValue {get;set;}
    }
}