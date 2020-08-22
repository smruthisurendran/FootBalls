using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblCountry")]
    public class TblCountry
    {
        [Key]
        public int CountryId { get; set; }

        public string Country { get; set; }
        public int CreatedId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}