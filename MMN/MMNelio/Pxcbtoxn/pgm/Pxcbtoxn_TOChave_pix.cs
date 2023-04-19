using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela CHAVE_PIX</summary>
    public class TOChave_pix : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codAgencia;
        private CampoTabela<Int32> codConta;
        private CampoTabela<Int32> codEspecie;
        private CampoTabela<Int16> tipoChave;
        #endregion

        #region Campos da tabela
        private CampoTabela<String> conteudoChave;
        private CampoTabela<DateTime> dataCadastro;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo COD_AGENCIA da tabela CHAVE_PIX</summary>
        [XmlElement("cod_agencia")]
        public CampoTabela<Int32> CodAgencia
        {
            get { return codAgencia; }
            set { codAgencia = value; }
        }
        /// <summary>Campo COD_CONTA da tabela CHAVE_PIX</summary>
        [XmlElement("cod_conta")]
        public CampoTabela<Int32> CodConta
        {
            get { return codConta; }
            set { codConta = value; }
        }
        /// <summary>Campo COD_ESPECIE da tabela CHAVE_PIX</summary>
        [XmlElement("cod_especie")]
        public CampoTabela<Int32> CodEspecie
        {
            get { return codEspecie; }
            set { codEspecie = value; }
        }
        /// <summary>Campo CONTEUDO_CHAVE da tabela CHAVE_PIX</summary>
        [XmlElement("conteudo_chave")]
        public CampoTabela<String> ConteudoChave
        {
            get { return conteudoChave; }
            set { conteudoChave = value; }
        }
        /// <summary>Campo DATA_CADASTRO da tabela CHAVE_PIX</summary>
        [XmlElement("data_cadastro")]
        public CampoTabela<DateTime> DataCadastro
        {
            get { return dataCadastro; }
            set { dataCadastro = value; }
        }
        /// <summary>Campo TIPO_CHAVE da tabela CHAVE_PIX</summary>
        [XmlElement("tipo_chave")]
        public CampoTabela<Int16> TipoChave
        {
            get { return tipoChave; }
            set { tipoChave = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOChave_pix</summary>
        /// <param name="linha">Linha para popular os campos da TOChave_pix</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_AGENCIA":
                        this.codAgencia = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "COD_CONTA":
                        this.codConta = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "COD_ESPECIE":
                        this.codEspecie = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "CONTEUDO_CHAVE":
                        this.conteudoChave = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "DATA_CADASTRO":
                        this.dataCadastro = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "TIPO_CHAVE":
                        this.tipoChave = this.LeCampoTabela<Int16>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
