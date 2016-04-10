using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music
{
    public class Silence : INote
    {
        public string Name {
            get
            {
                return "S";
            }
        }
        public int Frequency
        {
            get
            {
                return 0;
            }
        }
        public double Duration { get; set; }

        public noteplayer NotePlayer { get; set; }
 

        public Silence(double duration = 1, noteplayer np = null)
        {
            Duration = duration;

            if (np != null)
                NotePlayer = np;
            else
                NotePlayer = PlayWithText;
        }


        public void Play(int? bpm = null)
        {
            NotePlayer(bpm);
        }
        public void PlayWithSound( int? bpm)
        {
            int duration = (bpm.HasValue) ? (int)Duration * 1000 * 60 / bpm.Value : (int)Duration * 1000;
            Thread.Sleep(duration);
        }
        public void PlayWithText(int? bpm)
        {
            Console.WriteLine(Name);
            int duration = (bpm.HasValue) ? (int)Duration * 1000 * 60 / bpm.Value : (int)Duration * 1000;
            Thread.Sleep(duration);
        }

        public override string ToString()
        {
            return " ";
        }
    }
}
