using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scene;
using Npc;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace SceneManager
{
    public class SceneManagerClass
    {
        public List<SceneClass> mScenes { get; set; }
        public SceneManagerClass()
        {
            mScenes = new List<SceneClass>();
        }
    
        public void SaveAsXML(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(List<SceneClass>));
                        xs.Serialize(fs, mScenes);
                    }
                    
                }
            }
            catch (Exception)
            {
            }
        }

        public void loadXML(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<SceneClass>));
                    List<SceneClass> temp = (List<SceneClass>)xs.Deserialize(fs);
                    mScenes.Clear();
                    mScenes.AddRange(temp);
                }
            }
            catch (Exception)
            {                
            }
        }
    }
}


