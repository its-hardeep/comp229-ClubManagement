using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C229_G4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C229_G4.Controllers
{
    [Authorize(Roles = "R_Admin")]
    public class ClubController : Controller
    {
        private IClubRepository repository;

        public ClubController(IClubRepository repo)
        {
            repository = repo;
        }

        [AllowAnonymous]
        public ViewResult ListClubs(string sortOrder, string searchClubName)
        {
            ViewData["ClubIdSort"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["ClubNameSort"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["SearchFilter"] = searchClubName;

            var clubs = repository.Clubs;
            if (!String.IsNullOrEmpty(searchClubName))
            {
                clubs = clubs.Where(c => c.ClubName.Contains(searchClubName));
            }
            
            switch (sortOrder)
            {
                case "id_desc":
                    clubs = clubs.OrderByDescending(c => c.ClubId);
                    break;
                case "name":
                    clubs = clubs.OrderBy(c => c.ClubName);
                    break;
                case "name_desc":
                    clubs = clubs.OrderByDescending(c => c.ClubName);
                    break;
                default:
                    clubs = clubs.OrderBy(c => c.ClubId);
                    break;
            }
            return View(clubs);
        }

        [AllowAnonymous]
        public ViewResult ClubDetails(int clubId)
        {
            var result = repository.Clubs.FirstOrDefault(c => c.ClubId == clubId);
            return View(result ?? new Club());
        }
        
        public ViewResult AddClub()
        {
            return View("AddClub", new Club());
        }
        
        [HttpGet]
        public ViewResult EditClub(int clubId)
        {
            return View("AddClub", repository.Clubs.FirstOrDefault(c => c.ClubId == clubId));
        }
        
        [HttpPost]
        public IActionResult SaveClub(Club aClub)
        {
            if (ModelState.IsValid)
            {
                if(aClub.ClubId == 0)
                {
                    TempData["message"] = $"{aClub.ClubName} was added successfully";
                }
                else
                {
                    TempData["message"] = $"{aClub.ClubName} was updated successfully";
                }
                repository.SaveClub(aClub);
                ModelState.Clear();
                return RedirectToAction("ListClubs");
            }
            else
            {
                return View("AddClub", aClub);
            }
            
        }
        
        [HttpPost]
        public IActionResult DeleteClub(int clubId)
        {
            Club deletedClub = repository.DeleteClub(clubId);
            if (deletedClub != null)
            {
                TempData["message"] = $"{deletedClub.ClubName} was deleted successfully";
            }
            return RedirectToAction("ListClubs");
        }
        
    }
}