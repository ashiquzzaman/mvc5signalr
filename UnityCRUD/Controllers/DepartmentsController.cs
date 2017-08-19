using System.Net;
using System.Web.Mvc;
using SignalRMVCUnityCRUD.Hubs;
using SignalRMVCUnityCRUD.Managers;
using SignalRMVCUnityCRUD.Models;

namespace SignalRMVCUnityCRUD.Controllers
{
    public class DepartmentsController : Controller
    {
        private IDepertmentManager _depertment;

        public DepartmentsController(IDepertmentManager depertment)
        {
            _depertment = depertment;
        }

        // GET: Departments
        public ActionResult Index()
        {
            var model = _depertment.GetAll();
            return View(model);
        }
        public ActionResult List()
        {
            var model = _depertment.GetAll();
            return PartialView(model);
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _depertment.Get(id ?? 0);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                _depertment.Create(department);
                EmployeeHub.BroadcastData();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _depertment.Get(id ?? 0);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                _depertment.Update(department);
                EmployeeHub.BroadcastData();

                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _depertment.Get(id ?? 0);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _depertment.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
