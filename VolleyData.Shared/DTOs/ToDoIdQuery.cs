﻿using System.Runtime.Serialization;

namespace VolleyData.Shared.DTOs
{
    [DataContract]
    public class ToDoIdQuery
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
    }
}
