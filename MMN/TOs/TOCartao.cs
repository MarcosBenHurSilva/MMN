using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela CARTAO</summary>
    public class TOCartao : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> numCartao;
        #endregion

        #region Campos da tabela
        private CampoTabela<String> bandeira;
        private CampoTabela<Double> codCliente;
        private CampoTabela<Double> limite;
        private CampoTabela<String> tipoPessoa;
        #endregion
        private CampoTabela<String> nomeCliente;

        #endregion

        #region Propriedades
        /// <summary>Campo BANDEIRA da tabela CARTAO</summary>
        [XmlElement("bandeira")]
        public CampoTabela<String> Bandeira
        {
            get { return bandeira; }
            set { bandeira = value; }
        }
        /// <summary>Campo COD_CLIENTE da tabela CARTAO</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo LIMITE da tabela CARTAO</summary>
        [XmlElement("limite")]
        public CampoTabela<Double> Limite
        {
            get { return limite; }
            set { limite = value; }
        }
        /// <summary>Campo NUM_CARTAO da tabela CARTAO</summary>
        [XmlElement("num_cartao")]
        public CampoTabela<Int32> NumCartao
        {
            get { return numCartao; }
            set { numCartao = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela CARTAO</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
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
        /// <summary>Método para popular os campos da TOCartao</summary>
        /// <param name="linha">Linha para popular os campos da TOCartao</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "BANDEIRA":
                        this.bandeira = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "LIMITE":
                        this.limite = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "NUM_CARTAO":
                        this.numCartao = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
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
