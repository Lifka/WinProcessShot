using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WinProcessShot.Model;

namespace WinProcessShot.Controller
{
    internal static class FiltersController
    {
        #region MEMBERS

        static object __lockObj = new object();

        private static IEnumerable<Filter> currentFilters = new List<Filter>();
        public static IEnumerable<Filter> CurrentFilters { 
            get
            {
                lock (__lockObj)
                {
                    return currentFilters;
                }
            }
            set
            {
                currentFilters = value;
            } 
        }

        public static bool IncludeTrusted = true;

        #endregion

        #region METHODS

        public static void AddFilter(string property, string condition, string value)
        {
            lock (__lockObj)
            {
                ((List<Filter>)CurrentFilters).Add(new Filter(property, condition, value));
            }
        }

        public static void AddFilter(FilterPropertiesEnum property, FilterConditionsEnum condition, string value)
        {
            lock (__lockObj)
            {
                ((List<Filter>)CurrentFilters).Add(new Filter(property, condition, value));
            }
        }

        public static void AddFilter(FilterConfig filterConfig)
        {
            lock (__lockObj)
            {
                ((List<Filter>)CurrentFilters).Add(new Filter(filterConfig));
            }
        }

        public static void ResetFilters()
        {
            CurrentFilters = new List<Filter>();
        }

        public static void SaveFilters()
        {
            Properties.Settings.Default.FilterObjs = new List<Model.FilterConfig>();

            foreach (Filter filter in CurrentFilters)
            {
                Properties.Settings.Default.FilterObjs.Add(filter.FilterConfig);
            }

            Properties.Settings.Default.IncludeTrusted = IncludeTrusted;
            Properties.Settings.Default.Save();
        }

        public static void LoadFilters()
        {
            if (Properties.Settings.Default == null 
                || Properties.Settings.Default.FilterObjs == null)
            {
                return;
            }

            foreach (WinProcessShot.Model.FilterConfig filterConfig in Properties.Settings.Default.FilterObjs)
            {
                AddFilter(filterConfig);
            }

            IncludeTrusted = Properties.Settings.Default.IncludeTrusted;
        }

        public static bool PassFilters(ProcessInfoObj process)
        {
            if (process.Trusted.HasValue 
                && process.Trusted.Value 
                && !IncludeTrusted)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(process.MD5)
                && process.MD5.Equals("68a1f7c796de1d0df6b2d78e182df3a0")
                || process.Name.Contains("procdump64")) // avoid to add procdump64.exe
            {
                return false;
            }

            if (currentFilters == null)
            {
                return true;
            }

            foreach (Filter filter in currentFilters)
            {
                if (filter.MatchFilter(process))
                {
                    return false;
                }
            }

            return true;
        }


        #endregion
    }
}
