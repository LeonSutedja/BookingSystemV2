using System.Text;

namespace LeonSutedja.BookingSystem.Shared.TableCreator
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ColumnFilter;

    public class TableInput
    {
        [Required]
        public int ProductCategoryId { get; set; }

        public List<TableColumnFilter> Filters { get; set; }

        public List<TableColumnIdentifier> ColumnsRequested { get; set; }

        public TableColumnIdentifier SortBy { get; set; }

        public bool? SortDirectionAsc { get; set; }

        [Required]
        public int PageSize { get; set; }

        [Required]
        public int PageNumber { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Product Category Id: " + ProductCategoryId);
            stringBuilder.AppendLine("Page Size: " + PageSize);
            stringBuilder.AppendLine("Page Number: " + PageNumber);
            stringBuilder.AppendLine("Sort By: " + SortBy);
            stringBuilder.AppendLine("Is Sort Direction Asc: " + SortDirectionAsc);
            if (Filters != null)
            {
                Filters.ForEach((filter) =>
                {
                    stringBuilder.AppendLine("Filter: " + filter.ToString());
                });
            }

            if (ColumnsRequested != null)
            {
                ColumnsRequested.ForEach((column) =>
                {
                    stringBuilder.AppendLine("Column Requested: " + column.ToString());
                });
            }

            return stringBuilder.ToString();
        }
    }
}
