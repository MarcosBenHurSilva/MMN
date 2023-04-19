using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela ENDERECO</summary>
    public class TOEndereco : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codEndereco;
        #endregion

        #region Campos da tabela
        private CampoTabela<Int32> cep;
        private CampoTabela<Double> codCliente;
        private CampoTabela<String> indPreferencial;
        private CampoTabela<String> indTipo;
        private CampoTabela<String> logradouro;
        private CampoTabela<String> tipoPessoa;
        private CampoTabela<String> uf;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo CEP da tabela ENDERECO</summary>
        [XmlElement("cep")]
        public CampoTabela<Int32> Cep
        {
            get { return cep; }
            set { cep = value; }
        }
        /// <summary>Campo COD_CLIENTE da tabela ENDERECO</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo COD_ENDERECO da tabela ENDERECO</summary>
        [XmlElement("cod_endereco")]
        public CampoTabela<Int32> CodEndereco
        {
            get { return codEndereco; }
            set { codEndereco = value; }
        }
        /// <summary>Campo IND_PREFERENCIAL da tabela ENDERECO</summary>
        [XmlElement("ind_preferencial")]
        public CampoTabela<String> IndPreferencial
        {
            get { return indPreferencial; }
            set { indPreferencial = value; }
        }
        /// <summary>Campo IND_TIPO da tabela ENDERECO</summary>
        [XmlElement("ind_tipo")]
        public CampoTabela<String> IndTipo
        {
            get { return indTipo; }
            set { indTipo = value; }
        }
        /// <summary>Campo LOGRADOURO da tabela ENDERECO</summary>
        [XmlElement("logradouro")]
        public CampoTabela<String> Logradouro
        {
            get { return logradouro; }
            set { logradouro = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela ENDERECO</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        /// <summary>Campo UF da tabela ENDERECO</summary>
        [XmlElement("uf")]
        public CampoTabela<String> Uf
        {
            get { return uf; }
            set { uf = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOEndereco</summary>
        /// <param name="linha">Linha para popular os campos da TOEndereco</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "CEP":
                        this.cep = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_ENDERECO":
                        this.codEndereco = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "IND_PREFERENCIAL":
                        this.indPreferencial = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "IND_TIPO":
                        this.indTipo = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "LOGRADOURO":
                        this.logradouro = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "UF":
                        this.uf = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
