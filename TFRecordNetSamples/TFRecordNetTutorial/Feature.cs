using System.IO;
using System.Text;
using Google.Protobuf;
using Google.Protobuf.Collections;

namespace Tensorflow.Models
{
    public sealed partial class Feature
    {
        public static Feature FromFile(string filePath)
        {
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return FromStream(stream);
        }

        public static Feature FromBytes(byte[] bytes)
        {
            using var stream = new MemoryStream(bytes);
            return FromStream(stream);
        }

        public static Feature FromString(string value)
        {
            return new Feature()
            {
                BytesList = new BytesList()
                {
                    Value =
                    {
                        ByteString.CopyFrom(value, Encoding.UTF8)
                    }
                }
            };
        }
        
        public static Feature FromStream(Stream stream)
        {
            var feature = new Feature()
            {
                BytesList = new BytesList
                {
                    Value =
                    {
                        ByteString.FromStream(stream)
                    },
                }
            };
            return feature;
        }

        public static Feature FromValues(float[] values)
        {
            var feature = new Feature()
            {
                FloatList = new FloatList()
                {
                    Value = 
                    {
                        values // features.FloatList.Value.Add(values) 가 호출됨. (사실 .AddRange()와 같음)
                    }
                }
            };
            return feature;
        }

        public static Feature FromValues(long[] values)
        {
            var feature = new Feature()
            {
                Int64List = new Int64List()
                {
                    Value = 
                    {
                        values // features.FloatList.Value.Add(values) 가 호출됨. (사실 .AddRange()와 같음)
                    }
                }
            };
            return feature;
        }
        
    }
}