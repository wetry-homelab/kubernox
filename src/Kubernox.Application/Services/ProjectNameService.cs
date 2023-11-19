using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kubernox.Application.Interfaces;

namespace Kubernox.Application.Services
{
    public class ProjectNameService : IProjectNameService
    {
        private readonly List<string> constellations = new List<string>
        {
            "Andromeda", "Antlia", "Apus", "Aquarius", "Aquila", "Ara", "Aries", "Auriga",
            "Boötes", "Caelum", "Camelopardalis", "Cancer", "Canes Venatici", "Canis Major",
            "Canis Minor", "Capricornus", "Carina", "Cassiopeia", "Centaurus", "Cepheus",
            "Cetus", "Chamaeleon", "Circinus", "Columba", "Coma Berenices", "Corona Australis",
            "Corona Borealis", "Corvus", "Crater", "Crux", "Cygnus", "Delphinus", "Dorado",
            "Draco", "Equuleus", "Eridanus", "Fornax", "Gemini", "Grus", "Hercules", "Horologium",
            "Hydra", "Hydrus", "Indus", "Lacerta", "Leo", "Leo Minor", "Lepus", "Libra",
            "Lupus", "Lynx", "Lyra", "Mensa", "Microscopium", "Monoceros", "Musca", "Norma",
            "Octans", "Ophiuchus", "Orion", "Pavo", "Pegasus", "Perseus", "Phoenix", "Pictor",
            "Pisces", "Piscis Austrinus", "Puppis", "Pyxis", "Reticulum", "Sagitta", "Sagittarius",
            "Scorpius", "Sculptor", "Scutum", "Serpens", "Sextans", "Taurus", "Telescopium",
            "Triangulum", "Triangulum Australe", "Tucana", "Ursa Major", "Ursa Minor", "Vela",
            "Virgo", "Volans", "Vulpecula"
        };

        public string GenerateUniqueName()
        {
            var random = new Random();
            var firstName = constellations.OrderBy(x => random.Next()).Take(1).First();
            var secondName = constellations.Where(n => n != firstName).OrderBy(x => random.Next()).Take(1).First();
            var thirdName = constellations.Where(n => n != secondName && n != firstName).OrderBy(x => random.Next()).Take(1).First();

            return $"{firstName.ToLowerInvariant().Replace(" ", "")}-{secondName.ToLowerInvariant().Replace(" ", "")}-{thirdName.ToLowerInvariant().Replace(" ", "")}";
        }
    }
}
