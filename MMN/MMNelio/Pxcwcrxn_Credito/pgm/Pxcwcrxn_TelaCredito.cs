using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcscrxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;
using Bergs.Pxc.Pxcsclxn;

namespace Bergs.Pxc.Pxcwcrxn
{
    class MinhaTela : AplicacaoTela
    {
        public MinhaTela(String caminho)
            : base(caminho)
        { }

        public void Executar()
        {
            //...
            try
            {
                //...
                Menu menu = new Menu(
                new ItemMenu[] {
                        new ItemMenu(new KeyValuePair<int,string>(1, "Solicitar Crédito"), SolicitarCredito, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(2, "Consultar Crédito"), ConsultarCredito, false),
                        new ItemMenu(new KeyValuePair<int,string>(3, "Excluir Crédito"), ExcluirCredito, false, true),
                        //new ItemMenu(new KeyValuePair<int,string>(3, "Alterar"), Alterar, false),                        
                        new ItemMenu(new KeyValuePair<int,string>(0, "Sair"), null, true)
                        }, null);
                Console.ForegroundColor = ConsoleColor.White;
                Tela.ControlaMenu("Credito", menu);
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}\nTecle algo...", e.Message);
                Console.ReadKey();
            }
        }

        void SolicitarCredito(object obj)
        {
            try
            {
                //clean code - código limpo

                char tipoCredito = Tela.Confirma("Informe o tipo de crédito <R>esidencial ou <C>omercial: ", "RC");
                TOCliente toCliente = new TOCliente();
                if (Tela.Confirma("Deseja filtrar por CPF/CNPJ ou nome?", "SN") == 'S')
                {
                    String opcao = Tela.Confirma("Informe o tipo de pessoa <F>isica ou <J>uridica ou <n>ome: ", "FJN").ToString();
                    if (opcao == "N")
                    {
                        toCliente.NomeCliente = Tela.Ler<String>("Informe o nome do cliente: ");
                    }
                    else
                    {
                        toCliente.TipoPessoa = opcao;
                        toCliente.CodCliente = Tela.Ler<Double>("Informe o CPF/CNPJ: ");
                    }
                }
                Retorno<TOCliente> retornoClienteSelecionado;
                retornoClienteSelecionado = ListarClientes("Selecione um cliente para operação de crédito",
                                                            true, toCliente);

                if (retornoClienteSelecionado.Ok)
                {
                    if (tipoCredito == 'R')
                    {
                        //Solicitar Crédito Residencial
                    }
                    else
                    {
                        //Solicitar Crédito Comercial
                    }
                }


            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }




        void ConsultarCredito(object obj)
        {

        }

        void ExcluirCredito(object obj)
        {

        }

        Retorno<TOCliente> ListarClientes(String titulo, Boolean paginacao, TOCliente toCliente1)
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            //TOCliente toCliente1 = new TOCliente();
            Retorno<List<TOCliente>> retListar = rnCliente.ListarClientesMaioresIdade(toCliente1);

            if (!retListar.Ok)
            {
                return this.Infra.RetornarFalha<TOCliente>(retListar.Mensagem);
            }

            Int32 itemSelecionado = -1;
            Formatador formatador = new Formatador();

            //TODO: incluir os campos do cabeçalho da lista
            CabecalhoLista[] cabecalho = new CabecalhoLista[7];
            cabecalho[0] = new CabecalhoLista("CPF/CNPJ", CabecalhoLista.AlinhamentoCelula.Centralizado);
            cabecalho[1] = new CabecalhoLista("DATA NASC", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[2] = new CabecalhoLista("DATA CADASTRO", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[3] = new CabecalhoLista("NOME");
            cabecalho[4] = new CabecalhoLista("RATING");
            cabecalho[5] = new CabecalhoLista("RENDA", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[6] = new CabecalhoLista("TELEFONE");
            List<LinhaLista> registros = new List<LinhaLista>();
            foreach (TOCliente toCliente in retListar.Dados)
            {
                Formatador f = new Formatador();

                LinhaLista linha = new LinhaLista();
                if (toCliente.TipoPessoa.LerConteudoOuPadrao() == "F")
                {
                    //toCliente.CodCliente.LerConteudoOuPadrao().ToString(@"000\.000\.000\-00");
                    //toCliente.CodCliente.LerConteudoOuPadrao().ToString(@"00\.000\.000\/0000\-00");
                    linha.Celulas.Add(
                        String.Format(f, "{0:cpf}",
                        toCliente.CodCliente.LerConteudoOuPadrao())
                        );
                }
                else
                {
                    linha.Celulas.Add(
                        String.Format(f, "{0:cnpj}",
                        toCliente.CodCliente.LerConteudoOuPadrao())
                        );
                }

                if (!toCliente.DataNasc.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toCliente.DataNasc.LerConteudoOuPadrao().ToString("dd/MM/yyyy"));
                }
                if (!toCliente.DataCadastro.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toCliente.DataCadastro.LerConteudoOuPadrao().ToString("dd/MM/yyyy"));
                }
                linha.Celulas.Add(toCliente.NomeCliente.LerConteudoOuPadrao());
                if (!toCliente.RatingCliente.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toCliente.RatingCliente.LerConteudoOuPadrao());
                }

                if (!toCliente.RendaFamiliar.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toCliente.RendaFamiliar.LerConteudoOuPadrao().ToString("N2"));
                }
                if (!toCliente.Telefone.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toCliente.Telefone.LerConteudoOuPadrao().ToString());
                }
                registros.Add(linha);
            }

            if (paginacao)
            {
                itemSelecionado = Tela.ImprimeLista(titulo, cabecalho, registros, 20);
            }
            else
            {
                itemSelecionado = Tela.ImprimeLista(titulo, cabecalho, registros);
            }
            if (itemSelecionado >= 0)
            {
                return this.Infra.RetornarSucesso<TOCliente>(retListar.Dados[itemSelecionado], new OperacaoRealizadaMensagem());

            }

            return this.Infra.RetornarSucesso<TOCliente>(null, new OperacaoRealizadaMensagem());

        }

        void Incluir(object obj)
        {
            try
            {
                RNCredito rnCredito = this.Infra.InstanciarRN<RNCredito>();
                TOCredito toCredito = new TOCredito();
                //TODO: ler campos da tabela
                toCredito.CodCliente = Tela.Ler<Double>("Informe o conteúdo para cod_cliente: ");
                toCredito.CodCredito = Tela.Ler<Int32>("Informe o conteúdo para cod_credito: ");
                toCredito.DataCredito = Tela.Ler<DateTime>("Informe o conteúdo para data_credito: ");
                toCredito.Tamanho = Tela.Ler<Double>("Informe o conteúdo para tamanho: ");
                toCredito.TempoFinanc = Tela.Ler<Int32>("Informe o conteúdo para tempo_financ: ");
                toCredito.TipoImovel = Tela.Ler<String>("Informe o conteúdo para tipo_imovel: ");
                toCredito.TipoPessoa = Tela.Ler<String>("Informe o conteúdo para tipo_pessoa: ");
                toCredito.ValorFinanc = Tela.Ler<Double>("Informe o conteúdo para valor_financ: ");
                Retorno<Int32> retIncluir = rnCredito.Incluir(toCredito);
                if (!retIncluir.Ok)
                {
                    Console.WriteLine("Erro na inclusão: {0}", retIncluir.Mensagem);
                }
                else
                {
                    Console.WriteLine(retIncluir.Mensagem.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }

        void Listar(object obj)
        {
            RNCredito rnCredito = this.Infra.InstanciarRN<RNCredito>();
            TOCredito toCreditoFiltro = new TOCredito();
            //TODO: incluir os campos de filtro para listagem
            Retorno<List<TOCredito>> retListar = rnCredito.Listar(toCreditoFiltro);
            if (!retListar.Ok)
            {
                Console.WriteLine(retListar.Mensagem);
                return;
            }
            ImprimeLista("Lista\n", retListar.Dados, true);
        }

        TOCredito ImprimeLista(String titulo, List<TOCredito> listaCreditos, Boolean paginacao)
        {
            Int32 itemSelecionado = -1;
            Formatador formatador = new Formatador();

            //TODO: incluir os campos do cabeçalho da lista
            CabecalhoLista[] cabecalho = new CabecalhoLista[8];
            cabecalho[0] = new CabecalhoLista("CODCLIENTE");
            cabecalho[1] = new CabecalhoLista("CODCREDITO");
            cabecalho[2] = new CabecalhoLista("DATACREDITO");
            cabecalho[3] = new CabecalhoLista("TAMANHO");
            cabecalho[4] = new CabecalhoLista("TEMPOFINANC");
            cabecalho[5] = new CabecalhoLista("TIPOIMOVEL");
            cabecalho[6] = new CabecalhoLista("TIPOPESSOA");
            cabecalho[7] = new CabecalhoLista("VALORFINANC");
            List<LinhaLista> registros = new List<LinhaLista>();
            foreach (TOCredito toCredito in listaCreditos)
            {
                LinhaLista linha = new LinhaLista();
                linha.Celulas.Add(toCredito.CodCliente.ToString());
                linha.Celulas.Add(toCredito.CodCredito.ToString());
                linha.Celulas.Add(toCredito.DataCredito.ToString());
                if (!toCredito.Tamanho.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toCredito.Tamanho.ToString());
                }
                linha.Celulas.Add(toCredito.TempoFinanc.ToString());
                linha.Celulas.Add(toCredito.TipoImovel.ToString());
                linha.Celulas.Add(toCredito.TipoPessoa.ToString());
                linha.Celulas.Add(toCredito.ValorFinanc.ToString());
                registros.Add(linha);
            }
            if (paginacao)
            {
                itemSelecionado = Tela.ImprimeLista(titulo, cabecalho, registros, 20);
            }
            else
            {
                itemSelecionado = Tela.ImprimeLista(titulo, cabecalho, registros);
            }
            if (itemSelecionado >= 0)
            {
                return listaCreditos[itemSelecionado];
            }
            return null;
        }

        void Alterar(object obj)
        {
            try
            {
                RNCredito rnCredito = this.Infra.InstanciarRN<RNCredito>();
                TOCredito toCreditoFiltro = new TOCredito();
                //TODO: incluir os campos de filtro para a listagem
                Retorno<List<TOCredito>> retListar = rnCredito.Listar(toCreditoFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                }

                TOCredito toCreditoSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para alterar", retListar.Dados, true);
                if (toCreditoSelecionado != null)
                {
                    TOCredito toCredito = new TOCredito();
                    //popula os campos da PK
                    toCredito.CodCredito = toCreditoSelecionado.CodCredito;
                    //TODO: ler os campos que serão alterados na tabela
                    toCredito.CodCliente = Tela.Ler<Double>("Informe o conteúdo para cod_cliente: ");
                    toCredito.DataCredito = Tela.Ler<DateTime>("Informe o conteúdo para data_credito: ");
                    toCredito.Tamanho = Tela.Ler<Double>("Informe o conteúdo para tamanho: ");
                    toCredito.TempoFinanc = Tela.Ler<Int32>("Informe o conteúdo para tempo_financ: ");
                    toCredito.TipoImovel = Tela.Ler<String>("Informe o conteúdo para tipo_imovel: ");
                    toCredito.TipoPessoa = Tela.Ler<String>("Informe o conteúdo para tipo_pessoa: ");
                    toCredito.ValorFinanc = Tela.Ler<Double>("Informe o conteúdo para valor_financ: ");
                    Retorno<Int32> retAlterar = rnCredito.Alterar(toCredito);
                    if (!retAlterar.Ok)
                    {
                        Console.WriteLine("Erro na alteração: {0}", retAlterar.Mensagem);
                    }
                    else
                    {
                        Console.WriteLine(retAlterar.Mensagem.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }

        void Excluir(object obj)
        {
            try
            {
                RNCredito rnCredito = this.Infra.InstanciarRN<RNCredito>();
                TOCredito toCreditoFiltro = new TOCredito();
                //TODO: incluir os campos de filtro para listagem
                Retorno<List<TOCredito>> retListar = rnCredito.Listar(toCreditoFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                }
                TOCredito toCreditoSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para excluir", retListar.Dados, true);
                if (toCreditoSelecionado != null)
                {
                    if (Tela.Confirma("Confirma a exclusão do credito?"))
                    {
                        Retorno<Int32> retExcluir = rnCredito.Excluir(toCreditoSelecionado);
                        if (!retExcluir.Ok)
                        {
                            Console.WriteLine("Erro na exclusão: {0}", retExcluir.Mensagem);
                        }
                        else
                        {
                            Console.WriteLine(retExcluir.Mensagem.ToString());
                        }
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }
    }
}
