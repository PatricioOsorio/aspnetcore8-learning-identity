using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TIdentity.Controllers
{
  public class SuperAdmin : Controller
  {
    // GET: SuperAdmin
    public ActionResult Index()
    {
      return View();
    }

    // GET: SuperAdmin/Details/5
    public ActionResult Details(int id)
    {
      return View();
    }

    // GET: SuperAdmin/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: SuperAdmin/Create
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

    // GET: SuperAdmin/Edit/5
    public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: SuperAdmin/Edit/5
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

    // GET: SuperAdmin/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: SuperAdmin/Delete/5
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
