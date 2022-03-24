namespace XMLParser.Elements.Attributes
{
    class numeric_list
    {
        int pos;
        int n;
        List<string> keys = new();
        List<string> values = new();
        public numeric_list(int pos)
        {
            this.pos = pos;
            this.n = 0;
        }

        public void Add(string key, string value)
        {
            keys.Add(key);
            values.Add(value);
            n++;
        }

        public void Update(string key, string value)
        {
        }

        public string Get(string key)
        {
            return null;
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string s = prefix + this.pos.ToString() + "\n";
            for (int i = 0; i < n; i++)
            {
                s += prefix + "\t" + keys[i] + " " + values[i] + "\n";
            }
            return s;
        }
    }
}