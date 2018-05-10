using System;
using System.Collections.Generic;
using System.Linq;

namespace GitImporter
{
    public class LabelInfo
    {
        public string Name { get; private set; }
        public List<ElementVersion> Versions { get; private set; }
        public Dictionary<string, HashSet<ElementVersion>> MissingVersions { get; private set; }
        // first = expected, second = actual
        public List<Tuple<ElementVersion, ElementVersion>> PossiblyBroken { get; set; }

        public LabelInfo(string name)
        {
            Name = name;
            Versions = new List<ElementVersion>();
            PossiblyBroken = new List<Tuple<ElementVersion, ElementVersion>>();
        }

        public void Reset()
        {
            MissingVersions = Versions.Where(v => v.VersionNumber != 0)
                .GroupBy(v => v.Branch.BranchName)
                .ToDictionary(g => g.Key, g => new HashSet<ElementVersion>(g));
        }
    }
}
