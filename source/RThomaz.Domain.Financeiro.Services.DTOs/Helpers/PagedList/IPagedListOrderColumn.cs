namespace RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList
{
    public interface IPagedListOrderColumn
    {
        string ColumnName { get; }
        PagedListOrderDirection OrderDirection { get; }
    }
}
