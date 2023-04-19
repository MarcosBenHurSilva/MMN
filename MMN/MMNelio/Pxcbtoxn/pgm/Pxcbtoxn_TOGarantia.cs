using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela GARANTIA</summary>
    public class TOGarantia : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codGarantia;
        #endregion

        #region Campos da tabela
        private CampoTabela<Int32> codCredito;
        private CampoTabela<String> descrGarantia;
        private CampoTabela<Double> percGarantia;
        private CampoTabela<String> placa;
        private CampoTabela<String> tipoGarantia;
        #endregion

        private CampoTabela<Double> valorCredito;
        #endregion

        #region Propriedades
        /// <summary>Campo COD_CREDITO da tabela GARANTIA</summary>
        [XmlElement("cod_credito")]
        public CampoTabela<Int32> CodCredito
        {
            get { return codCredito; }
            set { codCredito = value; }
        }
        /// <summary>Campo COD_GARANTIA da tabela GARANTIA</summary>
        [XmlElement("cod_garantia")]
        public CampoTabela<Int32> CodGarantia
        {
            get { return codGarantia; }
            set { codGarantia = value; }
        }
        /// <summary>Campo DESCR_GARANTIA da tabela GARANTIA</summary>
        [XmlElement("descr_garantia")]
        public CampoTabela<String> DescrGarantia
        {
            get { return descrGarantia; }
            set { descrGarantia = value; }
        }
        /// <summary>Campo PERC_GARANTIA da tabela GARANTIA</summary>
        [XmlElement("perc_garantia")]
        public CampoTabela<Double> PercGarantia
        {
            get { return percGarantia; }
            set { percGarantia = value; }
        }
        /// <summary>Campo PLACA da tabela GARANTIA</summary>
        [XmlElement("placa")]
        public CampoTabela<String> Placa
        {
            get { return placa; }
            set { placa = value; }
        }
        /// <summary>Campo TIPO_GARANTIA da tabela GARANTIA</summary>
        [XmlElement("tipo_garantia")]
        public CampoTabela<String> TipoGarantia
        {
            get { return tipoGarantia; }
            set { tipoGarantia = value; }
        }
        /// <summary>Campo VALOR_FINANC da tabela CREDITO</summary>
        [XmlElement("valor_credito")]
        public CampoTabela<Double> ValorCredito
        {
            get { return valorCredito; }
            set { valorCredito = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOGarantia</summary>
        /// <param name="linha">Linha para popular os campos da TOGarantia</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_CREDITO":
                        this.codCredito = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "COD_GARANTIA":
                        this.codGarantia = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "DESCR_GARANTIA":
                        this.descrGarantia = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "PERC_GARANTIA":
                        this.percGarantia = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "PLACA":
                        this.placa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_GARANTIA":
                        this.tipoGarantia = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "VALOR_CREDITO":
                        this.valorCredito = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
