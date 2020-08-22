using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FootBalls.Models
{
    [Table("TblChampionshipRequest")]
    public class TblChampionshipRequest
    {
        [Key]
        public int ChampionshipRequestId { get; set; }

        [ForeignKey("TblChampionship")]
        public int ChampionshipId { get; set; }
        public virtual TblChampionship TblChampionship { get; set; }

        [ForeignKey("TblPlayGroundOwner")]
        public int EntityId { get; set; }
        public virtual TblPlayGroundOwner TblPlayGroundOwner { get; set; }

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