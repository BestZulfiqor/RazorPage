using System.Net;
using AutoMapper;
using Domain.DTOs.Tables;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;
[Authorize]
public class TableService(IBaseRepository<Table, int> tableRepository, IMapper mapper, ILogger logger) : ITableService
{
    public async Task<Response<GetTableDto>> CreateTableAsync(CreateTableDto tableDto)
    {
        logger.LogInformation("Начинаем создание стола");
        var table = mapper.Map<Table>(tableDto);
        var result = await tableRepository.AddAsync(table);
        var dto = mapper.Map<GetTableDto>(table);
        return result == 0
            ? new Response<GetTableDto>(HttpStatusCode.BadRequest, "Table not add!")
            : new Response<GetTableDto>(dto);
    }

    public async Task<Response<string>> DeleteTableAsync(int id)
    {
        var exist = await tableRepository.GetById(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Table not found");
        }

        var result = await tableRepository.DeleteAsync(exist);
        
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Table not deleted")
            : new Response<string>("Table deleted!");
    }

    public async Task<Response<GetTableDto>> GetTableByIdAsync(int id)
    {
        logger.LogInformation("Taking table id");
        var exist = await tableRepository.GetById(id);
        if (exist == null)
        {
            return new Response<GetTableDto>(HttpStatusCode.NotFound, "Table not found");
        }
        var dto = mapper.Map<GetTableDto>(exist);
        return new Response<GetTableDto>(dto);
    }

    public async Task<Response<List<GetTableDto>>> GetTables()
    {
        var tables = await tableRepository.GetAllAsync();
        var data = mapper.Map<List<GetTableDto>>(tables);
        return new Response<List<GetTableDto>>(data);
    }

    public async Task<Response<GetTableDto>> UpdateTableAsync(int id, UpdateTableDto tableDto)
    {
        var exist = await tableRepository.GetById(id);
        if (exist == null)
        {
            return new Response<GetTableDto>(HttpStatusCode.NotFound, "Table not found");
        }
        exist.Number = tableDto.Number;
        exist.Seats = tableDto.Seats;
        
        var result = await tableRepository.UpdateAsync(exist);
        
        var dto = mapper.Map<GetTableDto>(exist);
        return result == 0
            ? new Response<GetTableDto>(HttpStatusCode.BadRequest, "Table not updated")
            : new Response<GetTableDto>(dto);
    }

}

