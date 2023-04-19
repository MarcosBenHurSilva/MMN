using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela BOLETO</summary>
    public class TOBoleto : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codBoleto;
        #endregion

        #region Campos da tabela
        private CampoTabela<Double> codClienteEmissor;
        private CampoTabela<Double> codClienteSacado;
        private CampoTabela<DateTime> dataVencimento;
        private CampoTabela<String> situacaoBoleto;
        private CampoTabela<String> tipoPessoaEmissor;
        private CampoTabela<String> tipoPessoaSacado;
        private CampoTabela<Double> valor;

        private CampoTabela<String> nomeEmissor;
        private CampoTabela<String> nomeSacado;

        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo COD_BOLETO da tabela BOLETO</summary>
        [XmlElement("cod_boleto")]
        public CampoTabela<Int32> CodBoleto
        {
            get { return codBoleto; }
            set { codBoleto = value; }
        }
        /// <summary>Campo COD_CLIENTE_EMISSOR da tabela BOLETO</summary>
        [XmlElement("cod_cliente_emissor")]
        public CampoTabela<Double> CodClienteEmissor
        {
            get { return codClienteEmissor; }
            set { codClienteEmissor = value; }
        }
        /// <summary>Campo COD_CLIENTE_SACADO da tabela BOLETO</summary>
        [XmlElement("cod_cliente_sacado")]
        public CampoTabela<Double> CodClienteSacado
        {
            get { return codClienteSacado; }
            set { codClienteSacado = value; }
        }
        /// <summary>Campo DATA_VENCIMENTO da tabela BOLETO</summary>
        [XmlElement("data_vencimento")]
        public CampoTabela<DateTime> DataVencimento
        {
            get { return dataVencimento; }
            set { dataVencimento = value; }
        }
        /// <summary>Campo SITUACAO_BOLETO da tabela BOLETO</summary>
        [XmlElement("situacao_boleto")]
        public CampoTabela<String> SituacaoBoleto
        {
            get { return situacaoBoleto; }
            set { situacaoBoleto = value; }
        }
        /// <summary>Campo TIPO_PESSOA_EMISSOR da tabela BOLETO</summary>
        [XmlElement("tipo_pessoa_emissor")]
        public CampoTabela<String> TipoPessoaEmissor
        {
            get { return tipoPessoaEmissor; }
            set { tipoPessoaEmissor = value; }
        }
        /// <summary>Campo TIPO_PESSOA_SACADO da tabela BOLETO</summary>
        [XmlElement("tipo_pessoa_sacado")]
        public CampoTabela<String> TipoPessoaSacado
        {
            get { return tipoPessoaSacado; }
            set { tipoPessoaSacado = value; }
        }
        /// <summary>Campo VALOR da tabela BOLETO</summary>
        [XmlElement("valor")]
        public CampoTabela<Double> Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        /// <summary>Campo NOME_EMISSOR obtido por join na tabela CLIENTE</summary>
        [XmlElement("nome_emissor")]
        public CampoTabela<String> NomeEmissor
        {
            get { return nomeEmissor; }
            set { nomeEmissor = value; }
        }

        /// <summary>Campo NOME_SACADO obtido por join na tabela CLIENTE</summary>
        [XmlElement("nome_sacado")]
        public CampoTabela<String> NomeSacado
        {
            get { return nomeSacado; }
            set { nomeSacado = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOBoleto</summary>
        /// <param name="linha">Linha para popular os campos da TOBoleto</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_BOLETO":
                        this.codBoleto = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "COD_CLIENTE_EMISSOR":
                        this.codClienteEmissor = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_CLIENTE_SACADO":
                        this.codClienteSacado = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "DATA_VENCIMENTO":
                        this.dataVencimento = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "SITUACAO_BOLETO":
                        this.situacaoBoleto = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA_EMISSOR":
                        this.tipoPessoaEmissor = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA_SACADO":
                        this.tipoPessoaSacado = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "VALOR":
                        this.valor = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "NOME_EMISSOR":
                        this.nomeEmissor = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "NOME_SACADO":
                        this.nomeSacado = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
