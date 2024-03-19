using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class RanksController : Controller
    {
        private readonly PomeloDB _context;

        public RanksController(PomeloDB context)
        {
       
            _context = context;
        }

        public async Task<IActionResult> Back()
        {
            return Redirect("http://localhost:3000/Login");
        }

        // GET: Ranks
        public async Task<IActionResult> Index()
        {
              return _context.Rank != null ? 
                          View(await _context.Rank.ToListAsync()) :
                          Problem("Entity set 'MVCContext.Rank'  is null.");
        }


        public async Task<IActionResult> Search()
        {
            return _context.Rank != null ?
                        View(await _context.Rank.ToListAsync()) :
                        Problem("Entity set 'MVCContext.Rank'  is null.");
        }
        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction(nameof(Index));
            }
            var q = from rank in _context.Rank
                    where rank.UserName.StartsWith(query)             
                    select rank;
            return View(await q.ToListAsync());
                       
        }
 
      
        // GET: Ranks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Rank == null)
            {
                return NotFound();
            }

            var rank = await _context.Rank
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (rank == null)
            {
                return NotFound();
            }

            return View(rank);
        }

        // GET: Ranks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ranks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,RankNum,Details")] Rank rank)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Rank.Where(x => x.UserName == rank.UserName).FirstOrDefault();
                    rank.Created = DateTime.Now;
                var exist = _context.User.Where(x => x.UserName == rank.UserName).FirstOrDefault();

                if (user != null)
                {
                    TempData["AlreadyRank"] = "Already rank try edit button on this user";
                    //error => user already exist
                    return RedirectToAction(nameof(Index));
                }
                if (exist != null)
                {
                    _context.Add(rank);

                }
                else
                {
                    TempData["AlreadyRank"] = "User not exists in system";
                    //error => user already exist
                    return RedirectToAction(nameof(Index));
                }

                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rank);
        }

        // GET: Ranks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Rank == null)
            {
                return NotFound();
            }

            var rank = await _context.Rank.FindAsync(id);
            if (rank == null)
            {
                return NotFound();
            }
            return View(rank);
        }

        // POST: Ranks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserName,RankNum,Details")] Rank rank)
        {
            if (id != rank.UserName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    rank.Created = DateTime.Now;
                    _context.Update(rank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankExists(rank.UserName))
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
            return View(rank);
        }

        // GET: Ranks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Rank == null)
            {
                return NotFound();
            }

            var rank = await _context.Rank
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (rank == null)
            {
                return NotFound();
            }

            return View(rank);
        }

        // POST: Ranks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Rank == null)
            {
                return Problem("Entity set 'MVCContext.Rank'  is null.");
            }
            var rank = await _context.Rank.FindAsync(id);
            if (rank != null)
            {
                _context.Rank.Remove(rank);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankExists(string id)
        {
          return (_context.Rank?.Any(e => e.UserName == id)).GetValueOrDefault();
        }
    }
}
