using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblMatchDetail")]
    public class TblMatchDetail
    {
        [Key]
        public int MatchDetailId { get; set; }

        [ForeignKey("TblMatch")]
        public int MatchId { get; set; }
        public virtual TblMatch TblMatch { get; set; }

        [ForeignKey("TblTeam")]
        public int TeamId { get; set; }
        public virtual TblTeam TblTeam { get; set; }

        [ForeignKey("TblPlayer")]
        public int PlayerId { get; set; }
        public virtual TblPlayer TblPlayer { get; set; }

        public int Event { get; set; }
        public DateTimeOffset Date { get; set; }
        public TimeZone Time { get; set; }

        public int Status { get; set; }

        public int CreatedId { get; set; }
        public DateTime CreatedTime { get; set; }

        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}