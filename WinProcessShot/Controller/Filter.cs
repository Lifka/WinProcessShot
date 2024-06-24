using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WinProcessShot.Model;

namespace WinProcessShot.Controller
{
    [Serializable]
    public class Filter
    {
        #region MEMBERS

        public Model.FilterConfig FilterConfig { get; }

        #endregion

        #region CONSTRUCTORS
        public Filter()
        {
        }

        public Filter(FilterConfig filter)
        {
            this.FilterConfig = filter;
        }

        public Filter(FilterPropertiesEnum property, FilterConditionsEnum condition, string value)
        {
            this.FilterConfig = new FilterConfig(property, condition, value);
        }

        public Filter(string property, string condition, string value)
        {
            this.FilterConfig = new FilterConfig(property, condition, value);
        }

        #endregion

        #region METHODS

        public bool MatchFilter(ProcessInfoObj processesInfo)
        {
            PropertyInfo property = processesInfo.GetType().GetProperty(FilterConfig.Property.ToString());
            if (property == null)
            {
                return false;
            }

            object value = property.GetValue(processesInfo, null);
            if (value == null)
            {
                return false;
            }

            return MeetCriteria(value.ToString());
        }

        private bool MeetCriteria(string propertyValue)
        {
            bool meetCriteria = false;

            switch (FilterConfig.Condition)
            {
                case FilterConditionsEnum.Contains:
                    meetCriteria = propertyValue.Contains(FilterConfig.Value);
                    break;
                case FilterConditionsEnum.NotContains:
                    meetCriteria = !propertyValue.Contains(FilterConfig.Value);
                    break;
                case FilterConditionsEnum.Is:
                    meetCriteria = propertyValue.Equals(FilterConfig.Value);
                    break;
                case FilterConditionsEnum.IsNot:
                    meetCriteria = !propertyValue.Equals(FilterConfig.Value);
                    break;
                case FilterConditionsEnum.LessThan:
                    meetCriteria = String.Compare(propertyValue, FilterConfig.Value, StringComparison.Ordinal) < 0;
                    break;
                case FilterConditionsEnum.MoreThan:
                    meetCriteria = String.Compare(propertyValue, FilterConfig.Value, StringComparison.Ordinal) > 0;
                    break;
            }

            return meetCriteria;
        }

        #endregion
    }
}
