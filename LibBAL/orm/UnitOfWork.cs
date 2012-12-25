using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibModels;

namespace LibBAL.orm
{
    public class UnitOfWork : IDisposable
    {
        private SocialGEOContext context = new SocialGEOContext();
        private GenericRepository<Article> _articleRepository;
        private GenericRepository<Category> _categoryRepository;
        private GenericRepository<Comment> _commentRepository;
        private GenericRepository<User> _userRepository;
        private GenericRepository<Profile> _profileRepository;
        private GenericRepository<Role> _roleRepository;

        public GenericRepository<Article> ArticleRepository
        {
            get
            {

                if (this._articleRepository == null)
                {
                    this._articleRepository = new GenericRepository<Article>(context);
                }
                return _articleRepository;
            }
        }

        public GenericRepository<Category> CategoryRepository
        {
            get
            {

                if (this._categoryRepository == null)
                {
                    this._categoryRepository = new GenericRepository<Category>(context);
                }
                return _categoryRepository;
            }
        }

        public GenericRepository<Comment> CommentRepository
        {
            get
            {

                if (this._commentRepository == null)
                {
                    this._commentRepository = new GenericRepository<Comment>(context);
                }
                return _commentRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this._userRepository == null)
                {
                    this._userRepository = new GenericRepository<User>(context);
                }
                return _userRepository;
            }
        }

        public GenericRepository<Profile> ProfileRepository
        {
            get
            {

                if (this._profileRepository == null)
                {
                    this._profileRepository = new GenericRepository<Profile>(context);
                }
                return _profileRepository;
            } 
        }

        public GenericRepository<Role> RoleRepository
        {
            get
            {

                if (this._roleRepository == null)
                {
                    this._roleRepository = new GenericRepository<Role>(context);
                }
                return _roleRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
