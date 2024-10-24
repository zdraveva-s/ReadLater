using Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;

namespace ReadLater5.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarksApiController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;
        private readonly UserManager<IdentityUser> _userManager;

        public BookmarksApiController(IBookmarkService bookmarkService, UserManager<IdentityUser> userManager)
        {
            _bookmarkService = bookmarkService;
            _userManager = userManager;
        }

        // GET: api/BookmarksApi
        [HttpGet]
        public ActionResult<IEnumerable<Bookmark>> GetBookmarks()
        {
            var userId = _userManager.GetUserId(User);
            return Ok(_bookmarkService.GetBookmarks(userId));
        }

        // GET: api/BookmarksApi/5
        [HttpGet("{id}")]
        public ActionResult<Bookmark> GetBookmark(int id)
        {
            var userId = _userManager.GetUserId(User);
            var bookmark = _bookmarkService.GetBookmark(id);
            if (bookmark == null || bookmark.UserId != userId) return NotFound();

            return Ok(bookmark);
        }

        // POST: api/BookmarksApi
        [HttpPost]
        public ActionResult<Bookmark> CreateBookmark([FromBody] Bookmark bookmark)
        {
            var userId = _userManager.GetUserId(User);
            bookmark.UserId = userId;
            _bookmarkService.CreateBookmark(bookmark);

            return CreatedAtAction(nameof(GetBookmark), new { id = bookmark.ID }, bookmark);
        }

        // PUT: api/BookmarksApi/5
        [HttpPut("{id}")]
        public IActionResult UpdateBookmark(int id, [FromBody] Bookmark bookmark)
        {
            var userId = _userManager.GetUserId(User);
            bookmark.UserId = userId;
            if (id != bookmark.ID) return BadRequest();

            _bookmarkService.UpdateBookmark(bookmark);
            return NoContent();
        }

        // DELETE: api/BookmarksApi/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBookmark(int id)
        {
            var userId = _userManager.GetUserId(User);
            var bookmark = _bookmarkService.GetBookmark(id);
            if (bookmark == null || bookmark.UserId != userId) return NotFound();

            _bookmarkService.DeleteBookmark(bookmark);
            return NoContent();
        }
    }
}
