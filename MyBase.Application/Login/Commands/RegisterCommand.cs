using MyBase.Application.Books.Models;
using MyBase.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyBase.Application.Login.Commands;
public class RegisterCommand : IRequest<CommandResponse<string>>
{
    [Required(ErrorMessage = "Name is Requried")]
    public string Name { get; set; }


    [Required(ErrorMessage = "Email is Requried")]
    [EmailAddress(ErrorMessage = "Email dosn't match")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is Requried")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Password Not Match")]
    public string ConfirmPassword { get; set; }
    public string Role { get; set; }


    public class Handler : IRequestHandler<RegisterCommand, CommandResponse<string>>
    {
        private UserManager<AppUser> _userManager;
        public Handler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CommandResponse<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userExists = await _userManager.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
                if (userExists != null) return new CommandResponse<string>
                {
                    Success = false,
                    Data = null,
                    Message = "Email Already in use!"
                }; ;
                var user = new AppUser
                {
                    Name = request.Name,
                    UserName = request.Email,
                    Email = request.Email,
                    NormalizedEmail = request.Email.ToUpper(),
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded) return new CommandResponse<string>
                {
                    Success = false,
                    Data = null,
                    Message = string.Join(Environment.NewLine, result.Errors.Select(x => x.Description))
                };

                var roleResult = await _userManager.AddToRoleAsync(user, request.Role);


                return new CommandResponse<string>
                {
                    Success = true,
                    Data = null,
                    Message = "Your are Registered Succsfully"
                };

            }
            catch (Exception ex)
            {
                return new CommandResponse<string>
                {
                    Success = true,
                    Data = null,
                    Message = ex.Message
                };
            }
        }
    }
}


