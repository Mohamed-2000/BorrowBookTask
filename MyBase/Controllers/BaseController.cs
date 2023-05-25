using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBase.Application.Exceptions;
using MyBase.Application.Infrastructure;
using MyBase.Application.Interfaces;
using MyBase.Common;
using System.Net;
using System.Runtime.InteropServices;

namespace MyBase.Web
{
    public abstract class BaseController<T> : Controller where T : BaseController<T>
    {
        //private IFileUploader _fileUploader;
        //protected IFileUploader FileUploader => _fileUploader ?? (_fileUploader = HttpContext.RequestServices.GetService<IFileUploader>());
        private IWebHostEnvironment _env;
        protected IWebHostEnvironment Env => _env ?? (_env = HttpContext?.RequestServices.GetService<IWebHostEnvironment>());
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        private Globals _Global;
        protected Globals Global => _Global ?? (_Global = HttpContext.RequestServices.GetService<Globals>());
        private ILogger _logger;
        protected ILogger Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger>());
        private IMyBaseContext _ctx;
        protected IMyBaseContext _context => _ctx ?? (_ctx = HttpContext?.RequestServices.GetService<IMyBaseContext>());
        private IRedisCache _cach;
        protected IRedisCache _cache => _cach ?? (_cach = HttpContext?.RequestServices.GetService<IRedisCache>());
        //private IHasPrmission _hasPermission;
        //protected IHasPrmission HasPrmission => _hasPermission ?? (_hasPermission = HttpContext?.RequestServices.GetService<IHasPrmission>());
        //private BaseManager _baseManager;
        //protected BaseManager BaseManager => _baseManager ?? (_baseManager = HttpContext?.RequestServices.GetService<BaseManager>());

        //public SiteSettings SiteSettings
        //{
        //    get
        //    {
        //        var Claims = HttpContext.User.Claims.ToList();
        //        SiteSettings siteSettings = JsonConvert.DeserializeObject<SiteSettings>(Claims.FirstOrDefault(x => x.Type == "SiteSettings").Value);
        //        return siteSettings;
        //    }
        //}

        //public string CurrentUserId
        //{
        //    get
        //    {
        //        try
        //        {
        //            var userId = User.Identities.First().Claims.First().Value;
        //            return userId;
        //        }
        //        catch //(Exception e)
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public APIClaims APIClaims
        //{
        //    get
        //    {
        //        try
        //        {
        //            return new APIClaims
        //            {
        //                Email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
        //                UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value,
        //                UserTypeIds = User.Claims.Where(x => x.Type == "TypeId").Select(x => Convert.ToInt32(x.Value)).ToList(),
        //                UserProfileIds = User.Claims.Where(x => x.Type == "ProfileId").Select(x => Convert.ToInt32(x.Value)).ToList()
        //            };
        //        }
        //        catch
        //        {
        //            return new APIClaims();
        //        }

        //    }
        //}

        //public StudentProfile CurrentStudent
        //{
        //    get
        //    {
        //        try
        //        {
        //            StudentProfile _currentStudent = new StudentProfile();
        //            List<StudentProfile> students = new List<StudentProfile>();
        //            var studentsValue = Request.Cookies.FirstOrDefault(a => a.Key == "Students").Value;
        //            if (!string.IsNullOrEmpty(studentsValue))
        //            {
        //                ParentStudent parentStudent = JsonConvert.DeserializeObject<ParentStudent>(studentsValue);
        //                if (parentStudent != null)
        //                {
        //                    students = parentStudent.Students;
        //                    _currentStudent = parentStudent.CurrentStudent == null ? new StudentProfile() : parentStudent.CurrentStudent;
        //                    return _currentStudent;
        //                }
        //                else
        //                    return new StudentProfile();
        //            }
        //            else
        //                return new StudentProfile();
        //        }
        //        catch (Exception)
        //        {
        //            return new StudentProfile();
        //        }
        //    }
        //}

        public async Task<ActionResult> Execute<TRequest>(IRequest<TRequest> request, string Msg)
        {
            try
            {
                if (request.GetType().FullName.ToLower().Contains("command"))
                {
                    var response = await Mediator.Send(request);
                    string _Id = response.ToString();
                    return Ok(new CommandResponse { Id = _Id, Message = Msg });
                }
                else
                {
                    var response = await Mediator.Send(request);
                    return Ok(response);
                }
            }
            catch (NotFoundException ex) { return NotFound(new { errors = ex.Message }); }
            catch (DeleteFailureException ex) { return BadRequest(new { errors = ex.Message }); }
            catch (KeyIsAlreadyExistsException ex) { return Conflict(new { errors = ex.Message }); }
            catch (NameIsAlreadyExistsException ex) { return Conflict(new { errors = ex.Message }); }
            catch (ValidationException ex) { return BadRequest(new { errors = ex.Failures }); }
            catch (Exception ex) { return BadRequest(new { errors = ex.Message }); }
        }

        public async Task<ActionResult> MVCExecute<TRequest>(IRequest<TRequest> request, string Msg)
        {
            try
            {
                var response = await Mediator.Send(request);
                return Ok(new JsonRespone<TRequest> { Data = response, Message = Msg, StatusCode = (int)HttpStatusCode.OK, Success = true });
            }
            catch (NotFoundException ex) { return NotFound(new JsonRespone<int> { Data = 0, Message = ex.Message, StatusCode = (int)HttpStatusCode.NotFound, Success = false }); }

            catch (KeyIsAlreadyExistsException ex) { return BadRequest(new JsonRespone<int> { Data = 0, Message = ex.Message, StatusCode = (int)HttpStatusCode.Conflict }); }

            catch (NameIsAlreadyExistsException ex) { return BadRequest(new JsonRespone<int> { Data = 0, Message = ex.Message, StatusCode = (int)HttpStatusCode.Conflict }); }

            catch (ValidationException ex)
            { return BadRequest(new JsonRespone<int> { Data = 0, Message = ex.Failures.First().Value.First(), StatusCode = (int)HttpStatusCode.BadRequest }); }

            catch (Exception ex) { return BadRequest(new JsonRespone<int> { Data = 0, Message = ex.Message, StatusCode = (int)HttpStatusCode.BadRequest }); }
        }

        /// <summary>
        /// this function that can used in action mvc only in success will return parameter action name
        /// </summary>
        /// <typeparam name="TRequest">instance of object that implement for IRequest</typeparam>
        /// <param name="request">instance of object that implement for IRequest</param>
        /// <param name="ActionName">Atcion name that redirect to it in success</param>
        /// <returns></returns>
        public async Task<ActionResult> MVCExecuteReturnView<TRequest>(IRequest<TRequest> request, string ActionName, string ControllerName, string AreaName)
        {
            try
            {
                var response = await Mediator.Send(request);
                return RedirectToAction(ActionName, ControllerName, new { area = AreaName });
            }
            catch (NotFoundException ex)
            {
                ViewData["error"] = ex.Message;
                ModelState.AddModelError("Fail", ex.Message);
                return View(request);
            }
            catch (ValidationException ex)
            {
                ViewData["error"] = ex.Failures.FirstOrDefault().Value.FirstOrDefault();
                ModelState.AddModelError("Fail", ex.Failures.FirstOrDefault().Value.FirstOrDefault());
                return View(request);
            }
            catch (KeyIsAlreadyExistsException ex)
            {
                ViewData["error"] = ex.Message;
                ModelState.AddModelError("Fail", ex.Message);
                return View(request);
            }
            catch (NameIsAlreadyExistsException ex)
            {
                ViewData["error"] = ex.Message;
                return View(request);
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View(request);
            }
        }

        public async Task<ObjectResult> GenaricExecute<TRequest>(IRequest<TRequest> request, string Msg)
        {
            try
            {
                var response = await Mediator.Send(request);
                return Ok(new GenaricResponse<TRequest> { Data = response, Message = Msg, StatusCode = (int)HttpStatusCode.OK });
            }
            catch (NotFoundException ex)
            { return NotFound(new GenaricResponse<object> { Data = new object(), Message = ex.Message, StatusCode = (int)HttpStatusCode.NotFound }); }
            catch (DeleteFailureException ex)
            { return BadRequest(new GenaricResponse<object> { Data = new object(), Message = ex.Message, StatusCode = (int)HttpStatusCode.BadRequest }); }
            catch (KeyIsAlreadyExistsException ex)
            { return Conflict(new GenaricResponse<object> { Data = new object(), Message = ex.Message, StatusCode = (int)HttpStatusCode.Conflict }); }
            catch (ValidationException ex)
            { return BadRequest(new GenaricResponse<IDictionary<string, string[]>> { Data = ex.Failures, Message = ex.Message, StatusCode = (int)HttpStatusCode.BadRequest }); }
            catch (DbUpdateConcurrencyException ex)
            { return BadRequest(new GenaricResponse<object> { Data = new object(), Message = ex.ToString(), StatusCode = (int)HttpStatusCode.BadRequest }); }
            catch (Exception ex)
            { return BadRequest(new GenaricResponse<object> { Data = new object(), Message = ex.Message, StatusCode = (int)HttpStatusCode.BadRequest }); }
        }
        private string StringfyImage(string ImagePath)
        {
            // to be able to use the code on the wondows machine
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) ImagePath = ImagePath.Replace("/", @"\");
            string base64String = "";
            try
            {
                using (FileStream stream = System.IO.File.OpenRead($"{Env.WebRootPath}{ImagePath}"))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        stream.CopyTo(m);
                        byte[] imageBytes = m.ToArray();
                        base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error StringfyImage", ImagePath);
                return base64String;
            }
        }

    }
}