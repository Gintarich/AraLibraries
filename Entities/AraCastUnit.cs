using AraLibraries.Entities;
using System;
using System.Collections.Generic;
 
using Tekla.Structures.Model;

namespace AraLibraries.Entities
{
    public class AraCastUnit
    {
        public List<Embed> Embeds { get;private set; }
        public AraMainPart MainPart { get;private set; }
        public AraCastUnit(ModelObject modelObject)
        {
            Embeds = new List<Embed>();
            if (modelObject is null)
            {
                MainPart = null;
            }
            MainPart = new AraMainPart(modelObject);
            InitializeEmbeds();

        }

        private void InitializeEmbeds()
        {
            if (MainPart is null)
            {
                return;
            }
            Part part = MainPart.CastUnitMainpart as Part;
            var assembly = part.GetAssembly();
            var subassemblies = assembly.GetSubAssemblies();
            foreach (var subassemblyObject in subassemblies)
            {
                var subassembly = subassemblyObject as Assembly;
                var mainPart = subassembly.GetMainPart();
                //TODO: Add logic for Is Embed Check

                //if (mainPart.IsEmbed())
                //{
                //    var embed = new Embed(mainPart);
                //    Embeds.Add(embed);
                //    embed.CalculateFace(MainPart);
                //}
                throw new NotImplementedException("Loģika nav uzrakstīta :/");
            }
        }
    }
}
