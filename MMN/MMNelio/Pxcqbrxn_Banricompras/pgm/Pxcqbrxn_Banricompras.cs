using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;
using System.Globalization;

namespace Bergs.Pxc.Pxcqbrxn
{
    /// <summary>
    /// Classe de acesso a tabela BANRICOMPRAS
    /// </summary>
    public class BDBanricompras : AplicacaoDados
    {
        #region Métodos
        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toBanricompras">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOBanricompras>> Listar(TOBanricompras toBanricompras)
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
                this.Sql.Comando.Append("COD_CLIENTE, ");
                this.Sql.Comando.Append("COD_MOVIMENTO, ");
                this.Sql.Comando.Append("DATA_ADIANTAMENTO, ");
                this.Sql.Comando.Append("DATA_VENCTO, ");
                this.Sql.Comando.Append("TIPO_PESSOA, ");
                this.Sql.Comando.Append("VALOR_ADIANTADO, ");
                this.Sql.Comando.Append("VALOR_MOVTO ");
                this.Sql.Comando.Append("FROM BANRICOMPRAS");
                //Monta os campos de chave primária
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toBanricompras);
                //Monta os demais campos da tabela
                this.MontarCampos(this.Sql.MontarCampoWhere, toBanricompras);
                //Executa a consulta na tabela
                Retorno<List<Linha>> retListar = this.Consultar();
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOBanricompras>>(retListar.Mensagem);
                }
                List<TOBanricompras> lista = new List<TOBanricompras>();
                foreach (Linha linha in retListar.Dados)
                {
                    TOBanricompras toBanricomprasLinha = new TOBanricompras();
                    toBanricomprasLinha.PopularRetorno(linha);
                    lista.Add(toBanricomprasLinha);
                }
                return this.Infra.RetornarSucesso<List<TOBanricompras>>(lista, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOBanricompras>>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toBanricompras">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOBanricompras>> ListarMovimentos(TOBanricompras toBanricompras)
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
                this.Sql.Comando.Append("COD_CLIENTE, ");
                this.Sql.Comando.Append("COD_MOVIMENTO, ");
                this.Sql.Comando.Append("DATA_ADIANTAMENTO, ");
                this.Sql.Comando.Append("DATA_VENCTO, ");
                this.Sql.Comando.Append("TIPO_PESSOA, ");
                this.Sql.Comando.Append("VALOR_ADIANTADO, ");
                this.Sql.Comando.Append("VALOR_MOVTO ");
                this.Sql.Comando.Append("FROM BANRICOMPRAS");
                //Monta os campos de chave primária
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toBanricompras);
                //Monta os demais campos da tabela
                this.MontarCampos(this.Sql.MontarCampoWhere, toBanricompras);
                this.Sql.MontarCampoWhere("DATA_ADIANTAMENTO", ConstrutorSql.OperadorUnario.IsNull);
                this.Sql.MontarCampoWhere("VALOR_ADIANTADO", ConstrutorSql.OperadorUnario.IsNull);
                //Executa a consulta na tabela
                Retorno<List<Linha>> retListar = this.Consultar();
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOBanricompras>>(retListar.Mensagem);
                }
                List<TOBanricompras> lista = new List<TOBanricompras>();
                foreach (Linha linha in retListar.Dados)
                {
                    TOBanricompras teste = new TOBanricompras();
                    teste.PopularRetorno(linha);
                    lista.Add(teste);
                }
                return this.Infra.RetornarSucesso<List<TOBanricompras>>(lista, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOBanricompras>>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toBanricompras">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOBanricompras>> ListarAdiantamentos(TOBanricompras toBanricompras)
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
                this.Sql.Comando.Append("COD_CLIENTE, ");
                this.Sql.Comando.Append("COD_MOVIMENTO, ");
                this.Sql.Comando.Append("DATA_ADIANTAMENTO, ");
                this.Sql.Comando.Append("DATA_VENCTO, ");
                this.Sql.Comando.Append("TIPO_PESSOA, ");
                this.Sql.Comando.Append("VALOR_ADIANTADO, ");
                this.Sql.Comando.Append("VALOR_MOVTO ");
                this.Sql.Comando.Append("FROM BANRICOMPRAS");
                //Monta os campos de chave primária
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toBanricompras);
                //Monta os demais campos da tabela
                this.MontarCampos(this.Sql.MontarCampoWhere, toBanricompras);
                this.Sql.MontarCampoWhere("DATA_ADIANTAMENTO", ConstrutorSql.OperadorUnario.IsNotNull);
                this.Sql.MontarCampoWhere("VALOR_ADIANTADO", ConstrutorSql.OperadorUnario.IsNotNull);
                //Executa a consulta na tabela
                Retorno<List<Linha>> retListar = this.Consultar();
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOBanricompras>>(retListar.Mensagem);
                }
                List<TOBanricompras> lista = new List<TOBanricompras>();
                foreach (Linha linha in retListar.Dados)
                {
                    TOBanricompras teste = new TOBanricompras();
                    teste.PopularRetorno(linha);
                    lista.Add(teste);
                }
                return this.Infra.RetornarSucesso<List<TOBanricompras>>(lista, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOBanricompras>>(new Mensagem(e));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toBanricompras"></param>
        /// <returns></returns>
        public Retorno<Int32> RealizarAdiantamento(TOBanricompras toBanricompras)
        {
            try
            {
                CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                culture.NumberFormat = new NumberFormatInfo();
                culture.NumberFormat.NumberDecimalSeparator = ".";

                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("UPDATE BANRICOMPRAS SET ");
                this.Sql.MontarCampoSet("DATA_ADIANTAMENTO", new CampoTabela<DateTime>(DateTime.Now));
                this.Sql.Comando.Append(", VALOR_ADIANTADO = ");
                this.Sql.Comando.AppendFormat("VALOR_MOVTO - (VALOR_MOVTO * ({0} / 100))", toBanricompras.Taxa.LerConteudoOuPadrao().ToString(culture));
                this.MontarCampos(this.Sql.MontarCampoWhere, toBanricompras);
                Retorno<Int32> retExecutar = this.ExecutarMultiplosDados();
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
            catch (Exception ex)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(ex));
            }
        }

        /// <summary>
        /// Busca o total de movimentos dos últimos X dias
        /// </summary>
        /// <param name="toBanricompras"></param>
        /// <returns></returns>
        public Retorno<Double> BuscarTotalMovimentosUltimosDias(TOBanricompras toBanricompras)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();

                this.Sql.Comando.Append("SELECT IIF(COUNT(*)=0, 0, SUM(VALOR_MOVTO)) AS VALOR_MOVTO ");
                this.Sql.Comando.Append("FROM BANRICOMPRAS");
                this.Sql.MontarCampoWhere("COD_CLIENTE", toBanricompras.CodCliente);
                this.Sql.MontarCampoWhere("TIPO_PESSOA", toBanricompras.TipoPessoa);
                this.Sql.MontarCampoWhere("DATA_VENCTO", toBanricompras.DataVencto,
                    new CampoTabela<DateTime>(DateTime.Now), ConstrutorSql.OperadorUnario.Between);

                Retorno<List<Linha>> retListar = this.Consultar();
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<double>(retListar.Mensagem);
                }

                toBanricompras.PopularRetorno(retListar.Dados[0]);
                /*double totalMovimentos;
                double.TryParse(retListar.Dados[0].Campos[0].Conteudo.ToString(), out totalMovimentos);
                return this.RetornarSucesso<double>(totalMovimentos, new OperacaoRealizadaMensagem());*/
                return this.Infra.RetornarSucesso<double>(toBanricompras.ValorMovto.LerConteudoOuPadrao(), new OperacaoRealizadaMensagem());
            }
            catch (Exception ex)
            {
                return this.Infra.RetornarFalha<double>(new Mensagem(ex));
            }
        }

        /// <summary>
        /// Executa o comando de inclusão na tabela
        /// </summary>
        /// <param name="toBanricompras">Campos para inclusão</param>
        /// <returns>Retorna a quantidade de registros incluídos</returns>
        public Retorno<Int32> Incluir(TOBanricompras toBanricompras)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("SELECT NEXTVAL AS COD_MOVIMENTO FROM SEQ_COD_MOVIMENTO");
                Retorno<List<Linha>> retListar = this.Consultar();
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(retListar.Mensagem);
                }
                toBanricompras.PopularRetorno(retListar.Dados[0]);

                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("INSERT INTO BANRICOMPRAS (");
                this.MontarCamposChave(this.Sql.MontarCampoInsert, toBanricompras);
                this.MontarCampos(this.Sql.MontarCampoInsert, toBanricompras);
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
        /// <param name="toBanricompras">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOBanricompras toBanricompras)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("UPDATE BANRICOMPRAS SET ");
                this.MontarCampos(this.Sql.MontarCampoSet, toBanricompras);
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toBanricompras);
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
        /// <param name="toBanricompras">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOBanricompras toBanricompras)
        {
            try
            {
                this.Sql.Comando.Length = 0;
                this.Sql.Temporario.Length = 0;
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("DELETE FROM BANRICOMPRAS");
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toBanricompras);

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
        /// <param name="toBanricompras"></param>
        private void MontarCamposChave(MontarCampo montagem, TOBanricompras toBanricompras)
        {
            montagem.Invoke("COD_MOVIMENTO", toBanricompras.CodMovimento);
        }

        /// <summary>
        /// Campos não chave primária na tabela
        /// </summary>
        /// <param name="montagem">Método que será acionado na execução</param>
        /// <param name="toBanricompras">Campos da tabela</param>
        private void MontarCampos(MontarCampo montagem, TOBanricompras toBanricompras)
        {
            montagem.Invoke("COD_CLIENTE", toBanricompras.CodCliente);
            montagem.Invoke("DATA_ADIANTAMENTO", toBanricompras.DataAdiantamento);
            montagem.Invoke("DATA_VENCTO", toBanricompras.DataVencto);
            montagem.Invoke("TIPO_PESSOA", toBanricompras.TipoPessoa);
            montagem.Invoke("VALOR_ADIANTADO", toBanricompras.ValorAdiantado);
            montagem.Invoke("VALOR_MOVTO", toBanricompras.ValorMovto);
        }
        #endregion
    }
}
