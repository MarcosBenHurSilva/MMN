using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela BANRICOMPRAS</summary>
    public class TOBanricompras : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codMovimento;
        #endregion

        #region Campos da tabela
        private CampoTabela<Double> codCliente;
        private CampoTabela<DateTime> dataAdiantamento;
        private CampoTabela<DateTime> dataVencto;
        private CampoTabela<String> tipoPessoa;
        private CampoTabela<Double> valorAdiantado;
        private CampoTabela<Double> valorMovto;
        #endregion
        private CampoTabela<Double> taxa;

        #endregion

        #region Propriedades
        /// <summary>Campo COD_CLIENTE da tabela BANRICOMPRAS</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo COD_MOVIMENTO da tabela BANRICOMPRAS</summary>
        [XmlElement("cod_movimento")]
        public CampoTabela<Int32> CodMovimento
        {
            get { return codMovimento; }
            set { codMovimento = value; }
        }
        /// <summary>Campo DATA_ADIANTAMENTO da tabela BANRICOMPRAS</summary>
        [XmlElement("data_adiantamento")]
        public CampoTabela<DateTime> DataAdiantamento
        {
            get { return dataAdiantamento; }
            set { dataAdiantamento = value; }
        }
        /// <summary>Campo DATA_VENCTO da tabela BANRICOMPRAS</summary>
        [XmlElement("data_vencto")]
        public CampoTabela<DateTime> DataVencto
        {
            get { return dataVencto; }
            set { dataVencto = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela BANRICOMPRAS</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        /// <summary>Campo VALOR_ADIANTADO da tabela BANRICOMPRAS</summary>
        [XmlElement("valor_adiantado")]
        public CampoTabela<Double> ValorAdiantado
        {
            get { return valorAdiantado; }
            set { valorAdiantado = value; }
        }
        /// <summary>Campo VALOR_MOVTO da tabela BANRICOMPRAS</summary>
        [XmlElement("valor_movto")]
        public CampoTabela<Double> ValorMovto
        {
            get { return valorMovto; }
            set { valorMovto = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("taxa")]
        public CampoTabela<Double> Taxa
        {
            get { return taxa; }
            set { taxa = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOBanricompras</summary>
        /// <param name="linha">Linha para popular os campos da TOBanricompras</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_MOVIMENTO":
                        this.codMovimento = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "DATA_ADIANTAMENTO":
                        this.dataAdiantamento = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "DATA_VENCTO":
                        this.dataVencto = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "VALOR_ADIANTADO":
                        this.valorAdiantado = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "VALOR_MOVTO":
                        this.valorMovto = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
