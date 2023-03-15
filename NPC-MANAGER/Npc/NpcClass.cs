using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npc
{
    public class NpcClass
    {
        public enum npcType
        {
            DemonHunters,
            Blacksmith,
            Jeweler,
            Mystic,
            Armor,
            Weapons,
            Consumables,
            Crafting,
            Dyes,
            Gems
        }
        public string Name { get; set; }

        public string FaceImage { get; set; }

        public npcType Type { get; set; }

        public List<string> Dialogues { get; set; }
    }
}
