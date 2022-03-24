namespace XMLParser.Elements.Attributes
{
    class trigger_list
    {
        private readonly int id;
        private readonly string name;
        private readonly int notify_unit_id;
        private readonly string notify_unit_sequence;
        private readonly string time;
        private string notify_unit_id_string = null;
        private string pos = null;
        public trigger_list(int id, string name, int notify_unit_id, string notify_unit_sequence, string time)
        {
            this.id = id;
            this.name = name;
            this.notify_unit_id = notify_unit_id;
            this.notify_unit_sequence = notify_unit_sequence;
            this.time = time;
        }

        public int GetElementID()
        {
            return this.notify_unit_id;
        }

        public void UpdateUnitName(string name, string pos)
        {
            this.notify_unit_id_string = name;
            this.pos = pos;
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string double_prefix = prefix + "\t";
            string s = prefix + this.id.ToString() + "\n";
            s += double_prefix + "id " + this.id.ToString() + "\n";
            s += double_prefix + "name " + this.name + "\n";
            if (this.notify_unit_id_string == null)
            {
                s += double_prefix + "notify_unit_id " + this.notify_unit_id.ToString() + "\n";
            }
            else
            {
                s += double_prefix + "notify_unit_id " + this.notify_unit_id_string + " " + pos + "\n";
            }
            s += double_prefix + "notify_unit_sequence " + this.notify_unit_sequence + "\n";
            s += double_prefix + "time " + this.time + "\n";
            return s;
        }
    }
}