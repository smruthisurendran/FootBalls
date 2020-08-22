using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblTeamMembers")]
    public class TblTeamMembers
    {
        [Key]
        public int TeamMemberId { get; set; }

        [ForeignKey("TblTeam")]
        public int TeamId { get; set; }
        public virtual TblTeam TblTeam { get; set; }

        public string Category { get; set; }
        public string Position { get; set; }
        public string TeamReferenceNumber { get; set; }

        public int Status { get; set; }
        public int CreatedId { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int PlayerId { get; set; }
        public int CoachId { get; set; }

    }
}