namespace TwitterChannelsExplorer.EntityFramework.TwitterEntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Channel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Channel()
        {
            Tweets = new HashSet<Tweet>();
        }

        public int channelId { get; set; }

        [Required]
        [StringLength(50)]
        public string src_Name { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public DateTime? date { get; set; }

        public int? categoryId { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tweet> Tweets { get; set; }
    }
}
