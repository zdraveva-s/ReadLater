using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
namespace ReadLater5.Controllers
{
    public class BookmarkController : Controller
    {
        private readonly IBookmarkService _bookmarkService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<IdentityUser> _userManager;

        public BookmarkController(IBookmarkService bookmarkService, ICategoryService categoryService, UserManager<IdentityUser> userManager)
        {
            _bookmarkService = bookmarkService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        // GET: Bookmarks
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var bookmarks = _bookmarkService.GetBookmarks(userId);
            return View(bookmarks);
        }

        // GET: Bookmarks/Create
        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(User);
            ViewBag.Categories = _categoryService.GetCategories(userId);
            return View();
        }

        // POST: Bookmarks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bookmark bookmark)
        {
            var userId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                bookmark.UserId = userId;
                bookmark.CreateDate = DateTime.Now;
                _bookmarkService.CreateBookmark(bookmark);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryService.GetCategories(userId);
            return View(bookmark);
        }

        // GET: Bookmarks/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            ViewBag.Categories = _categoryService.GetCategories(userId);
            return View(bookmark);
        }

        // POST: Bookmarks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bookmark bookmark)
        {
            var userId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                bookmark.UserId = userId;
                bookmark.CreateDate = DateTime.Now;
                _bookmarkService.UpdateBookmark(bookmark);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _categoryService.GetCategories(userId);
            return View(bookmark);
        }

        // GET: Bookmarks/Details/5
        public IActionResult Details(int? id)
        {
            var userId = _userManager.GetUserId(User);
            if (id == null) return NotFound();

            var bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null || bookmark.UserId != userId) return NotFound();

            return View(bookmark);
        }

        // GET: Bookmarks/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null) return NotFound();

            return View(bookmark);
        }

        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var bookmark = _bookmarkService.GetBookmark(id);
            _bookmarkService.DeleteBookmark(bookmark);
            return RedirectToAction(nameof(Index));
        }
    }
}
