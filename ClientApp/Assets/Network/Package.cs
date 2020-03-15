using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ClientApp.Network
{
    public class Package
    {
        private List<byte> mainBuffer;
        private byte[] readableBuffer;
        private int readPosition;

        public Package()
        {
            mainBuffer = new List<byte>();
            readPosition = 0;
        }


        public Package(int id)
        {
            mainBuffer = new List<byte>();
            readPosition = 0; 
            Write(id);
        }


        public byte[] ToArray()
        {
            return mainBuffer.ToArray();
        }



        public Package(byte[] data)
        {
            mainBuffer = new List<byte>();
            readPosition = 0; 
            SetBytes(data);
        }



        public void SetBytes(byte[] data)
        {
            Write(data);
            readableBuffer = mainBuffer.ToArray();
        }



        public void WriteRange()
        {
            mainBuffer.InsertRange(0, BitConverter.GetBytes(mainBuffer.Count));
        }


        public void InsertInt(int _value)
        {
            mainBuffer.InsertRange(0, BitConverter.GetBytes(_value));
        }



        public int Length
        {
            get { return mainBuffer.Count; }
        }



        public int UnreadLength
        {
            get { return mainBuffer.Count - readPosition; }
        }

        public void Reset(bool _shouldReset = true)
        {
            if (_shouldReset)
            {
                mainBuffer.Clear();
                readableBuffer = null;
                readPosition = 0;
            }
            else
            {
                readPosition -= 4;
            }
        }




        public void Write(byte value)
        {
            mainBuffer.Add(value);
        }

        
        public void Write(byte[] value)
        {
            mainBuffer.AddRange(value);
        }

        public void Write(short value)
        {
            mainBuffer.AddRange(BitConverter.GetBytes(value));
        }

        public void Write(int value)
        {
            mainBuffer.AddRange(BitConverter.GetBytes(value));
        }


        public void Write(long value)
        {
            mainBuffer.AddRange(BitConverter.GetBytes(value));
        }
        

        public void Write(float value)
        {
            mainBuffer.AddRange(BitConverter.GetBytes(value));
        }
        

        public void Write(bool value)
        {
            mainBuffer.AddRange(BitConverter.GetBytes(value));
        }


        public void Write(string value)
        {
            Write(value.Length);
            mainBuffer.AddRange(Encoding.ASCII.GetBytes(value));
        }



        public byte ReadByte(bool movereadPosition = true)
        {
            if (mainBuffer.Count > readPosition)
            {
                byte _value = readableBuffer[readPosition];
                if (movereadPosition)
                {
                    readPosition += 1;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type 'byte'!");
            }
        }


        public byte[] ReadBytes(int length, bool movereadPosition = true)
        {
            if (mainBuffer.Count > readPosition)
            {
                byte[] _value = mainBuffer.GetRange(readPosition, length).ToArray();
                if (movereadPosition)
                {
                    readPosition += length;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type 'byte[]'!");
            }
        }


        public short ReadShort(bool movereadPosition = true)
        {
            if (mainBuffer.Count > readPosition)
            {
                short _value = BitConverter.ToInt16(readableBuffer, readPosition);
                if (movereadPosition)
                {
                    readPosition += 2;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type 'short'!");
            }
        }


        public int ReadInt(bool movereadPosition = true)
        {
            if (mainBuffer.Count > readPosition)
            {
                int _value = BitConverter.ToInt32(readableBuffer, readPosition);
                if (movereadPosition)
                {
                    readPosition += 4;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type 'int'!");
            }
        }


        public long ReadLong(bool movereadPosition = true)
        {
            if (mainBuffer.Count > readPosition)
            {
                long _value = BitConverter.ToInt64(readableBuffer, readPosition);
                if (movereadPosition)
                {
                    readPosition += 8;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type 'long'!");
            }
        }



        public float ReadFloat(bool movereadPosition = true)
        {
            if (mainBuffer.Count > readPosition)
            {
                float _value = BitConverter.ToSingle(readableBuffer, readPosition);
                if (movereadPosition)
                {
                    readPosition += 4;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type 'float'!");
            }
        }

        public bool ReadBool(bool movereadPosition = true)
        {
            if (mainBuffer.Count > readPosition)
            {
                bool _value = BitConverter.ToBoolean(readableBuffer, readPosition);
                if (movereadPosition)
                {
                    readPosition += 1;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type 'bool'!");
            }
        }
        

        public string ReadString(bool movereadPosition = true)
        {
            try
            {
                int length = ReadInt();
                string _value = Encoding.ASCII.GetString(readableBuffer, readPosition, length);
                if (movereadPosition && _value.Length > 0)
                {
                    readPosition += length;
                }
                return _value;
            }
            catch
            {
                throw new Exception("Could not read value of type 'string'!");
            }
        }
    }
}