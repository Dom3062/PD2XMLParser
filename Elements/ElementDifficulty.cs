namespace XMLParser.Elements
{
    class ElementDifficulty : ElementBase
    {
        public ElementDifficulty(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
        }

        protected override void FormatAttribute(string attribute)
        {
            if (attribute == "difficulty")
            {
                string value = GetAttributeValue(attribute).Replace(",", ".");
                AddAttribute("\t" + attribute, value + "\n", true);
                return;
            }
            base.FormatAttribute(attribute);
        }
    }
}