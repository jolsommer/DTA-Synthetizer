using System;
using System.Collections.Generic;
using System.Text;
using Music;

namespace SignWaveTest
{
    public static class TrackExport
    {
        public static void Export(string path, Track track, IOccilator occilator, int sampleRate = 44100)
        {
            var sampledNotes = new List<double>();

            foreach (var note in track.Notes)
            {
                occilator.SetFrequency(note.Frequency);
                for (int i = 0; i < sampleRate * note.Duration; i++)
                {
                    sampledNotes.Add(occilator.GetNext(i));
                }
            }

            Program.SaveIntoStream(path, sampledNotes.ToArray(), sampledNotes.Count, sampleRate);
        }

        
    }
}
