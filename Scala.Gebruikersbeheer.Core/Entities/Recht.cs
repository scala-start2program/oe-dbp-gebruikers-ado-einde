using System;
using System.Collections.Generic;
using System.Text;

namespace Scala.Gebruikersbeheer.Core.Entities
{
    public class Recht
    {
        public string GebruikerId { get; private set; }
        public string OnderdeelId { get; private set; }
        public Recht(string gebruikerId, string onderdeelId)
        {
            GebruikerId = gebruikerId;
            OnderdeelId = onderdeelId;
        }

    }
}
