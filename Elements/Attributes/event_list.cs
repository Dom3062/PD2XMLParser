namespace XMLParser.Elements.Attributes
{
    class event_list
    {
        private readonly int element_id;
        private readonly int pos;
        private readonly string event_name;
        private string instance;
        public event_list(int element_id, int pos, string event_name, string instance)
        {
            this.element_id = element_id;
            this.pos = pos;
            this.event_name = event_name;
            this.instance = instance;
        }

        public int GetElementID()
        {
            return this.element_id;
        }

        public void UpdateInstance(string old_name, string instance)
        {
            if (this.instance == old_name)
            {
                this.instance = instance;
            }
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string double_prefix = prefix + "\t";
            string s = prefix + this.pos.ToString() + "\n";
            if (this.event_name != null)
            {
                s += double_prefix + "event " + this.event_name + "\n";
            }
            s += double_prefix + "instance " + this.instance + "\n";
            return s;
        }
    }
}