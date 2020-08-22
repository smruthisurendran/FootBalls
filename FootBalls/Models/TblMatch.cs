using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblMatch")]
    public class TblMatch
    {
        [Key]
        public int MatchId { get; set; }

        public string Match { get; set; }
        public int MatchType { get; set; }

        public int Team1Id { get; set; }
        public int TotalGoal1 { get; set; }

        public int Team2Id { get; set; }
        public int TotalGoal2 { get; set; }

        public int WonTeamId { get; set; }

        [ForeignKey("TblChampionship")]
        public int ChampionshipId { get; set; }
        public virtual TblChampionship TblChampionship { get; set; }

        public DateTime MatchDate { get; set; }
        public TimeZone MatchStartTime { get; set; }
        public TimeZone MatchEndTime { get; set; }

        [ForeignKey("TblPlayGround")]
        public int PGId { get; set; }
        public virtual TblPlayGround TblPlayGround { get; set; }

        public int Status { get; set; }

        public int CreatedId { get; set; }
        public DateTime CreatedTime { get; set; }

        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }



    }
}