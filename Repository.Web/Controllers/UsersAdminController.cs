using Repository.DataLayer.Context;
using Repository.DomainModel.AppUser;
using Repository.ServiceLayer.Contracts;
using Repository.ViewModel.AppUser;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Repository.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersAdminController : Controller
    {
        private readonly IApplicationRoleManager _roleManager;
        private readonly IApplicationUserManager _userManager;
        public UsersAdminController(IApplicationUserManager userManager,
                                    IApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await _roleManager.GetAllCustomRolesAsync(), "Name", "Name");
            RegisterViewModel register = new RegisterViewModel();
           
            return PartialView(viewName:"Create",model:register);
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = userViewModel.UserName, Email = userViewModel.Email };
                var adminresult = await _userManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await _userManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await _roleManager.GetAllCustomRolesAsync(), "Name", "Name");
                            return Json(new { success = true });
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(await _roleManager.GetAllCustomRolesAsync(), "Name", "Name");
                    return Json(new { success = true });

                }
                return PartialView(viewName:"Create",model:userViewModel);
            }
            ViewBag.RoleId = new SelectList(await _roleManager.GetAllCustomRolesAsync(), "Name", "Name");
            return PartialView(viewName:"Create",model:userViewModel);
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _userManager.FindByIdAsync(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Delete",model:user);
        }

        //
        // POST: /Users/Delete/5
        //[HttpDelete, ActionName("Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await _userManager.FindByIdAsync(id.Value);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return PartialView(viewName:"Delete",model:user);
                }
                return Json(new { success = true });
            }
            return PartialView(viewName:"Delete",model:User);
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _userManager.FindByIdAsync(id.Value);
           

            ViewBag.RoleNames = await _userManager.GetRolesAsync(user.Id);

            return PartialView(viewName:"Details",model:user);
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _userManager.FindByIdAsync(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user.Id);

            return PartialView(viewName:"Edit",model:new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                RolesList = (await _roleManager.GetAllCustomRolesAsync()).Select(x => new SelectListItem
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserName,Id")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.UserName;
                //user.Email = editUser.Email;

                var userRoles = await _userManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await _userManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await _userManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return Json(new { success = true });
            }
            ModelState.AddModelError("", "خطا");
            return PartialView(viewName:"Edit",model:editUser);
        }

        //
        // GET: /Users/
        public  ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user1 = db.Users.ToList();
            var u = _userManager.GetAllUsersAsync();
            //var user = await _userManager.GetAllUsersAsync();
           
            return View(user1);
        }


        public async Task<ActionResult> AdminUsers()
        {
            var user = await _userManager.GetAllUsersAsync();
            return Json(user, JsonRequestBehavior.AllowGet);
            //return View(_roleManager.GetApplicationUsersInRole("Admin"));

        }

        [HttpGet]
        public ActionResult getRole()
        {

            var user = _userManager.GetAllUsersAsync();
            IList<EditUserViewModel> ud = (IList<EditUserViewModel>)_userManager.GetAllUsersAsync();
            
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}