namespace ShortLinksApi.BLL.Exceptions
{
    public class FullUrlNotFoundException : Exception
    {
        public FullUrlNotFoundException(string message): base(message) { }
    }
}
