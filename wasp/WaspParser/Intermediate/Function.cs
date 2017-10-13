using wasp.Parsing;

namespace wasp.Intermediate
{
    class Function
    {
        public Signature Signature { get; }

        public Context Context { get; }

        public Function(Signature signature, Context context)
        {
            Signature = signature;
            Context = context;
        }

        public byte[] Codebody;
    }
}