namespace Asas.AsasHash.Models
{
    public class GoodHash
    {
        public Object Obj { get; set; } = default!;

        public int Iterations { get; set; }

        public byte[] Salt { get; set; } = default!;

        public byte[] IV { get; set; } = default!;
    }
}