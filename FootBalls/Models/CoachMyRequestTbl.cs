using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootBalls.Models
{
    public class CoachMyRequestTbl
    {
        public List<TblCoachRequest> CoachRequestTbl { get; set; }
        public List<TblTeamRequest> TeamRequestTbl { get; set; }
    }
}