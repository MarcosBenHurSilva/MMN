using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcqscxn
{
    /// <summary>
    /// Classe de acesso a tabela SOCIO
    /// </summary>
    public class BDSocio : AplicacaoDados
    {
        #region Métodos
        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toSocio">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOSocio>> Listar(TOSocio toSocio)
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
                this.Sql.Comando.Append("SOC.COD_CLIENTE_EMPRESA, ");
                this.Sql.Comando.Append("SOC.COD_CLIENTE_SOCIO, ");
                this.Sql.Comando.Append("SOC.PARTICIP_SOCIETARIA, ");
                this.Sql.Comando.Append("SOC.TIPO_PESSOA_EMPRESA, ");
                this.Sql.Comando.Append("SOC.TIPO_PESSOA_SOCIO, ");
                this.Sql.Comando.Append("CLE.NOME_CLIENTE AS NOME_CLIENTE_EMPRESA, ");
                this.Sql.Comando.Append("CLS.NOME_CLIENTE AS NOME_CLIENTE_SOCIO ");
                this.Sql.Comando.Append("FROM ((SOCIO AS SOC ");
                this.Sql.Comando.Append("INNER JOIN CLIENTE AS CLE ON ");
                this.Sql.Comando.Append("SOC.COD_CLIENTE_EMPRESA = CLE.COD_CLIENTE AND ");
                this.Sql.Comando.Append("SOC.TIPO_PESSOA_EMPRESA = CLE.TIPO_PESSOA) ");
                this.Sql.Comando.Append("INNER JOIN CLIENTE AS CLS ON ");
                this.Sql.Comando.Append("SOC.COD_CLIENTE_SOCIO = CLS.COD_CLIENTE AND ");
                this.Sql.Comando.Append("SOC.TIPO_PESSOA_SOCIO = CLS.TIPO_PESSOA)");
                //Monta os campos de chave primária
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toSocio, "SOC.");
                //Monta os demais campos da tabela
                this.MontarCampos(this.Sql.MontarCampoWhere, toSocio, "SOC.");
                //Executa a consulta na tabela
                Retorno<List<Linha>> retListar = this.Consultar();
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOSocio>>(retListar.Mensagem);
                }
                List<TOSocio> lista = new List<TOSocio>();
                foreach (Linha linha in retListar.Dados)
                {
                    TOSocio teste = new TOSocio();
                    teste.PopularRetorno(linha);
                    lista.Add(teste);
                }
                return this.Infra.RetornarSucesso<List<TOSocio>>(lista, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOSocio>>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de inclusão na tabela
        /// </summary>
        /// <param name="toSocio">Campos para inclusão</param>
        /// <returns>Retorna a quantidade de registros incluídos</returns>
        public Retorno<Int32> Incluir(TOSocio toSocio)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("INSERT INTO SOCIO (");
                this.MontarCamposChave(this.Sql.MontarCampoInsert, toSocio, String.Empty);
                this.MontarCampos(this.Sql.MontarCampoInsert, toSocio, String.Empty);
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
        /// <param name="toSocio">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOSocio toSocio)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("UPDATE SOCIO SET ");
                this.MontarCampos(this.Sql.MontarCampoSet, toSocio, String.Empty);
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toSocio, String.Empty);
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
        /// <param name="toSocio">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOSocio toSocio)
        {
            try
            {
                this.Sql.Comando.Length = 0;
                this.Sql.Temporario.Length = 0;
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("DELETE FROM SOCIO");
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toSocio, String.Empty);

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
        /// <param name="toSocio"></param>
        /// <param name="alias"></param>
        private void MontarCamposChave(MontarCampo montagem, TOSocio toSocio, String alias)
        {
            montagem.Invoke(alias + "TIPO_PESSOA_SOCIO", toSocio.TipoPessoaSocio);
            montagem.Invoke(alias + "COD_CLIENTE_SOCIO", toSocio.CodClienteSocio);
            montagem.Invoke(alias + "TIPO_PESSOA_EMPRESA", toSocio.TipoPessoaEmpresa);
            montagem.Invoke(alias + "COD_CLIENTE_EMPRESA", toSocio.CodClienteEmpresa);
        }

        /// <summary>
        /// Campos não chave primária na tabela
        /// </summary>
        /// <param name="montagem">Método que será acionado na execução</param>
        /// <param name="toSocio">Campos da tabela</param>
        /// <param name="alias"></param>
        private void MontarCampos(MontarCampo montagem, TOSocio toSocio, String alias)
        {
            montagem.Invoke(alias + "PARTICIP_SOCIETARIA", toSocio.ParticipSocietaria);
        }
        #endregion
    }
}
