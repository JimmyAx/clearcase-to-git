using System;
using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace GitImporter
{
    [ProtoContract]
    public class LabelMeta
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string AuthorName { get; set; }
        [ProtoMember(3)]
        public string AuthorLogin { get; set; }
        [ProtoMember(4)]
        public DateTime Created { get; set; }
    }
}
