using AutoMapper;
using MBlog.API.CustomActionFilters;
using MBlog.API.Models.Domain;
using MBlog.API.Models.DTO;
using MBlog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MBlog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IArticleRepository articleRepository;

    public ArticlesController(IMapper mapper, IArticleRepository articleRepository)
    {
        this.mapper = mapper;
        this.articleRepository = articleRepository;
    }

    // Create article
    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddArticleRequestDto addArticleRequestDto)
    {
        // Map Dto to domain model
        var articleDomain = mapper.Map<Article>(addArticleRequestDto);
        articleDomain = await articleRepository.CreateAsync(articleDomain);

        // Map domain model to Dto
        return Ok(mapper.Map<ArticleDto>(articleDomain));
    }

    // GET articles
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
        [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
    {  
        var articlesDomain = await articleRepository.GetAllAsync(filterOn, filterQuery, sortBy,
        isAscending ?? true, pageNumber, pageSize);

        // Create an exception
        //throw new Exception("This is a new exception");

        // Map domain model to Dto
        return Ok(mapper.Map<List<ArticleDto>>(articlesDomain));
    }

    // GET article by id
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var articleDomain =  await articleRepository.GetByIdAsync(id);
        if(articleDomain == null)
        {
            return NotFound();
        }
        return Ok(mapper.Map<ArticleDto>(articleDomain));
    }

    // Update article by id
    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateArticleRequestDto updateArticleRequestDto)
    {
        // Map Dto to domain model
        var articleDomain = mapper.Map<Article>(updateArticleRequestDto);
        articleDomain = await articleRepository.UpdateAsync(id, articleDomain);
        if (articleDomain == null)
        {
            return NotFound();
        }

        // Map domain model to Dto
        return Ok(mapper.Map<ArticleDto>(articleDomain));
    }

    // DELETE article by id
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deletedArticleDomain = articleRepository.DeleteAsync(id);
        if(deletedArticleDomain == null)
        {
            return NotFound();
        }

        // Map domain model to Dto
        return Ok(mapper.Map<ArticleDto>(deletedArticleDomain));
    }
}
