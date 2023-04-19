using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela SOCIO</summary>
    public class TOSocio : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Double> codClienteEmpresa;
        private CampoTabela<Double> codClienteSocio;
        private CampoTabela<String> tipoPessoaEmpresa;
        private CampoTabela<String> tipoPessoaSocio;
        #endregion

        #region Campos da tabela
        private CampoTabela<Double> participSocietaria;
        private CampoTabela<String> nomeClienteEmpresa;
        private CampoTabela<String> nomeClienteSocio;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo COD_CLIENTE_EMPRESA da tabela SOCIO</summary>
        [XmlElement("cod_cliente_empresa")]
        public CampoTabela<Double> CodClienteEmpresa
        {
            get { return codClienteEmpresa; }
            set { codClienteEmpresa = value; }
        }
        /// <summary>Campo COD_CLIENTE_SOCIO da tabela SOCIO</summary>
        [XmlElement("cod_cliente_socio")]
        public CampoTabela<Double> CodClienteSocio
        {
            get { return codClienteSocio; }
            set { codClienteSocio = value; }
        }
        /// <summary>Campo PARTICIP_SOCIETARIA da tabela SOCIO</summary>
        [XmlElement("particip_societaria")]
        public CampoTabela<Double> ParticipSocietaria
        {
            get { return participSocietaria; }
            set { participSocietaria = value; }
        }
        /// <summary>Campo TIPO_PESSOA_EMPRESA da tabela SOCIO</summary>
        [XmlElement("tipo_pessoa_empresa")]
        public CampoTabela<String> TipoPessoaEmpresa
        {
            get { return tipoPessoaEmpresa; }
            set { tipoPessoaEmpresa = value; }
        }
        /// <summary>Campo TIPO_PESSOA_SOCIO da tabela SOCIO</summary>
        [XmlElement("tipo_pessoa_socio")]
        public CampoTabela<String> TipoPessoaSocio
        {
            get { return tipoPessoaSocio; }
            set { tipoPessoaSocio = value; }
        }
        /// <summary>Campo NOME_CLIENTE da tabela CLIENTE</summary>
        [XmlElement("nome_cliente_empresa")]
        public CampoTabela<String> NomeClienteEmpresa
        {
            get { return nomeClienteEmpresa; }
            set { nomeClienteEmpresa = value; }
        }
        /// <summary>Campo NOME_CLIENTE da tabela CLIENTE</summary>
        [XmlElement("nome_cliente_socio")]
        public CampoTabela<String> NomeClienteSocio
        {
            get { return nomeClienteSocio; }
            set { nomeClienteSocio = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOSocio</summary>
        /// <param name="linha">Linha para popular os campos da TOSocio</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_CLIENTE_EMPRESA":
                        this.codClienteEmpresa = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_CLIENTE_SOCIO":
                        this.codClienteSocio = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "PARTICIP_SOCIETARIA":
                        this.participSocietaria = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA_EMPRESA":
                        this.tipoPessoaEmpresa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA_SOCIO":
                        this.tipoPessoaSocio = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "NOME_CLIENTE_SOCIO":
                        this.nomeClienteSocio = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "NOME_CLIENTE_EMPRESA":
                        this.nomeClienteEmpresa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
