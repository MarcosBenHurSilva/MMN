using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela COTACAO_MOEDAS</summary>
    public class TOCotacao_moedas : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<DateTime> dataCotacao;
        #endregion

        #region Campos da tabela
        private CampoTabela<Double> valorCotacaoEur;
        private CampoTabela<Double> valorCotacaoUsd;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo DATA_COTACAO da tabela COTACAO_MOEDAS</summary>
        [XmlElement("data_cotacao")]
        public CampoTabela<DateTime> DataCotacao
        {
            get { return dataCotacao; }
            set { dataCotacao = value; }
        }
        /// <summary>Campo VALOR_COTACAO_EUR da tabela COTACAO_MOEDAS</summary>
        [XmlElement("valor_cotacao_eur")]
        public CampoTabela<Double> ValorCotacaoEur
        {
            get { return valorCotacaoEur; }
            set { valorCotacaoEur = value; }
        }
        /// <summary>Campo VALOR_COTACAO_USD da tabela COTACAO_MOEDAS</summary>
        [XmlElement("valor_cotacao_usd")]
        public CampoTabela<Double> ValorCotacaoUsd
        {
            get { return valorCotacaoUsd; }
            set { valorCotacaoUsd = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOCotacao_moedas</summary>
        /// <param name="linha">Linha para popular os campos da TOCotacao_moedas</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "DATA_COTACAO":
                        this.dataCotacao = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "VALOR_COTACAO_EUR":
                        this.valorCotacaoEur = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "VALOR_COTACAO_USD":
                        this.valorCotacaoUsd = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
