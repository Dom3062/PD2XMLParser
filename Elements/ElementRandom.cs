using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace XMLParser.Elements
{
    class ElementRandomInstance : ElementBase
    {
        public ElementRandomInstance(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
        }
    }

    class ElementRandomInstanceInput : ElementRandomInstance
    {
        public ElementRandomInstanceInput(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
        }
    }

    class ElementRandomInstanceOutput : ElementRandomInstance
    {
        public ElementRandomInstanceOutput(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
        }
    }
}