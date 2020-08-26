using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblPlayGround")]
    public class TblPlayGround
    {
        [Key]
        public int PGId { get; set; }

        public string PGReferenceNumber { get; set; }

        [ForeignKey("TblPlayGroundOwner")]
        public int PGOwnerId { get; set; }
        public virtual TblPlayGroundOwner TblPlayGroundOwner { get; set; }

        [Required(ErrorMessage = "Please Enter PlayGroundName")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Length")]
        public int Length { get; set; }

        [Required(ErrorMessage = "Please Enter Width")]
        public int Width { get; set; }

        [Required(ErrorMessage = "Please Enter Goal Length")]
        public int GoalLength { get; set; }

        [Required(ErrorMessage = "Please Enter Goal Width")]
        public int GoalWidth { get; set; }

        [Required(ErrorMessage = "Please Enter Number of Players")]
        public int NoOfPlayer { get; set; }

        [ForeignKey("TblCity")]
        public int CityId { get; set; }
        public virtual TblCity TblCity { get; set; }

        public string Location { get; set; }

        public byte[] Image { get; set; }
        public int RentingPrice { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int Status { get; set; }

        public int CreatedId { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}