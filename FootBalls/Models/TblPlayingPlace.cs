using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FootBalls.Models
{
    [Table("TblPlayingPlace")]
    public class TblPlayingPlace
    {
        [Key]
        public int PlayingPlaceId { get; set; }

        public string PlayingPlace { get; set; }

        public int Status { get; set; }
        public int CreatedId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}