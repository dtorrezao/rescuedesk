using MySql.Data.MySqlClient;
using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public class FuncionariosService
    {
        private MySqlConnection Conn = new MySqlConnection("server=localhost;database=rescuedesk;username=root;Convert Zero Datetime=True");

        public List<Funcionario> ObterFuncionarios()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from funcionarios", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Funcionario funcionario = ParseFuncionarios(linha);
                funcionarios.Add(funcionario);
            }
            return funcionarios;
        }

        public Funcionario ObterFuncionarioDefault()
        {
            Funcionario funcionario = new Funcionario();
            funcionario.ultlogin = DateTime.Now;
            return funcionario;
        }

        public bool CreateFuncionario(Funcionario funcionario)
        {
            string query = "INSERT INTO `funcionarios` " +
               " (`idfuncionario`, `nome`, `morada`, `codpostal`, `iddept`, `cargo`, `contacto`, `email`, `ativo`, `ultlogin`, `obs`) " +
               " VALUES (NULL, '" + funcionario.nome + "', '" + funcionario.morada + "', '" + funcionario.codpostal + "', '" + funcionario.iddept + "' , '" +
               "" + funcionario.cargo + "', '" + funcionario.contacto + "', '" + funcionario.email + "', '" + (funcionario.ativo ? "1" : "0") + "' , '" +funcionario.ultlogin.ToString("yyyy-MM-dd hh:mm:ss") + "' , '" + funcionario.obs + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool UpdateFuncionario(Funcionario funcionario)
        {
            string query = "UPDATE `funcionarios` ";
            query += "SET `nome` = '" + funcionario.nome + "', " +
                "`morada` =  '" + funcionario.morada + "', " +
                "`codpostal` =  '" + funcionario.codpostal + "', " +
                "`iddept` =  '" + funcionario.iddept + "'," +
                " `cargo` = '" + funcionario.cargo + "'," +
                " `contacto` = '" + funcionario.contacto + "'," +
                "`email` = '" + funcionario.email + "'," +
                "`ativo` = '" + (funcionario.ativo ? "1" : "0") + "'," +
                " `ultlogin` = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'," +
                "`obs` = '" + funcionario.obs + "' " +
                "WHERE `funcionarios`.`idfuncionario` = '" + funcionario.idfuncionario.ToString() + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool DeleteFuncionario(int id)
        {
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM funcionarios where idfuncionario='" + id.ToString() + "'", this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;

        }

        public Funcionario ObterFuncionario(int id)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from funcionarios where idfuncionario='" + id.ToString() + "'", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Funcionario funcionario = ParseFuncionarios(linha);
                return funcionario;
            }
            return null;
        }


        private static Funcionario ParseFuncionarios(DataRow linha)
        {
            Funcionario funcionario = new Funcionario();
            funcionario.idfuncionario = int.Parse(linha["idfuncionario"].ToString());
            funcionario.nome = linha["nome"].ToString();
            funcionario.morada = linha["morada"].ToString();
            funcionario.codpostal = linha["codpostal"].ToString();
            funcionario.iddept = int.Parse(linha["iddept"].ToString());
            funcionario.cargo = linha["cargo"].ToString();
            funcionario.contacto = int.Parse(linha["contacto"].ToString());
            funcionario.email = linha["email"].ToString();
            funcionario.ativo = bool.Parse(linha["ativo"].ToString());
            funcionario.ultlogin= DateTime.Parse(linha["ultlogin"].ToString());
            funcionario.obs = linha["obs"].ToString();
            return funcionario;
        }

    }
}