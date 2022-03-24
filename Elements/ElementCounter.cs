using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementCounter : ElementEnableUnit
    {
        List<instance_params> instance_var_names;
        public ElementCounter(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values, "digital_gui_unit_ids")
        {
            XmlNode node = null;
            LoadNode(ref node, "instance_var_names");
            if (node != null)
            {
                instance_var_names = new();
                foreach (XmlAttribute var_name in node.Attributes)
                {
                    string var = var_name.Name;
                    string value = var_name.Value;
                    instance_var_names.Add(new instance_params(var, value));
                }
            }
        }

        protected override void PrepareAttributes()
        {
            base.PrepareAttributes();
            if (this.instance_var_names != null)
            {
                string s = "";
                foreach (instance_params param in this.instance_var_names)
                {
                    s += param.ToString("\t\t");
                }
                AddAttribute(FormatCustomElement("instance_var_names"), s);
            }
        }
    }
}