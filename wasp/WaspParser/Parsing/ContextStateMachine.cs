using System.Collections.Generic;
using wasp.Intermediate;

namespace wasp.Parsing
{
    class ContextStateMachine
    {
        readonly Stack<Context> contextQueue = new Stack<Context>();
        public Signature CurrentSignature { get; private set; }

        public Context Context => contextQueue.Peek();

        public bool Export
        {
            get => contextQueue.Contains(Context.Export);
            set
            {
                if (value && !contextQueue.Contains(Context.Export))
                    contextQueue.Push(Context.Export);
                else if (contextQueue.Peek() == Context.Export)
                    contextQueue.Pop();
            }
        }

        public void SetFunction(Signature signature)
        {
            CurrentSignature = signature;
            contextQueue.Push(Context.Function);
        }

        public void RemoveFunction()
        {
            if (contextQueue.Peek() == Context.Function)
                contextQueue.Pop();
            CurrentSignature = null;
        }
    }

    enum Context
    {
        None,
        Export,
        Function,
        Return
    }
}