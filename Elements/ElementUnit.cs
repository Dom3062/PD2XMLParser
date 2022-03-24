using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementEnableUnit : ElementBase
    {
        protected List<unit_ids> unit_ids;
        private readonly string node;
        public ElementEnableUnit(string c, string editor_name, int id, string node_name, XmlNodeList values, string id_to_parse = "unit_ids") : base(c, editor_name, id, node_name, values)
        {
            XmlNode unit_ids = null;
            this.node = id_to_parse;
            LoadNode(ref unit_ids, id_to_parse);
            ParseUnitIDs(unit_ids);
        }

        public override void UpdateUnitElements(string ElementName, int id, string unit_name, string unit_pos)
        {
            foreach (unit_ids unit_id in this.unit_ids)
            {
                if (unit_id.GetElementID() == id)
                {
                    unit_id.UpdateUnitName(unit_name, unit_pos);
                }
            }
        }

        public override bool HasUnits()
        {
            return this.unit_ids.Count > 0;
        }

        private void ParseUnitIDs(XmlNode unit_ids)
        {
            this.unit_ids = new List<unit_ids>();
            if (unit_ids == null)
            {
                return;
            }
            int pos = 1;
            foreach (XmlNode unit_id in unit_ids)
            {
                int id = Convert.ToInt32(unit_id.Attributes["value"].Value);
                this.unit_ids.Add(new unit_ids(id, pos));
                pos++;
            }
        }

        protected override void PrepareAttributes()
        {
            if (this.unit_ids.Count > 0 || this.node == "unit_ids")
            {
                string s = "";
                foreach (unit_ids unit_id in this.unit_ids)
                {
                    s += unit_id.ToString("\t\t");
                }
                AddAttribute(FormatCustomElement(this.node), s);
            }
        }
    }

    class ElementUnitSequence : ElementBase
    {
        private List<trigger_list> trigger_list;
        public ElementUnitSequence(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode trigger_list = null;
            LoadNode(ref trigger_list, "trigger_list");
            ParseTriggerList(trigger_list);
        }

        public override void UpdateUnitElements(string ElementName, int id, string unit_name, string unit_pos)
        {
            foreach (trigger_list trigger in this.trigger_list)
            {
                if (trigger.GetElementID() == id)
                {
                    trigger.UpdateUnitName(unit_name, unit_pos);
                }
            }
        }

        public override bool HasUnits()
        {
            return this.trigger_list.Count > 0;
        }

        private void ParseTriggerList(XmlNode trigger_list)
        {
            this.trigger_list = new List<trigger_list>();
            if (trigger_list == null)
            {
                return;
            }
            int i = 1;
            foreach (XmlNode trigger in trigger_list)
            {
                int id = i;
                if (trigger.Attributes["id"] != null)
                {
                    id = Convert.ToInt32(trigger.Attributes["id"].Value);
                }
                string name = trigger.Attributes["name"].Value;
                int notify_unit_id = Convert.ToInt32(trigger.Attributes["notify_unit_id"].Value);
                string notify_unit_sequence = trigger.Attributes["notify_unit_sequence"].Value;
                string time = trigger.Attributes["time"].Value;
                this.trigger_list.Add(new trigger_list(id, name, notify_unit_id, notify_unit_sequence, time));
                i++;
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (trigger_list trigger in this.trigger_list)
            {
                s += trigger.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("trigger_list"), s);
        }
    }

    class ElementUnitSequenceTrigger : ElementBase
    {
        private List<sequence_list> sequence_list;
        public ElementUnitSequenceTrigger(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode sequence_list = null;
            LoadNode(ref sequence_list, "sequence_list");
            ParseSequenceList(sequence_list);
        }

        public override void UpdateUnitElements(string ElementName, int id, string unit_name, string unit_pos)
        {
            foreach (sequence_list sequence in this.sequence_list)
            {
                if (sequence.GetElementID() == id)
                {
                    sequence.UpdateUnitName(unit_name, unit_pos);
                }
            }
        }

        public override bool HasUnits()
        {
            return this.sequence_list.Count > 0;
        }

        private void ParseSequenceList(XmlNode sequence_list)
        {
            this.sequence_list = new List<sequence_list>();
            if (sequence_list == null)
            {
                return;
            }
            int i = 1;
            foreach (XmlNode trigger in sequence_list)
            {
                int id = i;
                if (trigger.Attributes["guis_id"] != null)
                {
                    id = Convert.ToInt32(trigger.Attributes["guis_id"].Value);
                }
                string sequence = trigger.Attributes["sequence"].Value;
                int unit_id = Convert.ToInt32(trigger.Attributes["unit_id"].Value);
                this.sequence_list.Add(new sequence_list(id, sequence, unit_id));
                i++;
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (sequence_list trigger in this.sequence_list)
            {
                s += trigger.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("sequence_list"), s);
        }
    }
}