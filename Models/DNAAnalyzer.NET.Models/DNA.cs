using System.Text;
using System.Text.RegularExpressions;
using DNAAnalyzer.NET.Models.Contracts;
using Extensions;

namespace DNAAnalyzer.NET.Models
{
    public class DNA : IDNA
    {
        private IDNAConfiguration dnaConfiguration;

        public DNA(IDNAConfiguration dnaConfiguration, string[] components)
        {
            this.Components = components;
            this.dnaConfiguration = dnaConfiguration;
        }

        public string[] Components
        {
            get;
            set;
        }

        public bool IsValid()
        {
            bool valid = true;
            if (this.Components != null)
            {
                foreach (var component in this.Components)
                {
                    if (!Regex.Match(component, this.dnaConfiguration.ComponentsPattern, RegexOptions.IgnoreCase).Success)
                    {
                        valid = false;
                    }
                }

                if (!this.Components.IsSquare())
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }

            return valid;
        }

        public string StringRepresentation()
        {
            if (this.Components != null && this.Components.Length > 0)
            {
                StringBuilder result = new StringBuilder();
                foreach (var component in this.Components)
                {
                    result.Append(component + "-");
                }

                string stringresult = result.ToString();
                return stringresult.Substring(0, stringresult.Length - 1);
            }

            return string.Empty;
        }
    }
}
