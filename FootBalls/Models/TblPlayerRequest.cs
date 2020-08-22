using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FootBalls.Models
{
    [Table("TblPlayerRequest")]
    public class TblPlayerRequest
    {
        [Key]
        public int PlayerRequestId { get; set; }

        [ForeignKey("TblPlayer")]
        public int PlayerId { get; set; }
        public virtual TblPlayer TblPlayer { get; set; }

        [ForeignKey("TblTeam")]
        public int RequestFrom { get; set; }
        public virtual TblTeam TblTeam { get; set; }

        public int RequestFor{ get; set; }
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