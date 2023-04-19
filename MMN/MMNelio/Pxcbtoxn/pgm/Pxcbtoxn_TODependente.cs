using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela DEPENDENTE</summary>
    public class TODependente : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codDependente;
        #endregion

        #region Campos da tabela
        private CampoTabela<Double> codCliente;
        private CampoTabela<Double> cpf;
        private CampoTabela<DateTime> dataNasc;
        private CampoTabela<String> indSituacao;
        private CampoTabela<String> nomeDependente;
        private CampoTabela<String> tipoPessoa;
        private CampoTabela<String> nomeCliente;
        private CampoTabela<String> telefone;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo COD_CLIENTE da tabela DEPENDENTE</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo COD_DEPENDENTE da tabela DEPENDENTE</summary>
        [XmlElement("cod_dependente")]
        public CampoTabela<Int32> CodDependente
        {
            get { return codDependente; }
            set { codDependente = value; }
        }
        /// <summary>Campo CPF da tabela DEPENDENTE</summary>
        [XmlElement("cpf")]
        public CampoTabela<Double> Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }
        /// <summary>Campo DATA_NASC da tabela DEPENDENTE</summary>
        [XmlElement("data_nasc")]
        public CampoTabela<DateTime> DataNasc
        {
            get { return dataNasc; }
            set { dataNasc = value; }
        }
        /// <summary>Campo IND_SITUACAO da tabela DEPENDENTE</summary>
        [XmlElement("ind_situacao")]
        public CampoTabela<String> IndSituacao
        {
            get { return indSituacao; }
            set { indSituacao = value; }
        }
        /// <summary>Campo NOME_DEPENDENTE da tabela DEPENDENTE</summary>
        [XmlElement("nome_dependente")]
        public CampoTabela<String> NomeDependente
        {
            get { return nomeDependente; }
            set { nomeDependente = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela DEPENDENTE</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        /// <summary>Campo NOME_CLIENTE da tabela CLIENTE</summary>
        [XmlElement("nome_cliente")]
        public CampoTabela<String> NomeCliente
        {
            get { return nomeCliente; }
            //set { nomeCliente = value; }
        }
        /// <summary>Campo TELEFONE da tabela CLIENTE</summary>
        [XmlElement("telefone")]
        public CampoTabela<String> Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TODependente</summary>
        /// <param name="linha">Linha para popular os campos da TODependente</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_DEPENDENTE":
                        this.codDependente = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "CPF":
                        this.cpf = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "DATA_NASC":
                        this.dataNasc = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "IND_SITUACAO":
                        this.indSituacao = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "NOME_DEPENDENTE":
                        this.nomeDependente = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "NOME_CLIENTE":
                        this.nomeCliente = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TELEFONE":
                        this.telefone = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
