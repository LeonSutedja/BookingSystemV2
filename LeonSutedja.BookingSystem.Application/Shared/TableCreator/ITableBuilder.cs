namespace LeonSutedja.BookingSystem.Shared.TableCreator
{
    using System.Collections.Generic;
    using Abp.Domain.Entities;

    public interface ITableBuilder<T> where T : Entity
    {
        ITableBuilder<T> Category(int productCategoryId);

        ITableBuilder<T> ColumnRequested(List<TableColumnIdentifier> colIdsRequested);

        ITableBuilder<T> PageSize(int pageSize);

        ITableBuilder<T> PageNumber(int pageNumber);

        ITableBuilder<T> SortBy(TableColumnIdentifier sortBy);

        ITableBuilder<T> IsSortAscending(bool? isSortAscending);

        ITableBuilder<T> FilterBy(List<ColumnFilter.TableColumnFilter> columnFilters);

        TableOutput Build();
    }
}
