using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Data;
using Proyecto.models.Models;
using Proyecto.roles;

namespace Proyecto.Areas.Admin.Controllers
{

    [Area("Admin"),Authorize(Roles =Roles.Admin)]
  
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }




        #region
        [HttpGet]
        public IActionResult GetAll() {
            List<ApplicationUser> users = _db.applicationsUser.ToList();
            var userRole=_db.UserRoles.ToList();
            var roles=_db.Roles.ToList();
            foreach (var user in users) {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.role = roles.FirstOrDefault(u => u.Id == roleId).Name;

                    }
            return Json(new { data = users });
        }
        [HttpPost]
        public  IActionResult LockUnLock([FromBody]string id)
        {
            var objectDb = _db.applicationsUser.FirstOrDefault(u => u.Id == id);
            if (objectDb == null)
            {
                return Json(new { success = false });

            }
            if (objectDb.LockoutEnd != null && objectDb.LockoutEnd > DateTime.Now)
            {
                //unlock user
                objectDb.LockoutEnd = DateTime.Now;
            }
            else {

                objectDb.LockoutEnd = DateTime.Now.AddYears(5);
            }
            _db.SaveChanges();
            return Json(new{ success=true, message="Action Complited" });
        }
        #endregion
    }
}
