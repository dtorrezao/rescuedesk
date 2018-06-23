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
        //Conexão à BD//
        private MySqlConnection conn = new MySqlConnection(Utils.ConnectionString());

        public List<Funcionario> ObterFuncionarios()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            this.conn.Open();

            //QUERY//
            string query = "SELECT funcionarios.*, email, localidade, dept " +
                           "FROM funcionarios " +
                           "INNER JOIN utilizadores AS ut ON ut.idUtilizador = funcionarios.idUtilizador " +
                           "INNER JOIN localidades ON localidades.codpostal = funcionarios.codpostal " +
                           "INNER JOIN departamentos AS dept ON dept.iddept = funcionarios.iddept";

            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.conn.Close();

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
            funcionario.ativo = true;
            return funcionario;
        }

        public bool CreateFuncionario(Funcionario funcionario)
        {
            string query = "INSERT INTO `funcionarios` " +
               " (`idfuncionario`, `nome`, `morada`, `codpostal`, `iddept`, `cargo`, `contacto`, `idutilizador`, `ativo`, `ultlogin`, `obs`) " +
               " VALUES (NULL, '" + funcionario.nome + "', '" + funcionario.morada + "', '" + funcionario.codpostal + "', '" + funcionario.iddept + "' , '" +
               "" + funcionario.cargo + "', '" + funcionario.contacto + "', '" + funcionario.idUtilizador + "', '" + (funcionario.ativo ? "1" : "0") + "' , '" + funcionario.ultlogin.ToString("yyyy-MM-dd hh:mm:ss") + "' , '" + funcionario.obs + "')";
            this.conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.conn);
            int resultados = cmd.ExecuteNonQuery();
            this.conn.Close();
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
                //"`idutilizador` = '" + funcionario.idUtilizador + "'," +
                "`ativo` = '" + (funcionario.ativo ? "1" : "0") + "'," +
                " `ultlogin` = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'," +
                "`obs` = '" + funcionario.obs + "' " +
                "WHERE `funcionarios`.`idfuncionario` = '" + funcionario.idfuncionario.ToString() + "'";
            this.conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.conn);
            int resultados = cmd.ExecuteNonQuery();
            this.conn.Close();
            return resultados > 0;
        }

        public bool DeleteFuncionario(int id)
        {
            this.conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM funcionarios where idfuncionario='" + id.ToString() + "'", this.conn);
            int resultados = cmd.ExecuteNonQuery();
            this.conn.Close();
            return resultados > 0;

        }

        public Funcionario ObterFuncionarioByIdUtilizador(int idUser)
        {
            this.conn.Open();
            string query = "SELECT funcionarios.*, email, localidade, dept " +
                           "FROM funcionarios " +
                           "INNER JOIN utilizadores AS ut ON ut.idUtilizador = funcionarios.idUtilizador " +
                           "INNER JOIN localidades ON localidades.codpostal = funcionarios.codpostal " +
                           "INNER JOIN departamentos AS dept ON dept.iddept = funcionarios.iddept ";
            query += "WHERE ut.idUtilizador = '" + idUser.ToString() + "'";

            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Funcionario funcionario = ParseFuncionarios(linha);
                return funcionario;
            }
            return null;
        }

        public Funcionario ObterFuncionario(int id)
        {
            this.conn.Open();
            string query = "SELECT funcionarios.*, email, localidade, dept " +
                           "FROM funcionarios " +
                           "INNER JOIN utilizadores AS ut ON ut.idUtilizador = funcionarios.idUtilizador " +
                           "INNER JOIN localidades ON localidades.codpostal = funcionarios.codpostal " +
                           "INNER JOIN departamentos AS dept ON dept.iddept = funcionarios.iddept ";
            query += "WHERE idfuncionario = '" + id.ToString() + "'";

            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.conn.Close();
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
            funcionario.localidade = linha["localidade"].ToString();
            funcionario.iddept = int.Parse(linha["iddept"].ToString());
            funcionario.dept = linha["dept"].ToString();
            funcionario.cargo = linha["cargo"].ToString();
            funcionario.contacto = int.Parse(linha["contacto"].ToString());
            funcionario.idUtilizador = int.Parse(linha["idUtilizador"].ToString());
            funcionario.email = linha["email"].ToString();
            funcionario.ativo = bool.Parse(linha["ativo"].ToString());
            funcionario.ultlogin = DateTime.Parse(linha["ultlogin"].ToString());
            funcionario.obs = linha["obs"].ToString();
            return funcionario;
        }

    }
}