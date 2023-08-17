namespace SkillUp.Entity.ViewModels
{
    public class PaginationVM<T>
    {
        public int MaxPageCount { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<T> Items { get; set; }

        public string? Query { get; set; }
    }
}
