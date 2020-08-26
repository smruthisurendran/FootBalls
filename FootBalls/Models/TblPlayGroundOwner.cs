using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FootBalls.Models
{
    [Table("TblPlayGroundOwner")]
    public class TblPlayGroundOwner
    {
        [Key]
        public int PGOwnerId { get; set; }

        public string PGOwnerReferenceNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Owner Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile Number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please Enter Category")]
        public string Category { get; set; }

        [ForeignKey("TblCity")]
        public int CityId { get; set; }
        public virtual TblCity TblCity { get; set; }

        public int Confirmed { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

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