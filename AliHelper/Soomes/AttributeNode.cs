using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class AttributeNodeJson
    {
        public string BindAttrjson {  get; set;}
        public List<AttributeNode> BindAttributeNodes { set; get; }
        public int CategoryId { set; get; }
        public bool Success { set; get; }
        public string SysAttrjson { set; get; }
        public List<AttributeNode> SysAttributeNodes { set; get; }
    }

    public class AttributeNode
    {
        public Attribute Data { set; get; }
        public List<AttributeNode> Nodes { set; get; }
    }

    public class Attribute
    {
        public string Id { set; get; }
        public string NodeType { set; get; }
        public string ShowType { set; get; }
        public List<string> Rules { set; get; }
        public string Value { set; get; }
        public bool Selected { set; get; }
        public string ErrorMessage { set; get; }

    }

    public class NoteType 
    {
        public const string Name = "name";
        public const string Value = "value";
    }

    public class ShowType
    {
        public const string ListBox = "list_box";
        public const string InputString = "input_string";
        public const string CheckBox = "check_box";
        public const string Province = "province";
        public const string Country = "country";
        public const string FixValue = "fix_value";
        public const string Other = "other";
    };
    
}
