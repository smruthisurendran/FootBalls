using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblPlayGroundWorkingDetails")]
    public class TblPlayGroundWorkingDetails
    {
        [Key]
        public int PGWorkingDetailid { get; set; }

        [ForeignKey("TblPlayGround")]
        public int PGId { get; set; }

        public int WorkingDay { get; set; }
        public int WorkingHour { get; set; }
        public int Status { get; set; }

        public int CreatedId { get; set; }
        public DateTime CreatedTime { get; set; }

        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}