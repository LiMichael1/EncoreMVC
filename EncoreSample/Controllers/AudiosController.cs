using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EncoreSample.Data;
using EncoreSample.Models;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net;
using System.Security.Policy;
using System.Reflection.Metadata;

namespace EncoreSample.Controllers
{
    [Authorize]
    public class AudiosController : Controller
    {
        private readonly ApplicationDbContext _context;


        public AudiosController(ApplicationDbContext context)
        {
            _context = context;

        }

        [AllowAnonymous]
        // GET: Audios
        public async Task<IActionResult> Index()
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Vote> votes = await _context.Votes.Where(x => x.UserId.Equals(userId)).ToListAsync();

            List<Audio> audios = await _context.Audios
                                        .Include(x => x.User)
                                        .OrderByDescending(x => x.NumberOfLikes)
                                        .ToListAsync();

            ViewData["votes"] = votes;

            return View(audios);
        }



        // GET: Audios/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Audios == null)
            {
                return NotFound();
            }

            var audio = await _context.Audios
                .FirstOrDefaultAsync(m => m.AudioId == id);

            if (audio == null)
            {
                return NotFound();
            }

            List<Comment> comments = await _context.Comments.Where(x => x.AudioId == audio.AudioId).Include(x => x.User).ToListAsync();

            audio.Comments = comments;

            return View(audio);
        }

        // GET: Audios/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult CreateModal()
        {
            Audio audio = new Audio();
            return PartialView("_CreateAudio", audio);
        }

        [AllowAnonymous]
        [HttpPost("Audio/Upvote")]
        public async Task<IActionResult> Upvote(int audioId, string userId)
        {
            Audio? audio = await _context.Audios.FindAsync(audioId);

            Vote? find = await _context.Votes.FirstOrDefaultAsync(x => x.AudioId == audioId && x.UserId == userId);

            if (find == null)
            {
                Vote vote = new Vote();
                vote.UserId = userId;
                vote.AudioId = audioId;
                vote.VoteType = 1;

                _context.Add(vote);
                audio.NumberOfLikes++;
            } else
            {
                switch(find.VoteType)
                {
                    case 1:
                        find.VoteType = 0;
                        audio.NumberOfLikes--;
                        break;
                    case 0:
                        find.VoteType = 1;
                        audio.NumberOfLikes++;
                        break;

                    default:
                        break;
                }
                _context.Update(find);
            }


            _context.Update(audio);

            await _context.SaveChangesAsync();

            return Ok(audio.NumberOfLikes);

        }

        // POST: Audios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Audio audio, IFormFile? upload)
        {
            if (ModelState.IsValid)
            {
                string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                audio.UserId = userId;

                if (upload != null)
                {
                    string fileName = Path.GetFileName(upload.FileName);
                    string filePath = Path.Combine(@"wwwroot\audios", fileName);

                    audio.AudioPath = Path.Combine(@"\audios", fileName);


                    _context.Add(audio);
                    await _context.SaveChangesAsync();

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }

                    return RedirectToAction(nameof(Index));
                } 
            }
			return View(audio);
        }

        // GET: Audios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Audios == null)
            {
                return NotFound();
            }

            var audio = await _context.Audios.FindAsync(id);
            if (audio == null)
            {
                return NotFound();
            }
            return View(audio);
        }

        // POST: Audios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AudioId,UserId,Song,OriginalArtist,SubmittedOn,AudioPath,SongCoverLink")] Audio audio)
        {
            if (id != audio.AudioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AudioExists(audio.AudioId))
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
            return View(audio);
        }

        // GET: Audios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Audios == null)
            {
                return NotFound();
            }

            var audio = await _context.Audios
                .FirstOrDefaultAsync(m => m.AudioId == id);
            if (audio == null)
            {
                return NotFound();
            }

            return View(audio);
        }

        // POST: Audios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Audios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Audios'  is null.");
            }
            var audio = await _context.Audios.FindAsync(id);
            if (audio != null)
            {
                _context.Audios.Remove(audio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AudioExists(int id)
        {
          return _context.Audios.Any(e => e.AudioId == id);
        }
    }

    public static class GeneralExtensions
    {
        public static string GetValueForJS(this bool argValue)
        {
            return argValue ? "true" : "false";
        }
    }
}
