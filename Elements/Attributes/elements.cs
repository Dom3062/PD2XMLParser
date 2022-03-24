namespace XMLParser.Elements.Attributes
{
    class elements
    {
        private readonly int id;
        private readonly int pos;
        private string name = null;
        private bool disabled;
        private string alternative = null;
        public elements(int id, int pos, bool disabled)
        {
            this.id = id;
            this.pos = pos;
            this.disabled = disabled;
        }

        public elements(string name, int pos, bool disabled)
        {
            this.name = name;
            this.pos = pos;
            this.disabled = disabled;
        }

        public int GetElementID()
        {
            return this.id;
        }

        public void UpdateElementName(string name)
        {
            this.name = name;
        }

        public void UpdateElementDisabled(bool disabled)
        {
            this.disabled = disabled;
        }

        public void UpdateAlternativeExecution(string alternative)
        {
            this.alternative = alternative;
        }

        private string FormatAlternative()
        {
            if (this.alternative == null)
            {
                return "";
            }
            return " (alternative " + this.alternative + ")";
        }

        public override string ToString()
        {
            if (this.name == null)
            {
                if (disabled)
                {
                    return this.pos.ToString() + " " + this.id.ToString() + " DISABLED" + FormatAlternative();
                }
                return this.pos.ToString() + " " + this.id.ToString() + FormatAlternative();
            }
            if (disabled)
            {
                return this.pos.ToString() + " " + this.name + " DISABLED" + FormatAlternative();
            }
            return this.pos.ToString() + " " + this.name + FormatAlternative();
        }
    }
}