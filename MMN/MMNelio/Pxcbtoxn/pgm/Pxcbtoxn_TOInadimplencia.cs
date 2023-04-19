using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela INADIMPLENCIA</summary>
    public class TOInadimplencia : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codInadimplencia;
        #endregion

        #region Campos da tabela
        private CampoTabela<Double> cnpjResponsavel;
        private CampoTabela<Double> codCliente;
        private CampoTabela<DateTime> dataInadimplencia;
        private CampoTabela<String> indSituacao;
        private CampoTabela<String> nomeResponsavel;
        private CampoTabela<String> tipoPessoa;
        private CampoTabela<Double> valorInadimplente;
        private CampoTabela<String> nomeCliente;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo CNPJ_RESPONSAVEL da tabela INADIMPLENCIA</summary>
        [XmlElement("cnpj_responsavel")]
        public CampoTabela<Double> CnpjResponsavel
        {
            get { return cnpjResponsavel; }
            set { cnpjResponsavel = value; }
        }
        /// <summary>Campo COD_CLIENTE da tabela INADIMPLENCIA</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo COD_INADIMPLENCIA da tabela INADIMPLENCIA</summary>
        [XmlElement("cod_inadimplencia")]
        public CampoTabela<Int32> CodInadimplencia
        {
            get { return codInadimplencia; }
            set { codInadimplencia = value; }
        }
        /// <summary>Campo DATA_INADIMPLENCIA da tabela INADIMPLENCIA</summary>
        [XmlElement("data_inadimplencia")]
        public CampoTabela<DateTime> DataInadimplencia
        {
            get { return dataInadimplencia; }
            set { dataInadimplencia = value; }
        }
        /// <summary>Campo IND_SITUACAO da tabela INADIMPLENCIA</summary>
        [XmlElement("ind_situacao")]
        public CampoTabela<String> IndSituacao
        {
            get { return indSituacao; }
            set { indSituacao = value; }
        }
        /// <summary>Campo NOME_RESPONSAVEL da tabela INADIMPLENCIA</summary>
        [XmlElement("nome_responsavel")]
        public CampoTabela<String> NomeResponsavel
        {
            get { return nomeResponsavel; }
            set { nomeResponsavel = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela INADIMPLENCIA</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        /// <summary>Campo VALOR_INADIMPLENTE da tabela INADIMPLENCIA</summary>
        [XmlElement("valor_inadimplente")]
        public CampoTabela<Double> ValorInadimplente
        {
            get { return valorInadimplente; }
            set { valorInadimplente = value; }
        }
        /// <summary>NomeCliente.</summary>
        [XmlAttribute("nome_cliente")]
        public CampoTabela<String> NomeCliente
        {
            get { return this.nomeCliente; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOInadimplencia</summary>
        /// <param name="linha">Linha para popular os campos da TOInadimplencia</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "CNPJ_RESPONSAVEL":
                        this.cnpjResponsavel = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_INADIMPLENCIA":
                        this.codInadimplencia = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "DATA_INADIMPLENCIA":
                        this.dataInadimplencia = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "IND_SITUACAO":
                        this.indSituacao = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "NOME_RESPONSAVEL":
                        this.nomeResponsavel = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "VALOR_INADIMPLENTE":
                        this.valorInadimplente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "NOME_CLIENTE":
                        this.nomeCliente = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
