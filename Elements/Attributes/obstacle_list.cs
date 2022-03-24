namespace XMLParser.Elements.Attributes
{
    class obstacle_list
    {
        private readonly int pos;
        private readonly int? guis_id;
        private readonly string obj_name;
        private readonly int unit_id;
        private string unit_name;
        private string unit_pos = "()";
        public obstacle_list(int pos, int? guis_id, string obj_name, int unit_id)
        {
            this.pos = pos;
            this.guis_id = guis_id;
            this.obj_name = obj_name;
            this.unit_id = unit_id;
        }

        public int GetUnitID()
        {
            return this.unit_id;
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
            string double_prefix = prefix + "\t";
            string s = prefix + this.pos.ToString() + "\n";
            if (this.guis_id != null)
            {
                s += double_prefix + "guis_id " + this.guis_id.ToString() + "\n";
            }
            s += double_prefix + "obj_name " + this.obj_name + "\n";
            if (this.unit_name == null)
            {
                s += double_prefix + "unit_id " + this.unit_id.ToString() + "\n";
            }
            else
            {
                s += double_prefix + "unit_id " + this.unit_name + " " + this.unit_pos + "\n";
            }
            return s;
        }
    }
}