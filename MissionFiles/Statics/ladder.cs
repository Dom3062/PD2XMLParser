namespace XMLParser.MissionFiles.Statics
{
    class ladder
    {
        private string height;
        private string width;
        public ladder(string height, string width)
        {
            this.height = height;
            this.width = width;
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string s = prefix + "height " + this.height + "\n";
            s += prefix + "width " + this.width + "\n";
            return s;
        }
    }
}