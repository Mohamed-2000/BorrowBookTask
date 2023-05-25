namespace MyBase.Common
{
    public class DataTableDto<T>
    {
        public int? draw { get; set; }
        public int? recordsFiltered { get; set; }
        public int? recordsTotal { get; set; }
        public List<T> data{ get; set; }
    }

    public class Order
    {
        public int Column { get; set; }
        public string dir { get; set; }
    }
    public class Search
    {
        public string Value { get; set; }
        public bool regex { get; set; }
    }
    public class Columns
    {
        public string data { get; set; }
        public string Name { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }
}
