using xingyi.application;

namespace gui.Pages.Shared
{
    public class TodoDisplayDto<T>
    {
        public List<T> Items { get; set; }
        public string Message { get; set; }
    }
}
