using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TaskManagementProject.data;
using TaskManagementProject.Models;

namespace TaskManagementProject.Controllers
{
    public class TaskController:Controller
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasks.ToListAsync());
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskManagmentModel task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        public async Task<IActionResult> EditTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(int id, TaskManagmentModel task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ViewTask(int id)
        {
            var task = _context.Tasks.Find(id); 

            if (task == null)
            {
                return NotFound();
            }

            return View(task); 
        }
    }
}

