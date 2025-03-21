using System.Text;

using ZX.Core.Cpu;

namespace ZX.Core.FileFormats;

public class TapFileFormat
{
    public (ushort address, byte[]) Load(string filename)
        => Load(File.ReadAllBytes(filename));

    public (ushort address, byte[]) Load(byte[] data)
    {
        Console.WriteLine($"[===========]");

        var memory = new byte[65536];
        var address = (ushort)0;

        var offset = 0;

        while (offset < data.Length)
        {
            var length = BitConverter.ToUInt16(data, offset);
            offset += 2;

            var flag = (Flag)data[offset];

            Console.WriteLine($"{flag} {length} ===========");

            if (flag == Flag.Header)
            {
                var dataType = (DataType)data[offset + 1];
                var fileName = Encoding.ASCII.GetString(data, offset + 2, 10).TrimEnd();

                Console.WriteLine($"  {fileName} ({dataType})");

                var dataLength = BitConverter.ToUInt16(data, offset + 12);

                Console.WriteLine($"  dataLength = {dataLength} ({dataLength:X})");

                if (dataType == DataType.Bytes)
                {
                    //var loadingAddress = BitConverter.ToUInt16(data, offset + 14);

                    //address = loadingAddress;

                    //Console.WriteLine($"  loadingAddress = {loadingAddress} ({loadingAddress:X})");

                    offset += length;

                    ////var anotherDataLength = BitConverter.ToUInt16(data, offset); //two bytes bigger

                    //Array.Copy(data, offset + 3, memory, loadingAddress, dataLength);

                    offset += dataLength + 4;
                }
                else if (dataType == DataType.Program)
                {
                    var autoStartLine = BitConverter.ToUInt16(data, offset + 14);

                    Console.WriteLine($"  autoStartLine = {autoStartLine} ({autoStartLine:X})");

                    offset += length;

                    var loadingAddress = 0x5CCB;

                    Array.Copy(data, offset, memory, loadingAddress, dataLength);

                    offset += dataLength + 4;
                }
            }
            else
            {
                throw new NotSupportedException();
                //Array.Copy(data, offset + 1, memory, 0/*???*/, dataBlockLength - 2);
                //offset += length;
            }
        }

        return (address, memory);
    }

    public enum Flag
    {
        Header = 0,
        Data = 255,
    }

    public enum DataType
    {
        Program = 0,
        NumArray = 1,
        CharArray = 2,
        Bytes = 3,
    }
}
