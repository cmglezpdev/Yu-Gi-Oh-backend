using AutoMapper;
using backend.Application.Providers;
using backend.Application.Repositories;
using backend.Common.Enums;
using backend.Domain;
using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Auth;
using backend.Presentation.DTOs.Municipality;
using backend.Presentation.DTOs.User;
using backend.Presentation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMunicipalityRepository _municipalityRepository;
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;
    
    public AuthController(IUserRepository userRepository, IMapper mapper, 
        IMunicipalityRepository municipalityRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _municipalityRepository = municipalityRepository;
        _jwtProvider = jwtProvider;
    }
    
    [HttpPost("signup")]
    public async Task<ActionResult> SignUp([FromBody] SignUpInputDto input)
    {
        var userByEmail = await _userRepository.GetUserByEmailAsync(input.Email);
        if (userByEmail.IsSuccess) 
            return BadRequest(McResult<string>.Failure("The user already exists"));
        
        var userByUserName = await _userRepository.GetUserByUserNameAsync(input.UserName);
        if (userByUserName.IsSuccess) 
            return BadRequest(McResult<string>.Failure("The user already exists"));
        
        var municipality = await _municipalityRepository.GetMunicipalityByIdAsync(input.MunicipalityId);
        if(municipality is null) 
            return BadRequest(McResult<string>.Failure($"Municipality with id {input.MunicipalityId} not found", ErrorCodes.NotFound));

        var userDomain = new UserDomain(input.Email, input.UserName, input.Password, input.Name, new MunicipalityDomain(municipality.Name, municipality.Id));
        var userPersistent = await _userRepository.CreateUserAsync(new UserInputDto()
        {
            Id = userDomain.Id,
            Email = userDomain.Email,
            UserName = userDomain.UserName,
            Password = userDomain.Password,
            Name = userDomain.Name,
            MunicipalityId = userDomain.Municipality.Id
        });
        
        return Ok(McResult<UserOutputDto>.Succeed(new UserOutputDto()
          {
              Id = userPersistent.Result.Id,
              Email = userPersistent.Result.Email,
              UserName = userPersistent.Result.UserName,
              Name = userPersistent.Result.Name,
              Municipality = _mapper.Map<MunicipalityOutputDto>(userPersistent.Result.Municipality),
              Token = await _jwtProvider.Generate(userPersistent.Result)
          }));
    }
    
    [HttpPost("signin")]
    public async Task<ActionResult> SignIn([FromBody] SignInInputDto input)
    {
        if(input.Email is null && input.UserName is null)
            return BadRequest(McResult<string>.Failure("Email or UserName are required", ErrorCodes.InvalidInput));

        var user = input.Email is not null
            ? await _userRepository.GetUserByEmailAsync(input.Email!)
            : await _userRepository.GetUserByUserNameAsync(input.UserName!);
        
        if (user.IsFailure) return BadRequest(McResult<string>.Failure("Error to signup user"));
        if (BCrypt.Net.BCrypt.Verify(input.Password, user.Result.Password) == false) 
            return BadRequest(McResult<string>.Failure("Invalid password", ErrorCodes.InvalidInput));

        return Ok(McResult<UserOutputDto>.Succeed(new UserOutputDto()
          {
              Id = user.Result.Id,
              Email = user.Result.Email,
              UserName = user.Result.UserName,
              Name = user.Result.Name,
              Municipality = _mapper.Map<MunicipalityOutputDto>(user.Result.Municipality),
              Token = await _jwtProvider.Generate(user.Result)
          }));
    }
}