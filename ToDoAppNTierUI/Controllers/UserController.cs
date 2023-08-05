using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoAppNTierBLL.Interfaces;
using ToDoAppNTierDataAccess.UnitOfWork;
using ToDoAppNTierDTO;
using ToDoAppNTierEntities.Domains;
using ToDoAppNTierUI.Extensions;
using ToDoAppNTierUI.Manager;

namespace ToDoAppNTierUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            var response = await _userService.Register(model);
            return this.ResponseRedirectToAction(response, "Login");
        }

        public IActionResult Login(string path)
        {
            ViewBag.Path = path;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDto model, string path)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork.GetRepository<User>().GetByFilter(x => x.UserName.Equals(model.UserName) && x.Password.Equals(model.Password));
                if (user is not null)
                {
                    HttpContext.Session.SetString("userId", user.Id.ToString());
                    HttpContext.Session.SetString("userName", user.UserName);
                    if (string.IsNullOrEmpty(path))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(path);
                    }

                }
                else
                {
                    ModelState.AddModelError("", "No such a user found");
                    return View();
                }
            }
            return View();

        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }

        public async Task<IActionResult> Profile()
        {
            string userName = HttpContext.Session.GetString("userName");
            var user = await _unitOfWork.GetRepository<User>().GetByFilter(x => x.UserName == userName);
            if (user is not null)
            {
                UserListDto dto = _mapper.Map<UserListDto>(user);
                return View(dto);
            }
            else
            {
                return NotFound();
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _userService.GetByIdAsync<UserEditDto>(id);
            return this.ResponseView(response);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditDto model)
        {
            var response = await _userService.ProfileEdit(model);
            return this.ResponseRedirectToAction(response, "Profile");
        }

        public async Task<IActionResult> PhotoEdit(int id)
        {
            var response = await _userService.GetByIdAsync<UserEditDto>(id);
            return this.ResponseView(response);
        }

        [HttpPost]
        public async Task<IActionResult> PhotoEdit(UserEditDto model)
        {
            string userName = HttpContext.Session.GetString("userName");
            User user = _mapper.Map<User>(model);
            string image = model.UploadImage.GetUniqueNameAndSavePhotoToDisk(model.Image, _webHostEnvironment);
            if (image != null)
            {
                user.Image = image;
            }
            
       
            User updatedUser = await _unitOfWork.GetRepository<User>().Find(model.Id);
            if (updatedUser is not null)
            {
               _unitOfWork.GetRepository<User>().Update(user, updatedUser);
                await _unitOfWork.SaveChanges();
                TempData["message"] = "Your profile photo has successfully updated!";
                //return Redirect($"/User/Profile?userName={userName}");
                return RedirectToAction("Profile");
            }
            else
            {
                return NotFound();
            }

        }
        






    }
}
