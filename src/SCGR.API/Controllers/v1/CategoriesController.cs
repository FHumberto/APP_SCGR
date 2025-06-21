using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SCGR.Application.Common.Params;
using SCGR.Application.Common.Wrappers;
using SCGR.Application.Contracts.Services;
using SCGR.Application.Features.Categories.Requests;
using SCGR.Application.Features.Categories.Responses;
using SCGR.Domain.Abstractions.Types;
using Swashbuckle.AspNetCore.Annotations;

namespace SCGR.API.Controllers.v1;

[ApiVersion(1)]
public sealed class CategoriesController(ICategoryService categoryService) : BaseController
{
    #region [ LEITURA ]

    [HttpGet]
    [ProducesResponseType(typeof(Paged<CategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Obter todas as categorias", Description = "Retorna uma lista paginada de todas as categorias.")]
    public async Task<IActionResult> GetAllCategories([FromQuery] PaginationParams request)
    {
        Result<Paged<CategoryResponseDto>> result = await categoryService.GetAllCategoriesAsync(request);
        return result.Match(onSuccess: Ok, onFailure: Problem);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obter categoria por ID", Description = "Retorna os detalhes de uma categoria com base no ID.")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        Result<CategoryResponseDto> result = await categoryService.GetCategoryByIdAsync(id);
        return result.Match(onSuccess: Ok, onFailure: Problem);
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Buscar categorias por nome", Description = "Retorna uma lista de categorias filtradas pelo nome.")]
    public async Task<IActionResult> GetCategoryByName([FromQuery] CategoryQueryDto request)
    {
        Result<Paged<CategoryResponseDto>> result = await categoryService.GetCategoryByNameAsync(request);
        return result.Match(onSuccess: Ok, onFailure: Problem);
    }

    #endregion

    #region [ GRAVACAO ]

    [HttpPost]
    [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Criar nova categoria", Description = "Cria uma nova categoria com base nos dados fornecidos.")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCommandDto request)
    {
        Result<CategoryCreatedResponseDto> result = await categoryService.CreateCategoryAsync(request);
        return result.Match
          (
              onSuccess: dto => CreatedAtAction
              (
                  actionName: nameof(GetCategoryById),
                  routeValues: new { id = dto.Id },
                  value: dto
              ),
              onFailure: Problem
          );
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Atualizar categoria", Description = "Atualiza os detalhes de uma categoria existente com base no ID.")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryCommandDto request)
    {
        Result result = await categoryService.UpdateCategoryAsync(id, request);
        return result.Match(onSuccess: NoContent, onFailure: Problem);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Excluir categoria", Description = "Exclui uma categoria com base no ID.")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        Result result = await categoryService.DeleteCategoryAsync(id);
        return result.Match(onSuccess: NoContent, onFailure: Problem);
    }

    #endregion
}