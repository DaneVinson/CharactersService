﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CharactersService.Domain
{
    [DataContract(Namespace = "http://charactersservice.com")]
    public class Character
    {
        [DataMember]
        public string LongName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Source { get; set; }

        public override string ToString()
        {
            return $"{Name}, {LongName}, {Source}";
        }
    }
}
