using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hash
{
    internal class SHA3
    {
        private static readonly ulong[] RC = new ulong[]
    {
        0x0000000000000001, 0x0000000000008082, 0x800000000000808A,
        0x8000000080008000, 0x000000000000808B, 0x0000000080000001,
        0x8000000080008081, 0x8000000000008009, 0x000000000000008A,
        0x0000000000000088, 0x0000000080008009, 0x000000008000000A,
        0x000000008000808B, 0x800000000000008B, 0x8000000000008089,
        0x8000000000008003, 0x8000000000008002, 0x8000000000000080,
        0x000000000000800A, 0x800000008000000A, 0x8000000080008081,
        0x8000000000008080, 0x0000000080000001, 0x8000000080008008
    };

        private static readonly int[,] RHO = new int[5, 5]
        {
        {  0,  1, 62, 28, 27 },
        { 36, 44,  6, 55, 20 },
        {  3, 10, 43, 25, 39 },
        { 41, 45, 15, 21,  8 },
        { 18,  2, 61, 56, 14 }
        };

        private static readonly int[] POW_2 = new int[6] { 1, 2, 4, 8, 16, 32 };

        public static byte[] ComputeHash(byte[] message, int outputSize = 32)
        {
            int rate = 1600 - (2 * outputSize);
            ulong[] state = new ulong[25];
            int blockSize = rate / 8;
            byte[] paddedMessage = PadMessage(message, blockSize);
            int numBlocks = paddedMessage.Length / blockSize;

            for (int i = 0; i < numBlocks; i++)
            {
                for (int j = 0; j < blockSize; j++)
                {
                    int position = i * blockSize + j;
                    if(state.Length <= j % 5 + 5 * (j / 5)) continue;
                    state[j % 5 + 5 * (j / 5)] ^= BitConverter.ToUInt64(paddedMessage, position);
                }
                state = KeccakF1600(state);
            }

            byte[] hash = new byte[outputSize];
            for (int i = 0; i < outputSize; i++)
            {
                if(state.Length <= i % 5 + 5 * (i / 5)) continue;
                hash[i] = (byte)(state[i % 5 + 5 * (i / 5)] >> (8 * (i % 8)));
            }
            return hash;
        }

        private static byte[] PadMessage(byte[] message, int blockSize)
        {
            int remainder = message.Length % blockSize;
            int paddingLength = (remainder == 0) ? blockSize : blockSize - remainder;
            byte[] paddedMessage = new byte[message.Length + paddingLength];
            Array.Copy(message, 0, paddedMessage, 0, message.Length);
            paddedMessage[message.Length] = 0x06;
            paddedMessage[paddedMessage.Length - 1] |= 0x80;
            return paddedMessage;
        }

        private static ulong[] KeccakF1600(ulong[] state)
        {
            for (int round = 0; round < 24; round++)
            {
                ulong[] C = new ulong[5];
                for (int x = 0; x < 5; x++)
                {
                    C[x] = state[x] ^ state[x + 5] ^ state[x + 10] ^ state[x + 15] ^ state[x + 20];
                }

                ulong[] D = new ulong[5];
                for (int x = 0; x < 5; x++)
                {
                    D[x] = C[(x + 4) % 5] ^ RotateLeft(C[(x + 1) % 5], 1);
                }

                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        state[x + 5 * y] ^= D[x];
                    }
                }

                ulong[] B = new ulong[25];
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        B[y + 5 * ((2 * x + 3 * y) % 5)] = RotateLeft(state[x + 5 * y], RHO[x, y]);
                    }
                }

                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        state[x + 5 * y] = B[x + 5 * y] ^ (~B[(x + 1) % 5 + 5 * y] & B[(x + 2) % 5 + 5 * y]);
                    }
                }

                state[0] ^= RC[round];
            }

            return state;
        }

        private static ulong RotateLeft(ulong value, int offset)
        {
            return (value << offset) | (value >> (64 - offset));
        }
    }

    public static class Sha3Extensions
    {
        public static string ToHexString(this byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
    }
}
