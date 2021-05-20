using System;
using System.Collections.Generic;
using System.Text;
using Scala.Gebruikersbeheer.Core.Services;

namespace Scala.Gebruikersbeheer.Core.Entities
{
    public class Gebruiker
    {
        public string Id { get; private set; }
        public string Gebruikersnaam { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public string Email { get; set; }
        public string Telefoon { get; set; }
        public string Paswoord { get; private set; }

        public void SetPaswoord(string paswoord)
        {
            Paswoord = Helper.EncryptString(paswoord);
        }

        public Gebruiker()
        {
            Id = Guid.NewGuid().ToString();
        }
        public Gebruiker(string gebruikersnaam, string voornaam, string familienaam, string email, string telefoon, string paswoord)
        {
            Id = Guid.NewGuid().ToString();
            Gebruikersnaam = gebruikersnaam;
            Voornaam = voornaam;
            Familienaam = familienaam;
            Email = email;
            Telefoon = telefoon;
            Paswoord = Helper.EncryptString(paswoord);
        }
        public Gebruiker(string id, string gebruikersnaam, string voornaam, string familienaam, string email, string telefoon, string paswoord)
        {
            Id = id;
            Gebruikersnaam = gebruikersnaam;
            Voornaam = voornaam;
            Familienaam = familienaam;
            Email = email;
            Telefoon = telefoon;
            Paswoord = Helper.EncryptString(paswoord);
        }
        public override string ToString()
        {
            return $"{Familienaam} {Voornaam}";
        }
    }
}
