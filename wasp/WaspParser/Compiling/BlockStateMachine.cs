using System;
using wasp.enums;
using wasp.Tokenization;

namespace wasp.Compiling
{
    class BlockStateMachine
    {
        public int BracketLayer { get; private set; }

        public int ParensLayer { get; private set; }

        public void Set(Token token)
        {
            switch (token.Id)
            {
                case Tokens.Lbracket:
                    BracketBlockStarted?.Invoke(BracketLayer);
                    BracketLayer++;
                    break;
                case Tokens.Rbracket:
                    BracketLayer--;
                    BracketBlockTerminated?.Invoke(BracketLayer);
                    break;
                case Tokens.Lparens:
                    ParensBlockStarted?.Invoke(ParensLayer);
                    ParensLayer++;
                    break;
                case Tokens.Rparens:
                    ParensLayer--;
                    ParensBlockTerminated?.Invoke(ParensLayer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(token.Id), token, null);
            }
        }
        public event Action<int> ParensBlockStarted;

        public event Action<int> BracketBlockStarted;

        public event Action<int> ParensBlockTerminated;

        public event Action<int> BracketBlockTerminated;
    }
}