using System;
using System.Collections.Generic;
using System.Text;
using Scala.Gebruikersbeheer.Core.Entities;
using System.Data;
namespace Scala.Gebruikersbeheer.Core.Services
{
    public class GebruikersService
    {
        public List<Gebruiker> GetAlleGebruikers()
        {
            List<Gebruiker> gebruikers = new List<Gebruiker>();
            string sql = "select * from gebruikers order by familienaam, voornaam";

            DataTable dataTable = DBService.ExecuteSelect(sql);
            if (dataTable == null)
            {
                return null;
            }
            else
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    string id = dr["id"].ToString();
                    string gebruikersnaam = dr["gebruikersnaam"].ToString();
                    string voornaam = dr["voornaam"].ToString();
                    string familienaam = dr["familienaam"].ToString();
                    string email = dr["email"].ToString();
                    string telefoon = dr["telefoon"].ToString();
                    Gebruiker gebruiker = new Gebruiker(id, gebruikersnaam, voornaam, familienaam, email, telefoon,""); ;
                    gebruikers.Add(gebruiker);
                }
                return gebruikers;
            }
        }
        public List<Gebruiker> GetGebruikersMetToegangTotOnderdeel(Onderdeel onderdeel)
        {
            string onderdeelId = onderdeel.Id;
            List<Gebruiker> gebruikers = new List<Gebruiker>();
            string sql = "select * from gebruikers ";
            sql += " where id in (";
            sql += "    select gebruikerId from rechten ";
            sql += "    where onderdeelId = '" + onderdeelId + "' )";
            sql += " order by familienaam, voornaam";

            DataTable dataTable = DBService.ExecuteSelect(sql);
            if (dataTable == null)
            {
                return null;
            }
            else
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    string id = dr["id"].ToString();
                    string gebruikersnaam = dr["gebruikersnaam"].ToString();
                    string voornaam = dr["voornaam"].ToString();
                    string familienaam = dr["familienaam"].ToString();
                    string email = dr["email"].ToString();
                    string telefoon = dr["telefoon"].ToString();
                    Gebruiker gebruiker = new Gebruiker(id, gebruikersnaam, voornaam, familienaam, email, telefoon, ""); ;
                    gebruikers.Add(gebruiker);
                }
                return gebruikers;
            }
        }
        public List<Gebruiker> GetGebruikersZonderToegangTotOnderdeel(Onderdeel onderdeel)
        {
            string onderdeelId = onderdeel.Id;
            List<Gebruiker> gebruikers = new List<Gebruiker>();
            string sql = "select * from gebruikers ";
            sql += " where id NOT in (";
            sql += "    select gebruikerId from rechten ";
            sql += "    where onderdeelId = '" + onderdeelId + "' )";
            sql += " order by familienaam, voornaam";

            DataTable dataTable = DBService.ExecuteSelect(sql);
            if (dataTable == null)
            {
                return null;
            }
            else
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    string id = dr["id"].ToString();
                    string gebruikersnaam = dr["gebruikersnaam"].ToString();
                    string voornaam = dr["voornaam"].ToString();
                    string familienaam = dr["familienaam"].ToString();
                    string email = dr["email"].ToString();
                    string telefoon = dr["telefoon"].ToString();
                    Gebruiker gebruiker = new Gebruiker(id, gebruikersnaam, voornaam, familienaam, email, telefoon, ""); ;
                    gebruikers.Add(gebruiker);
                }
                return gebruikers;
            }
        }
        public bool IsGebruikersnaamUniek(string gebruikersnaam)
        {
            string sql;
            sql = "select count(*) from gebruikers where gebruikersnaam = '" + gebruikersnaam + "' ";
            string aantal = DBService.ExecuteScalar(sql);
            if (aantal == "0")
                return true;
            else
                return false;

        }
        public List<Onderdeel> GetOnderdelen()
        {
            List<Onderdeel> onderdelen = new List<Onderdeel>();
            string sql = "select * from onderdelen order by naam";

            DataTable dataTable = DBService.ExecuteSelect(sql);
            if (dataTable == null)
            {
                return null;
            }
            else
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    string id = dr["id"].ToString();
                    string naam = dr["naam"].ToString();
                    Onderdeel onderdeel = new Onderdeel(id, naam);
                    onderdelen.Add(onderdeel);
                }
                return onderdelen;
            }
        }
        public List<Onderdeel> GetOnderdelenMetToegangVoorGebruiker(Gebruiker gebruiker)
        {
            string gebruikerId = gebruiker.Id;
            List<Onderdeel> onderdelen = new List<Onderdeel>();
            string sql = "select * from onderdelen ";
            sql += " where id in (";
            sql += "   select onderdeelId from rechten ";
            sql += "   where gebruikerId = '" + gebruikerId + "') ";
            sql += " order by naam";

            DataTable dataTable = DBService.ExecuteSelect(sql);
            if (dataTable == null)
            {
                return null;
            }
            else
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    string id = dr["id"].ToString();
                    string naam = dr["naam"].ToString();
                    Onderdeel onderdeel = new Onderdeel(id, naam);
                    onderdelen.Add(onderdeel);
                }
                return onderdelen;
            }
        }
        public List<Onderdeel> GetOnderdelenZonderToegangVoorGebruiker(Gebruiker gebruiker)
        {
            string gebruikerId = gebruiker.Id;
            List<Onderdeel> onderdelen = new List<Onderdeel>();
            string sql = "select * from onderdelen ";
            sql += " where id not in (";
            sql += "   select onderdeelId from rechten ";
            sql += "   where gebruikerId = '" + gebruikerId + "' )";
            sql += " order by naam";

            DataTable dataTable = DBService.ExecuteSelect(sql);
            if (dataTable == null)
            {
                return null;
            }
            else
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    string id = dr["id"].ToString();
                    string naam = dr["naam"].ToString();
                    Onderdeel onderdeel = new Onderdeel(id, naam);
                    onderdelen.Add(onderdeel);
                }
                return onderdelen;
            }
        }
        public bool GebruikerToevoegen(Gebruiker gebruiker)
        {
            string sql;
            sql = "insert into gebruikers(id,  gebruikersnaam, voornaam, familienaam, email, telefoon, paswoord) values (";
            sql += "'" + gebruiker.Id + "' , ";
            sql += "'" + gebruiker.Gebruikersnaam + "' , ";
            sql += "'" + gebruiker.Voornaam + "' , ";
            sql += "'" + gebruiker.Familienaam + "' , ";
            sql += "'" + gebruiker.Email + "' , ";
            sql += "'" + gebruiker.Telefoon + "' , ";
            sql += "'" + gebruiker.Paswoord + "' ) ";
            return DBService.ExecuteCommand(sql);
        }
        public bool GebruikerWijzigen(Gebruiker gebruiker)
        {
            string sql;

            sql = "update gebruikers set ";
            sql += " gebruikersnaam = '" + gebruiker.Gebruikersnaam + "' , ";
            sql += " voornaam = '" + gebruiker.Voornaam + "' , ";
            sql += " familienaam = '" + gebruiker.Familienaam + "' , ";
            sql += " email = '" + gebruiker.Email + "' , ";
            sql += " telefoon = '" + gebruiker.Telefoon + "'  ";
            if (gebruiker.Paswoord != "")
                sql += " , paswoord = '" + gebruiker.Paswoord + "' ";
            sql += " where id = '" + gebruiker.Id + "'  ";

            return DBService.ExecuteCommand(sql);
        }
        public bool GebruikerVerwijderen(Gebruiker gebruiker)
        {
            string sql;
            sql = "delete from rechten where gebruikersId = '" + gebruiker.Id + "' ";
            if (DBService.ExecuteCommand(sql) == true)
            {
                sql = "delete from gebruikers where id = '" + gebruiker.Id + "' ";
                return DBService.ExecuteCommand(sql);
            }
            else
            {
                return false;
            }
        }
        public bool KenRechtToeAanGebruiker(Gebruiker gebruiker, Onderdeel onderdeel)
        {
            string sql;
            sql = "insert into rechten (gebruikerId, onderdeelId) values (";
            sql += "'" + gebruiker.Id + "' , ";
            sql += "'" + onderdeel.Id + "' )";
            return DBService.ExecuteCommand(sql);
        }
        public bool OntneemRechtVanGebruiker(Gebruiker gebruiker, Onderdeel onderdeel)
        {
            string sql;
            sql = "delete from rechten ";
            sql += " where gebruikerId = '" + gebruiker.Id + "' ";
            sql += " and onderdeelId = '" + onderdeel.Id + "' ";
            return DBService.ExecuteCommand(sql);
        }
    }
}
