namespace XMLParser.Elements
{
    class ElementRandom : ElementBase
    {
        bool has_counter_id = false;
        int counter_id;
        public ElementRandom(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            if (AttributeInElementExists("counter_id"))
            {
                has_counter_id = true;
                counter_id = Convert.ToInt32(GetAttributeValue("counter_id"));
            }
        }

        public override void UpdateElements(int id, string editor_name, bool disabled)
        {
            if (CheckIfElementIDIsTheSame(id))
            {
                return;
            }
            base.UpdateElements(id, editor_name, disabled);
            if (has_counter_id && id == counter_id)
            {
                SetAttributeValue("counter_id", editor_name);
            }
        }
    }

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