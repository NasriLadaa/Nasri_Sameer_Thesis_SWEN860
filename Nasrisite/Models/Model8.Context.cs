﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nasrisite.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SWENEntities : DbContext
    {
        public SWENEntities()
            : base("name=SWENEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<NonFuncationalRequirment> NonFuncationalRequirments { get; set; }
        public virtual DbSet<participant> participants { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Sheet1_> Sheet1_ { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamTournament> TeamTournaments { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersReview> UsersReviews { get; set; }
    }
}
