using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela CONTATO</summary>
    public class TOContato : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Double> codCliente;
        private CampoTabela<DateTime> datahora;
        private CampoTabela<String> tipoPessoa;
        #endregion

        #region Campos da tabela
        private CampoTabela<String> descricao;
        private CampoTabela<String> indSituacao;
        private CampoTabela<String> telefone;
        private CampoTabela<String> tipoContato;
        private CampoTabela<Int32> valorEnvolvido;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo COD_CLIENTE da tabela CONTATO</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo DATAHORA da tabela CONTATO</summary>
        [XmlElement("datahora")]
        public CampoTabela<DateTime> Datahora
        {
            get { return datahora; }
            set { datahora = value; }
        }
        /// <summary>Campo DESCRICAO da tabela CONTATO</summary>
        [XmlElement("descricao")]
        public CampoTabela<String> Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        /// <summary>Campo IND_SITUACAO da tabela CONTATO</summary>
        [XmlElement("ind_situacao")]
        public CampoTabela<String> IndSituacao
        {
            get { return indSituacao; }
            set { indSituacao = value; }
        }
        /// <summary>Campo TELEFONE da tabela CONTATO</summary>
        [XmlElement("telefone")]
        public CampoTabela<String> Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }
        /// <summary>Campo TIPO_CONTATO da tabela CONTATO</summary>
        [XmlElement("tipo_contato")]
        public CampoTabela<String> TipoContato
        {
            get { return tipoContato; }
            set { tipoContato = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela CONTATO</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        /// <summary>Campo VALOR_ENVOLVIDO da tabela CONTATO</summary>
        [XmlElement("valor_envolvido")]
        public CampoTabela<Int32> ValorEnvolvido
        {
            get { return valorEnvolvido; }
            set { valorEnvolvido = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOContato</summary>
        /// <param name="linha">Linha para popular os campos da TOContato</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "DATAHORA":
                        this.datahora = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "DESCRICAO":
                        this.descricao = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "IND_SITUACAO":
                        this.indSituacao = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TELEFONE":
                        this.telefone = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_CONTATO":
                        this.tipoContato = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "VALOR_ENVOLVIDO":
                        this.valorEnvolvido = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
