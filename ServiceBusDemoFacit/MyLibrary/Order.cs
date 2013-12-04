using System.Runtime.Serialization;

namespace MyLibrary
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public int StoreNumber { get; set; }

        [DataMember]
        public int SequenceNumber { get; set; }

        [DataMember]
        public double Amount { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}: {2} kr", StoreNumber, SequenceNumber, Amount);
        }
    }
}
