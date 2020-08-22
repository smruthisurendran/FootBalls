using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblRegion")]
    public class TblRegion
    {
        [Key]
        public int RegionId { get; set; }
        public string Region { get; set; }

        [ForeignKey("TblCountry")]
        public int CountryId { get; set; }
        public virtual TblCountry TblCountry { get; set; }

        public int CreatedId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}