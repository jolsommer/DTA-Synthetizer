using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    public class MusicStyleFactory
    {
        static Dictionary<string, MusicStyle> styles = new Dictionary<string, MusicStyle>();

        private static MusicStyleFactory instance;
        public static MusicStyleFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MusicStyleFactory();
                }
                return instance;
            }
        }

        private MusicStyleFactory()
        {
            styles.Add("dark_core", new MusicStyle(MusicStyleName.DARK_CORE));
            styles.Add("dubstep", new MusicStyle(MusicStyleName.DUBSTEP));
            styles.Add("electro", new MusicStyle(MusicStyleName.ELECTRO));
            styles.Add("extracore", new MusicStyle(MusicStyleName.EXTRACORE));
            styles.Add("frenchcore", new MusicStyle(MusicStyleName.FRENCHCORE));
            styles.Add("funky_house", new MusicStyle(MusicStyleName.FUNKY_HOUSE));
            styles.Add("goa_trance", new MusicStyle(MusicStyleName.GOA_TRANCE));
            styles.Add("happy_hardcore", new MusicStyle(MusicStyleName.HAPPY_HARDCORE));
            styles.Add("hardstyle", new MusicStyle(MusicStyleName.HARDSTYLE));
            styles.Add("hip_hop", new MusicStyle(MusicStyleName.HIP_HOP));
            styles.Add("house", new MusicStyle(MusicStyleName.HOUSE));
            styles.Add("kuduro", new MusicStyle(MusicStyleName.KUDURO));
            styles.Add("makina", new MusicStyle(MusicStyleName.MAKINA));
            styles.Add("new_beat", new MusicStyle(MusicStyleName.NEW_BEAT));
            styles.Add("raggaeton", new MusicStyle(MusicStyleName.REGGAETON));
            styles.Add("salsa", new MusicStyle(MusicStyleName.SALSA));
            styles.Add("speedcore", new MusicStyle(MusicStyleName.SPEEDCORE));
            styles.Add("splittercore", new MusicStyle(MusicStyleName.SPLITTERCORE));
            styles.Add("tango", new MusicStyle(MusicStyleName.TANGO));
            styles.Add("techno", new MusicStyle(MusicStyleName.TECHNO));
            styles.Add("techno_hardcore", new MusicStyle(MusicStyleName.TECHNO_HARDCORE));
            styles.Add("trance", new MusicStyle(MusicStyleName.TRANCE));
            styles.Add("tribe", new MusicStyle(MusicStyleName.TRIBE));
            styles.Add("trip_hop", new MusicStyle(MusicStyleName.TRIP_HOP));
        }

        public MusicStyle GetMusicStyle(MusicStyleName musicStyle)
        {
            var result = from style in styles
                         where style.Value.StyleName == musicStyle
                         select style.Value;

            return result.First();
        }
        public MusicStyle GetMusicStyle(string musicStyle)
        {
            var result = from style in styles
                         where style.Key == musicStyle
                         select style.Value;

            return result.First();
        }
    }
}
