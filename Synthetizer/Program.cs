using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music;

namespace Synthetizer
{
    class Program
    {
        static void Main(string[] args)
        {
            //PlaySimpleSong();
            var synthetizer = new Synthetizer();
            synthetizer.Launch();
        }

        static void PlaySimpleSong()
        {
            var mySong = new Song();
            var myTrack1 = new Track();

            //smooooooooock on the waaaater
            myTrack1.Notes.Add(new Note(NoteName.LA));
            myTrack1.Notes.Add(new Note(NoteName.LA));
            myTrack1.Notes.Add(new Note(NoteName.LA, 1.75));
            myTrack1.Notes.Add(new Note(NoteName.FA, 1.75));
            myTrack1.Notes.Add(new Note(NoteName.SOL, 1.75));
            myTrack1.Notes.Add(new Note(NoteName.LA, 1.75));
            myTrack1.Notes.Add(new Note(NoteName.SOL));
            myTrack1.Notes.Add(new Note(NoteName.LA, 2));

            mySong.Tracks.Add(myTrack1);

            mySong.PlayInRythm(new MusicStyle(MusicStyleName.TECHNO));
        }
    }
}
