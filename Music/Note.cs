using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music
{
    public enum NoteTuning
    {
        LA_432 = 432,
        LA_434 = 434,
        LA_436 = 436,
        LA_438 = 438,
        LA_440 = 440,
        LA_442 = 442,
        LA_444 = 444,
        LA_446 = 446,
    }
    public enum NoteName
    {
        DO = 0,
        DO_SHARP = 1,
        RE = 2,
        RE_SHARP = 3,
        MI = 4,
        FA = 5,
        FA_SHARP = 6,
        SOL = 7,
        SOL_SHARP = 8,
        LA = 9,
        LA_SHARP = 10,
        SI = 11
    }

    [DataContract]
    public class Note : INote
    {
        static Dictionary<string, int> frequencies = new Dictionary<string, int>();

        public const double SemiToneFactor = 1.059463094359;
        public const int BaseOctave = 4;
        public const int SemiTonesPerOctave = 12;

        [DataMember]
        public string Name { get; protected set; }
        [DataMember]
        public int Octave { get; protected set; }
        [DataMember]
        public NoteTuning Tuning { get; protected set; }

        [DataMember]
        public int Frequency { get; protected set; }
        [DataMember]
        public double Duration { get; set; }

        [DataMember]
        public noteplayer NotePlayer { get; set; }


        public Note(NoteName noteName, double duration = 1, int octave = BaseOctave, NoteTuning tuning = NoteTuning.LA_440)
        {
            if(octave < 1 || (octave == 1 && noteName < NoteName.RE))
            {
                throw new Exception("Can't create a note lower than RE1");
            }

            Name = Enum.GetName(typeof(NoteName), noteName).ToLower().Replace("_sharp", "#");
            Octave = octave;
            Tuning = tuning;

            Frequency = GetFrequency(noteName, octave, tuning);
            Duration = duration;

            NotePlayer = PlayWithTextAndSound;
        }


        static string GetNoteKey(NoteName noteName, int octave = BaseOctave, NoteTuning tuning = NoteTuning.LA_440)
        {
            var sb = new StringBuilder();
            sb.Append(noteName.ToString());
            sb.Append(octave.ToString());
            sb.Append(tuning.ToString());
            return sb.ToString();
        }
        static int GetSemiTone(NoteName noteName, int octave)
        {
            return octave * SemiTonesPerOctave + (int)noteName;
        }
        static int GetFrequency(NoteName noteName, int octave = BaseOctave, NoteTuning tuning = NoteTuning.LA_440)
        {
            string noteKey = GetNoteKey(noteName, octave, tuning);

            int frequency;
            if (!frequencies.TryGetValue(noteKey, out frequency))
            {
                int baseFrequency;
                string baseNoteKey = GetNoteKey(NoteName.LA, BaseOctave, tuning);
                if (!frequencies.TryGetValue(baseNoteKey, out baseFrequency))
                {
                    baseFrequency = (int)tuning;
                    frequencies[baseNoteKey] = baseFrequency;
                }

                int semiTone = GetSemiTone(noteName, octave);
                int baseSemitone = GetSemiTone(NoteName.LA, BaseOctave);
                int deltaSemiTones = semiTone - baseSemitone;
            
                frequency = (int)Math.Round(baseFrequency * Math.Pow(SemiToneFactor, deltaSemiTones));
                if (frequency < 37)
                {
                    throw new Exception("Console.Beep won't work .. so don't make a note below RE_octave1_LA_446");
                }
                frequencies[noteKey] = frequency;
            }

            return frequency;
        }

        public void Play(int? bpm = null)
        {
            NotePlayer(bpm); 
        }
        public void PlayWithSound(int? bpm)
        {
            int durationFactor = (bpm.HasValue) ?
            (int)1000 * 60 / bpm.Value :
            (int)1000;

            Console.Beep((int)Frequency, (int)(Duration*durationFactor));
        }
        public void PlayWithText(int? bpm)
        {
            Console.Write(this);
        }
        public void PlayWithTextAndSound(int? bpm)
        {
            PlayWithText(bpm);
            PlayWithSound(bpm);
        }

        public override string ToString()
        {
            return Name;
        }
    }




}
