using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcqbrxn;
using Bergs.Pxc.Pxcsclxn;

namespace Bergs.Pxc.Pxcsbrxn
{
    /// <summary>
    /// Classe de acesso a tabela BANRICOMPRAS
    /// </summary>
    public class RNBanricompras : AplicacaoRegraNegocio
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
                //regras de negócio
                BDBanricompras bdBanricompras = this.Infra.InstanciarBD<BDBanricompras>();
                Retorno<List<TOBanricompras>> retListar = bdBanricompras.Listar(toBanricompras);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOBanricompras>>(retListar.Mensagem);
                }
                return this.Infra.RetornarSucesso<List<TOBanricompras>>(retListar.Dados, new OperacaoRealizadaMensagem());
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
                //regras de negócio
                if (toBanricompras.TipoPessoa.TemConteudo && toBanricompras.TipoPessoa != "J")
                {
                    return this.Infra.RetornarFalha<List<TOBanricompras>>(new MensagemBanricompras(TipoFalha.TipoPessoaInvalido));
                }
                BDBanricompras bdBanricompras = this.Infra.InstanciarBD<BDBanricompras>();
                Retorno<List<TOBanricompras>> retListar = bdBanricompras.ListarMovimentos(toBanricompras);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOBanricompras>>(retListar.Mensagem);
                }
                return this.Infra.RetornarSucesso<List<TOBanricompras>>(retListar.Dados, new OperacaoRealizadaMensagem());
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
                //regras de negócio
                if (toBanricompras.TipoPessoa.TemConteudo && toBanricompras.TipoPessoa != "J")
                {
                    return this.Infra.RetornarFalha<List<TOBanricompras>>(new MensagemBanricompras(TipoFalha.TipoPessoaInvalido));
                }
                BDBanricompras bdBanricompras = this.Infra.InstanciarBD<BDBanricompras>();
                Retorno<List<TOBanricompras>> retListar = bdBanricompras.ListarAdiantamentos(toBanricompras);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOBanricompras>>(retListar.Mensagem);
                }
                return this.Infra.RetornarSucesso<List<TOBanricompras>>(retListar.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOBanricompras>>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Valida regras de negócio
        /// </summary>
        /// <param name="toBanricompras"></param>
        /// <returns></returns>
        private Retorno ValidacaoRegrasNegocio(TOBanricompras toBanricompras)
        {
            if (toBanricompras.ValorMovto <= 0)
            {
                return this.Infra.RetornarFalha(new MensagemBanricompras(TipoFalha.ValorMenorZero, "valor do movimento"));
            }

            if (toBanricompras.TipoPessoa != "J")
            {
                return this.Infra.RetornarFalha(new MensagemBanricompras(TipoFalha.TipoPessoaInvalido));
            }

            return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem());
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
                #region Validação de campos obrigatórios
                if (!toBanricompras.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                if (!toBanricompras.DataVencto.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("DATA_VENCTO"));
                }
                if (!toBanricompras.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TipoPessoa"));
                }
                if (!toBanricompras.ValorMovto.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("VALOR_MOVTO"));
                }
                #endregion
                //regras de negócio
                Retorno retValidacao = ValidacaoRegrasNegocio(toBanricompras);
                if (!retValidacao.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(retValidacao.Mensagem);
                }
                toBanricompras.ValorMovto = Math.Round(toBanricompras.ValorMovto, 2);

                BDBanricompras bdBanricompras = this.Infra.InstanciarBD<BDBanricompras>();
                Retorno<Int32> retIncluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retIncluir = bdBanricompras.Incluir(toBanricompras);
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
        /// Atualiza o rating
        /// </summary>
        /// <param name="toBanricompras">Campos para inclusão</param>
        /// <returns>Retorna a quantidade de registros incluídos</returns>
        public Retorno<string> AtualizarRating(TOCliente toCliente)
        {
            try
            {
                #region Validação de campos obrigatórios
                if (!toCliente.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<String>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                if (!toCliente.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<String>(new CampoObrigatorioMensagem("TipoPessoa"));
                }
                #endregion
                if (toCliente.TipoPessoa != "J")
                {
                    return this.Infra.RetornarFalha<String>(new MensagemBanricompras(TipoFalha.TipoPessoaInvalido));
                }

                RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                //Busca os dados do cliente na tabela CLIENTE
                TOCliente toClienteBusca = new TOCliente();
                toClienteBusca.CodCliente = toCliente.CodCliente;
                toClienteBusca.TipoPessoa = toCliente.TipoPessoa;
                Retorno<List<TOCliente>> retListar = rnCliente.Listar(toClienteBusca);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<String>(retListar.Mensagem);
                }
                if (retListar.Dados.Count == 0)
                {
                    return this.Infra.RetornarFalha<String>(new RegistroInexistenteMensagem());
                }
                toClienteBusca = retListar.Dados[0];
                //fim da busca

                if (toClienteBusca.RatingCliente.LerConteudoOuPadrao(String.Empty) == "A")
                {
                    return this.Infra.RetornarFalha<string>(new MensagemBanricompras(TipoFalha.RatingA));
                }

                if (toClienteBusca.DataAtuRating.TemConteudo && toClienteBusca.DataAtuRating > DateTime.Now.AddMonths(-1))
                {
                    return this.Infra.RetornarFalha<string>(new MensagemBanricompras(TipoFalha.TempoAtualizacaoRatingInvalido,
                        toClienteBusca.DataAtuRating.LerConteudoOuPadrao().ToString("dd/MM/yyyy")));
                }

                TOBanricompras toBanricompras = new TOBanricompras();
                toBanricompras.CodCliente = toCliente.CodCliente;
                toBanricompras.TipoPessoa = toCliente.TipoPessoa;
                toBanricompras.DataVencto = DateTime.Now.AddDays(-30);

                BDBanricompras bdBanricompras = this.Infra.InstanciarBD<BDBanricompras>();
                Retorno<double> retPodeAtualizar = bdBanricompras.BuscarTotalMovimentosUltimosDias(toBanricompras);
                if (!retPodeAtualizar.Ok)
                {
                    return this.Infra.RetornarFalha<string>(retPodeAtualizar.Mensagem);
                }
                else if (retPodeAtualizar.Dados < 5000)
                {
                    return this.Infra.RetornarFalha<string>(new MensagemBanricompras(TipoFalha.MovimentacaoInferior5000,
                        retPodeAtualizar.Dados.ToString("C")));
                }

                TOCliente toClienteAtualizar = new TOCliente();
                toClienteAtualizar.CodCliente = toCliente.CodCliente;
                toClienteAtualizar.TipoPessoa = toCliente.TipoPessoa;
                if (!toClienteBusca.RatingCliente.TemConteudo)
                {
                    toClienteAtualizar.RatingCliente = "E";
                }
                else
                {
                    switch (toClienteBusca.RatingCliente)
                    {
                        case "B":
                            toClienteAtualizar.RatingCliente = "A";
                            break;
                        case "C":
                            toClienteAtualizar.RatingCliente = "B";
                            break;
                        case "D":
                            toClienteAtualizar.RatingCliente = "C";
                            break;
                        case "E":
                            toClienteAtualizar.RatingCliente = "D";
                            break;
                        default:
                            break;
                    }
                }
                toClienteAtualizar.DataAtuRating = DateTime.Now;

                Retorno<Int32> retAlterar = rnCliente.Alterar(toClienteAtualizar);
                if (!retAlterar.Ok)
                {
                    return this.Infra.RetornarFalha<string>(retAlterar.Mensagem);
                }

                return this.Infra.RetornarSucesso<string>(toCliente.RatingCliente, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<string>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Realizar adiantamento
        /// </summary>
        /// <param name="toBanricompras">Filtro para adiantamento</param>
        /// <returns>Quantidade de adiantamentos realizados</returns>
        public Retorno<Int32> RealizarAdiantamento(TOBanricompras toBanricompras)
        {
            try
            {
                #region Validação de campos obrigatórios
                if (!toBanricompras.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                if (!toBanricompras.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TipoPessoa"));
                }
                if (!toBanricompras.DataVencto.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("DATA_VENCTO"));
                }
                #endregion
                if (toBanricompras.TipoPessoa != "J")
                {
                    return this.Infra.RetornarFalha<Int32>(new MensagemBanricompras(TipoFalha.TipoPessoaInvalido));
                }
                BDBanricompras bdBanricompras = this.Infra.InstanciarBD<BDBanricompras>();
                Retorno<Int32> retAdiantamento = bdBanricompras.RealizarAdiantamento(toBanricompras);
                if (!retAdiantamento.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(retAdiantamento.Mensagem);
                }

                return this.Infra.RetornarSucesso<Int32>(retAdiantamento.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception ex)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(ex));
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
                #region Validação dos campos da chave primária
                if (!toBanricompras.CodMovimento.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_MOVIMENTO"));
                }
                #endregion
                //regras de negócio
                BDBanricompras bdBanricompras = this.Infra.InstanciarBD<BDBanricompras>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdBanricompras.Alterar(toBanricompras);
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
        /// <param name="toBanricompras">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOBanricompras toBanricompras)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toBanricompras.CodMovimento.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_MOVIMENTO"));
                }
                #endregion
                //regras de negócio
                BDBanricompras bdBanricompras = this.Infra.InstanciarBD<BDBanricompras>();
                Retorno<Int32> retExcluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retExcluir = bdBanricompras.Excluir(toBanricompras);
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
        #endregion
    }
}
