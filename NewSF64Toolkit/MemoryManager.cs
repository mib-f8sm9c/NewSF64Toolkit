using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit
{
    //This class will provide a front to access the data through. It will simulate the way
    // that the data is loaded in segments.
    public class MemoryManager
    {
        public struct BankData
        {
            public uint VirtualStart;
            public uint VirtualEnd;
            public byte[] Data;

            public BankData(uint vStart, uint vEnd, byte[] data)
            {
                VirtualStart = vStart;
                VirtualEnd = vEnd;
                Data = data;
            }

            public bool IsValid()
            {
                return Data != null;
            }
        }

        private Dictionary<byte, List<BankData>> _dataBanks;

        public MemoryManager()
        {
            _dataBanks = new Dictionary<byte, List<BankData>>();
        }

        #region Singleton code

        //Comment out to break any memorymanager code
        //private static MemoryManager _instance;

        //public static MemoryManager Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //            _instance = new MemoryManager();

        //        return _instance;
        //    }
        //}

        #endregion

        #region Data Bank Functions

        public void AddBank(byte bankNo, byte[] bankData, uint startPos)
        {
            //Check that the bank won't interfere with others
            if (_dataBanks.ContainsKey(bankNo))
            {
                foreach (BankData b in _dataBanks[bankNo])
                {
                    if (Overlaps(b.VirtualStart, b.VirtualEnd, startPos, startPos + (uint)bankData.Length))
                    {
                        //Throw error
                        return;
                    }
                }
            }
            else
            {
                //Add in the bank
                _dataBanks.Add(bankNo, new List<BankData>());
            }

            _dataBanks[bankNo].Add(new BankData(startPos, startPos + (uint)bankData.Length, bankData));
        }

        public void RemoveBank(byte bankNo, uint startPos)
        {
            int bankIndex;

            if (_dataBanks.ContainsKey(bankNo) && (bankIndex = _dataBanks[bankNo].FindIndex(b => b.VirtualStart == startPos)) >= 0)
            {
                _dataBanks[bankNo].RemoveAt(bankIndex);
            }
        }

        public void ClearBanks()
        {
            _dataBanks.Clear();
        }

        private bool Overlaps(uint start1, uint end1, uint start2, uint end2) //exclusive end
        {
            if (start2 <= start1 && start1 < end2)
                return true;

            if (start2 < end1 && end1 <= end2)
                return true;

            if (start1 <= start2 && start2 < end1)
                return true;

            if (start1 < end2 && end2 <= end1)
                return true;

            return false;
        }

        public BankData LocateBank(byte bankNo, uint offset)
        {
            if (_dataBanks.ContainsKey(bankNo))
            {
                if (_dataBanks[bankNo].Count(b => b.VirtualStart <= offset && offset < b.VirtualEnd) > 0)
                {
                    return _dataBanks[bankNo].First(b => b.VirtualStart <= offset && offset < b.VirtualEnd);
                }
            }

            return new BankData();
        }

        public uint ReadUInt(byte bankNo, uint offset)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                return ByteHelper.ReadUInt(bank.Data, offset - bank.VirtualStart);
            }

            //Bad value
            return uint.MaxValue;
        }

        public int ReadInt(byte bankNo, uint offset)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                return ByteHelper.ReadInt(bank.Data, offset - bank.VirtualStart);
            }

            //Bad value
            return int.MaxValue;
        }

        public ushort ReadUShort(byte bankNo, uint offset)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                return ByteHelper.ReadUShort(bank.Data, offset - bank.VirtualStart);
            }

            //Bad value
            return ushort.MaxValue;
        }

        public short ReadShort(byte bankNo, uint offset)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                return ByteHelper.ReadShort(bank.Data, offset - bank.VirtualStart);
            }

            //Bad value
            return short.MaxValue;
        }

        public byte ReadByte(byte bankNo, uint offset)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                return ByteHelper.ReadByte(bank.Data, offset - bank.VirtualStart);
            }

            //Bad value
            return byte.MaxValue;
        }

        public float ReadFloat(byte bankNo, uint offset)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                return ByteHelper.ReadFloat(bank.Data, offset - bank.VirtualStart);
            }

            //Bad value
            return float.MaxValue;
        }

        public void WriteFloat(byte bankNo, uint offset, float val)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                ByteHelper.WriteFloat(val, bank.Data, offset);
            }
        }

        public void WriteUShort(byte bankNo, uint offset, ushort val)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                ByteHelper.WriteUShort(val, bank.Data, offset);
            }
        }

        public void WriteShort(byte bankNo, uint offset, short val)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                ByteHelper.WriteShort(val, bank.Data, offset);
            }
        }

        public bool HasBank(byte bankNo)
        {
            return _dataBanks.ContainsKey(bankNo);
        }

        #endregion

    }
}
