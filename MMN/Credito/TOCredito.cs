using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela CREDITO</summary>
    public class TOCredito : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codCredito;
        #endregion

        #region Campos da tabela
        private CampoTabela<Double> codCliente;
        private CampoTabela<DateTime> dataCredito;
        private CampoTabela<Double> tamanho;
        private CampoTabela<Int32> tempoFinanc;
        private CampoTabela<String> tipoImovel;
        private CampoTabela<String> tipoPessoa;
        private CampoTabela<Double> valorFinanc;
        #endregion
        private CampoTabela<String> nomeCliente;

        #endregion

        #region Propriedades
        /// <summary>Campo COD_CLIENTE da tabela CREDITO</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo COD_CREDITO da tabela CREDITO</summary>
        [XmlElement("cod_credito")]
        public CampoTabela<Int32> CodCredito
        {
            get { return codCredito; }
            set { codCredito = value; }
        }
        /// <summary>Campo DATA_CREDITO da tabela CREDITO</summary>
        [XmlElement("data_credito")]
        public CampoTabela<DateTime> DataCredito
        {
            get { return dataCredito; }
            set { dataCredito = value; }
        }
        /// <summary>Campo TAMANHO da tabela CREDITO</summary>
        [XmlElement("tamanho")]
        public CampoTabela<Double> Tamanho
        {
            get { return tamanho; }
            set { tamanho = value; }
        }
        /// <summary>Campo TEMPO_FINANC da tabela CREDITO</summary>
        [XmlElement("tempo_financ")]
        public CampoTabela<Int32> TempoFinanc
        {
            get { return tempoFinanc; }
            set { tempoFinanc = value; }
        }
        /// <summary>Campo TIPO_IMOVEL da tabela CREDITO</summary>
        [XmlElement("tipo_imovel")]
        public CampoTabela<String> TipoImovel
        {
            get { return tipoImovel; }
            set { tipoImovel = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela CREDITO</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        /// <summary>Campo VALOR_FINANC da tabela CREDITO</summary>
        [XmlElement("valor_financ")]
        public CampoTabela<Double> ValorFinanc
        {
            get { return valorFinanc; }
            set { valorFinanc = value; }
        }
        /// <summary>Campo NOME_CLIENTE da tabela CLIENTE</summary>
        [XmlElement("nome_cliente")]
        public CampoTabela<String> NomeCliente
        {
            get { return nomeCliente; }
            set { nomeCliente = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOCredito</summary>
        /// <param name="linha">Linha para popular os campos da TOCredito</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_CREDITO":
                        this.codCredito = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "DATA_CREDITO":
                        this.dataCredito = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "TAMANHO":
                        this.tamanho = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "TEMPO_FINANC":
                        this.tempoFinanc = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "TIPO_IMOVEL":
                        this.tipoImovel = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "VALOR_FINANC":
                        this.valorFinanc = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "NOME_CLIENTE":
                        this.NomeCliente = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
