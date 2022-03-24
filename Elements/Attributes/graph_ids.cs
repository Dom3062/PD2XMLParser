namespace XMLParser.Elements.Attributes
{
    class graph_ids
    {
        private readonly int pos;
        private readonly string graph_id;
        public graph_ids(int pos, string graph_id)
        {
            this.pos = pos;
            this.graph_id = graph_id;
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            return prefix + this.pos.ToString() + " " + this.graph_id + "\n";
        }
    }
}