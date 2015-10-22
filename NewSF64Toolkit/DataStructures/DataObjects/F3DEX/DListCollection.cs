using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures.DataObjects.F3DEX
{
    //Represents a continuous set of DListCommands
    public class DListCollection : IGameDataStructure
    {
        public int Offset;

        public List<DListCommand> DListCommands;

        public DListCollection(int offset)
        {
            Offset = offset;
            DListCommands = new List<DListCommand>();
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[Size];

            foreach (DListCommand command in DListCommands)
            {
                Array.Copy(command.GetAsBytes(), 0, bytes, command.Offset - Offset, command.Size);
            }

            return bytes;
        }

        public int Size { get { return DListCommands.Sum(x => x.Size); } }

        public bool Contains(int offset)
        {
            return (Offset <= offset && offset < Offset + Size);
        }

        public bool IsAdjacentTo(int offset, int size)
        {
            return (offset + size == Offset || Offset + Size == offset);
        }
    }
}
