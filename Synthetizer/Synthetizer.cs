using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music;
using SignWaveTest;

namespace Synthetizer
{
    public enum SynthetizerMode
    {
        END = 0,
        MENU = 1,
        PLAY = 2,
    }

    class Synthetizer
    {
        Dictionary<ConsoleKey, Action> mapping = new Dictionary<ConsoleKey, Action>();
        Track track = new Track();

        public const int BaseOctave = 4;
        int octave = BaseOctave;
        public int Octave
        {
            get
            {
                return octave;
            }
            protected set
            {
                octave = (value < 2) ? 2 : (value > 9) ? 9 : value;
            }
        }

        public bool Recording { get; protected set; }

        public SynthetizerMode Mode { get; protected set; }

        public const NoteTuning BaseTuning = NoteTuning.LA_440;
        NoteTuning tuning = BaseTuning;
        public NoteTuning Tuning
        {
            get
            {
                return tuning;
            }
            protected set
            {
                tuning = (value < NoteTuning.LA_432) ? NoteTuning.LA_432 : (value > NoteTuning.LA_446) ? NoteTuning.LA_446 : value;
            }
        } 

        double noteDuration = 1;
        public double NoteDuration {
            get
            {
                return noteDuration;
            }
            protected set
            {
                noteDuration = (value < 0.125) ? 0.125 : (value > 12) ? 12 : value;  
            }
        }

        public MusicStyle CurrentMusicStyle { get; protected set; }


        public Synthetizer()
        {
            MapPlayMode();
            Recording = false;
            CurrentMusicStyle = MusicStyleFactory.Instance.GetMusicStyle(MusicStyleName.TECHNO);
            
        }


        public void Launch()
        {
            ChangeMode(SynthetizerMode.MENU);
            while(Mode != SynthetizerMode.END)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                Execute(key);
            }
        }

        void ChangeMode(SynthetizerMode mode)
        {
            switch (mode)
            {
                case SynthetizerMode.END:
                    break;
                case SynthetizerMode.MENU:
                    MapMenuMode();
                    MenuModeInfo();
                    break;
                case SynthetizerMode.PLAY:
                    MapPlayMode();
                    PlayModeInfo();
                    break;
                default:
                    break;
            }

            Mode = mode;
        }
        //mapping
        void MapMenuMode()
        {
            mapping.Clear();
            mapping[ConsoleKey.P] = () => { ChangeMode(SynthetizerMode.PLAY); };
            mapping[ConsoleKey.H] = () => { ShowPlayModeHelp(); };
            //mapping[ConsoleKey.S] = () => { GoToMusicStyleSelection(); };
            mapping[ConsoleKey.Escape] = () => { ChangeMode(SynthetizerMode.END); };
        }
        void MapPlayMode()
        {
            mapping.Clear();
            mapping[ConsoleKey.Q] = () => { PlayNote(NoteName.DO); };
            mapping[ConsoleKey.Z] = () => { PlayNote(NoteName.DO_SHARP); };
            mapping[ConsoleKey.S] = () => { PlayNote(NoteName.RE); };
            mapping[ConsoleKey.E] = () => { PlayNote(NoteName.RE_SHARP); };
            mapping[ConsoleKey.D] = () => { PlayNote(NoteName.MI); };
            mapping[ConsoleKey.F] = () => { PlayNote(NoteName.FA); };
            mapping[ConsoleKey.T] = () => { PlayNote(NoteName.FA_SHARP); };
            mapping[ConsoleKey.G] = () => { PlayNote(NoteName.SOL); };
            mapping[ConsoleKey.Y] = () => { PlayNote(NoteName.SOL_SHARP); };
            mapping[ConsoleKey.H] = () => { PlayNote(NoteName.LA); };
            mapping[ConsoleKey.U] = () => { PlayNote(NoteName.LA_SHARP); };
            mapping[ConsoleKey.J] = () => { PlayNote(NoteName.SI); };
            mapping[ConsoleKey.A] = () => { PlaySilence(); };

            mapping[ConsoleKey.Enter] = () => { Record(); PlayModeInfo(); };
            mapping[ConsoleKey.Escape] = () => { ChangeMode(SynthetizerMode.MENU); };

            mapping[ConsoleKey.UpArrow] = () => { NoteDuration += 0.125; PlayModeInfo(); };
            mapping[ConsoleKey.DownArrow] = () => { NoteDuration -= 0.125; PlayModeInfo(); };
            mapping[ConsoleKey.LeftArrow] = () => { Tuning -= 2; PlayModeInfo(); };
            mapping[ConsoleKey.RightArrow] = () => { Tuning += 2; PlayModeInfo(); };
            mapping[ConsoleKey.W] = () => { Octave -= 1; PlayModeInfo(); };
            mapping[ConsoleKey.X] = () => { Octave += 1; PlayModeInfo(); };
        }
        //infos
        void MenuModeInfo()
        {
            Console.Clear();
            Console.WriteLine("<('-'<) SYNTX TEASER 2000 (>'-')>");
            Console.WriteLine("\n[P] play mode\n[H] play mode help");
            Console.WriteLine("\n[-L] load track\n[-S] save track");
            Console.WriteLine("\n[-E] export track\n[-X] play track");
            Console.WriteLine("\n[Esc] quit");
        }
        void PlayModeInfo()
        {
            var sb = new StringBuilder();
            
            Console.Clear();
            if (Recording)
            {
                sb.Append("[Record] :");
            }
            else
            {
                sb.Append("[Play] :");
            }
            sb.AppendLine("tune:" + Tuning + " oct:" + Octave);
            sb.AppendLine(":note_duration " + NoteDuration);
            sb.AppendLine(":music_style " + CurrentMusicStyle + "(" + CurrentMusicStyle.BPM + "bpm)");
            sb.AppendLine();
            sb.Append("Track partition: ");
            foreach (var note in track.Notes)
            {
                sb.Append(note.Name);
                if (note is Note)
                {
                    Note n = (Note)note;
                    sb.Append(n.Octave);
                }
                sb.Append(" ");                 
            }
            Console.Write(sb.ToString());
        }
        void ShowPlayModeHelp()
        {
            Console.Clear();
            Console.WriteLine("[A] Silence");
            Console.WriteLine("[Q, Z, S, E, D, F, T, G, Y, H, U, J] Do Do# Re Re# Mi Fa Fa# Sol Sol# La La# Si");
            Console.WriteLine("[W, X] Octave [-, +]");
            Console.WriteLine("[Left, Right] Tune [-, +]");
            Console.WriteLine("[Down, Up] Note duration [-, +]");
            Console.WriteLine("[Esc] Return to menu");
            Console.ReadKey();
        }

        void Execute(ConsoleKey key)
        {
            Action execute;
            if (mapping.TryGetValue(key, out execute))
                execute();
        }
        void Record()
        {
            if (Recording)
            {
                TrackExport.Export("tmpTrack", track, new SineOccilator(44100));
            }
            Recording = !Recording;
        }
         
        MusicStyle AskForMusicStyle()
        {
            Console.Write("Choose a music style (help):");
            string in_str = Console.ReadLine();
            MusicStyleName style;
            while (Enum.TryParse<MusicStyleName>(in_str, true, out style))
            {
                if (in_str.ToLower() == "help")
                {
                    ShowMusicStyles();
                    Console.Write("Choose a music style (help):");
                }
                else
                {
                    Console.Write("This isn't a valid music style:");
                }
                in_str = Console.ReadLine();
            }

            return MusicStyleFactory.Instance.GetMusicStyle(style);
        }
        void ShowMusicStyles()
        {
            foreach (var mstyle in Enum.GetNames(typeof(MusicStyleName)))
            {
                Console.Write(mstyle + " ");
            }
            Console.WriteLine();
        }

        void PlayNote(NoteName noteName)
        {
            var note = new Note(noteName, NoteDuration, Octave, Tuning);
            note.NotePlayer = note.PlayWithSound;

            if (Recording)
            {
                track.Notes.Add(note);
                Console.Write(note.Name+note.Octave+" ");
            }

            note.Play(CurrentMusicStyle.BPM);
        }
        void PlaySilence()
        {
            var note = new Silence(NoteDuration);
            note.NotePlayer = note.PlayWithSound;

            if (Recording)
            {
                track.Notes.Add(note);
                Console.Write(note.Name+" ");
            }

            note.Play(CurrentMusicStyle.BPM);
        }

        void ResetTrack()
        {
            track.Notes.Clear();
        }
    }
}
