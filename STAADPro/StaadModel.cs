using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OSUI = OpenSTAADUI;

namespace STAADPro
{
    class StaadModel
    {
        private readonly OSUI.OpenSTAAD staadModel = Marshal.GetActiveObject("StaadPro.OpenSTAAD") as OpenSTAADUI.OpenSTAAD;
        public readonly StaadModelWrapper wrapper;

        public StaadModel()
        {
            wrapper = new StaadModelWrapper(staadModel);
            GetAllNodes();
            GetAllElements();
        }


        public HashSet<Element> Elements { get; set; } = new HashSet<Element>();
        public HashSet<Node> Nodes { get; set; } = new HashSet<Node>();

        public HashSet<Node> GetNodesById(params int[] ids)
        {
            HashSet<Node> nodesById = new HashSet<Node>();
            foreach (int id in ids)
            {
                nodesById.Add(GetNodeById(id));
            }

            return nodesById;

        }

        /// <summary>
        /// Retrieves node instance by id from Hashset<Node> Nodes Property
        /// </summary>
        /// <param name="id">id of the node instance to be retrieved</param>
        /// <returns>Node instance with the id</returns>
        public Node GetNodeById(int id) => Nodes.Where(n => n.Id == id).Single();

        /// <summary>
        /// Retrieves element instance by id from Hashset<Element> Elements Property 
        /// </summary>
        /// <param name="id"> id of the element instance to be retrieved</param>
        /// <returns>Element instance with the id</returns>
        public Element GetElementById(int id) => Elements.Where(n => n.Id == id).Single();

        /// <summary>
        /// Retrieves all elements from active StaadPro application, then creates Element objects and stores them in Hashset<Element> Elements Property.
        /// </summary>
        private void GetAllElements()
        {
            dynamic ids = new int[(int)wrapper.Geometry.GetMemberCount()];
            wrapper.Geometry.GetBeamList(ref ids);

            foreach (int id in ids)
            {
                Element.CreateElementFromStaad(this, id);
            }

        }
        /// <summary>
        /// Retrieves all nodes from active StaadPro application, then creates Node objects and stores them in Hashset<Node> Nodes Property.
        /// </summary>
        private void GetAllNodes()
        {
            dynamic nodes_ids = new int[(int)wrapper.Geometry.GetNodeCount()];
            wrapper.Geometry.GetNodeList(ref nodes_ids);

            foreach (int id in nodes_ids)
            {
                Node.CreateNodeFromStaad(this, id);
            }
        }
    }
}
