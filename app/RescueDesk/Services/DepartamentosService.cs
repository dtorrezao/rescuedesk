using MySql.Data.MySqlClient;
using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public class DepartamentosService
    {

        private MySqlConnection Conn = new MySqlConnection("server=localhost;database=rescuedesk;username=root;Convert Zero Datetime=True");

        public List<Departamento> ObterDepartamentos()
        {
            List<Departamento> Departamentos = new List<Departamento>();
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from departamentos", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Departamento departamento = ParseDepartamento(linha);
                Departamentos.Add(departamento);
            }
            return Departamentos;



        }
        private static Departamento ParseDepartamento(DataRow linha)
        {
            
            Departamento departamento = new Departamento();
            departamento.iddept = int.Parse(linha["iddept"].ToString());
            departamento.dept = linha["dept"].ToString();

            return departamento;
        }
    }
}