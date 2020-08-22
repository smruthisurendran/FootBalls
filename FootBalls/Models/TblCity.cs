using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblCity")]
    public class TblCity
    {
        [Key]
        public int CityId { get; set; }
        public string City { get; set; }

        [ForeignKey("TblRegion")]
        public int Regionid { get; set; }
        public virtual TblRegion TblRegion { get; set; }

        public int CreatedId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}