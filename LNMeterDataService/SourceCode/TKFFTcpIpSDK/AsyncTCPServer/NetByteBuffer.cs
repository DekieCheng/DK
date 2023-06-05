using System;
using System.Threading;

namespace TKFFTcpIpSDK.Infrastructure
{
    public class NetByteBuffer
    {
        //byte buffer area
        private readonly byte[] buf;
        private readonly byte[] byfTemp;
        private int readIndex = 0;
        private int writeIndex = 0;
        private int markReadIndex = 0;
        private int markWirteIndex = 0;

        private NetByteBuffer(int capacity)
        {
            buf = new byte[capacity];
            byfTemp = new byte[capacity];
        }

        public static NetByteBuffer Allocate(int capacity)
        {
            return new NetByteBuffer(capacity);
        }

        public NetByteBuffer WriteBytes(byte[] bytes, int startIndex, int length)
        {
            try
            {
                Monitor.Enter(this);
                int offset = length - startIndex;
                int total = offset + writeIndex;
                Buffer.BlockCopy(bytes, startIndex, buf, writeIndex, length);
                writeIndex = total;
            }
            catch (Exception exp)
            {
                Console.WriteLine($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!   Receive buffer area is out of the max capacity! Exp = {exp}");
            }
            finally
            {
                Monitor.Exit(this);
            }
            return this;
        }

        public NetByteBuffer WriteBytes(byte[] bytes, int length)
        {
            return WriteBytes(bytes, 0, length);
        }

        public NetByteBuffer WriteBytes(byte[] bytes)
        {
            return WriteBytes(bytes, 0, bytes.Length);
        }

        public byte ReadByte()
        {
            byte b = buf[readIndex];
            readIndex++;
            return b;
        }

        readonly byte[] intBytes = new byte[4];
        public int ReadInt()
        {
            Buffer.BlockCopy(buf, readIndex, intBytes, 0, 4);
            readIndex += 4;

            return BitConverter.ToInt32(intBytes, 0);
        }

        public void ReadBytes(byte[] disbytes, int disstart, int len)
        {
            int size = disstart + len;
            for (int i = disstart; i < size; i++)
            {
                disbytes[i] = buf[readIndex];
                readIndex++;
            }
        }

        public void Clear(bool pReset0 = false)
        {
            if (pReset0)
                Array.Clear(buf, 0, buf.Length);
            readIndex = 0;
            writeIndex = 0;
            markReadIndex = 0;
            markWirteIndex = 0;
        }

        public void ReSetBuf()
        {
            var L = ReadableBytes();
            if (L > 0)
            {
                ReadBytes(byfTemp, 0, ReadableBytes()); //copy temp buffer area
                Clear(); //clear
                WriteBytes(byfTemp, L); //reset
                MarkReaderIndex();
            }
            else
                Clear();
        }

        public void SetReaderIndex(int index)
        {
            if (index < 0) return;
            readIndex = index;
        }

        public int MarkReaderIndex()
        {
            markReadIndex = readIndex;
            return markReadIndex;
        }

        public void MarkWriterIndex()
        {
            markWirteIndex = writeIndex;
        }

        public void ResetReaderIndex()
        {
            readIndex = markReadIndex;
        }

        public void ResetWriterIndex()
        {
            writeIndex = markWirteIndex;
        }

        public int ReadableBytes()
        {
            return writeIndex - readIndex;
        }
    }
}
