using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela TABELA1</summary>
    public class TOTabela1 : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codigo;
        #endregion

        #region Campos da tabela
        private CampoTabela<DateTime> data;
        private CampoTabela<String> texto;
        private CampoTabela<Double> valor;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo CODIGO da tabela TABELA1</summary>
        [XmlElement("codigo")]
        public CampoTabela<Int32> Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        /// <summary>Campo DATA da tabela TABELA1</summary>
        [XmlElement("data")]
        public CampoTabela<DateTime> Data
        {
            get { return data; }
            set { data = value; }
        }
        /// <summary>Campo TEXTO da tabela TABELA1</summary>
        [XmlElement("texto")]
        public CampoTabela<String> Texto
        {
            get { return texto; }
            set { texto = value; }
        }
        /// <summary>Campo VALOR da tabela TABELA1</summary>
        [XmlElement("valor")]
        public CampoTabela<Double> Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOTabela1</summary>
        /// <param name="linha">Linha para popular os campos da TOTabela1</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "CODIGO":
                        this.codigo = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "DATA":
                        this.data = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "TEXTO":
                        this.texto = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "VALOR":
                        this.valor = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
