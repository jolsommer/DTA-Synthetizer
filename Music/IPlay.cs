using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    public interface IPlay
    {
        void Play(int? bpm = null);
    }

    public interface IPlayInRythm : IPlay
    {
        void PlayInRythm(MusicStyle musicStyle);
    }
}
