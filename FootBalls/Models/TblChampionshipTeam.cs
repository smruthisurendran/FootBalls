using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblChampionshipTeam")]
    public class TblChampionshipTeam
    {
        [Key]
        public int ChampionshipTeamId { get; set; }

        [ForeignKey("TblChampionship")]
        public int ChampionshipId { get; set; }

        public int TeamId { get; set; }

        public int Status { get; set; }

        public int CreatedId { get; set; }
        public DateTime CreatedTime { get; set; }

        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}