using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly ReadLaterDataContext _dbContext;

        public BookmarkService(ReadLaterDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            _dbContext.Bookmark.Add(bookmark);
            _dbContext.SaveChanges();
            return bookmark;
        }

        public List<Bookmark> GetBookmarks(string userId)
        {
            return _dbContext.Bookmark.Include(b => b.Category).Where(b => b.UserId == userId).ToList();
        }

        public Bookmark GetBookmark(int id)
        {
            return _dbContext.Bookmark.Include(b => b.Category).FirstOrDefault(b => b.ID == id);
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            _dbContext.Bookmark.Update(bookmark);
            _dbContext.SaveChanges();
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            _dbContext.Bookmark.Remove(bookmark);
            _dbContext.SaveChanges();
        }
    }
}

   
