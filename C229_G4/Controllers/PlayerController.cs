using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C229_G4.Models;
using C229_G4.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C229_G4.Controllers
{
    [Authorize]
    public class PlayerController : Controller
    {
        private IClubRepository repository;

        public PlayerController(IClubRepository repo)
        {
            repository = repo;
        }

        public ViewResult ListPlayers(string sortOrder, string searchPlayerName)
        {
            ViewData["PlayerIdSort"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["PlayerNameSort"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["PlayerAgeSort"] = sortOrder == "age" ? "age_desc" : "age";
            ViewData["PlayerCountrySort"] = sortOrder == "country" ? "country_desc" : "country";
            ViewData["PlayerClubIdSort"] = sortOrder == "clubid" ? "clubid_desc" : "clubid";
            ViewData["SearchFilter"] = searchPlayerName;

            var players = repository.Players;
            if (!String.IsNullOrEmpty(searchPlayerName))
            {
                players = players.Where(p => p.PlayerName.Contains(searchPlayerName));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    players = players.OrderByDescending(p => p.PlayerId);
                    break;
                case "name":
                    players = players.OrderBy(p => p.PlayerName);
                    break;
                case "name_desc":
                    players = players.OrderByDescending(p => p.PlayerName);
                    break;
                case "age":
                    players = players.OrderBy(p => p.Age);
                    break;
                case "age_desc":
                    players = players.OrderByDescending(p => p.Age);
                    break;
                case "country":
                    players = players.OrderBy(p => p.Country);
                    break;
                case "country_desc":
                    players = players.OrderByDescending(p => p.Country);
                    break;
                case "clubid":
                    players = players.OrderBy(p => p.ClubId);
                    break;
                case "clubid_desc":
                    players = players.OrderByDescending(p => p.ClubId);
                    break;
                default:
                    players = players.OrderBy(p => p.PlayerId);
                    break;
            }
            return View(players);
        }
        
        [HttpGet]
        public ViewResult EditPlayer(int playerId)
        {
            return View("ManagePlayers", new PlayersClubsViewModel() { Player = repository.Players.FirstOrDefault(p => p.PlayerId == playerId), Clubs = repository.Clubs, Players = repository.Players.Where(p => p.PlayerId == playerId) });
        }

        [HttpPost]
        public IActionResult DeletePlayer(int playerId)
        {
            Player deletedPlayer = repository.DeletePlayer(playerId);
            if (deletedPlayer != null)
            {
                TempData["message"] = $"{deletedPlayer.PlayerName} was deleted successfully";
            }
            return RedirectToAction("ListPlayers");
        }

        [HttpGet]
        public ViewResult ManagePlayers(int clubId)
        {
            return View(new PlayersClubsViewModel() { Player = new Player { ClubId = clubId }, Clubs = repository.Clubs, Players = repository.Players });
        }

        [HttpPost]
        public IActionResult AddPlayer(Player player)
        {
            if (ModelState.IsValid)
            {
                if (player.PlayerId == 0)
                {
                    TempData["message"] = $"{player.PlayerName} was added successfully";
                }
                else
                {
                    TempData["message"] = $"{player.PlayerName} was updated successfully";
                }
                repository.SavePlayer(player);
                ModelState.Clear();
                return RedirectToAction("ListPlayers");
            }
            else
            {
                if(player.PlayerId == 0)
                {
                    return View("ManagePlayers", new PlayersClubsViewModel() { Player = new Player { ClubId = 0}, Clubs = repository.Clubs, Players = repository.Players });
                }
                else
                {
                    return View("ManagePlayers", new PlayersClubsViewModel() { Player = repository.Players.FirstOrDefault(p => p.PlayerId == player.PlayerId), Clubs = repository.Clubs, Players = repository.Players.Where(p => p.PlayerId == player.PlayerId) });
                }
            }

        }

        [HttpPost]
        public IActionResult ReassignPlayer(int playerId, int clubId)
        {
            var player = repository.Players.FirstOrDefault(p => p.PlayerId == playerId);
            player.ClubId = clubId;
            repository.SavePlayer(player);
            return RedirectToAction("ListPlayers");
        }

        [HttpPost]
        public IActionResult DeregisterPlayer(int playerId)
        {
            var player = repository.Players.FirstOrDefault(p => p.PlayerId == playerId);
            player.ClubId = null;
            repository.SavePlayer(player);
            return RedirectToAction("ListPlayers");
        }
    }
}