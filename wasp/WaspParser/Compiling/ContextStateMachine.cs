using System;
using System.Collections.Generic;
using wasp.enums;
using wasp.Tokenization;

namespace wasp.Compiling
{
    class ContextStateMachine : IDisposable
    {
        Dictionary<int, ModuleSections> levelMap;

        HashSet<ModuleSections> currentStates;
            
       BlockStateMachine blockStateMachine;

        public ContextStateMachine(BlockStateMachine blockStateMachine)
        {
            levelMap = new Dictionary<int, ModuleSections>();
            currentStates = new HashSet<ModuleSections>();
            this.blockStateMachine = blockStateMachine;
            blockStateMachine.BracketBlockStarted += BlockStateMachineOnBracketBlockStarted;
            blockStateMachine.BracketBlockTerminated += BlockStateMachineOnBracketBlockTerminated;
            blockStateMachine.ParensBlockStarted += BlockStateMachineOnParensBlockStarted;
            blockStateMachine.ParensBlockTerminated += BlockStateMachine_ParensBlockTerminated;
        }

        void SetState()

        void BlockStateMachine_ParensBlockTerminated(int layer)
        {
            currentStates.Remove(levelMap[layer]);
            levelMap.Remove(layer);
        }

        void BlockStateMachineOnParensBlockStarted(int i)
        {
            throw new NotImplementedException();
        }

        void BlockStateMachineOnBracketBlockTerminated(int i)
        {
            throw new NotImplementedException();
        }

        void BlockStateMachineOnBracketBlockStarted(int i)
        {
            throw new NotImplementedException();
        }

        public void NewKeyword(Token token, int bracketLevel)
        {
            switch (token.Id)
            {
                case Tokens.Export:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Dispose()
        {
            blockStateMachine.BracketBlockStarted -= BlockStateMachineOnBracketBlockStarted;
            blockStateMachine.BracketBlockTerminated -= BlockStateMachineOnBracketBlockTerminated;
            blockStateMachine.ParensBlockStarted -= BlockStateMachineOnParensBlockStarted;
            blockStateMachine.ParensBlockTerminated -= BlockStateMachine_ParensBlockTerminated;
        }
    }
}
