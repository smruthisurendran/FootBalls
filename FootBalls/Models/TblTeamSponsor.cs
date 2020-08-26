using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblTeamSponsor")]
    public class TblTeamSponsor
    {
        public string TeamSponsorReferenceNumber { get; set; }

        [Key]
        public int TeamSponsorId { get; set; }

        [Required(ErrorMessage = "Please Enter Sponsor Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Category")]
        public string Category { get; set; }

        public int Confirmed { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string Mobile { get; set; }

        [ForeignKey("TblUser")]
        public int UserId { get; set; }
        public virtual TblUser TblUser { get; set; }

        [ForeignKey("TblCity")]
        public int CityId { get; set; }
        public virtual TblCity TblCity { get; set; }

        public int CreatedId { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}