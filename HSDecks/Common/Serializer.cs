﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HSDecks.Common
{
    public static class Serializer
    {
        public static byte[] Serialize<T>(T obj)
        {
            MemoryStream stream = new MemoryStream();
            DataContractSerializer dcs = new DataContractSerializer(typeof(T));
            dcs.WriteObject(stream, obj);
            return stream.ToArray();
        }

        public static T Deserialize<T>(byte[] buffer)
        {
            MemoryStream stream = new MemoryStream(buffer);
            DataContractSerializer dcs = new DataContractSerializer(typeof(T));
            return (T)dcs.ReadObject(stream);
        }
    }
}
