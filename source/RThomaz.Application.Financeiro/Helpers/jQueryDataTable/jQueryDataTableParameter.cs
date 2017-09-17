namespace RThomaz.Application.Financeiro.Helpers.jQueryDataTable
{
    public class jQueryDataTableParameter
    {
        public int draw { get; set; }
        public int length { get; set; }
        public int start { get; set; }
        public jQueryDataTableSearch search { get; set; }
        public jQueryDataTableColumn[] columns { get; set; }
        public jQueryDataTableOrder[] order { get; set; }
    }
}