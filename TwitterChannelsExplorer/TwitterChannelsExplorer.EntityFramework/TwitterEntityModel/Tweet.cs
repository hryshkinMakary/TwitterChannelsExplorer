namespace TwitterChannelsExplorer.EntityFramework.TwitterEntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tweet
    {
        public int? channelId { get; set; }

        public string text { get; set; }

        [StringLength(300)]
        public string src_image { get; set; }

        public DateTime date_time { get; set; }

        [StringLength(300)]
        public string image { get; set; }

        public int id { get; set; }

        public virtual Channel Channel { get; set; }
    }
}
