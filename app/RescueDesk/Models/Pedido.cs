﻿using RescueDesk.Models.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Pedido
    {
        [DisplayName("ID")]
        public int idpedido { get; set; }
        [DisplayName("Assunto")]
        public string assunto { get; set; }

        public string assuntoRelatorio
        {
            get
            {
                int qtdCaracteres = 20;
                if (assunto.Length > qtdCaracteres)
                {
                    string myString = assunto.Substring(0, qtdCaracteres);
                    
                    if (myString.LastIndexOf(' ') != -1)
                    {
                        int index = myString.LastIndexOf(' ');

                        string outputString = myString.Substring(0, index);

                        return outputString + "...";
                    }

                    return myString + "...";
                }

                return assunto;
            }
        }

        public string assuntoAbreviado
        {
            get
            {
                int qtdCaracteres = 30;
                if (assunto.Length > qtdCaracteres)
                {
                    string myString = assunto.Substring(0, qtdCaracteres);

                    if (myString.LastIndexOf(' ') != -1)
                    {
                        int index = myString.LastIndexOf(' ');

                        string outputString = myString.Substring(0, index);

                        return outputString + "...";
                    }

                    return myString + "...";
                }

                return assunto;
            }
        }

        [DisplayName("Descrição")]
        public string descricao { get; set; }

        public string descricaoAbreviada
        {
            get
            {
                int qtdCaracteres = 30;
                if (descricao.Length > qtdCaracteres)
                {
                    string myString = descricao.Substring(0, qtdCaracteres);

                    int index = myString.LastIndexOf(' ');

                    string outputString = myString.Substring(0, index);

                    return outputString + "...";
                }

                return descricao;
            }
        }

        //[DisplayName("Serviço")]
        public int? idatividade { get; set; }
        [DisplayName("Serviço")]
        public string atividade { get; set; }
        [DisplayName("Pedido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dtpedido { get; set; }
        [DisplayName("Lido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dtlido { get; set; }
        [DisplayName("Marcado")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dtmarcado { get; set; }
        [DisplayName("Prioridade")]
        public prioridade? prioridade { get; set; }
        [DisplayName("Resolvido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dtresolvido { get; set; }

        public bool Resolvido
        {
            get { return dtresolvido != null; }
            set
            {
                if (value)
                {
                    dtresolvido = DateTime.Now;
                }
                else
                {
                    dtresolvido = null;
                }
            }
        }
        [DisplayName("Nº Contribuinte")]
        public int nrcontribuinte { get; set; }
        //[DisplayName("Funcionário")]
        public int? idfuncionario { get; set; }
        [DisplayName("Funcionário")]
        public string funcionario { get; set; }
        [DisplayName("Observações")]
        public string obs { get; set; }
    }
}