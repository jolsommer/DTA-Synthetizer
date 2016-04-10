using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    public enum MusicStyleName
    {
        TANGO,
        TRIP_HOP,
        HIP_HOP,
        REGGAETON,
        SALSA,
        NEW_BEAT,
        DARK_CORE,
        HOUSE,
        TECHNO,
        TRIBE,
        FRENCHCORE,
        ELECTRO,
        FUNKY_HOUSE,
        TRANCE,
        GOA_TRANCE,
        KUDURO,
        DUBSTEP,
        HARDSTYLE,
        MAKINA,
        TECHNO_HARDCORE,
        HAPPY_HARDCORE,
        SPEEDCORE,
        SPLITTERCORE,
        EXTRACORE
    }

    public class MusicStyle
    {
        public MusicStyleName StyleName { get; protected set; }
        public string Name { get; protected set; }
        public int BPMMin { get; protected set; }
        public int BPMMax { get; protected set; }

        int bpm;
        public int BPM
        {
            get
            {
                return bpm;
            }
            protected set
            {
                bpm = value;
                if (bpm < BPMMin || bpm > BPMMax)
                    throw new Exception("incorrect bpm for this music style");
            }
        }

        public MusicStyle(MusicStyleName musicStyleName, int? bpm = null)
        {
            StyleName = musicStyleName;

            // set BPMMin, BPMMAx and Name 
            switch (musicStyleName)
            {
                case MusicStyleName.TANGO:
                    Name = "Tango";
                    BPMMin = 50;
                    BPMMax = 56;           
                    break;
                case MusicStyleName.TRIP_HOP:
                    Name = "Trip Hop";
                    BPMMin = 60;
                    BPMMax = 120;
                    break;
                case MusicStyleName.HIP_HOP:
                    Name = "Hip-Hop";
                    BPMMin = 80;
                    BPMMax = 100;
                    break;
                case MusicStyleName.REGGAETON:
                    Name = "Raggaetón";
                    BPMMin = 80;
                    BPMMax = 90;
                    break;
                case MusicStyleName.SALSA:
                    Name = "Salsa";
                    BPMMin = 150;
                    BPMMax = 220;
                    break;
                case MusicStyleName.NEW_BEAT:
                    Name = "New Beat";
                    BPMMin = 110;
                    BPMMax = 120;
                    break;
                case MusicStyleName.DARK_CORE:
                    Name = "Darkcore";
                    BPMMin = 120;
                    BPMMax = 150;
                    break;
                case MusicStyleName.HOUSE:
                    Name = "House";
                    BPMMin = 124;
                    BPMMax = 130;
                    break;
                case MusicStyleName.TECHNO:
                    Name = "Techno";
                    BPMMin = 125;
                    BPMMax = 145;
                    break;
                case MusicStyleName.TRIBE:
                    Name = "Tribe";
                    BPMMin = 145;
                    BPMMax = 180;
                    break;
                case MusicStyleName.FRENCHCORE:
                    Name = "Frenchcore";
                    BPMMin = 185;
                    BPMMax = 205;
                    break;
                case MusicStyleName.ELECTRO:
                    Name = "Electro";
                    BPMMin = 126;
                    BPMMax = 135;
                    break;
                case MusicStyleName.FUNKY_HOUSE:
                    Name = "Funky House";
                    BPMMin = 128;
                    BPMMax = 136;
                    break;
                case MusicStyleName.TRANCE:
                    Name = "Trance";
                    BPMMin = 128;
                    BPMMax = 140;
                    break;
                case MusicStyleName.GOA_TRANCE:
                    Name = "Goa Trance";
                    BPMMin = 130;
                    BPMMax = 146;
                    break;
                case MusicStyleName.KUDURO:
                    Name = "Kuduro";
                    BPMMin = 135;
                    BPMMax = 145;
                    break;
                case MusicStyleName.DUBSTEP:
                    Name = "Dubstep";
                    BPMMin = 138;
                    BPMMax = 145;
                    break;
                case MusicStyleName.HARDSTYLE:
                    Name = "Hardstyle";
                    BPMMin = 140;
                    BPMMax = 160;
                    break;
                case MusicStyleName.MAKINA:
                    Name = "Makina";
                    BPMMin = 150;
                    BPMMax = 190;
                    break;
                case MusicStyleName.TECHNO_HARDCORE:
                    Name = "Techno Hardcore";
                    BPMMin = 165;
                    BPMMax = 220;
                    break;
                case MusicStyleName.HAPPY_HARDCORE:
                    Name = "Happy Hardcore";
                    BPMMin = 180;
                    BPMMax = 200;
                    break;
                case MusicStyleName.SPEEDCORE:
                    Name = "Speedcore";
                    BPMMin = 195;
                    BPMMax = 245;
                    break;
                case MusicStyleName.SPLITTERCORE:
                    Name = "Splittercore";
                    BPMMin = 500;
                    BPMMax = 1000;
                    break;
                case MusicStyleName.EXTRACORE:
                    Name = "Extracore";
                    BPMMin = 1000;
                    BPMMax = 2000;
                    break;
                default:
                    throw new Exception("unknown music style");
            }

            if (bpm.HasValue)
            {
                if (bpm < BPMMin)
                    throw new Exception("bpm too low for this style");
                if (bpm > BPMMax)
                    throw new Exception("bpm too high for this style");
                BPM = bpm.Value;
            }
            else
            {
                BPM = (BPMMin + BPMMax) / 2;
            }         
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
