//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace miAlert.Models.RSSDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Content { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<bool> Deleted { get; set; }
    }
}
