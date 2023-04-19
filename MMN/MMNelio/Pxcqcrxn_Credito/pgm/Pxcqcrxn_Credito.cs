using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcqcrxn
{
    /// <summary>
    /// Classe de acesso a tabela CREDITO
    /// </summary>
    public class BDCredito : AplicacaoDados
    {
        #region Métodos
        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toCredito">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOCredito>> Listar(TOCredito toCredito)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("SELECT ");
                this.Sql.Comando.Append("CRE.COD_CLIENTE, ");
                this.Sql.Comando.Append("CRE.TIPO_PESSOA, ");
                this.Sql.Comando.Append("CRE.COD_CREDITO, ");
                this.Sql.Comando.Append("CRE.DATA_CREDITO, ");
                this.Sql.Comando.Append("CRE.TAMANHO, ");
                this.Sql.Comando.Append("CRE.TEMPO_FINANC, ");
                this.Sql.Comando.Append("CRE.TIPO_IMOVEL, ");
                this.Sql.Comando.Append("CRE.VALOR_FINANC, ");
                this.Sql.Comando.Append("CLI.NOME_CLIENTE ");
                this.Sql.Comando.Append("FROM CREDITO CRE INNER JOIN CLIENTE CLI ON CLI.COD_CLIENTE = CRE.COD_CLIENTE AND CLI.TIPO_PESSOA = CRE.TIPO_PESSOA");
                //Monta os campos de chave primária
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toCredito, "CRE.");
                //Monta os demais campos da tabela
                this.MontarCampos(this.Sql.MontarCampoWhere, toCredito, "CRE.");
                //Executa a consulta na tabela
                Retorno<List<Linha>> retListar = this.Consultar();
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOCredito>>(retListar.Mensagem);
                }
                List<TOCredito> lista = new List<TOCredito>();
                foreach (Linha linha in retListar.Dados)
                {
                    TOCredito toCreditoLinha = new TOCredito();
                    toCreditoLinha.PopularRetorno(linha);
                    lista.Add(toCreditoLinha);
                }
                return this.Infra.RetornarSucesso<List<TOCredito>>(lista, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOCredito>>(new Mensagem(e));
            }
        }


        /// <summary>
        /// Executa o comando de inclusão na tabela
        /// </summary>
        /// <param name="toCredito">Campos para inclusão</param>
        /// <returns>Retorna a quantidade de registros incluídos</returns>
        public Retorno<Int32> Incluir(TOCredito toCredito)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("INSERT INTO CREDITO (");
                this.MontarCamposChave(this.Sql.MontarCampoInsert, toCredito, String.Empty);
                this.MontarCampos(this.Sql.MontarCampoInsert, toCredito, String.Empty);
                this.Sql.Comando.Append(") VALUES (");
                this.Sql.Comando.Append(this.Sql.Temporario.ToString());
                this.Sql.Comando.Append(")");
                Retorno<Int32> retExecutar = this.Executar();
                if (!retExecutar.Ok)
                {
                    return retExecutar;
                }
                return this.Infra.RetornarSucesso<Int32>(retExecutar.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de atualização na tabela
        /// </summary>
        /// <param name="toCredito">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOCredito toCredito)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("UPDATE CREDITO SET ");
                this.MontarCampos(this.Sql.MontarCampoSet, toCredito, String.Empty);
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toCredito, String.Empty);
                Retorno<Int32> retExecutar = this.Executar();
                if (!retExecutar.Ok)
                {
                    return retExecutar;
                }
                if (retExecutar.Dados == 0)
                {
                    return this.Infra.RetornarFalha<Int32>(new RegistroInexistenteMensagem());
                }
                return this.Infra.RetornarSucesso<Int32>(retExecutar.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de exclusão na tabela
        /// </summary>
        /// <param name="toCredito">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOCredito toCredito)
        {
            try
            {
                this.Sql.Comando.Length = 0;
                this.Sql.Temporario.Length = 0;
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("DELETE FROM CREDITO");
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toCredito, String.Empty);

                Retorno<Int32> retExecutar = this.Executar();
                if (!retExecutar.Ok)
                {
                    return retExecutar;
                }
                if (retExecutar.Dados == 0)
                {
                    return this.Infra.RetornarFalha<Int32>(new RegistroInexistenteMensagem());
                }
                else
                {
                    return this.Infra.RetornarSucesso<Int32>(retExecutar.Dados, new OperacaoRealizadaMensagem());
                }
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Campos de chave primária da tabela
        /// </summary>
        /// <param name="montagem"></param>
        /// <param name="toCredito"></param>
        /// <param name="alias">Alias da tabela</param>
        private void MontarCamposChave(MontarCampo montagem, TOCredito toCredito, string alias)
        {
            montagem.Invoke(alias + "COD_CREDITO", toCredito.CodCredito);
        }

        /// <summary>
        /// Campos não chave primária na tabela
        /// </summary>
        /// <param name="montagem">Método que será acionado na execução</param>
        /// <param name="toCredito">Campos da tabela</param>
        /// <param name="alias">Alias da tabela</param>
        private void MontarCampos(MontarCampo montagem, TOCredito toCredito, string alias)
        {
            montagem.Invoke(alias + "COD_CLIENTE", toCredito.CodCliente);
            montagem.Invoke(alias + "DATA_CREDITO", toCredito.DataCredito);
            montagem.Invoke(alias + "TAMANHO", toCredito.Tamanho);
            montagem.Invoke(alias + "TEMPO_FINANC", toCredito.TempoFinanc);
            montagem.Invoke(alias + "TIPO_IMOVEL", toCredito.TipoImovel);
            montagem.Invoke(alias + "TIPO_PESSOA", toCredito.TipoPessoa);
            montagem.Invoke(alias + "VALOR_FINANC", toCredito.ValorFinanc);
        }
        #endregion
    }
}
