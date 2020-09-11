using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FootBalls.Controllers.AccountController;

namespace FootBalls.Models
{
    [Table("TblChangeBackground")]
    public class TblChangeBackground
    {
        [Key]
        public int ChangBackgrndId { get; set; }

        public int UserRole { get; set; }

       

       public byte[] Photo { get; set; }

    }
}