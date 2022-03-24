using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementInstance : ElementBase
    {
        List<event_list> event_list;
        private string save_node;
        public ElementInstance(string c, string editor_name, int id, string node_name, XmlNodeList values, string save_node = "event_list") : base(c, editor_name, id, node_name, values)
        {
            this.save_node = save_node;
            XmlNode event_list = null;
            LoadNode(ref event_list, save_node);
            ParseEventList(event_list);
        }

        public override void UpdateInstances(string instance, string instance_path)
        {
            foreach(event_list instance_event in this.event_list)
            {
                instance_event.UpdateInstance(instance, instance_path);
            }
        }

        public override bool HasInstances()
        {
            return this.event_list.Count > 0;
        }

        private void ParseEventList(XmlNode event_list)
        {
            this.event_list = new List<event_list>();
            if (event_list == null)
            {
                return;
            }
            int pos = 1;
            foreach (XmlNode instance_event in event_list)
            {
                string event_name = null;
                if (instance_event.Attributes["event"] != null)
                {
                    event_name = instance_event.Attributes["event"].Value;
                }
                string instance = instance_event.Attributes["instance"].Value;
                this.event_list.Add(new event_list(this.GetElementID(), pos, event_name, instance));
                pos++;
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (event_list instance_event in this.event_list)
            {
                s += instance_event.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement(this.save_node), s);
        }
    }

    class ElementInstancePoint : ElementBase
    {
        public ElementInstancePoint(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
        }

        public override void UpdateInstances(string instance, string instance_path)
        {
            if (this.AttributeInElementExists("instance") && this.GetAttributeValue("instance") == instance)
            {
                this.SetAttributeValue("instance", instance_path);
            }
        }

        public override bool HasInstances()
        {
            return true;
        }
    }

    class ElementInstanceParams : ElementBase
    {
        List<numeric_list> @params;
        public ElementInstanceParams(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode @params = null;
            LoadNode(ref @params, "params");
            ParseParamsList(@params);
        }

        private void ParseParamsList(XmlNode i_params)
        {
            this.@params = new List<numeric_list>();
            if (i_params == null)
            {
                return;
            }
            int pos = 1;
            foreach (XmlNode param in i_params)
            {
                numeric_list list = new(pos);
                foreach (XmlAttribute attribute in param.Attributes)
                {
                    list.Add(attribute.Name, attribute.Value);
                }
                this.@params.Add(list);
                pos++;
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (numeric_list list in this.@params)
            {
                s += list.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("params"), s);
        }
    }

    class ElementInstanceSetParams : ElementBase
    {
        List<instance_params> i_params;
        private string save_node;
        public ElementInstanceSetParams(string c, string editor_name, int id, string node_name, XmlNodeList values, string save_node = "params") : base(c, editor_name, id, node_name, values)
        {
            XmlNode i_params = null;
            this.save_node = save_node;
            LoadNode(ref i_params, save_node);
            ParseParamsList(i_params);
        }

        private void ParseParamsList(XmlNode i_params)
        {
            this.i_params = new List<instance_params>();
            if (i_params == null)
            {
                return;
            }
            foreach (XmlAttribute i_param in i_params.Attributes)
            {
                string name = i_param.Name;
                string value = i_param.Value;
                this.i_params.Add(new instance_params(name, value));
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (instance_params i_params in this.i_params)
            {
                s += i_params.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement(this.save_node), s);
        }
    }
}