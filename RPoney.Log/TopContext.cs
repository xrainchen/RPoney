using System;
using System.Collections.Specialized;

namespace RPoney.Log
{
    public class TopContext
    {
        // Methods
        public TopContext() : this(new NameValueCollection())
        {
        }

        public TopContext(NameValueCollection keyValues)
        {
            ContextCollection = keyValues;
        }

        // Properties
        public NameValueCollection ContextCollection { get; set; }

        public int Count
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ContextCollection["TopDataCount"]))
                {
                    return Convert.ToInt32(this.ContextCollection["TopDataCount"]);
                }
                return 0;
            }
            set
            {
                ContextCollection["TopDataCount"] = value.ToString();
            }
        }

        public int CurrentPageNumber
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ContextCollection["CurrentPageNumber"]))
                {
                    return Convert.ToInt32(this.ContextCollection["CurrentPageNumber"]);
                }
                return 1;
            }
            set
            {
                ContextCollection["CurrentPageNumber"] = value.ToString();
            }
        }

        public string DeptId
        {
            get
            {
                return ContextCollection["DeptId"];
            }
            set
            {
                ContextCollection["DeptId"] = value;
            }
        }

        public string EventNo
        {
            get
            {
                return ContextCollection["EventNo"];
            }
            set
            {
                ContextCollection["EventNo"] = value;
            }
        }

        public string OrganId
        {
            get
            {
                return ContextCollection["OrganId"];
            }
            set
            {
                ContextCollection["OrganId"] = value;
            }
        }

        public int PageSize
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ContextCollection["PageSize"]))
                {
                    return Convert.ToInt32(this.ContextCollection["PageSize"]);
                }
                return 0;
            }
            set
            {
                ContextCollection["PageSize"] = value.ToString();
            }
        }

        public string RealName
        {
            get
            {
                string text1 = this.ContextCollection["RealName"];
                if (text1 == null)
                {
                    return null;
                }
                return text1.ToString();
            }
            set
            {
                ContextCollection["RealName"] = value;
            }
        }

        public string TenancyId
        {
            get
            {
                return ContextCollection["TenancyId"];
            }
            set
            {
                ContextCollection["TenancyId"] = value;
            }
        }

        public string Theme
        {
            get
            {
                return ContextCollection["Theme"];
            }
            set
            {
                ContextCollection["Theme"] = value;
            }
        }

        public string TopActionId
        {
            get
            {
                return ContextCollection["TopActionId"];
            }
            set
            {
                ContextCollection["TopActionId"] = value;
            }
        }

        public string URL
        {
            get
            {
                string text1 = this.ContextCollection["URL"];
                if (text1 == null)
                {
                    return null;
                }
                return text1.ToString();
            }
            set
            {
                ContextCollection["URL"] = value;
            }
        }

        public string UserId
        {
            get
            {
                return ContextCollection["UserId"];
            }
            set
            {
                ContextCollection["UserId"] = value;
            }
        }
    }
}
