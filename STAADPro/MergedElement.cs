using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace STAADPro
{
    internal class MergedElement
    {
        static int count = 0;
        public MergedElement(Vector3 directionVector, Vector3 positionVector)
        {
            count++;
            Id = count;
            DirectionVector = directionVector;
            PositionVector = positionVector;
        }
        public int Id { get; set; }
        public HashSet<Element> Elements { get; set; } = new HashSet<Element>();
        public Vector3 DirectionVector { get; set; }
        public Vector3 PositionVector { get; set; }

        public override string ToString()
        {
            return $"Merged Element Id: {Id},   DirectionVector: {DirectionVector.ToString()},   " +
                $"PositionVector: {PositionVector.ToString()}, Element: {Elements.ElementAt(0).Id}";
        }
    }
}
