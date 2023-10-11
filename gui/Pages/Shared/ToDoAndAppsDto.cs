using xingyi.application;

namespace gui.Pages.Shared
{
    public class ToDoAndAppsDto<T>
    {
        public List<T> Items { get; set; }
        public string Title { get; set; }
        public string Message { get; set; } = "";
        public bool AllowEdits { get; set; } = true;
        public bool ShowOwner { get; set; } = false;
    }
}
