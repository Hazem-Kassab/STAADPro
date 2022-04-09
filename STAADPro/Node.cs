using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAADPro
{
    class Node
    {
        public Node(int id, double xCoord, double yCoord, double zCoord)
        {
            this.Id = id;
            this.X = xCoord;
            this.Y = yCoord;
            this.Z = zCoord;
        }

        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public bool XDispRestrained { get; set; }
        public bool YDispRestrained { get; set; }
        public bool ZDispRestrained { get; set; }
        public bool XRotRestrained { get; set; }
        public bool YRotRestrained { get; set; }
        public bool ZRotRestrained { get; set; }

        public override string ToString()
        {
            return $"Node Id: {Id}, X coordinate: {X}, Y coordinate: {Y}, Z coordinate: {Z}";
        }
        
        public static void CreateNodeFromStaad(StaadModel staadModel, int id)
        {
            dynamic xCoord = 0.0;
            dynamic yCoord = 0.0;
            dynamic zCoord = 0.0;

            staadModel.wrapper.Geometry.GetNodeCoordinates(id, ref xCoord, ref yCoord, ref zCoord);

            staadModel.Nodes.Add(new Node(id, xCoord, yCoord, zCoord));
        }

        public static void CreateNodeToStaad(StaadModel staadModel, double x, double y, double z)
        {
            staadModel.wrapper.Geometry.AddNode(x, y, z);
            int id = staadModel.wrapper.Geometry.GetLastNodeNo();
            staadModel.Nodes.Add(new Node(id, x, y, z));
        }

        public override bool Equals(object obj)
        {
            Node node = obj as Node;
            return node != null && node.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}
