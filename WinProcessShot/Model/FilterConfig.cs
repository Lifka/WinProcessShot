using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WinProcessShot.Model
{
    [Serializable]
    public class FilterConfig
    {
        #region MEMBERS

        public FilterPropertiesEnum Property { get; set; }
        public FilterConditionsEnum Condition { get; set; }
        public string Value { get; set; }

        #endregion

        #region CONSTRUCTORS
        public FilterConfig() { }

        public FilterConfig(FilterPropertiesEnum property, FilterConditionsEnum condition, string value)
        {
            this.Property = property;
            this.Condition = condition;
            this.Value = value;
        }
        
        public FilterConfig(string property, string condition, string value)
        {
            this.Property = Enum.TryParse(property, true, out FilterPropertiesEnum resultProp) ? resultProp : default;
            this.Condition = Enum.TryParse(condition, true, out FilterConditionsEnum resultCond) ? resultCond : default;
            this.Value = value;
        }

        #endregion
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum FilterPropertiesEnum
    {
        Name,
        InternalName,
        ExecutablePath,
        CompanyName,
        PID,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum FilterConditionsEnum
    {
        Contains,
        NotContains,
        Is,
        IsNot,
        LessThan,
        MoreThan,
    }
}
