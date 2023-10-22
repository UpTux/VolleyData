using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace VolleyData.Shared.DTOs
{
    [DataContract, Table("ToDoData")]
    public class ToDoData
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
        [DataMember(Order = 2)]
        public string Title { get; set; }
        [DataMember(Order = 3)]
        public string Description { get; set; }
        [DataMember(Order = 4)]
        public bool Status { get; set; }
        [DataMember(Order = 5)]
        public uint AttackTotal { get; set; }
        [DataMember(Order = 6)]
        public uint AttackError { get; set; }
        [DataMember(Order = 7)]
        public uint AttackBlock { get; set; }
        [DataMember( Order = 8)]
        public uint AttackKill { get; set; }
        [DataMember(Order = 9)]
        public uint ReceiveTotal { get; set; }
        [DataMember(Order = 10)]
        public uint ReceiveError { get; set; }
        [DataMember(Order = 11)]
        public uint ReceivePositiv { get; set; }
        [DataMember(Order = 12)]
        public uint ReceiveExcellent { get; set; }
        [DataMember(Order = 13)]
        public uint ServeTotal { get; set; }
        [DataMember(Order = 14)]
        public uint ServeError { get; set; }
        [DataMember(Order = 15)]
        public uint ServeKill { get; set; }
        [DataMember(Order = 16)] 
        public uint ActionsTotal { get; set; }
        [DataMember(Order = 17)]
        public uint ActionsError
        {
            get;
            set;
        }
        [DataMember(Order = 18)]
        public double ErrorPercentage
        {
            get;
            set;
        }

    }
}
