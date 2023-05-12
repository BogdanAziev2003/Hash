using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash
{
    public class MD5
    {
        private uint[] IV = new uint[] { 0x67452301, 0xefcdab89, 0x98badcfe, 0x10325476 };
        private uint[] T = new uint[64];
        private uint[] X = new uint[16];

        public  string Hash(string message)
        {
            // Преобразование сообщения в байты
            byte[] messageBytes = StringToBytes(message);

            // Добавление дополнительных битов
            byte[] paddedMessage = PadMessage(messageBytes);

            // Разбивка сообщения на блоки и хеширование каждого блока
            for (int i = 0; i < paddedMessage.Length; i += 64)
            {
                ProcessBlock(paddedMessage, i);
            }

            // Конечный хеш
            return HashToString(IV);
        }

        private byte[] PadMessage(byte[] message)
        {
            // Добавление дополнительных битов
            int initialLength = message.Length;
            int paddingLength = (initialLength % 64 < 56) ? 56 - initialLength % 64 : 120 - initialLength % 64;
            byte[] paddedMessage = new byte[initialLength + paddingLength + 8];
            Array.Copy(message, paddedMessage, initialLength);
            paddedMessage[initialLength] = 0x80;
            Array.Copy(BitConverter.GetBytes((ulong)initialLength * 8), 0, paddedMessage, paddedMessage.Length - 8, 8);
            return paddedMessage;
        }

        private void ProcessBlock(byte[] message, int offset)
        {
            // Инициализация переменных
            Array.Copy(IV, X, IV.Length);
            for (int i = 0; i < 64; i++)
            {
                T[i] = (uint)(Math.Pow(2, 32) * Math.Abs(Math.Sin(i + 1)));
            }

            // Разбиение блока на 16 слов
            for (int i = 0; i < 16; i++)
            {
                X[i] = BytesToUInt(message, offset + i * 4);
            }

            // Шаги 1-4
            uint A = X[0];
            uint B = X[1];
            uint C = X[2];
            uint D = X[3];

            for (int i = 0; i < 64; i++)
            {
                uint F, g;

                if (i < 16)
                {
                    F = (B & C) | ((~B) & D);
                    g = (uint)i;
                }
                else if (i < 32)
                {
                    F = (D & B) | ((~D) & C);
                    g = (uint)((5 * i + 1) % 16);
                }

                else if (i < 48)
                {
                    F = B ^ C ^ D;
                    g = (uint)((3 * i + 5) % 16);
                }
                else
                {
                    F = C ^ (B | (~D));
                    g = (uint)((7 * i) % 16);
                }

                F = F + A + T[i] + X[g];
                A = D;
                D = C;
                C = B;
                B = B + LeftRotate(F, (int)(i % 4 * 4 + i % 16 / 4 * 8));
            }

            // Добавление результатов в итоговый хеш
            IV[0] += A;
            IV[1] += B;
            IV[2] += C;
            IV[3] += D;
        }

        private uint BytesToUInt(byte[] bytes, int offset)
        {
            // Чтение четырех байтов и преобразование в UInt32
            return (uint)(bytes[offset] | (bytes[offset + 1] << 8) | (bytes[offset + 2] << 16) | (bytes[offset + 3] << 24));
        }

        private byte[] StringToBytes(string message)
        {
            // Преобразование строки в массив байтов
            byte[] messageBytes = new byte[message.Length * 2];
            for (int i = 0; i < message.Length; i++)
            {
                byte[] charBytes = BitConverter.GetBytes(message[i]);
                messageBytes[i * 2] = charBytes[0];
                messageBytes[i * 2 + 1] = charBytes[1];
            }
            return messageBytes;
        }

        private string HashToString(uint[] hash)
        {
            // Преобразование хеша в строку
            byte[] hashBytes = new byte[16];
            for (int i = 0; i < 4; i++)
            {
                byte[] temp = BitConverter.GetBytes(hash[i]);
                Array.Reverse(temp);
                Array.Copy(temp, 0, hashBytes, i * 4, 4);
            }
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        private uint LeftRotate(uint value, int amount)
        {
            // Циклический сдвиг влево
            return (value << amount) | (value >> (32 - amount));
        }

    }
}
