using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    public delegate void noteplayer(int? bpm);

    public interface INote : IPlay
    {
        string Name { get; }
        int Frequency { get; }
        double Duration { get; }

        noteplayer NotePlayer { get; set; }

        void PlayWithSound(int? bpm);
        void PlayWithText(int? bpm);
    }
}
