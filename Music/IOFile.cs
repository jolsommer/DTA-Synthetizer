using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;

 namespace Music
{
    static class IOFile<T> where T : class
    {
        public static void Export(T obj, string path)
        {
            try
            {
                WriteObject(path, obj);
            }
            catch (SerializationException serExc)
            {
                Console.WriteLine("Serialization Failed");
                Console.WriteLine(serExc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine("The serialization operation failed: {0} StackTrace: {1}",
                exc.Message, exc.StackTrace);
            }

            finally
            {
                Console.WriteLine("Serialization ended");
                Console.ReadLine();
            }
        }
        public static T Import(string path)
        {
            try
            {
               return ReadObject(path);
            }
            catch (SerializationException serExc)
            {
                Console.WriteLine("Serialization Failed");
                Console.WriteLine(serExc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine("The serialization operation failed: {0} StackTrace: {1}",
                exc.Message, exc.StackTrace);
            }

            finally
            {
                Console.WriteLine("Serialization ended");
                Console.ReadLine();
            }

            return null;
        }

        private static void WriteObject(string fileName, T piste)
        {
            FileStream writer = new FileStream(fileName, FileMode.Create);
            DataContractSerializer ser = new DataContractSerializer(typeof(T));

            ser.WriteObject(writer, piste);
            writer.Close();
        }
        private static T ReadObject(string fileName)
        {
            Console.WriteLine("Deserializing an instance of the object.");
            FileStream fs = new FileStream(fileName, FileMode.Open);
            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

            DataContractSerializer ser = new DataContractSerializer(typeof(T));

            // Deserialize the data and read it from the instance.
            T obj = (T)ser.ReadObject(reader, true);

            reader.Close();
            fs.Close();

            return obj;
        }
    }
}
