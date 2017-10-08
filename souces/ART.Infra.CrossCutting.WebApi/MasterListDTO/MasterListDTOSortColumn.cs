namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    using System;
    using System.Linq.Expressions;

    using ART.Infra.CrossCutting.WebApi.MasterList;

    public class MasterListDTOSortColumn<TSource, TProperty> : IMasterListSortColumn
    {
        #region Fields

        private readonly string _columnName;
        private readonly int _priority;
        private readonly MasterListSortDirection _sortDirection;

        #endregion Fields

        #region Constructors

        public MasterListDTOSortColumn(Expression<Func<TSource, TProperty>> selector, MasterListSortDirection sortDirection, int priority)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            var propertyName = ExpressionHelper.GetPropertyName<TSource, TProperty>(selector);

            _columnName = propertyName;
            _sortDirection = sortDirection;
            _priority = priority;
        }

        #endregion Constructors

        #region Properties

        public string ColumnName
        {
            get { return _columnName; }
        }

        public int Priority
        {
            get { return _priority; }
        }

        public MasterListSortDirection SortDirection
        {
            get { return _sortDirection; }
        }

        #endregion Properties
    }
}