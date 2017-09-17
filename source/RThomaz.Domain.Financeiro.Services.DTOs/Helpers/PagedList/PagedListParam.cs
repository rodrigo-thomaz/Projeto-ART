using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList
{
    public class PagedListParam
    {
        private readonly int _skip;
        private readonly int _take;

        public PagedListParam(uint skip, uint take)
        {
            if(take == 0)
            {
                throw new ArgumentOutOfRangeException("take", "The value should be greater than zero");
            }

            _skip = Convert.ToInt32(skip);
            _take = Convert.ToInt32(take);
        }

        public int Skip { get { return _skip; } }
        public int Take { get { return _take; } }
    }
}
