using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FootBalls.Models
{
    public class AllUsersContext : DbContext
    {
        public DbSet<TblUser> User_tbl { get; set; }
        public DbSet<TblPlayer> Player_tbl { get; set; }
        public DbSet<TblPlayerRequest> PlayerRequest_tbl { get; set; }
        public DbSet<TblCity> City_tbl { get; set; }
        public DbSet<TblCountry> Country_tbl { get; set; }
        public DbSet<TblPlayingPlace> PlayingPlace_tbl { get; set; }
        public DbSet<TblRegion> Region_tbl { get; set; }
        public DbSet<TblCoach> Coach_tbl { get; set; }
        public DbSet<TblCoachRequest> CoachRequest_tbl { get; set; }
        public DbSet<TblTeam> Team_tbl { get; set; }
        public DbSet<TblTeamMembers> TeamMembers_tbl { get; set; }
        public DbSet<TblReferee> Referee_tbl { get; set; }
        public DbSet<TblPlayGroundOwner> PlayGroundOwner_tbl { get; set; }
        public DbSet<TblPlayGround> PlayGround_tbl { get; set; }
        public DbSet<TblChampionshipSponsor> ChampionshipSponsor_tbl { get; set; }
        public DbSet<TblChampionship> Championship_tbl { get; set; }
        public DbSet<TblChampionshipRequest> ChampionshipRequest_tbl { get; set; }
        public DbSet<TblTeamSponsor> TeamSponsor_tbl { get; set; }


       /* public DbSet<TblPlayGround> PlayGround_tbl { get; set; }
        public DbSet<TblChampionship> Championship_tbl { get; set; }
        public DbSet<TblTeam> Team_tbl { get; set; }
        public DbSet<TblMatch> Match_tbl { get; set; }
        public DbSet<TblMatchDetail> MatchDetail_tbl { get; set; } */


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<AllUsersContext>(null);
            base.OnModelCreating(modelBuilder);

        }
    }
}