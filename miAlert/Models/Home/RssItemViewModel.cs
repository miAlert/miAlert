using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miAlert.Models.Home
{
    public class RssItemViewModel
    {
        public String Url;
        public String Title;
        public bool DeathInvolved;
        public RssItemViewModel()
        {
            DeathInvolved = false;
        }
    }
}