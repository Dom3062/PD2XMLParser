namespace XMLParser.Elements.Attributes
{
    class instance_params
    {
        string var_name;
        string var_value;
        public instance_params(string var_name, string var_value)
        {
            this.var_name = var_name;
            this.var_value = var_value;
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            return prefix + this.var_name + " " + this.var_value + "\n";
        }
    }
}