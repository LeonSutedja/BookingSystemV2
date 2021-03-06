﻿using System;
using Abp.Domain.Uow;

namespace LeonSutedja.BookingSystem.Shared.TableCreator
{
    using System.Collections.Generic;
    using System.Linq;
    using Abp.AutoMapper;
    using Abp.Domain.Entities;

    public abstract class TableBuilderGeneric<T> : TableBuilder<T> where T : Entity
    {
        // Initial Variables for Processing
        private List<ITableColumn<T>> _allItemsColumnsAvailableForCategory;

        protected delegate ITableColumn<T> InitializeColumnFilterFunction(ITableColumn<T> column);

        protected delegate ITableColumn<T> InitializeColumnSortFunction(ITableColumn<T> column);

        [UnitOfWork]
        public override TableOutput Build()
        {
            //// Setup initial variables for processing
            _allItemsColumnsAvailableForCategory = GetAllItemsColumnsAvailableForCategory();

            //// When the default column is null, this should get the initial display columns
            var displayColumnRequested = ColumnIdsRequestedValue == null
                ? GetInitialColumns(_allItemsColumnsAvailableForCategory.ToList())
                : GetDisplayColumnRequested(ColumnIdsRequestedValue).ToList();

            var itemsWithCharacteristicsQuery = GetItemsToSort();
            itemsWithCharacteristicsQuery = GetItemsQueryBaseOnCategory(itemsWithCharacteristicsQuery);

            if (ColumnFilters != null)
            {
                ColumnFilters.ForEach((filter) =>
                {
                    itemsWithCharacteristicsQuery = ProcessFilter(itemsWithCharacteristicsQuery, filter);
                });
            }

            var totalItems = itemsWithCharacteristicsQuery.Count();
            var sortedDisplay = ProcessSortAndPagination(itemsWithCharacteristicsQuery);

            var rowSummary = sortedDisplay
                                    .Select(owc => new TableRow()
                                    {
                                        Id = GetId(owc),
                                        PendingNextVersionId = GetNextPendingVersionId(owc),
                                        NextVersionId = GetNextVersionId(owc),
                                        Cells = displayColumnRequested.Select(cr => cr.GetValueAsString(owc)).ToList()
                                    }).ToList();

            var displayColumnDtos = displayColumnRequested.MapTo<List<TableColumnDefinition>>();

            return new TableOutput()
            {
                Rows = rowSummary,
                Columns = displayColumnDtos,
                TotalProductOffersCount = totalItems
            };
        }

        protected virtual int GetId(T entity)
        {
            return entity.Id;
        }

        protected virtual int? GetNextVersionId(T entity)
        {
            return null;
        }

        protected virtual int? GetNextPendingVersionId(T entity)
        {
            return null;
        }

        protected abstract List<ITableColumn<T>> GetAllItemsColumnsAvailableForCategory();

        protected abstract IQueryable<T> GetItemsToSort();

        protected abstract IQueryable<T> GetItemsQueryBaseOnCategory(IQueryable<T> items);

        protected abstract Dictionary<Type, InitializeColumnSortFunction> InitializeColumnSort();
        
        protected abstract Dictionary<Type, InitializeColumnFilterFunction> InitializeColumnFilters();

        protected abstract IEnumerable<ITableColumn<T>> GetInitialColumns(List<ITableColumn<T>> allColumnsAvailable);

        private IQueryable<T> ProcessFilter(IQueryable<T> productOffers, ColumnFilter.TableColumnFilter TableColumnFilter)
        {
            var offerViewColumn = _allItemsColumnsAvailableForCategory.First(column => column.Identifier.Equals(TableColumnFilter.ColumnIdentifier));

            var initializeColumnFilters = InitializeColumnFilters();
            if (initializeColumnFilters.ContainsKey(offerViewColumn.GetType()))
            {
                offerViewColumn = initializeColumnFilters[offerViewColumn.GetType()](offerViewColumn);
            }

            return offerViewColumn.ApplyFilter(productOffers, TableColumnFilter);
        }

        private IEnumerable<T> ProcessSortAndPagination(IQueryable<T> items)
        {
            if (SortByValue == null)
            {
                return items
                   .OrderBy(offer => offer.Id)
                   .Skip((PageNumberValue - 1) * PageSizeValue)
                   .Take(PageSizeValue)
                   .ToList();
            }

            var sortByColumn = _allItemsColumnsAvailableForCategory.First(cd => cd.Identifier.Equals(SortByValue));
            var initializeColumnSort = InitializeColumnSort();
            var type = sortByColumn.GetType().BaseType;
            //if (type.BaseType == typeof(OfferViewCharacteristicColumn))
            //{
            //    type = typeof(OfferViewCharacteristicColumn);
            //}

            if (initializeColumnSort.ContainsKey(type))
            {
                sortByColumn = initializeColumnSort[type](sortByColumn);
            }

            // Tell the column to apply its sort, which will give us a list of Offer IDs in display order. Apply pagination to this list.
            var displayOfferIds = sortByColumn.ApplySort(items, IsSortAscendingValue.Value)
                                            .Skip((PageNumberValue - 1) * PageSizeValue)
                                            .Take(PageSizeValue)
                                            .ToList();

            // Now that we have one page's worth of IDs we can do a full retrieval of those offers. But we have to re-apply the sort to those full offers!
            return items
                .Where(offer => displayOfferIds.Contains(offer.Id)) // This will lose the sort order...
                .ToList()
                .OrderBy(offer => displayOfferIds.IndexOf(offer.Id)); // ... so we have to re-apply it!
        }

        private IEnumerable<ITableColumn<T>> GetDisplayColumnRequested(IEnumerable<TableColumnIdentifier> columnIdentifiersRequested)
        {
            return columnIdentifiersRequested
                .Select(id => _allItemsColumnsAvailableForCategory
                    .First(allCol => allCol.Identifier.Equals(id)));
        }
    }
}
