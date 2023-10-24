using System.Runtime.Serialization;

namespace VolleyData.Shared.DTOs
{
    [DataContract]
    public class Summary
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Message => $"{Title}: {Text}";
    }
}
