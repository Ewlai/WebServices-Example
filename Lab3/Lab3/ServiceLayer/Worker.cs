﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using Lab3.Models;

namespace Lab3.ServiceLayer
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
                if (this._artist == null)
                {
                    this._artist = new Artist_repo(_ds);
                }
                return this._artist;
            }
        }

        private Album_repo _album;

        public Album_repo Albums
        {
            get
            {
                if (this._album == null)
                {
                    this._album = new Album_repo(_ds);
                }
                return this._album;
            }
        }

        private Song_repo _song;

        public Song_repo Songs
        {
            get
            {
                if (this._song == null)
                {
                    this._song = new Song_repo(_ds);
                }
                return this._song;
            }
        }

        // Other custom getters go here...

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
