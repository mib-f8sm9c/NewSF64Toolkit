using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace NewSF64Toolkit.DataStructures.DataObjects.F3DEX
{
    public class F3DEXCollection
    {
        //We need to release them only as readonlycollections so that we can manage the vertices better (merge adjacent collections)

        private List<DListCollection> _dLists;
        private List<VertexCollection> _vertices;
        private List<Texture> _textures;

        public ReadOnlyCollection<DListCollection> DLists { get { return _dLists.AsReadOnly(); } }

        public ReadOnlyCollection<VertexCollection> Vertices { get { return _vertices.AsReadOnly(); } }

        public ReadOnlyCollection<Texture> Textures { get { return _textures.AsReadOnly(); } }

        public F3DEXCollection()
        {
            _dLists = new List<DListCollection>();
            _vertices = new List<VertexCollection>();
            _textures = new List<Texture>();
        }

        public bool AddBytesTo(byte[] bytes)
        {
            //Here, go through all the vertices, dlists and textures and add them to the byte array.
            foreach (DListCollection dlist in _dLists)
            {
                if (dlist.Offset < 0 || dlist.Offset > bytes.Length || dlist.Offset + dlist.Size > bytes.Length)
                    return false;

                Array.Copy(dlist.GetAsBytes(), 0, bytes, dlist.Offset, dlist.Size);
            }

            foreach (VertexCollection vertex in _vertices)
            {
                if (vertex.Offset < 0 || vertex.Offset > bytes.Length || vertex.Offset + Vertex.Size > bytes.Length)
                    return false;

                Array.Copy(vertex.GetAsBytes(), 0, bytes, vertex.Offset, Vertex.Size);
            }

            foreach (Texture texture in _textures)
            {
                if (texture.Offset < 0 || texture.Offset > bytes.Length || texture.Offset + texture.Size > bytes.Length)
                    return false;

                Array.Copy(texture.GetAsBytes(), 0, bytes, texture.Offset, texture.Size);
            }

            return true;
        }

        //Add/remove functions
        public bool AddVertex(Vertex vertex)
        {
            //Check if there exists a vertexcollection that could need it
            for (int i = 0; i < _vertices.Count; i++)
            {
                VertexCollection coll = _vertices[i];

                if (coll.Contains(vertex.Offset))
                    return false;

                else if (coll.IsAdjacentTo(vertex.Offset, Vertex.Size))
                {
                    //Add to the collection
                    coll.Vertices.Add(vertex);
                    if (coll.Offset > vertex.Offset)
                        coll.Offset -= 0x10;

                    //Do some kind of reverse checking on the collection to make sure it now isn't adacent to another collection
                    for (int j = 0; j < _vertices.Count; j++)
                    {
                        if (i == j)
                            continue;

                        if (coll.IsAdjacentTo(_vertices[j].Offset, _vertices[j].Size))
                        {
                            int newOffset = Math.Min(coll.Offset, _vertices[j].Offset);
                            foreach (Vertex vert in _vertices[j].Vertices)
                            {
                                coll.Vertices.Add(vert);
                            }
                            coll.Offset = newOffset;
                            _vertices.RemoveAt(j);

                            return true;
                        }
                    }

                    return true;
                }
            }

            //Create new VertexCollection
            VertexCollection newColl = new VertexCollection(vertex.Offset);

            newColl.Vertices.Add(vertex);

            _vertices.Add(newColl);

            return true;
        }

        public bool AddTexture(Texture texture)
        {
            //Do more checking than just "Okay, they don't start in the same place"?
            if (_textures.SingleOrDefault(t => t.Offset == texture.Offset) != null)
                return false;

            _textures.Add(texture);

            return true;
        }

        public bool AddDList(DListCommand command)
        {
            //Check if there exists a vertexcollection that could need it
            for (int i = 0; i < _vertices.Count; i++)
            {
                DListCollection coll = _dLists[i];

                if (coll.Contains(command.Offset))
                    return false;

                else if (coll.IsAdjacentTo(command.Offset, command.Size))
                {
                    //Add to the collection
                    coll.DListCommands.Add(command);
                    if (coll.Offset > command.Offset)
                        coll.Offset -= command.Size;

                    //Do some kind of reverse checking on the collection to make sure it now isn't adacent to another collection
                    for (int j = 0; j < _dLists.Count; j++)
                    {
                        if (i == j)
                            continue;

                        if (coll.IsAdjacentTo(_dLists[j].Offset, _dLists[j].Size))
                        {
                            int newOffset = Math.Min(coll.Offset, _dLists[j].Offset);
                            foreach (DListCommand comm in _dLists[j].DListCommands)
                            {
                                coll.DListCommands.Add(comm);
                            }
                            coll.Offset = newOffset;
                            _dLists.RemoveAt(j);

                            return true;
                        }
                    }

                    return true;
                }
            }

            //Create new VertexCollection
            DListCollection newColl = new DListCollection(command.Offset);

            newColl.DListCommands.Add(command);

            _dLists.Add(newColl);

            return true;
        }
    }
}
