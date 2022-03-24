namespace XMLParser.Elements.Attributes
{
    class on_executed
    {
        private readonly int id;
        private string name = null;
        private readonly double? delay = null;
        private readonly double delay_min;
        private readonly double delay_max;
        private bool disabled;
        private string alternative = null;
        public on_executed(int id, double delay, bool disabled)
        {
            this.id = id;
            this.delay = delay;
            this.disabled = disabled;
        }

        public on_executed(int id, double delay_min, double delay_max, bool disabled)
        {
            this.id = id;
            this.delay_min = delay_min;
            this.delay_max = delay_max;
            this.disabled = disabled;
        }

        public on_executed(string name, double delay, bool disabled)
        {
            this.name = name;
            this.delay = delay;
            this.disabled = disabled;
        }

        public on_executed(string name, double delay_min, double delay_max, bool disabled)
        {
            this.name = name;
            this.delay_min = delay_min;
            this.delay_max = delay_max;
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

        private string GetElementName()
        {
            return this.name ?? "";
        }

        private string GetDelay()
        {
            if (this.delay == null)
            {
                return GetDelayMinMax();
            }
            return this.delay.ToString().Replace(",", ".");
        }

        private string GetDelayMinMax()
        {
            return this.delay_min.ToString().Replace(",", ".") + "-" + this.delay_max.ToString().Replace(",", ".");
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
            if (name == null)
            {
                if (disabled)
                {
                    return this.id.ToString() + " (delay " + GetDelay() + ") DISABLED" + FormatAlternative();
                }
                return this.id.ToString() + " (delay " + GetDelay() + ")" + FormatAlternative();
            }
            else
            {
                if (disabled)
                {
                    return GetElementName() + " (delay " + GetDelay() + ") DISABLED" + FormatAlternative();
                }
                return GetElementName() + " (delay " + GetDelay() + ")" + FormatAlternative();
            }
        }
    }
}