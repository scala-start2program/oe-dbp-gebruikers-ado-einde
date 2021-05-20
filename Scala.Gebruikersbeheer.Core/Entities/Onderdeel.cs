using System;
using System.Collections.Generic;
using System.Text;

namespace Scala.Gebruikersbeheer.Core.Entities
{
    public class Onderdeel
    {
        public string Id { get; private set; }
        public string Naam { get; set; }
        public Onderdeel()
        {
            Id = Guid.NewGuid().ToString();
        }
        public Onderdeel(string naam)
        {
            Id = Guid.NewGuid().ToString();
            Naam = naam;
        }
        public Onderdeel(string id, string naam)
        {
            Id = id;
            Naam = naam;
        }
        public override string ToString()
        {
            return $"{Naam}";
        }
    }

}
