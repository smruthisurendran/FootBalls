using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblTeamRequest")]
    public class TblTeamRequest
    {
        [Key]
        public int TeamRequestId { get; set; }

        [ForeignKey("TblTeam")]
        public int TeamId { get; set; }

        public int RequestFrom { get; set; }

        public int RequestFor { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Approved { get; set; }
        public int Status { get; set; }

        public int CreatedId { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}