using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblTeam")]
    public class TblTeam
    {
       

        [Key]
        public int TeamId { get; set; }
   
        public string TeamName { get; set; }

        [ForeignKey("TblCity")]
        public int CityId { get; set; }
        public virtual TblCity TblCity { get; set; }

        public byte[] Photo { get; set; }
        public int Confirmed { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        [ForeignKey("TblTeamSponsor")]
        public int? TeamSponsorId { get; set; }
        public virtual TblTeamSponsor TblTeamSponsor { get; set; }

        public int Status { get; set; }
        public int CreatedId { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string TeamReferenceNumber { get; set; }
    }
}