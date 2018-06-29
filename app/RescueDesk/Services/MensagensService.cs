using MySql.Data.MySqlClient;
using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public class MensagensService
    {
        public int TipoMensagem;

        private MySqlConnection Conn = new MySqlConnection(Utils.ConnectionString());

        public List<Mensagem> ObterMensagens(Utilizador utilizador, bool obterEnviadas, bool obterRecebidas)
        {
            List<Mensagem> Mensagens = new List<Mensagem>();
            this.Conn.Open();
            string query = "SELECT uEmissor.email EmissorEmail, uReceptor.email RecetorEmail, m. *FROM mensagens m";
            query += " INNER join utilizadores uEmissor on m.emissor = uEmissor.idUtilizador";
            query += " INNER join utilizadores uReceptor on m.recetor = uReceptor.idUtilizador";

            if (obterEnviadas || obterRecebidas)
            {
                query += " WHERE";

                if (obterEnviadas)
                {
                    query += " emissor = " + utilizador.idUtilizador;
                }

                if (obterEnviadas && obterRecebidas)
                {
                    query += " OR";
                }

                if (obterRecebidas)
                {
                    query += " recetor = " + utilizador.idUtilizador;
                }
            }

            query += " ORDER BY dtenviado DESC";

            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Mensagem mensagem = ParseMensagem(linha);
                Mensagens.Add(mensagem);
            }
            return Mensagens;
        }

        public bool UpdateMensagem(Mensagem mensagem, bool msg)
        {
            string query = "UPDATE `mensagens`";
            query += "SET `lido` = '" + (msg ? 1 : 0).ToString() + "' " +
                 " WHERE `mensagens`.`idmensagem` = '" + mensagem.idmensagem.ToString() + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public Mensagem ObterMensagem(int id)
        {
            this.Conn.Open();

            string query = "SELECT uEmissor.email EmissorEmail, uReceptor.email RecetorEmail, m. *FROM mensagens m";
            query += " INNER join utilizadores uEmissor on m.emissor = uEmissor.idUtilizador";
            query += " INNER join utilizadores uReceptor on m.recetor = uReceptor.idUtilizador";
            query += " where idmensagem = '" + id.ToString() + "'";

            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Mensagem mensagem = ParseMensagem(linha);
                return mensagem;
            }
            return null;
        }

        public bool CreateMensagem(Mensagem mensagem, Utilizador utilizador)
        {
            string query = "INSERT INTO `mensagens` " +
               " (`idmensagem`, `assunto`, `corpo`, `emissor`, `recetor`, `dtenviado`) " +
               "VALUES(null,'" + mensagem.assunto + "', '" + mensagem.corpo + "', '" + utilizador.idUtilizador + "', '" + mensagem.recetor.ToString() + "', '" + mensagem.dtenviado.ToString("yyyy-MM-dd hh:mm:ss") + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        private static Mensagem ParseMensagem(DataRow linha)
        {
            Mensagem mensagem = new Mensagem();
            mensagem.idmensagem = int.Parse(linha["idmensagem"].ToString());
            mensagem.assunto = linha["assunto"].ToString();
            mensagem.corpo = linha["corpo"].ToString();
            mensagem.emissor = int.Parse(linha["emissor"].ToString());
            mensagem.emissorEmail = linha["EmissorEmail"].ToString();
            mensagem.recetor = int.Parse(linha["recetor"].ToString());
            mensagem.recetorEmail = linha["RecetorEmail"].ToString();
            mensagem.dtenviado = DateTime.Parse(linha["dtenviado"].ToString());
            mensagem.lido = bool.Parse(linha["lido"].ToString());



            return mensagem;
        }


    }
}