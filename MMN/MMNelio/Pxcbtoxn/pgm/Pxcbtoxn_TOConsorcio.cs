using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela CONSORCIO</summary>
    public class TOConsorcio : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codConsorcio;
        #endregion

        #region Campos da tabela
        private CampoTabela<Int32> anoModelo;
        private CampoTabela<Double> codCliente;
        private CampoTabela<String> indSituacao;
        private CampoTabela<Int32> prazo;
        private CampoTabela<Double> taxa;
        private CampoTabela<String> tipoPessoa;
        private CampoTabela<String> tipoVeiculo;
        private CampoTabela<Double> valorVeiculo;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo ANO_MODELO da tabela CONSORCIO</summary>
        [XmlElement("ano_modelo")]
        public CampoTabela<Int32> AnoModelo
        {
            get { return anoModelo; }
            set { anoModelo = value; }
        }
        /// <summary>Campo COD_CLIENTE da tabela CONSORCIO</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo COD_CONSORCIO da tabela CONSORCIO</summary>
        [XmlElement("cod_consorcio")]
        public CampoTabela<Int32> CodConsorcio
        {
            get { return codConsorcio; }
            set { codConsorcio = value; }
        }
        /// <summary>Campo IND_SITUACAO da tabela CONSORCIO</summary>
        [XmlElement("ind_situacao")]
        public CampoTabela<String> IndSituacao
        {
            get { return indSituacao; }
            set { indSituacao = value; }
        }
        /// <summary>Campo PRAZO da tabela CONSORCIO</summary>
        [XmlElement("prazo")]
        public CampoTabela<Int32> Prazo
        {
            get { return prazo; }
            set { prazo = value; }
        }
        /// <summary>Campo TAXA da tabela CONSORCIO</summary>
        [XmlElement("taxa")]
        public CampoTabela<Double> Taxa
        {
            get { return taxa; }
            set { taxa = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela CONSORCIO</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        /// <summary>Campo TIPO_VEICULO da tabela CONSORCIO</summary>
        [XmlElement("tipo_veiculo")]
        public CampoTabela<String> TipoVeiculo
        {
            get { return tipoVeiculo; }
            set { tipoVeiculo = value; }
        }
        /// <summary>Campo VALOR_VEICULO da tabela CONSORCIO</summary>
        [XmlElement("valor_veiculo")]
        public CampoTabela<Double> ValorVeiculo
        {
            get { return valorVeiculo; }
            set { valorVeiculo = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOConsorcio</summary>
        /// <param name="linha">Linha para popular os campos da TOConsorcio</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "ANO_MODELO":
                        this.anoModelo = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_CONSORCIO":
                        this.codConsorcio = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "IND_SITUACAO":
                        this.indSituacao = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "PRAZO":
                        this.prazo = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "TAXA":
                        this.taxa = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_VEICULO":
                        this.tipoVeiculo = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "VALOR_VEICULO":
                        this.valorVeiculo = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
