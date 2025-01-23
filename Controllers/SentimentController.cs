using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PrompSentiments.Controllers
{
    public class SentimentController : Controller
    {
        // GET: SentimentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SentimentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SentimentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SentimentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SentimentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SentimentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SentimentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SentimentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
