using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.RN;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcqscxn;
using Bergs.Pxc.Pxcsclxn;

namespace Bergs.Pxc.Pxcsscxn
{
    /// <summary>
    /// Classe de acesso a tabela SOCIO
    /// </summary>
    public class RNSocio : AplicacaoRegraNegocio
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
                if (toSocio == null)
                {
                    return this.Infra.RetornarFalha<List<TOSocio>>(new CampoObrigatorioMensagem("TOSocio"));
                }
                BDSocio bdSocio = this.Infra.InstanciarBD<BDSocio>();
                Retorno<List<TOSocio>> retListar = bdSocio.Listar(toSocio);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOSocio>>(retListar.Mensagem);
                }
                return this.Infra.RetornarSucesso<List<TOSocio>>(retListar.Dados, new OperacaoRealizadaMensagem());
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
                #region Validação de campos obrigatórios
                if (toSocio == null)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TOSocio"));
                }
                if (!toSocio.CodClienteEmpresa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE_EMPRESA"));
                }
                if (!toSocio.CodClienteSocio.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE_SOCIO"));
                }
                if (!toSocio.ParticipSocietaria.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("PARTICIP_SOCIETARIA"));
                }
                if (!toSocio.TipoPessoaEmpresa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA_EMPRESA"));
                }
                if (!toSocio.TipoPessoaSocio.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA_SOCIO"));
                }
                #endregion

                #region Validação de Regras de Negócio
                Retorno ret = this.ValidarEmpresaJuridica(toSocio);
                if (!ret.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(ret.Mensagem);
                }

                ret = this.ValidarCnpjValido(toSocio);
                if (!ret.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(ret.Mensagem);
                }

                ret = this.ValidarEmpresaExistente(toSocio);
                if (!ret.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(ret.Mensagem);
                }

                ret = this.ValidarSocioFisico(toSocio);
                if (!ret.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(ret.Mensagem);
                }

                ret = this.ValidarCpfValido(toSocio);
                if (!ret.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(ret.Mensagem);
                }

                ret = this.ValidarClienteExistente(toSocio);
                if (!ret.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(ret.Mensagem);
                }

                ret = this.ValidarSocioExistente(toSocio);
                if (!ret.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(ret.Mensagem);
                }

                ret = this.ValidarParticipacaoMinima(toSocio);
                if (!ret.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(ret.Mensagem);
                }

                ret = this.ValidarParticipacaoTotal(toSocio);
                if (!ret.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(ret.Mensagem);
                }

                ret = this.ValidarSocioMenor(toSocio);
                if (!ret.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(ret.Mensagem);
                }
                #endregion

                BDSocio bdSocio = this.Infra.InstanciarBD<BDSocio>();
                Retorno<Int32> retIncluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retIncluir = bdSocio.Incluir(toSocio);
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
        /// <param name="toSocio">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOSocio toSocio)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (toSocio == null)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TOSocio"));
                }
                if (!toSocio.TipoPessoaSocio.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA_SOCIO"));
                }
                if (!toSocio.CodClienteSocio.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE_SOCIO"));
                }
                if (!toSocio.TipoPessoaEmpresa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA_EMPRESA"));
                }
                if (!toSocio.CodClienteEmpresa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE_EMPRESA"));
                }
                #endregion

                #region Validação de Regras de Negócio
                Retorno ret = this.ValidarParticipacaoMinima(toSocio);
                if (!ret.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(ret.Mensagem);
                }

                ret = this.ValidarParticipacaoTotal(toSocio);
                if (!ret.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(ret.Mensagem);
                }
                #endregion

                BDSocio bdSocio = this.Infra.InstanciarBD<BDSocio>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdSocio.Alterar(toSocio);
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
        /// <param name="toSocio">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOSocio toSocio)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (toSocio == null)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TOSocio"));
                }
                if (!toSocio.TipoPessoaSocio.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA_SOCIO"));
                }
                if (!toSocio.CodClienteSocio.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE_SOCIO"));
                }
                if (!toSocio.TipoPessoaEmpresa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA_EMPRESA"));
                }
                if (!toSocio.CodClienteEmpresa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE_EMPRESA"));
                }
                #endregion
                //TODO: regras de negócio
                BDSocio bdSocio = this.Infra.InstanciarBD<BDSocio>();
                Retorno<Int32> retExcluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retExcluir = bdSocio.Excluir(toSocio);
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

        #region Regras de Negócio
        private Retorno ValidarEmpresaJuridica(TOSocio toSocio)
        {
            if (toSocio.TipoPessoaEmpresa.TemConteudo && (toSocio.TipoPessoaEmpresa.LerConteudoOuPadrao() != "J"))
            {
                return this.Infra.RetornarFalha(new MensagemSocio(TipoMensagemSocio.EmpresaJuridica));
            }

            return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem("RN1"));
        }

        private Retorno ValidarCnpjValido(TOSocio toSocio)
        {
            if (toSocio.CodClienteEmpresa.TemConteudo
                && (!Util.ValidaCnpj(toSocio.CodClienteEmpresa.LerConteudoOuPadrao().ToString().PadLeft(14, '0').Substring(0, 14))))
            {
                return this.Infra.RetornarFalha(new MensagemSocio(TipoMensagemSocio.CnpjInvalido));
            }

            return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem("RN2"));
        }

        private Retorno ValidarEmpresaExistente(TOSocio toSocio)
        {
            TOCliente toClienteFiltro = new TOCliente();
            toClienteFiltro.CodCliente = toSocio.CodClienteEmpresa;
            toClienteFiltro.TipoPessoa = toSocio.TipoPessoaEmpresa;

            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            Retorno<List<TOCliente>> retListarClientes = rnCliente.Listar(toClienteFiltro);
            if (!retListarClientes.Ok)
            {
                return this.Infra.RetornarFalha(retListarClientes.Mensagem);
            }

            if (retListarClientes.Dados.Count == 0)
            {
                return this.Infra.RetornarFalha(new MensagemSocio(TipoMensagemSocio.EmpresaNaoCadastrada,
                    toSocio.CodClienteEmpresa.LerConteudoOuPadrao().ToString().PadLeft(14, '0')));
            }

            return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem());
        }

        private Retorno ValidarSocioFisico(TOSocio toSocio)
        {
            if (toSocio.TipoPessoaSocio.TemConteudo && (toSocio.TipoPessoaSocio.LerConteudoOuPadrao() != "F"))
            {
                return this.Infra.RetornarFalha(new MensagemSocio(TipoMensagemSocio.SocioFisico));
            }

            return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem());
        }

        private Retorno ValidarCpfValido(TOSocio toSocio)
        {
            if (toSocio.CodClienteSocio.TemConteudo
                && (!Util.ValidaCpf(toSocio.CodClienteSocio.LerConteudoOuPadrao().ToString().PadLeft(11, '0').Substring(0, 11))))
            {
                return this.Infra.RetornarFalha(new MensagemSocio(TipoMensagemSocio.CpfInvalido));
            }

            return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem());
        }

        private Retorno ValidarClienteExistente(TOSocio toSocio)
        {
            TOCliente toClienteFiltro = new TOCliente();
            toClienteFiltro.CodCliente = toSocio.CodClienteSocio;
            toClienteFiltro.TipoPessoa = toSocio.TipoPessoaSocio;

            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            Retorno<List<TOCliente>> retListarClientes = rnCliente.Listar(toClienteFiltro);
            if (!retListarClientes.Ok)
            {
                return this.Infra.RetornarFalha(retListarClientes.Mensagem);
            }

            if (retListarClientes.Dados.Count == 0)
            {
                return this.Infra.RetornarFalha(new MensagemSocio(TipoMensagemSocio.ClienteNaoCadastrado,
                    toSocio.CodClienteSocio.LerConteudoOuPadrao().ToString().PadLeft(11, '0')));
            }

            return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem());
        }

        private Retorno ValidarSocioExistente(TOSocio toSocio)
        {
            TOSocio toSocioFiltro = new TOSocio();
            toSocioFiltro.CodClienteEmpresa = toSocio.CodClienteEmpresa;
            toSocioFiltro.TipoPessoaEmpresa = toSocio.TipoPessoaEmpresa;
            toSocioFiltro.CodClienteSocio = toSocio.CodClienteSocio;
            toSocioFiltro.TipoPessoaSocio = toSocio.TipoPessoaSocio;

            Retorno<List<TOSocio>> retListarSocios = this.Listar(toSocioFiltro);
            if (!retListarSocios.Ok)
            {
                return this.Infra.RetornarFalha(retListarSocios.Mensagem);
            }

            if (retListarSocios.Dados.Count > 0)
            {
                return this.Infra.RetornarFalha(new MensagemSocio(TipoMensagemSocio.SocioJaCadastrado,
                    retListarSocios.Dados[0].ParticipSocietaria.LerConteudoOuPadrao().ToString("N")));
            }

            return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem());
        }

        private Retorno ValidarParticipacaoMinima(TOSocio toSocio)
        {
            if (toSocio.ParticipSocietaria <= 0)
            {
                return this.Infra.RetornarFalha(new MensagemSocio(TipoMensagemSocio.ParticipacaoMaiorQueZero));
            }

            return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem());
        }

        private Retorno ValidarParticipacaoTotal(TOSocio toSocio)
        {
            TOSocio toSocioFiltro = new TOSocio();
            toSocioFiltro.CodClienteEmpresa = toSocio.CodClienteEmpresa;
            toSocioFiltro.TipoPessoaEmpresa = toSocio.TipoPessoaEmpresa;

            Retorno<List<TOSocio>> retListarSocios = this.Listar(toSocioFiltro);
            if (!retListarSocios.Ok)
            {
                return this.Infra.RetornarFalha(retListarSocios.Mensagem);
            }

            double totalPartic = toSocio.ParticipSocietaria.LerConteudoOuPadrao();
            foreach (TOSocio socio in retListarSocios.Dados)
            {
                if (toSocio.CodClienteSocio.LerConteudoOuPadrao() != socio.CodClienteSocio.LerConteudoOuPadrao())
                {
                    totalPartic += socio.ParticipSocietaria.LerConteudoOuPadrao();
                }
            }
            if (totalPartic > 100)
            {
                return this.Infra.RetornarFalha(new MensagemSocio(TipoMensagemSocio.TotalParticipacaoCem));
            }

            return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem());
        }

        private Retorno ValidarSocioMenor(TOSocio toSocio)
        {
            TOCliente toClienteFiltro = new TOCliente();
            toClienteFiltro.CodCliente = toSocio.CodClienteSocio;
            toClienteFiltro.TipoPessoa = toSocio.TipoPessoaSocio;

            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            Retorno<List<TOCliente>> retListarClientes = rnCliente.Listar(toClienteFiltro);
            if (!retListarClientes.Ok)
            {
                return this.Infra.RetornarFalha(retListarClientes.Mensagem);
            }

            TOCliente toCliente = retListarClientes.Dados[0];
            if (!toCliente.DataNasc.TemConteudo)
            {
                return this.Infra.RetornarFalha(new MensagemSocio(TipoMensagemSocio.SocioMenor));
            }

            if (toCliente.DataNasc.TemConteudo
                && (toCliente.DataNasc.LerConteudoOuPadrao() > DateTime.Today.AddYears(-21)))
            {
                return this.Infra.RetornarFalha(new MensagemSocio(TipoMensagemSocio.SocioMenor));
            }

            return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem());
        }
        #endregion
    }
}
