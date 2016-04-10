
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace SignWaveTest
{
    class Program
    {
        static void Main(string[] args)
        {

            int sampleRate = 44100;

            List<double> data = new List<double>();

            SquareOccilator o = new SquareOccilator(sampleRate);
            SineOccilator j = new SineOccilator(sampleRate);
            SawToothOccilator s = new SawToothOccilator(sampleRate);
            RoyalSawToothOccilator rs = new RoyalSawToothOccilator(sampleRate);
            SawToothOccilatorSteadyDetunable detunableOccilator = new SawToothOccilatorSteadyDetunable(sampleRate);
            detunableOccilator.SetDetune(0.05);


            rs.SetFrequency(GetNoteFrequnecy.C);
            for (int i = 0; i < sampleRate * 2; i++)
            {
                data.Add(rs.GetNext(i));
            }
            
            SaveIntoStream("test", data.ToArray(), data.Count, sampleRate);
            Console.ReadLine();

        }

        public static FileStream SaveIntoStream(string fileName, double[] sampleData, long sampleCount, int samplesPerSecond)
        {
            // Export
            FileStream stream = File.Create(fileName + ".wav");
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);
            int RIFF = 0x46464952;
            int WAVE = 0x45564157;
            int formatChunkSize = 16;
            int headerSize = 8;
            int format = 0x20746D66;
            short formatType = 1;
            short tracks = 2;
            short bitsPerSample = 16;
            short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
            int bytesPerSecond = samplesPerSecond * frameSize;
            int waveSize = 4;
            int data = 0x61746164;
            int samples = (int)sampleCount;
            int dataChunkSize = samples * frameSize;
            int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
            writer.Write(RIFF);
            writer.Write(fileSize);
            writer.Write(WAVE);
            writer.Write(format);
            writer.Write(formatChunkSize);
            writer.Write(formatType);
            writer.Write(tracks);
            writer.Write(samplesPerSecond);
            writer.Write(bytesPerSecond);
            writer.Write(frameSize);
            writer.Write(bitsPerSample);
            writer.Write(data);
            writer.Write(dataChunkSize);

            double sample_l;
            short sl;
            for (int i = 0; i < sampleCount; i++)
            {
                sample_l = sampleData[i] * 30000.0;
                if (sample_l < -32767.0f) { sample_l = -32767.0f; }
                if (sample_l > 32767.0f) { sample_l = 32767.0f; }
                sl = (short)sample_l;
                stream.WriteByte((byte)(sl & 0xff));
                stream.WriteByte((byte)(sl >> 8));
                stream.WriteByte((byte)(sl & 0xff));
                stream.WriteByte((byte)(sl >> 8));
            }

            stream.Close();

            //BufferedWaveProvider bwp = new BufferedWaveProvider(new WaveFormat());
            //stream.Position = 0;
            //BinaryReader br = new BinaryReader(stream);
   

            //for (int i = 0; i < stream.Length / 4; i++)
            //{
            //    bwp.AddSamples(br.ReadBytes(4), (int)stream.Position, 4);
            //}
            
            //WaveOut wo = new WaveOut();
            //wo.Init(bwp);
            //wo.Play();

            return stream;
        }

    }

    public static class GetNoteFrequnecy
    {
        public static double C = 261.6;
        public static double Csharp = 277.2;
        public static double D = 293.7;
        public static double Dsharp = 311.1;
        public static double E = 329.6;
        public static double F = 349.2;
        public static double Fsharp = 370.0;
        public static double G = 392.0;
        public static double Gsharp = 415.3;
        public static double A = 440.0;
        public static double Asharp = 466.2;
        public static double B = 493.9;
        public static double hC = 523.2;

    }


}
