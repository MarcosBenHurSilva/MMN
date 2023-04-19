using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela CAMBIO</summary>
    public class TOCambio : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Double> codCliente;
        private CampoTabela<Int32> codMoeda;
        private CampoTabela<DateTime> dataCompra;
        private CampoTabela<String> tipoPessoa;
        #endregion

        #region Campos da tabela
        private CampoTabela<Int32> quantidade;
        private CampoTabela<Double> valorTaxas;
        private CampoTabela<Double> valorTotal;
        private CampoTabela<Double> valorUnitario;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo COD_CLIENTE da tabela CAMBIO</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo COD_MOEDA da tabela CAMBIO</summary>
        [XmlElement("cod_moeda")]
        public CampoTabela<Int32> CodMoeda
        {
            get { return codMoeda; }
            set { codMoeda = value; }
        }
        /// <summary>Campo DATA_COMPRA da tabela CAMBIO</summary>
        [XmlElement("data_compra")]
        public CampoTabela<DateTime> DataCompra
        {
            get { return dataCompra; }
            set { dataCompra = value; }
        }
        /// <summary>Campo QUANTIDADE da tabela CAMBIO</summary>
        [XmlElement("quantidade")]
        public CampoTabela<Int32> Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela CAMBIO</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        /// <summary>Campo VALOR_TAXAS da tabela CAMBIO</summary>
        [XmlElement("valor_taxas")]
        public CampoTabela<Double> ValorTaxas
        {
            get { return valorTaxas; }
            set { valorTaxas = value; }
        }
        /// <summary>Campo VALOR_TOTAL da tabela CAMBIO</summary>
        [XmlElement("valor_total")]
        public CampoTabela<Double> ValorTotal
        {
            get { return valorTotal; }
            set { valorTotal = value; }
        }
        /// <summary>Campo VALOR_UNITARIO da tabela CAMBIO</summary>
        [XmlElement("valor_unitario")]
        public CampoTabela<Double> ValorUnitario
        {
            get { return valorUnitario; }
            set { valorUnitario = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOCambio</summary>
        /// <param name="linha">Linha para popular os campos da TOCambio</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_MOEDA":
                        this.codMoeda = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "DATA_COMPRA":
                        this.dataCompra = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "QUANTIDADE":
                        this.quantidade = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "VALOR_TAXAS":
                        this.valorTaxas = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "VALOR_TOTAL":
                        this.valorTotal = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "VALOR_UNITARIO":
                        this.valorUnitario = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
