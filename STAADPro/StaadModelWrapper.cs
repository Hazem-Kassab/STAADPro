using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSUI = OpenSTAADUI;

namespace STAADPro
{
    class StaadModelWrapper
    {

        public StaadModelWrapper()
        {

        }
        public StaadModelWrapper(OSUI.OpenSTAAD staadModel)
        {
            StaadModel = staadModel;
        }

        public OSUI.OpenSTAAD StaadModel { get; set; }
        public OSUI.OSCommandsUI Commands { get { return StaadModel.Command; } }
        public OSUI.OSDesignUI Design { get { return StaadModel.Design; } }
        public OSUI.OSGeometryUI Geometry { get { return StaadModel.Geometry; } }
        public OSUI.OSLoadUI Load { get { return StaadModel.Load; } }
        public OSUI.OSOutputUI Output { get { return StaadModel.Output; } }
        public OSUI.OSPropertyUI Property { get { return StaadModel.Property; } }
        public OSUI.OSSupportUI Support { get { return StaadModel.Support; } }
        public OSUI.OSTableUI Table { get { return StaadModel.Table; } }
        public OSUI.OSViewUI View { get { return StaadModel.View; } }



    }
}
