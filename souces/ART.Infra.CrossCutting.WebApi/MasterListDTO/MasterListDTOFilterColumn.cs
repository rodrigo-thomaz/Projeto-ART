namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    using System;
    using System.Linq.Expressions;

    using ART.Infra.CrossCutting.WebApi.MasterList;

    public class MasterListDTOFilterColumn<TSource, TProperty> : IMasterListFilterColumn
    {
        #region Fields

        private readonly string _columnName;
        private readonly MasterListFilterCriteria[] _criteria;

        #endregion Fields

        #region Constructors

        public MasterListDTOFilterColumn(Expression<Func<TSource, TProperty>> selector, MasterListFilterCriteria[] criteria)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            var propertyName = ExpressionHelper.GetPropertyName<TSource, TProperty>(selector);

            _columnName = propertyName;
            _criteria = criteria;
        }

        #endregion Constructors

        #region Properties

        public string ColumnName
        {
            get { return _columnName; }
        }

        public MasterListFilterCriteria[] Criteria
        {
            get { return _criteria; }
        }

        #endregion Properties
    }
}