using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcqclxn;
using Bergs.Pxc.Pxcoiexn.Interface;

namespace Bergs.Pxc.Pxcsclxn
{
    /// <summary>
    /// Classe de acesso a tabela CLIENTE
    /// </summary>
    public class RNCliente : AplicacaoRegraNegocio
    {
        #region Métodos
        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toCliente">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOCliente>> Listar(TOCliente toCliente)
        {
            try
            {
                //TODO: regras de negócio
                BDCliente bdCliente = this.Infra.InstanciarBD<BDCliente>();
                Retorno<List<TOCliente>> retListar = bdCliente.Listar(toCliente);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOCliente>>(retListar.Mensagem);
                }
                return this.Infra.RetornarSucesso<List<TOCliente>>(retListar.Dados, new OperacaoRealizadaMensagem("Listagem"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOCliente>>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de inclusão na tabela
        /// </summary>
        /// <param name="toCliente">Campos para inclusão</param>
        /// <returns>Retorna a quantidade de registros incluídos</returns>
        public Retorno<Int32> Incluir(TOCliente toCliente)
        {
            try
            {
                #region Validação de campos obrigatórios
                if (!toCliente.NomeCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("NOME_CLIENTE"));
                }
                if (!toCliente.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toCliente.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                #endregion

                //regras de negócio
                //RN4 - validação do nome
                // "MIGUEL"

                //String emails = "miguel@gmail;jefferson@gmail.com";
                //String[] emailSplit = emails.Split(';');
                //for (int i = 0; i < emailSplit.Length; i++)
                //{

                //}

                // "MIGUEL JEFFERSON"
                //[0] = MIGUEL
                //[1] = JEFFERSON

                //Entrada, processamento e saida               

                String nome = toCliente.NomeCliente.LerConteudoOuPadrao().Trim();
                String[] nomes = nome.Split(' ');
                if (nomes.Length < 2)
                {
                    //mensagem de erro
                    return this.Infra.RetornarFalha<Int32>(new MensagemCliente(TipoFalha.NomePessoa));
                }
                String primeiroNome = nomes[0];
                if (primeiroNome.Length < 2)
                {
                    //mensagem de erro
                    return this.Infra.RetornarFalha<Int32>(new MensagemCliente(TipoFalha.NomePessoa));
                }

                if ((toCliente.TipoPessoa.LerConteudoOuPadrao() != "F") &&
                    (toCliente.TipoPessoa.LerConteudoOuPadrao() != "J"))
                {
                    return this.Infra.RetornarFalha<Int32>(new MensagemCliente(TipoFalha.TipoPessoaInvalido));
                }

                //Valide o CPF ou CNPJ
                //Item 2
                if (toCliente.TipoPessoa.LerConteudoOuPadrao() == "F")
                {
                    // 359557066
                    // 00359557066

                    if (!Util.ValidaCpf(
                        toCliente.CodCliente.LerConteudoOuPadrao().ToString().PadLeft(11, '0')))
                    {
                        return this.Infra.RetornarFalha<Int32>(new MensagemCliente(TipoFalha.CpfInvalido));
                    }
                }
                else
                {
                    if (!Util.ValidaCnpj(
                       toCliente.CodCliente.LerConteudoOuPadrao().ToString().PadLeft(14, '0')))
                    {
                        return this.Infra.RetornarFalha<Int32>(new MensagemCliente(TipoFalha.CnpjInvalido));
                    }
                }
                //exemplo: sempre atribuir a data atual como data de cadastro
                toCliente.DataCadastro = DateTime.Now.Date;

                BDCliente bdCliente = this.Infra.InstanciarBD<BDCliente>();
                Retorno<Int32> retIncluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retIncluir = bdCliente.Incluir(toCliente);
                    if (!retIncluir.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retIncluir.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retIncluir.Dados, new OperacaoRealizadaMensagem("Inclusão"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de atualização na tabela
        /// </summary>
        /// <param name="toCliente">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOCliente toCliente)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toCliente.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toCliente.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                #endregion
                //regras de negócio
                //RN4 - validação do nome
                if (toCliente.NomeCliente.FoiSetado)
                {
                    if (!toCliente.NomeCliente.TemConteudo)
                    {
                        return this.Infra.RetornarFalha<Int32>(new MensagemCliente(TipoFalha.NomePessoa));
                    }
                    String nome = toCliente.NomeCliente.LerConteudoOuPadrao().Trim();
                    String[] nomes = nome.Split(' ');
                    if (nomes.Length < 2)
                    {
                        //mensagem de erro
                        return this.Infra.RetornarFalha<Int32>(new MensagemCliente(TipoFalha.NomePessoa));
                    }
                    String primeiroNome = nomes[0];
                    if (primeiroNome.Length < 2)
                    {
                        //mensagem de erro
                        return this.Infra.RetornarFalha<Int32>(new MensagemCliente(TipoFalha.NomePessoa));
                    }
                }

                BDCliente bdCliente = this.Infra.InstanciarBD<BDCliente>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdCliente.Alterar(toCliente);
                    if (!retAlterar.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retAlterar.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retAlterar.Dados, new OperacaoRealizadaMensagem("Alteração"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de exclusão na tabela
        /// </summary>
        /// <param name="toCliente">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOCliente toCliente)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toCliente.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toCliente.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                #endregion
                //TODO: regras de negócio
                BDCliente bdCliente = this.Infra.InstanciarBD<BDCliente>();
                Retorno<Int32> retExcluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retExcluir = bdCliente.Excluir(toCliente);
                    if (!retExcluir.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retExcluir.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retExcluir.Dados, new OperacaoRealizadaMensagem("Exclusão"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toCliente">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada de clientes maiores de idade</returns>
        public Retorno<List<TOCliente>> ListarClientesMaioresIdade(TOCliente toCliente)
        {
            try
            {
                // (02/05/2022 - 18) - 02/05/2004 -> todos que nasceram até de 02/05/2004
                // DATA_NASC <= 02/05/2004
                toCliente.DataNasc = DateTime.Now.Date.AddYears(-18);
                BDCliente bDCliente = this.Infra.InstanciarBD<BDCliente>();

                Retorno<List<TOCliente>> retornoListar = bDCliente.ListarClientesMaioresIdade(toCliente);
                if (!retornoListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOCliente>>(retornoListar.Mensagem);
                }

                return this.Infra.RetornarSucesso<List<TOCliente>>(retornoListar.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOCliente>>(new Mensagem(e));
            }
        }
        #endregion
    }
}
