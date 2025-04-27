using System.Net;
using AutoMapper;
using Domain.DTOs.Tables;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TableService(DataContext context, IMapper mapper) : ITableService
{
    public async Task<Response<GetTableDto>> CreateTableAsync(CreateTableDto tableDto)
    {
        var Table = mapper.Map<Table>(tableDto);
        await context.Tables.AddAsync(Table);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetTableDto>(Table);
        return result == 0
            ? new Response<GetTableDto>(HttpStatusCode.BadRequest, "Table not add!")
            : new Response<GetTableDto>(dto);
    }

    public async Task<Response<string>> DeleteTableAsync(int id)
    {
        var exist = await context.Tables.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Table not found");
        }
        context.Tables.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Table not deleted")
            : new Response<string>("Table deleted!");
    }

    public async Task<Response<GetTableDto>> GetTableByIdAsync(int id)
    {
        var exist = await context.Tables.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetTableDto>(HttpStatusCode.NotFound, "Table not found");
        }
        var dto = mapper.Map<GetTableDto>(exist);
        return new Response<GetTableDto>(dto);
    }

    public async Task<Response<List<GetTableDto>>> GetTables()
    {
        var Tables = await context.Tables.ToListAsync();
        var data = mapper.Map<List<GetTableDto>>(Tables);
        return new Response<List<GetTableDto>>(data);
    }

    public async Task<Response<GetTableDto>> UpdateTableAsync(int id, UpdateTableDto tableDto)
    {
        var exist = await context.Tables.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetTableDto>(HttpStatusCode.NotFound, "Table not found");
        }
        exist.IsReserved = tableDto.IsReserved;
        exist.Number = tableDto.Number;
        exist.Seats = tableDto.Seats;
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetTableDto>(exist);
        return result == 0
            ? new Response<GetTableDto>(HttpStatusCode.BadRequest, "Table not updated")
            : new Response<GetTableDto>(dto);
    }

}
