using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Npc;

namespace Scene
{
    public class SceneClass
    {
        public List<NpcClass> mNPC { get; set; }
        public SceneClass()
        {
            mNPC= new List<NpcClass>();
        }
        public string backGroundPath { get; set; }
        public string nameForScene { get; set; }
    }
}
