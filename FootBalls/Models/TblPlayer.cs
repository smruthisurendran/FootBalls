using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBalls.Models
{
    [Table("TblPlayer")]
    public class TblPlayer
    {
      
        [Key]
        public int PlayerId { get; set; }

        [Required(ErrorMessage ="Please Enter Player Name")]
        public string Player { get; set; }
        //[Column(TypeName ="datetime2")]
        [Required(ErrorMessage ="Please enter DOB")]
        public DateTime DOB { get; set; }

        [ForeignKey("TblCity")]
        
        public int CityId { get; set; }
        public virtual TblCity TblCity { get; set; }

        [Required(ErrorMessage = "Please enter Length")]
        public int Length { get; set; }
        [Required(ErrorMessage = "Please enter Weight")]
        public int Weight { get; set; }
        [Required(ErrorMessage = "Please enter PlayingFoot")]
        public int PlayingFoot { get; set; }

        [ForeignKey("TblPlayingPlace")]
        public int PlayingPlace { get; set; }
        public virtual TblPlayingPlace TblPlayingPlace { get; set; }

        
        public byte[] Photo { get; set; }
        //public HttpPostedFileBase File { get; set; }

        public int Confirmed { get; set; }
        //[Column(TypeName ="datetime2")]
        public DateTime RegistrationDate { get; set; }

        //[Column(TypeName = "datetime2")]
        public DateTime ExpirationDate { get; set; }
        public int SponsorId { get; set; }
        public string Mobile { get; set; }

        [ForeignKey("TblUser")]
        public int UserId { get; set; }
        public virtual TblUser TblUser { get; set; }

        public int Status { get; set; }
        public int CreatedId { get; set; }

        //[Column(TypeName = "datetime2")]
        public DateTime? CreatedDate { get; set; }
        public int ModifiedId { get; set; }

        //[Column(TypeName = "datetime2")]
        public DateTime? ModifiedDate { get; set; }

        public string PlayerReferenceNumber { get; set; }
    }
}
