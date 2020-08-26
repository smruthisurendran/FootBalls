using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblChampionshipSponsor")]
    public class TblChampionshipSponsor
    {
        public string ChampionshipSponsorReferenceNumber { get; set; }

        [Key]
        public int ChampionshipSponsorId { get; set; }

        [Required(ErrorMessage = "Please Enter Sponsor Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Sponsor Category")]
        public string Category { get; set; }

        public int Confirmed { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [ForeignKey("TblUser")]
        public int UserId { get; set; }
        public virtual TblUser TblUser { get; set; }

        public int Status { get; set; }

        public int CreatedId { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}