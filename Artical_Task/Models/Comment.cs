//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Artical_Task.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int id { get; set; }
        public string text { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> art_id { get; set; }
        public Nullable<System.DateTime> dateTime { get; set; }
    
        public virtual Artical Artical { get; set; }
        public virtual User User { get; set; }
    }
}
