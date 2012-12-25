using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using LibModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LibBAL.orm
{
    public class SocialGEOContext : DbContext
    {
        public IDbSet<Item> Items { get; set; }
        public IDbSet<Article> Articles { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Profile> Profiles { get; set; }
        public IDbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //PLURAL REMOVAL
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //WE USE FLUENT API FOR MAPPING PURPOSES
            //--------------------------------------
            //ITEM     
            modelBuilder.Entity<Item>().HasKey(d => d.ID);
            modelBuilder.Entity<Item>()
                .Property(d => d.ID)
                .HasColumnName("item_id")
                .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Item>()
                .Property(d => d.Title)
                .HasColumnName("item_title")
                .IsRequired()
                .HasMaxLength(128);
            modelBuilder.Entity<Item>()
                .Property(d => d.Description)
                .HasColumnName("item_description")
                .IsRequired()
                .HasMaxLength(500);
            modelBuilder.Entity<Item>()
                .Property(d => d.CreatedDate)
                .HasColumnName("item_createddate")
                .IsRequired();
            modelBuilder.Entity<Item>()
                .Property(d => d.ModifiedDate)
                .HasColumnName("item_modifieddate");
            modelBuilder.Entity<Item>()
                .Property(d => d.DeletedDate)
                .HasColumnName("item_deleteddate");
            modelBuilder.Entity<Item>()
                .Property(d => d.PublishedDate)
                .HasColumnName("item_publisheddate");
            modelBuilder.Entity<Item>()
                .Property(d => d.ApprovedDate)
                .HasColumnName("item_approveddate");
            modelBuilder.Entity<Item>().ToTable("items");

            //ARTICLE
            modelBuilder.Entity<Article>()
                .Property(d => d.Body)
                .HasColumnName("article_body")
                .IsRequired();
            modelBuilder.Entity<Article>()
                .HasMany(a => a.Categories)
                .WithMany(c => c.Articles)
                .Map(m =>
                {
                    m.MapLeftKey("article_id");
                    m.MapRightKey("category_id");
                    m.ToTable("articles_has_categories");
                }); 

            //CATEGORY
            modelBuilder.Entity<Category>()
                .Property(d => d.ParentCategoryID)
                .HasColumnName("category_parentid");
            modelBuilder.Entity<Category>()
                .HasOptional(s => s.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(s => s.ParentCategoryID);

            //COMMENT
            modelBuilder.Entity<Comment>()
                .Property(c => c.Body)
                .HasColumnName("comment_body")
                .IsRequired();
            modelBuilder.Entity<Comment>()
               .Property(c => c.ParentCommentID)
               .HasColumnName("comment_parentid");
            modelBuilder.Entity<Comment>()
                .HasOptional(c => c.ParentComment)
                .WithMany(p => p.SubComments)
                .HasForeignKey(c => c.ParentCommentID);
            modelBuilder.Entity<Comment>()
               .Property(c => c.ArticleID)
               .HasColumnName("article_id");
            modelBuilder.Entity<Comment>()
                .HasOptional(c => c.Article)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.ArticleID);

            //USER
            modelBuilder.Entity<User>().HasKey(u => u.ID);
            modelBuilder.Entity<User>()
                .Property(u => u.ID)
                .HasColumnName("user_id")
                .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .HasColumnName("user_username")
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasColumnName("user_password")
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordSalt)
                .HasColumnName("user_passwordsalt")
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasColumnName("user_email")
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.ThumbnailUrl)
                .HasColumnName("user_thumbnailurl")
                .IsOptional();
            modelBuilder.Entity<User>()
                .Property(u => u.SecurityQuestion)
                .HasColumnName("user_securityquestion")
                .IsOptional();
            modelBuilder.Entity<User>()
                .Property(u => u.SecurityAnswer)
                .HasColumnName("user_securityanswer")
                .IsOptional();
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedDate)
                .HasColumnName("user_createddate")
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.ModifiedDate)
                .HasColumnName("user_modifieddate")
                .IsOptional();
            modelBuilder.Entity<User>()
                .Property(u => u.DeletedDate)
                .HasColumnName("user_deleteddate")
                .IsOptional();
            modelBuilder.Entity<User>()
                .Property(u => u.ActivatedDate)
                .HasColumnName("user_activateddate")
                .IsOptional();
            modelBuilder.Entity<User>()
                .Property(u => u.ActivationKey)
                .HasColumnName("user_activationkey")
                .IsOptional();
            modelBuilder.Entity<User>()
                .Property(u => u.LockedDate)
                .HasColumnName("user_lockeddate")
                .IsOptional();
            modelBuilder.Entity<User>()
                .Property(u => u.LastLoggedInDate)
                .HasColumnName("user_lastloggedindate")
                .IsOptional();
            modelBuilder.Entity<User>()
                .Property(u => u.LastActivityDate)
                .HasColumnName("user_lastactivitydate")
                .IsOptional();
            modelBuilder.Entity<User>()
                .Property(u => u.LastPasswordChangedDate)
                .HasColumnName("user_lastpasswordchangeddate")
                .IsOptional();
            modelBuilder.Entity<User>()
                .Property(u => u.AmountOfLoggedIn)
                .HasColumnName("user_amountofloggedin")
                .IsOptional();            
            modelBuilder.Entity<User>().ToTable("users");

            //PERSON
            modelBuilder.Entity<Profile>().HasKey(p => p.UserId);
            modelBuilder.Entity<Profile>()
                .Property(p => p.UserId)
                .HasColumnName("user_id")
                .IsRequired();
            modelBuilder.Entity<Profile>()
                .Property(p => p.JSON)
                .HasColumnName("profile_json")
                .IsOptional();
            modelBuilder.Entity<Profile>()
                .Ignore(p => p.ProfileData);
            modelBuilder.Entity<Profile>()
                .Property(p => p.CreatedDate)
                .HasColumnName("profile_createddate")
                .IsRequired();
            modelBuilder.Entity<Profile>()
                .Property(p => p.ModifiedDate)
                .HasColumnName("profile_modifieddate")
                .IsOptional();
            modelBuilder.Entity<Profile>()
                .Property(p => p.DeletedDate)
                .HasColumnName("profile_deleteddate")
                .IsOptional();
            modelBuilder.Entity<Profile>()
                .HasRequired(p => p.User)/*.WithRequiredPrincipal().Map(m => m.MapKey("UserId"))*/;
            modelBuilder.Entity<Profile>().ToTable("profiles");

            //ROLE
            modelBuilder.Entity<Role>()
                .HasMany(a => a.Users)
                .WithMany(c => c.Roles)
                .Map(m =>
                {
                    m.MapLeftKey("role_id");
                    m.MapRightKey("user_id");
                    m.ToTable("users_has_roles");
                });

            //INHERITANCE FROM ITEM
            modelBuilder.Entity<Item>()
                .Map<Article>(m =>
                {
                    m.ToTable("articles");
                })
                .Map<Category>(m =>
                {
                    m.ToTable("categories");
                })
                .Map<Comment>(m =>
                {
                    m.ToTable("comments");
                })
                .Map<Role>(m =>
                {
                    m.ToTable("roles");
                });
            
        }
    }
}
