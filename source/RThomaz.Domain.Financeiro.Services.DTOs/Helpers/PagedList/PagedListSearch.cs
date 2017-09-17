using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList
{
    public class PagedListSearch
    {
        private readonly string _value;
        private readonly bool _isRegex;

        public PagedListSearch(string value, bool isRegex)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("value");
            }

            _value = value;
            _isRegex = isRegex;
        }

        public string Value { get { return _value; } }
        public bool IsRegex { get { return _isRegex; } }
    }
}
