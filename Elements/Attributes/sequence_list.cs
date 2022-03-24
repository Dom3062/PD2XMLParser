namespace XMLParser.Elements.Attributes
{
    class sequence_list
    {
        private readonly int id;
        private readonly string sequence;
        private readonly int unit_id;
        private string unit_id_string = null;
        private string pos = null;
        public sequence_list(int id, string sequence, int unit_id)
        {
            this.id = id;
            this.sequence = sequence;
            this.unit_id = unit_id;
        }

        public int GetElementID()
        {
            return this.unit_id;
        }

        public void UpdateUnitName(string name, string pos)
        {
            this.unit_id_string = name;
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
            s += double_prefix + "guis_id " + this.id.ToString() + "\n";
            s += double_prefix + "sequence " + this.sequence + "\n";
            if (this.unit_id_string == null)
            {
                s += double_prefix + "notify_unit_id " + this.unit_id.ToString() + "\n";
            }
            else
            {
                s += double_prefix + "notify_unit_id " + this.unit_id_string + " " + pos + "\n";
            }
            return s;
        }
    }
}