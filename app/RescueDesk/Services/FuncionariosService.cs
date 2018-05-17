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

        public bool CreateFuncionario(Funcionario funcionario)
        {
            string query = "INSERT INTO `funcionarios` " +
               " (`idfuncionario`, `nome`, `morada`, `codpostal`, `iddept`, `cargo`, `contacto`, `email`, `ativo`, `ultlogin`, `obs`) " +
               " VALUES (NULL, '" + funcionario.nome + "', '" + funcionario.morada  + "', '" + funcionario.codpostal + "', '" + funcionario.iddept + "' , '" +
               "" + funcionario.cargo + "', '" + funcionario.contacto + "', '" + funcionario.email + "', '' , '' ,'" + funcionario.obs + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
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
            funcionario.obs = linha["obs"].ToString();
            return funcionario;
        }

    }
}