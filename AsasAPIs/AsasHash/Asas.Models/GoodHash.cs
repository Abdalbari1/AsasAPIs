namespace Asas.AsasHash.Asas.Models
{
    public class GoodHash
    {
        public Object Obj { get; set; }

        public int Iterations { get; set; }

        public byte[] Salt { get; set; }

        public byte[] IV { get; set; }
    }
}
