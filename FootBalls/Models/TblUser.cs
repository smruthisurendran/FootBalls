using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FootBalls.Models
{
    [Table("TblUser")]
    public class TblUser
    {
        [Key]
       public int UserId { get; set; }
       public int GeneralReferenceNumber { get; set; }

        [Required(ErrorMessage = "Please enter your EmailId")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmailId { get; set; }

       
       public int Category { get; set; }

        
        
        public string Name { get; set; }

        
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
       public int Status { get; set; }
       public int CreatedId { get; set; }
       public DateTime CreatedDate { get; set; }
       public int ModifiedId { get; set; }
       public DateTime ModifiedDate { get; set; }

    }
}