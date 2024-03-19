using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using API.Data;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using API.Data;
namespace API.Controllers
{
    [ApiController]
    [Route("api/invitations")]
    public class InvitationController : Controller
    {
        private readonly PomeloDB _context;
        private readonly IHubContext<MyHub> myHub;
        
        public InvitationController(PomeloDB context, IHubContext<MyHub> myHub)
        {
            _context = context;
            this.myHub = myHub;
        }
        [HttpPost]
        [Route("/api/invitations")]
        public async Task<IActionResult> GetInvitation([Bind("From,To,Server")] ContactRequest req)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new Random();

                Contact c = new Contact()
                {
                    ContactName = req.From,
                    Last = null,
                    LastDate = null,
                    Server = req.Server,
                    UserName = req.To
                };
                await _context.Contact.AddAsync(c);
                await myHub.Clients.All.SendAsync("NewContact", new ContactInfo(req.From, req.Server));
                return Ok();
        }
          return BadRequest();
       }

        // GET: Invitation
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Contact != null ? 
        //                  View(await _context.Contact.ToListAsync()) :
        //                  Problem("Entity set 'PomeloDB.Contact'  is null.");
        //}

        //// GET: Invitation/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _context.Contact == null)
        //    {
        //        return NotFound();
        //    }

        //    var contact = await _context.Contact
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(contact);
        //}

        //// GET: Invitation/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Invitation/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,NickName,Server,Last,LastDate")] Contact contact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(contact);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contact);
        //}

        //// GET: Invitation/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null || _context.Contact == null)
        //    {
        //        return NotFound();
        //    }

        //    var contact = await _context.Contact.FindAsync(id);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(contact);
        //}

        //// POST: Invitation/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Id,NickName,Server,Last,LastDate")] Contact contact)
        //{
        //    if (id != contact.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(contact);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ContactExists(contact.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contact);
        //}

        //// GET: Invitation/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null || _context.Contact == null)
        //    {
        //        return NotFound();
        //    }

        //    var contact = await _context.Contact
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(contact);
        //}

        //// POST: Invitation/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    if (_context.Contact == null)
        //    {
        //        return Problem("Entity set 'PomeloDB.Contact'  is null.");
        //    }
        //    var contact = await _context.Contact.FindAsync(id);
        //    if (contact != null)
        //    {
        //        _context.Contact.Remove(contact);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ContactExists(string id)
        //{
        //  return (_context.Contact?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
