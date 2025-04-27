using Domain.DTOs.Tables;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ITableService
{
    Task<Response<List<GetTableDto>>> GetTables();
    Task<Response<GetTableDto>> GetTableByIdAsync(int id);
    Task<Response<GetTableDto>> CreateTableAsync(CreateTableDto tableDto);
    Task<Response<GetTableDto>> UpdateTableAsync(int id, UpdateTableDto tableDto);
    Task<Response<string>> DeleteTableAsync(int id);
}
