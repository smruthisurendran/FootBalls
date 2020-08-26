using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootBalls.Models
{
    public class TeamDetailsTbl
    {
        public TblTeam TeamTbl { get; set; }
        public List<TblPlayerRequest> PlayerRequestTbl { get; set; }
        public List<TblCoachRequest> CoachRequestTbl { get; set; }
        public List<TblTeamMembers> TeamMembersTbl { get; set; }

    }
}