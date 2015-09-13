using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures
{
    /// <summary>
    /// This class will be used to allow parts of dma data to be pulled out of the whole and
    ///  converted to an object class to represent it, but still keep track of the remainder
    ///  of the unknown data. This will hold sections of data that can be pulled out or added
    ///  to.
    /// </summary>
    public class DynamicMemoryMapping : IGameDataStructure
    {
        public Dictionary<int, byte[]> MemoryMaps;

        //Add in later if desired
        //public bool OverwriteMode;
        //public bool ReturnPartialData;

        public int StartOffset { get; private set; }
        public int FullSize { get; private set; }

        public DynamicMemoryMapping()
        {
            MemoryMaps = new Dictionary<int, byte[]>();
            //OverwriteMode = false;
            //ReturnPartialData = false;
            StartOffset = -1;
            FullSize = -1;
        }

        public bool AddMemory(int offset, byte[] data)
        {
            if (data == null || data.Length == 0)
                return false;

            if (!IsEmptyRegion(offset, data.Length))
                return false;
            
            MemoryMaps.Add(offset, data);

            if (MemoryMaps.Count == 1)
            {
                StartOffset = offset;
                FullSize = data.Length;
            }
            else
            {
                int oldStart = StartOffset;
                StartOffset = Math.Min(oldStart, offset);

                int oldEndOffset = oldStart + FullSize;
                int endOffset = offset + data.Length;
                int finalEndOffset = Math.Max(endOffset, oldEndOffset);
                FullSize = finalEndOffset - StartOffset;
            }

            return true;
        }

        public bool TakeMemory(int offset, int size, out byte[] data)
        {
            data = null;

            if (!ContainsBytes(offset, size))
                return false;

            KeyValuePair<int, byte[]> startMap = GetContainingMap(offset);

            data = new byte[size];

            Array.Copy(startMap.Value, offset - startMap.Key, data, 0, size);

            MemoryMaps.Remove(startMap.Key);

            //now split up the data
            if (offset != startMap.Key)
            {
                //Make first half
                int firstHalfSize = offset - startMap.Key;
                byte[] firstHalfArray = new byte[firstHalfSize];
                Array.Copy(startMap.Value, 0, firstHalfArray, 0, firstHalfSize);
                MemoryMaps.Add(startMap.Key, firstHalfArray);
            }

            if (offset + size != startMap.Key + startMap.Value.Length)
            {
                //Make second half
                int secondHalfSize = (startMap.Key + startMap.Value.Length) - (offset + size);
                byte[] secondHalfArray = new byte[secondHalfSize];
                int startSecondHalf = (offset + size) - startMap.Key;
                Array.Copy(startMap.Value, startSecondHalf, secondHalfArray, 0, secondHalfSize);
                MemoryMaps.Add((offset + size), secondHalfArray);
            }

            return true;
        }

        public bool PeekMemory(int offset, int size, out byte[] data)
        {
            data = null;

            if (!ContainsBytes(offset, size))
                return false;

            KeyValuePair<int, byte[]> startMap = GetContainingMap(offset);

            data = new byte[size];

            Array.Copy(startMap.Value, offset - startMap.Key, data, 0, size);

            return true;
        }

        public void ClearMaps()
        {
            MemoryMaps.Clear();

            StartOffset = -1;
            FullSize = -1;
        }


        public bool ContainsByte(int offset)
        {
            return IsValidMap(GetContainingMap(offset));
        }

        public bool ContainsBytes(int offset, int size)
        {
            KeyValuePair<int, byte[]> startMap = GetContainingMap(offset);
            KeyValuePair<int, byte[]> endMap = GetContainingMap(offset + size - 1);

            return (startMap.Equals(endMap) && IsValidMap(startMap));
        }

        private int GetNextFreeByte(int mapIndex)
        {
            if(MemoryMaps.ContainsKey(mapIndex))
                return mapIndex + MemoryMaps[mapIndex].Length;

            return -1;
        }

        private KeyValuePair<int, byte[]> GetContainingMap(int offset)
        {
            return MemoryMaps.SingleOrDefault(mm => mm.Key <= offset && offset < GetNextFreeByte(mm.Key));
        }

        private bool IsValidMap(KeyValuePair<int, byte[]> map)
        {
            return map.Value != null;
        }

        private bool IsEmptyRegion(int offset, int size)
        {
            //Check if the region is contained inside an existing map, or if the region contains any existing maps
            return !ContainsByte(offset) && MemoryMaps.Count(mm => (offset <= mm.Key && mm.Key < offset + size) ||
                (offset <= mm.Key + mm.Value.Length && mm.Key + mm.Value.Length < offset + size)) == 0;
        }

        public byte[] GetAsBytes()
        {
            if (FullSize == -1)
                return new byte[0];

            byte[] bytes = new byte[FullSize];

            foreach (KeyValuePair<int, byte[]> map in MemoryMaps)
            {
                Array.Copy(map.Value, 0, bytes, map.Key - StartOffset, map.Value.Length);
            }

            return bytes;
        }
    }
}
