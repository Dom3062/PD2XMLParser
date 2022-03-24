namespace XMLParser.Elements.Attributes
{
    class filter
    {
        private readonly int pos;
        private readonly string value;
        public filter(int pos, string value)
        {
            this.pos = pos;
            this.value = value;
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string s = prefix + this.pos.ToString() + " ";
            if (this.value == "true")
            {
                s += "True\n";
            }
            else
            {
                s += "False\n";
            }
            return s;
        }
    }
}