using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using Lab4.Models;

namespace Lab4.ServiceLayer
{
    // Unit of work class

    public class Worker : IDisposable
    {
        private ApplicationDbContext _ds = new ApplicationDbContext();
        private bool disposed = false;

        // Properties for each repository...
        // (use the "propf" code snippet)
        // Custom getters only, for each repository

        private Artist_repo _artist;

        public Artist_repo Artists
        {
            get
            {
                if (_artist == null)
                {
                    _artist = new Artist_repo(_ds);
                }
                return _artist;
            }
        }

        // Marie - the Album_repo property was added

        private Album_repo _album;

        public Album_repo Albums
        {
            get
            {
                if (_album == null)
                {
                    _album = new Album_repo(_ds);
                }
                return _album;
            }
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _ds.Dispose();
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
