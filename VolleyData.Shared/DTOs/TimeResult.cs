using System.Runtime.Serialization;

namespace VolleyData.Shared.DTOs
{
    [DataContract]
    public class TimeResult
    {
        [DataMember(Order = 1)]
        public string Time { get; set; }
    }
}
