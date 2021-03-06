using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STAADPro;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StaadModel model = new StaadModel();
            model.MergeElements();
            foreach (MergedElement mergedElement in model.MergedElements)
            {
                Console.WriteLine(mergedElement.ToString());
                foreach (Element element in mergedElement.Elements)
                {
                    Console.WriteLine("\t" + element.Id);
                }
            }
            //ReportModelInfo(model);
            Console.ReadKey();
        }

        public static void ReportModelInfo(StaadModel model)
        {
            foreach (Node node in model.Nodes)
            {
                Console.WriteLine(node.ToString());
            }

            foreach (Element element in model.Elements)
            {
                Console.WriteLine(element.ToString());
            }
        }
    }
}
