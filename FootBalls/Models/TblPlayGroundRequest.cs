using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblPlayGroundRequest")]
    public class TblPlayGroundRequest
    {
        [Key]
        public int PGRequestId { get; set; }

        [ForeignKey("TblPlayGround")]
        public int PGId { get; set; }

        public int RequestFrom { get; set; }
        public int RequestFor { get; set; }

        public DateTime Date { get; set; }
        public TimeZone FromTime { get; set; }
        public TimeZone ToTime { get; set; }
        public int Approved { get; set; }


        public int Status { get; set; }

        public int CreatedId { get; set; }
        public DateTime CreatedTime { get; set; }

        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}