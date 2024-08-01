using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample.Data;
using Sample.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Sample.Repository;

namespace Sample.Controllers
{
    public class MpitController : Controller
    {
        private readonly IRepository _repo;

        public MpitController(IRepository context)
        {
            _repo = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Register register)
        {
            var employer = await _repo.LoginAsync(register);
            if (employer != null)
            {
                HttpContext.Session.SetString("UserEmail", employer.Email);
                return RedirectToAction("Mpitregister");
            }

            ViewBag.Error = "Invalid email or password";
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Mpitregister()
        {
            var users = await _repo.GetAllUsers();
            return View(users);
        }

        [HttpPost]
        public IActionResult Mpitregister(Register register)
        {

            _repo.AddUser(register);

            return RedirectToAction(nameof(Mpitregister));
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _repo.GetUserById(id);
            if (employee == null)
            {
                return NotFound(); // Handle case where employee is not found
            }
            return View(employee);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Register employee)
        {
            _repo.Update(employee);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Mpitregister");          
         }

        public IActionResult Delete(int id)
        {
            _repo.DeleteUser(id); 
            return RedirectToAction("Mpitregister");
        }
    }
}

