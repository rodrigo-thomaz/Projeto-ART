namespace RThomaz.Application.Financeiro.Helpers.jQueryDataTable
{
    public class jQueryDataTableColumn
    {
        public string name { get; set; }
        public string data { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public jQueryDataTableSearch search { get; set; }
    }
}