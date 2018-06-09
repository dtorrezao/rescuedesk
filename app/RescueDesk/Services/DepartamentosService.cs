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

        private MySqlConnection Conn = new MySqlConnection(Utils.ConnectionString());

        public List<Departamento> ObterDepartamentos()
        {
            List<Departamento> Departamentos = new List<Departamento>();
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("SELECT DISTINCT dept.*, case WHEN f.iddept IS NULL THEN 1 ELSE 0 END as 'PodeEliminar' FROM departamentos dept LEFT JOIN funcionarios f on dept.iddept = f.iddept", this.Conn);
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



        public Departamento ObterDepartamentoDefault()
        {
            Departamento departamento = new Departamento();
            return departamento;
        }

        public bool CreateDepartamento(Departamento departamento)
        {
            string query = "INSERT INTO `departamentos` " +
               " (`iddept`, `dept`) " +
               " VALUES (NULL, '" + departamento.dept + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public Departamento ObterDepartamento(int id)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from departamentos where iddept='" + id.ToString() + "'", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Departamento departamento = ParseDepartamento(linha);
                return departamento;
            }
            return null;
        }

        public bool UpdateDepartamento(Departamento departamento)
        {
            string query = "UPDATE `departamentos`";
            query += "SET `dept` = '" + departamento.dept + "' " +
                 " WHERE `departamentos`.`iddept` = '" + departamento.iddept.ToString() + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool DeleteDepartamentos(int id)
        {
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM departamentos where iddept='" + id.ToString() + "'", this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }



        private static Departamento ParseDepartamento(DataRow linha)
        {

            Departamento departamento = new Departamento();
            departamento.iddept = int.Parse(linha["iddept"].ToString());
            departamento.dept = linha["dept"].ToString();

            if (linha.Table.Columns.Contains("PodeEliminar"))
            {
                departamento.PodeEliminar = linha["PodeEliminar"].ToString().Equals("1");
            }

            return departamento;
        }
    }
}