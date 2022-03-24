namespace XMLParser.Elements.Attributes
{
    class element_item
    {
        private readonly int pos;
        private readonly int element_id;
        private string editor_name = null;
        private bool disabled = false;
        private string alternative = null;
        public element_item(int pos, int id)
        {
            this.pos = pos;
            this.element_id = id;
        }

        public element_item(int pos, string editor_name)
        {
            this.pos = pos;
            this.editor_name = editor_name;
        }

        public int GetElementID()
        {
            return this.element_id;
        }

        public void UpdateElementName(string name)
        {
            this.editor_name = name;
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
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            if (this.editor_name == null)
            {
                if (disabled)
                {
                    return prefix + this.pos.ToString() + " " + this.element_id.ToString() + " DISABLED" + FormatAlternative() + "\n";
                }
                return prefix + this.pos.ToString() + " " + this.element_id.ToString() + FormatAlternative() + "\n";
            }
            if (disabled)
            {
                return prefix + this.pos.ToString() + " " + this.editor_name + " DISABLED" + FormatAlternative() + "\n";
            }
            return prefix + this.pos.ToString() + " " + this.editor_name + FormatAlternative() + "\n";
        }
    }
}