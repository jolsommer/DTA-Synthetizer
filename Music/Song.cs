using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    public class Song : IPlayInRythm
    {
        public List<Track> Tracks { get; set; } = new List<Track>();


        public Song(List<Track> tracks = null)
        {
            if (tracks!=null)
            {
                Tracks = tracks;
            }
        }


        public void Play(int? bpm)
        {
            foreach (var track in Tracks)
            {
                track.Play(bpm);
            }
        }
        public void PlayInRythm(MusicStyle musicStyle)
        {
            foreach (var track in Tracks)
            {
                track.PlayInRythm(musicStyle);
            }
        }
    }
}
