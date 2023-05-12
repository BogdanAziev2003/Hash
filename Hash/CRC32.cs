using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash
{
    internal class CRC32
    {
        private uint[] crcTable;

        public CRC32()
        {
            crcTable = GenerateCRCTable();
        }

        private static uint[] GenerateCRCTable()
        {
            uint[] table = new uint[256];
            const uint poly = 0xedb88320;
            for (uint i = 0; i < 256; i++)
            {
                uint entry = i;
                for (int j = 0; j < 8; j++)
                {
                    if ((entry & 1) == 1)
                    {
                        entry = (entry >> 1) ^ poly;
                    }
                    else
                    {
                        entry >>= 1;
                    }
                }
                table[i] = entry;
            }
            return table;
        }

        public uint ComputeChecksum(byte[] bytes)
        {
            uint crc = 0xffffffff;
            foreach (byte b in bytes)
            {
                crc = (crc >> 8) ^ crcTable[(crc & 0xff) ^ b];
            }
            return crc ^ 0xffffffff;
        }
    }
}
