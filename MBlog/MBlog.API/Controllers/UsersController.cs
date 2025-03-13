using AutoMapper;
using MBlog.API.CustomActionFilters;
using MBlog.API.Data;
using MBlog.API.Models.Domain;
using MBlog.API.Models.DTO;
using MBlog.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MBlog.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UsersController : ControllerBase
{
    private readonly MBlogDbContext dbContext;
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly ILogger<UsersController> logger;

    public UsersController(MBlogDbContext dbContext, IUserRepository userRepository, IMapper mapper, ILogger<UsersController> logger)
    {
        this.dbContext = dbContext;
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    // GET all users
    [HttpGet]
    //[Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            //throw new Exception("This is a custom exception");
            // Get data from database - domain models
            var usersDomain = await userRepository.GetAllAsync();

            logger.LogInformation($"Finished GetAllUsers request with data: {JsonSerializer.Serialize(usersDomain)}");
            // Return DTOs
            return Ok(mapper.Map<List<UserDto>>(usersDomain));
        }
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }

    // GET single user (get user by Id)
    [HttpGet]
    [Route("{id:Guid}")]
    //[Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        // Get user domain model from database
        var userDomain = await userRepository.GetByIdAsync(id);
        if(userDomain == null)
        {
            return NotFound();
        }

        // Return user DTO back to client
        return Ok(mapper.Map<UserDto>(userDomain));
    }

    // POST to create new user
    [HttpPost]
    [ValidateModel]
    //[Authorize(Roles = "Writer")]
    public async Task<IActionResult> Create([FromBody] AddUserRequestDto addUserRequestDto)
    {  
        // Map or convert Dto to domain model
        var userDomainModel = mapper.Map<User>(addUserRequestDto);

        // Use domain model to create user
        userDomainModel = await userRepository.CreateAsync(userDomainModel);

        // Map domain model back to Dto
        var userDto = mapper.Map<UserDto>(userDomainModel);

        return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);  
    }

    //PUT to update user
    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    //[Authorize(Roles = "Writer")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserRequestDto updateUserRequestDto)
    {
        // Map Dto to domain model
        var userDomainModel = mapper.Map<User>(updateUserRequestDto);

        // Check if user exists
        userDomainModel = await userRepository.UpdateAsync(id, userDomainModel);
        if (userDomainModel == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<UserDto>(userDomainModel));
    }

    // DELETE user
    [HttpDelete]
    [Route("{id:Guid}")]
    //[Authorize(Roles = "Writer")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        // Check if user exists
        var userDomainModel = await userRepository.DeleteAsync(id);
        if (userDomainModel == null)
        {
            return NotFound();
        }

        // return deleted user back
        return Ok(mapper.Map<UserDto>(userDomainModel));
    }
}
