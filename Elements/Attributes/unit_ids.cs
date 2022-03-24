namespace XMLParser.Elements.Attributes
{
    class unit_ids
    {
        private readonly int id;
        private readonly int pos;
        private string unit_name = null;
        private string unit_pos = null;
        public unit_ids(int id, int pos)
        {
            this.id = id;
            this.pos = pos;
        }

        public int GetElementID()
        {
            return this.id;
        }

        public void UpdateUnitName(string name, string unit_pos)
        {
            this.unit_name = name;
            this.unit_pos = unit_pos;
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string s = prefix + this.pos.ToString();
            if (this.unit_name == null)
            {
                s += " " + this.id.ToString() + "\n";
            }
            else
            {
                s += " " + this.unit_name + " " + this.unit_pos + "\n";
            }
            return s;
        }
    }
}