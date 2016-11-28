using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using aspQuoting.Models;
using DapperApp.Factory;

namespace aspQuoting.Controllers
{
    public class QuoteController : Controller
    {
        private readonly QuoteRepository quoteFactory;

        public QuoteController()
        {
            //Instantiate a UserFactory object that is immutable (READONLY)
            //This is establish the initial DB connection for us.
            quoteFactory = new QuoteRepository();
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(TempData["errors"] != null)
            {
                ViewBag.errors = TempData["errors"];
            }
            return View("Quote");
        }
        [HttpPost]
        [Route("quotes")]
        public IActionResult Create(Quote newquote)
        {   
            if(ModelState.IsValid)
            {
                 quoteFactory.Add(newquote);
                 return RedirectToAction("DisplayQuotes");
            }
            List<string> temp_errors = new List<string>();
            foreach(var error in ModelState.Values)
            {
                if(error.Errors.Count > 0)
                {
                    temp_errors.Add(error.Errors[0].ErrorMessage);
                }
            }
            TempData["errors"] = temp_errors;
            Console.WriteLine(temp_errors);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("quotes")]
        public IActionResult DisplayQuotes()
        {
            ViewBag.quotes = quoteFactory.FindAll();
            return View("Display");
        }
        [HttpPost]
        [Route("like")]
        public IActionResult Like(int id)
        {
            Quote current = quoteFactory.FindByID(id);
            current.likes += 1;
            quoteFactory.update(current);
            return RedirectToAction("DisplayQuotes");
        }
    }
}

