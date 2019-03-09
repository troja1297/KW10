using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly FileUploadService _fileUploadService;
        private IHostingEnvironment _environment;
        private UserManager<IdentityUser> _userManager;
        private const int pageSize = 3;
        public CompaniesController(
            ApplicationDbContext context,
            IHostingEnvironment environment,
            FileUploadService fileUploadService,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _environment = environment;
            _fileUploadService = fileUploadService;
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index(int page = 1)
        {
            List<DetailsCompanyViewModel> vm = new List<DetailsCompanyViewModel>();

            var applicationDbContext = _context.Companies;
            
            foreach (Company company in applicationDbContext)
            {
                var pictures = _context.Pictures
                    .Where(o => o.CompanyId == company.Id)
                    .Where(o => o.CommentId == null).ToList();
                var rating = _context.Comments.Where(c => c.CompanyId == company.Id)
                    .Select(c => c.Rating);
                vm.Add(new DetailsCompanyViewModel()
                {
                    Description = company.Descriprion,
                    PhotoPath = company.PhotoPath,
                    Title = company.Title,
                    CommentCount = company.CommentCount,
                    Id = company.Id,
                    PicturesCount = pictures.Count,
                    Rating = (double) rating.Sum() / rating.Count()
                });
            }
            IPagedList<DetailsCompanyViewModel> institutions = vm.ToPagedList(page, pageSize);
            return View(institutions);
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var company = await _context.Companies.FirstOrDefaultAsync(m => m.Id == id);


            if (company == null)
            {
                return NotFound();
            }
            
            var pictures = _context.Pictures
                .Where(c => c.CompanyId == id).ToList();

            var comments = _context.Comments
                .Where(c => c.CompanyId == id).ToList();

            var rating = _context.Comments.Where(c => c.CompanyId == id)
                .Select(c => c.Rating);
            string userId = "";
            if (!User.Identity.IsAuthenticated)
            {
                userId = "empty";
            }
            else
            {
                userId = _userManager.GetUserAsync(User).Result.Id;
            }
           

            DetailsCompanyViewModel vm = new DetailsCompanyViewModel()
            {
                Description = company.Descriprion,
                PhotoPath = company.PhotoPath,
                Title = company.Title,
                CommentCount = company.CommentCount,
                Id = company.Id,
                PicturesCount = pictures.Count,
                Rating = (double) rating.Sum() / rating.Count(),
                Comments = comments,
                Pictures = pictures,
                UserId = userId,
            };
            ViewData["userId"] = userId;
            ViewData["companyId"] = id;
            return View(vm);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCompanyViewModel viewModel)
        {
            Company company = new Company();
            if (ModelState.IsValid)
            {
                string path = Path.Combine(
                    _environment.WebRootPath,
                    $"images\\{viewModel.Title}\\");
                string photo = $"/images/{viewModel.Title}/{viewModel.File.FileName}";
                _fileUploadService.Upload(path, viewModel.File.FileName, viewModel.File);

                company = new Company()
                {
                    CommentCount = 0,
                    PhotoPath = photo,
                    Descriprion = viewModel.Description,
                    Title = viewModel.Title,
                    PhotoCount = 1,
                };
                ViewData["UserId"] = new SelectList(_context.ApplicaionUsers, "Id", "Id");
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }


        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(CreateCommentViewModel viewModel)
        {
            Comment comment = new Comment();
            Picture picture = new Picture();
            if (ModelState.IsValid)
            {
                string path = Path.Combine(
                    _environment.WebRootPath,
                    $"images\\{viewModel.UserId}\\");
                string photo = $"/images/{viewModel.UserId}/{viewModel.File.FileName}";
                _fileUploadService.Upload(path, viewModel.File.FileName, viewModel.File);
                
                comment = new Comment()
                {
                    CompanyId = viewModel.CompanyId,
                    UserId = viewModel.UserId,
                    Body = viewModel.Body,
                    Date = DateTime.Now,
                    Rating = viewModel.Rating
                };
                var savedComment = _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                picture = new Picture()
                {
                    CommentId = savedComment.Entity.Id,
                    CompanyId = viewModel.CompanyId,
                    Path = photo,
                    UserId = viewModel.UserId
                };
                _context.Pictures.Add(picture);
                await _context.SaveChangesAsync();
                return Redirect($"Details/{comment.CompanyId}");
            }
            return PartialView("_CreateCommentPartial");
        }
        

        private bool CompanyExists(string id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
