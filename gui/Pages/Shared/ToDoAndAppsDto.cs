using xingyi.application;

namespace gui.Pages.Shared
{
    public class ToDoAndAppsDto<T>
    {
        public List<T> Items { get; set; }
        public string Title { get; set; }
        public string Message { get; set; } = "";
    }
}
