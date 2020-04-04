namespace Entity.shared
{
    public class SortInfo
    {
        public SortOrder Order { get; set; }
        public string OrderBy { get; set; }
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }
}
