using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootBalls.Models
{
    public class CoachDetailsTbl
    {
        public TblCoach CoachTbl { get; set; }
        public List<TblTeamMembers> TeamMembersTbl { get; set; }

    }
}