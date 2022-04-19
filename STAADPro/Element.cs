using System.Numerics;

namespace STAADPro
{
    class Element
    {
        public bool inMergedElement;
        public Element(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
        public Node StartNode { get; set; }
        public Node EndNode { get; set; }
        public double Length { get; set; }
        public string Profile { get; set; }
        public string Material { get; set; }
        public Vector3 PositionVector { get; set; }
        public Vector3 DirectionVector { get; set; }

        public override string ToString()
        {
            return $"Element Id: {Id}, Start Node Id: {StartNode.Id}, End Node Id: {EndNode.Id}, Profile: {Profile}, Material: {Material}";
        }
        
        public static void CreateElementFromStaad(StaadModel staadModel, int id)
        {
            Element element = new Element(id);
            dynamic nodeA = 0;
            dynamic nodeB = 0;

            staadModel.wrapper.Geometry.GetMemberIncidence(id, ref nodeA, ref nodeB);

            element.StartNode = staadModel.GetNodeById((int)nodeA);
            element.EndNode = staadModel.GetNodeById((int)nodeB);
            element.Length = staadModel.wrapper.Geometry.GetBeamLength(id);
            element.Profile = staadModel.wrapper.Property.GetBeamSectionName(id);
            element.Material = staadModel.wrapper.Property.GetBeamMaterialName(id);

            element.PositionVector = new Vector3((float)element.StartNode.X, (float)element.StartNode.Y, (float)element.StartNode.Z);
            
            element.DirectionVector = new Vector3((float)(element.EndNode.X - element.StartNode.X),
                                                  (float)(element.EndNode.Y - element.StartNode.Y),
                                                  (float)(element.EndNode.Z - element.StartNode.Z));
            staadModel.Elements.Add(element);
        }
        
        public static void CreateElementToStaad(StaadModel staadModel, Node start_node, Node end_node)
        {
            staadModel.wrapper.Geometry.AddBeam(start_node.Id, end_node.Id);
            int id = staadModel.wrapper.Geometry.GetLastBeamNo();
            Element.CreateElementFromStaad(staadModel, id);
        }
        public bool IsCollinear(Element element)
        {   
            float length = Vector3.Cross(DirectionVector, element.DirectionVector).Length();
            if (length == 0)
            {
                Vector3 collinear_vector = Vector3.Subtract(PositionVector, element.PositionVector);
                length = Vector3.Cross(DirectionVector, collinear_vector).Length();
                if (length == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public override bool Equals(object obj)
        {
            Element element = obj as Element;
            return element != null && element.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
