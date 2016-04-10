using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    [DataContract]
    public class Track : IPlayInRythm
    {
        [DataMember]
        public List<INote> Notes { get; set; } = new List<INote>();


        public Track(List<INote> notes = null)
        {
            if (notes != null)
            {
                Notes = notes;
            }
        }


        public void Play(int? bpm)
        {
            foreach (var note in Notes)
            {
                note.Play(bpm);
            }
        }
        public void PlayInRythm(MusicStyle musicStyle)
        {
            foreach (var note in Notes)
            {
                note.PlayWithSound(musicStyle.BPM);
            }
        }
    }
}