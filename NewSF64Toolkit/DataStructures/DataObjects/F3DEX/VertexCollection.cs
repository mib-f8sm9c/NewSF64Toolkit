using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures.DataObjects.F3DEX
{
    //Represents a continuous set of Vertices
    public class VertexCollection : IGameDataStructure
    {
        public int Offset;

        public List<Vertex> Vertices;

        public VertexCollection(int offset)
        {
            Offset = offset;

            Vertices = new List<Vertex>();
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[Size];

            foreach (Vertex vert in Vertices)
            {
                Array.Copy(vert.GetAsBytes(), 0, bytes, vert.Offset - Offset, Vertex.Size);
            }

            return bytes;
        }

        public int Size { get { return Vertices.Count * Vertex.Size; } }

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
