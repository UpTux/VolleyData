using System.Runtime.Serialization;

namespace VolleyData.Shared.DTOs
{
    [DataContract]
    public class ToDoPutQuery
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
        [DataMember(Order = 2)]
        public ToDoData ToDoDataItem { get; set; }
    }
}
