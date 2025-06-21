using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SCGR.Application.Common.Params;
using SCGR.Application.Common.Wrappers;
using SCGR.Application.Contracts.Services;
using SCGR.Application.Features.Transactions.Requests;
using SCGR.Application.Features.Transactions.Responses;
using SCGR.Domain.Abstractions.Types;
using Swashbuckle.AspNetCore.Annotations;

namespace SCGR.API.Controllers.v1;

[ApiVersion("1")]
public sealed class Transaction(ITransactionService transactionService) : BaseController
{
    #region [ LEITURA ]

    [HttpGet]
    [ProducesResponseType(typeof(Paged<TransactionResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Obter todas as transações", Description = "Retorna uma lista paginada de todas as transações.")]
    public async Task<IActionResult> GetAllTransactions([FromQuery] PaginationParams request)
    {
        Result<Paged<TransactionResponseDto>> result = await transactionService.GetAllTransactionsAsync(request);
        return result.Match(onSuccess: Ok, onFailure: Problem);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TransactionResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obter transação por ID", Description = "Retorna os detalhes de uma transação com base no ID.")]
    public async Task<IActionResult> GetTransactionById(int id)
    {
        Result<TransactionResponseDto> result = await transactionService.GetTransactionByIdAsync(id);
        return result.Match(onSuccess: Ok, onFailure: Problem);
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(Paged<TransactionResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Obter transações por intervalo de datas", Description = "Retorna uma lista paginada de transações em um intervalo de datas específico.")]
    public async Task<IActionResult> GetTransactionByDateRange([FromQuery] GetTransactionDateRangeQueryDto request)
    {
        Result<Paged<TransactionResponseDto>> result = await transactionService.GetTransactionByDateRangeAsync(request);
        return result.Match(onSuccess: Ok, onFailure: Problem);
    }

    [HttpGet("category")]
    [ProducesResponseType(typeof(Paged<TransactionResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obter transações por categoria", Description = "Retorna uma lista paginada de transações filtradas por categoria.")]
    public async Task<IActionResult> GetTransactionByCategory([FromQuery] GetTransactionByCategoryQueryDto request)
    {
        Result<Paged<TransactionResponseDto>> result = await transactionService.GetTransactionsByCategoryIdAsync(request);
        return result.Match(onSuccess: Ok, onFailure: Problem);
    }

    #endregion

    #region [ GRAVACAO ]

    [HttpPost]
    [ProducesResponseType(typeof(TransactionResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTransaction([FromBody] TransactionCommandDto request)
    {
        Result<TransactionCreatedResponseDto> result = await transactionService.CreateTransactionAsync(request);
        return result.Match
        (
            onSuccess: dto => CreatedAtAction
            (
                actionName: nameof(GetTransactionById),
                routeValues: new { id = dto.Id },
                value: dto
            ),
            onFailure: Problem
        );
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTransaction(int id, [FromBody] TransactionCommandDto request)
    {
        Result result = await transactionService.UpdateTransactionAsync(id, request);
        return result.Match(onSuccess: NoContent, onFailure: Problem);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        Result result = await transactionService.DeleteTransactionAsync(id);
        return result.Match(onSuccess: NoContent, onFailure: Problem);
    }

    #endregion
}
