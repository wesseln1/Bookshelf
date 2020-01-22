﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookShelf.Data;
using BookShelf.Models;
using Microsoft.AspNetCore.Identity;
using BookShelf.Models.ViewModel;

namespace BookShelf.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BooksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var books = await _context.Book
                .Include(b => b.Author)
                .Where(b => b.ApplicationUserId == user.Id)
           .Select(b => new BookCommentsViewModel
           {
               Id = b.Id,
               Title = b.Title,
               Genre = b.Genre,
               CommentCount = b.Comments.Count(),
               YearPublished = b.YearPublished,
               Rating = b.Rating,
               AuthorId = b.AuthorId,
               Author = b.Author
           }).ToListAsync();

            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            var authors = _context.Author.Where(a => a.ApplicationUserId == user.Id);
            ViewData["AuthorId"] = new SelectList(authors, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,AuthorId,YearPublished,Rating,Genre")] Book book)
        {
            var user = await GetCurrentUserAsync();
            book.ApplicationUserId = user.Id;
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name", book.AuthorId);
            return View(book);
        }

        // GET: Comments/Create
        public async Task<IActionResult> CreateComment()
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment([FromRoute]int id, [Bind("Id,Text,BookId")] Comment comment)
        {
            var user = await GetCurrentUserAsync();
            comment.ApplicationUserId = user.Id;
            comment.BookId = id;
            comment.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await GetCurrentUserAsync();

            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author.Where(a => a.ApplicationUserId == user.Id), "Id", "Name", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,AuthorId,YearPublished,Rating,Genre")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();
            book.ApplicationUserId = user.Id;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name", book.AuthorId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.ApplicationUser)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
