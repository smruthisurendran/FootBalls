using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblChampionship")]
    public class TblChampionship
    {
        [Key]
        public int ChampionshipId { get; set; }

        public string ChampionshipReferenceNumber { get; set; }

        [ForeignKey("TblChampionshipSponsor")]
        public int? ChampionshipSponsorId { get; set; }
        public virtual TblChampionshipSponsor TblChampionshipSponsor { get; set; }

        public string Championship { get; set; }

        public string Category { get; set; }

        public DateTime ChampionshipStartDate { get; set; }
        public DateTime ChampionshipEndDate { get; set; }

        public int SponsorId { get; set; }

        [ForeignKey("TblCity")]
        public int CityId { get; set; }
        public virtual TblCity TblCity { get; set; }
        
        public byte[] Image { get; set; }
        public int Status { get; set; }

        public int CreatedId { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}