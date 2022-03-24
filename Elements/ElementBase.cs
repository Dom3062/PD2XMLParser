using System.Linq;
using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    struct Attribute
    {
        public string Name;
        public string Value;
        public bool IncludeSpace;

        public override string ToString()
        {
            if (IncludeSpace)
            {
                return Name + " " + Value;
            }
            return Name + Value;
        }

        public string ToString(bool space)
        {
            if (space)
            {
                return ToString();
            }
            return Name + Value;
        }
    }
    class ElementBase
    {
        List<Attribute> ElementAttributes = new();
        protected readonly string c; //class
        private string editor_name;
        private readonly int id;
        private readonly string node_name;
        private readonly bool disabled;
        private readonly XmlNode values;
        private List<on_executed> on_executed;
        private List<elements> elements;
        private List<string> all_child_names = new();
        public ElementBase(string c, string editor_name, int id, string node_name, XmlNodeList values)
        {
            this.c = c;
            this.editor_name = editor_name;
            this.id = id;
            this.node_name = node_name;
            this.values = null;
            foreach (XmlNode node in values)
            {
                if (node.Name == "values")
                {
                    this.values = node;
                    break;
                }
            }
            if (this.values == null)
            {
                throw new ArgumentException("Hodnota neexistuje!", "values");
            }
            this.disabled = this.values.Attributes["enabled"].Value == "false";
            foreach (XmlNode node in this.values.ChildNodes)
            {
                this.all_child_names.Add(node.Name);
            }
            XmlNode on_executed = null;
            LoadNode(ref on_executed, "on_executed");
            if (on_executed == null)
            {
                throw new ArgumentException("Hodnota neexistuje!", "on_executed");
            }
            ParseOnExecuted(on_executed);
            XmlNode elements = null;
            LoadNode(ref elements, "elements");
            ParseElements(elements);
        }

        public string GetNodeName()
        {
            return this.node_name;
        }

        protected void AddFakeElement()
        {
            this.elements = new();
            this.elements.Add(new elements(0, 0, false));
        }

        protected int GetNumberOfUnloadedChilds()
        {
            return this.all_child_names.Count;
        }

        protected void LoadNode(ref XmlNode xml_node, string node_name)
        {
            foreach (XmlNode node in this.values.ChildNodes)
            {
                if (node.Name == node_name)
                {
                    xml_node = node;
                    RemoveLoadedChild(node_name);
                    break;
                }
            }
        }

        protected void LoadNodeAtPosition(ref XmlNode xml_node, int pos)
        {
            xml_node = this.values.ChildNodes[pos];
            RemoveLoadedChildAtPosition(pos);
        }

        protected void RemoveLoadedChild(string child_name)
        {
            this.all_child_names.Remove(child_name);
        }

        protected void RemoveLoadedChildAtPosition(int pos)
        {
            this.all_child_names.RemoveAt(pos);
        }

        public bool AllChildsLoaded()
        {
            return this.all_child_names.Count == 0;
        }

        public string[] GetChildsNotLoaded()
        {
            return this.all_child_names.ToArray();
        }

        public string FormatHeader()
        {
            return SharedClass.FormatElementName(this.editor_name) + " " + this.c + " " + this.id.ToString();
        }

        public string FormatCommonAttributes(string prefix = "")
        {
            string formatted_attributes = "";
            foreach (string attribute in SharedClass.common_attributes)
            {
                if (AttributeInElementExists(attribute))
                {
                    string value = FormatCommonAttribute(attribute);
                    if (value != "")
                    {
                        formatted_attributes += prefix + value + "\n";
                    }
                }
            }
            return formatted_attributes;
        }

        public string GetEditorNamePure()
        {
            return this.editor_name;
        }

        public void UpdateEditorName(string editor_name)
        {
            this.editor_name = editor_name;
        }

        public string GetEditorName()
        {
            return SharedClass.FormatElementName(this.editor_name);
        }

        public int GetElementID()
        {
            return this.id;
        }

        public bool IsElementDisabled()
        {
            return this.disabled;
        }

        protected bool CheckIfElementIDIsTheSame(int id)
        {
            return this.id == id;
        }

        public virtual void UpdateElements(int id, string editor_name, bool disabled)
        {
            if (CheckIfElementIDIsTheSame(id))
            {
                return;
            }
            foreach(on_executed on_executed in this.on_executed)
            {
                if (on_executed.GetElementID() == id)
                {
                    on_executed.UpdateElementName(editor_name);
                    on_executed.UpdateElementDisabled(disabled);
                }
            }
            if (this.elements != null) //Due to ElementEnableSoundEnvironment
            {
                foreach (elements element in this.elements)
                {
                    if (element.GetElementID() == id)
                    {
                        element.UpdateElementName(editor_name);
                        element.UpdateElementDisabled(disabled);
                    }
                }
            }
        }

        public virtual void UpdateUnitElements(string ElementName, int id, string unit_name, string unit_pos)
        {
        }

        public virtual bool HasUnits()
        {
            return false;
        }

        public virtual void UpdateInstances(string instance, string instance_path)
        {
        }

        public virtual bool HasInstances()
        {
            return false;
        }

        public override string ToString()
        {
            string s = FormatHeader() + "\n";
            s += FormatCommonAttributes("\t");
            PrepareElementAttributes();
            PrepareAttributesElements();
            PrepareAttributes();
            s += FormatAttributesNew();
            if (this.on_executed.Count > 0)
            {
                s += "\ton_executed\n";
                s += FormatOnExecuted("\t\t");
            }
            return s;
        }

        protected virtual void PrepareAttributes()
        {
        }

        protected virtual void PrepareAttributesElements()
        {
            if (this.elements == null || this.elements?.Count == 0)
            {
                return;
            }
            AddAttribute("\telements\n", FormatElements("\t\t"), true);
        }

        private string FormatAttributesNew(string prefix = "")
        {
            string s = "";
            List<Attribute> sorted = this.ElementAttributes.OrderBy(a => a.Name).ToList();
            foreach (Attribute a in sorted)
            {
                s += a.ToString();
            }
            return s;
        }

        private string FormatCommonAttribute(string attribute)
        {
            switch (attribute)
            {
                case "enabled":
                    {
                        string value = GetAttributeValue(attribute);
                        if (value == "false")
                        {
                            return "DISABLED";
                        }
                        return "";
                    }
                case "execute_on_startup":
                    {
                        string value = GetAttributeValue(attribute);
                        if (value == "false")
                        {
                            return "";
                        }
                        return "EXECUTE ON STARTUP";
                    }
                case "base_delay":
                    {
                        double value = GetAttributeValueDouble(attribute);
                        if (value == 0)
                        {
                            return "";
                        }
                        double random_value = GetAttributeValueDoubleOrDefault("base_delay_rand", 0);
                        string final_value;
                        if (random_value > 0)
                        {
                            final_value = value.ToString() + "-" + (value + random_value).ToString();
                        }
                        else
                        {
                            final_value = value.ToString();
                        }
                        return "BASE DELAY " + final_value.Replace(",", ".");
                    }
                case "base_delay_rand":
                    {
                        if (AttributeInElementExists("base_delay"))
                        {
                            double value = GetAttributeValueDouble("base_delay");
                            if (value == 0)
                            {
                                return "BASE DELAY 0-" + GetAttributeValueDouble(attribute).ToString().Replace(",", ".");
                            }
                            return "";
                        }
                        else
                        {
                            double value = GetAttributeValueDouble(attribute);
                            if (value == 0)
                            {
                                return "";
                            }
                            return "BASE DELAY 0-" + GetAttributeValueDouble(attribute).ToString().Replace(",", ".");
                        }
                    }
                case "trigger_times":
                    {
                        string value = GetAttributeValue(attribute);
                        if (value == "0")
                        {
                            return "";
                        }
                        return "TRIGGER TIMES " + value;
                    }
                default:
                    {
                        return attribute + " " + GetAttributeValue(attribute);
                    }
            }
        }

        private void PrepareElementAttributes()
        {
            foreach (XmlAttribute attribute in this.values.Attributes)
            {
                if (AttributeInElementIsNotCommonAttribute(attribute.Name))
                {
                    FormatAttribute(attribute.Name);
                }
            }
        }

        protected virtual void FormatAttribute(string attribute)
        {
            switch(attribute)
            {
                case "position":
                case "rotation":
                    {
                        string value = GetAttributeValue(attribute);
                        value = value.Replace(",", ".").Replace(" ", ", ");
                        AddAttribute("\t" + attribute, value + "\n", true);
                        break;
                    }
                case "interval":
                case "width":
                case "height":
                case "radius":
                    {
                        string value = GetAttributeValue(attribute).Replace(",", ".");
                        AddAttribute("\t" + attribute, value + "\n", true);
                        break;
                    }
                default:
                    {
                        string value = GetAttributeValue(attribute);
                        if (value == "true")
                        {
                            value = "True";
                        }
                        if (value == "false")
                        {
                            value = "False";
                        }
                        AddAttribute("\t" + attribute, value + "\n", true);
                        break;
                    }
            }
        }

        private string FormatOnExecuted(string prefix = "")
        {
            string s = "";
            foreach (on_executed on_executed in this.on_executed)
            {
                if (s != "")
                {
                    s += "\n";
                }
                s += prefix + on_executed.ToString();
            }
            return s += "\n";
        }

        protected virtual string FormatElements(string prefix = "")
        {
            string s = "";
            foreach (elements e in this.elements)
            {
                if (s != "")
                {
                    s += "\n";
                }
                s += prefix + e.ToString();
            }
            return s += "\n";
        }

        protected string FormatCustomElement(string s)
        {
            return "\t" + s + "\n";
        }

        protected void AddAttribute(string name, string value, bool include_space = false)
        {
            this.ElementAttributes.Add(new Attribute()
            {
                Name = name,
                Value = value,
                IncludeSpace = include_space
            });
        }

        protected string GetAttributeValue(string attribute)
        {
            return this.values.Attributes[attribute].Value;
        }

        private double GetAttributeValueDouble(string attribute)
        {
            return Convert.ToDouble(this.values.Attributes[attribute].Value);
        }

        private double GetAttributeValueDoubleOrDefault(string attribute, double default_value)
        {
            if (AttributeInElementExists(attribute))
            {
                return GetAttributeValueDouble(attribute);
            }
            return default_value;
        }

        protected void SetAttributeValue(string attribute, string value)
        {
            this.values.Attributes[attribute].Value = value;
        }

        protected bool AttributeInElementExists(string attribute)
        {
            return this.values.Attributes[attribute] != null;
        }

        private bool AttributeInElementIsNotCommonAttribute(string attribute)
        {
            return !SharedClass.common_attributes.ContainsExact(attribute);
        }

        private void ParseOnExecuted(XmlNode on_executed)
        {
            this.on_executed = new List<on_executed>();
            foreach (XmlNode element in on_executed.ChildNodes)
            {
                int id = Convert.ToInt32(element.Attributes["id"].Value);
                string delay_value = element.Attributes["delay"].Value;
                double delay = Convert.ToDouble(delay_value);
                on_executed new_element;
                if (element.Attributes["delay_rand"] != null)
                {
                    double delay_rand = Convert.ToDouble(element.Attributes["delay_rand"].Value);
                    new_element = new on_executed(id, delay, delay + delay_rand, false);
                }
                else
                {
                    new_element = new on_executed(id, delay, false);
                }
                if (element.Attributes["alternative"] != null)
                {
                    new_element.UpdateAlternativeExecution(element.Attributes["alternative"].Value);
                }
                this.on_executed.Add(new_element);
            }
        }

        protected virtual void ParseElements(XmlNode elements)
        {
            this.elements = new List<elements>();
            if (elements == null)
            {
                return;
            }
            int pos = 1;
            foreach(XmlNode element in elements)
            {
                int id = Convert.ToInt32(element.Attributes["value"].Value);
                elements new_element = new(id, pos, false);
                if (element.Attributes["alternative"] != null)
                {
                    new_element.UpdateAlternativeExecution(element.Attributes["alternative"].Value);
                }
                this.elements.Add(new_element);
                pos++;
            }
        }
    }
}