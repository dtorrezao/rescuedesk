using MySql.Data.MySqlClient;
using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public class AddressService
    {
        private MySqlConnection Conn = new MySqlConnection("server=localhost;database=rescuedesk;username=root;Convert Zero Datetime=True");

        public List<Localidade> ObterLocalidades()
        {
            List<Localidade> Localidades = new List<Localidade>();
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("SELECT * FROM localidades", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Localidade localidade = ParseLocalidade(linha);
                Localidades.Add(localidade);
            }
            return Localidades;

        }

        private Localidade ParseLocalidade(DataRow linha)
        {
            Localidade localidade = new Localidade();
            localidade.codpostal = linha["codpostal"].ToString();
            localidade.localidade = linha["localidade"].ToString();

            return localidade;
        }

    }
}