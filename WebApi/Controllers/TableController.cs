using Domain.DTOs.Tables;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TableController(ITableService service) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetTableDto>>> GetAllTable() => await service.GetTables();

    [HttpGet("{id:int}")]
    public async Task<Response<GetTableDto>> GetTable(int id) => await service.GetTableByIdAsync(id);

    [HttpPost]
    public async Task<Response<GetTableDto>> CreateTable(CreateTableDto tableDto) =>
        await service.CreateTableAsync(tableDto);

    [HttpPut("{id:int}")]
    public async Task<Response<GetTableDto>> UpdateTable(int id, UpdateTableDto tableDto) =>
        await service.UpdateTableAsync(id, tableDto);

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteTable(int id) =>
        await service.DeleteTableAsync(id);
}