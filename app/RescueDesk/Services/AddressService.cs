﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using RescueDesk.Models;
using RescueDesk.Utils;

namespace RescueDesk.Services
{
    public class AddressService
    {
        private MySqlConnection Conn = new MySqlConnection(Utils.ConnectionString());
        private static readonly string[] ChavesMestre = new string[] { "keyLocalidades" };

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

        public bool CreateLocalidade(Localidade localidade)
        {
            string query = "INSERT INTO localidades " +
                           "(codpostal, localidade) " +
                           "VALUES ('" + localidade.codpostal.ToString() + "', '" + localidade.nomeLocalidade.ToString() + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            ClearCache();
            return resultados > 0;
        }

        private static void ClearCache()
        {
            System.Web.Caching.Cache dadosCache = HttpRuntime.Cache;
            dadosCache.Remove("Localidades");
        }

        public void CreateLocalidades(List<Localidade> localidadesAInserir)
        {
            if (localidadesAInserir.Any())
            {
                this.Conn.Open();
                foreach (Localidade localidade in localidadesAInserir.DistinctBy(x => x.codpostal))
                {
                    string query = "";
                    query += @"INSERT INTO localidades " +
                               "(codpostal, localidade) " +
                               "VALUES ('" + localidade.codpostal.ToString() + "', '" + localidade.nomeLocalidade.ToString() + "')";

                    MySqlCommand cmd = new MySqlCommand(query, this.Conn);
                    int resultados = cmd.ExecuteNonQuery();
                }

                ClearCache();

                this.Conn.Close();
            }
        }

        public bool UpdateLocalidade(Localidade localidade, string oldID)
        {
            string query = "UPDATE localidades " +
                           "SET codpostal='" + localidade.codpostal + "', localidade = '" + localidade.nomeLocalidade + "' " +
                           "WHERE codpostal = '" + oldID + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();

            ClearCache();
            return resultados > 0;
        }

        public void UpdateLocalidades(List<Localidade> localidadesAActualizar)
        {
            if (localidadesAActualizar.Any())
            {
                this.Conn.Open();

                foreach (Localidade localidade in localidadesAActualizar.DistinctBy(x => x.codpostal))
                {
                    string query = "";

                    query += "UPDATE localidades " +
                             "SET codpostal='" + localidade.codpostal + "', localidade = '" + localidade.nomeLocalidade + "' " +
                             "WHERE codpostal = '" + localidade.codpostal + "' ";

                    MySqlCommand cmd = new MySqlCommand(query, this.Conn);
                    int resultados = cmd.ExecuteNonQuery();
                }

                this.Conn.Close();
            }
        }

        public Localidade ObterLocalidade(string id)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from localidades where codpostal='" + id + "'", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Localidade localidade = ParseLocalidade(linha);
                return localidade;
            }

            return null;
        }

        public bool DeleteLocalidade(string id)
        {
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM localidades where codpostal='" + id + "'", this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();

            ClearCache();
            return resultados > 0;
        }

        private Localidade ParseLocalidade(DataRow linha)
        {
            Localidade localidade = new Localidade();
            localidade.codpostal = linha["codpostal"].ToString();
            localidade.nomeLocalidade = linha["localidade"].ToString();

            return localidade;
        }


    }
}