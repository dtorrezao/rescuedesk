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
            string query = "SELECT * FROM mensagens ";

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

        public bool CreateMensagem(Mensagem mensagem, Utilizador utilizador)
        {
            string query = "INSERT INTO `mensagens` " +
               " (`idmensagem`, `assunto`, `corpo`, `emissor`, `recetor`, `dtenviado`) " +
               "VALUES(null,'" + mensagem.assunto + "', '" + mensagem.corpo + "', '" + utilizador.idUtilizador + "', '" + /*mensagem.recetor.ToString()*/ 15 + "', '" + mensagem.dtenviado.ToString("yyyy-MM-dd hh:mm:ss") + "')";
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
            mensagem.recetor = int.Parse(linha["recetor"].ToString());
            mensagem.dtenviado = DateTime.Parse(linha["dtenviado"].ToString());
            mensagem.lido = bool.Parse(linha["lido"].ToString());



            return mensagem;
        }


    }
}