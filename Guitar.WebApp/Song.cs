//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Guitar.WebApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Song
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Song()
        {
            this.InternetLinks = new HashSet<InternetLink>();
        }
    
        public int songID { get; set; }
        public string songName { get; set; }
        public int authorID { get; set; }
        public bool isPartialyLearned { get; set; }
        public bool isFullLearned { get; set; }
        public string youtubeLink { get; set; }
        public string groovesharkLink { get; set; }
    
        public virtual Author Author { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InternetLink> InternetLinks { get; set; }
    }
}
