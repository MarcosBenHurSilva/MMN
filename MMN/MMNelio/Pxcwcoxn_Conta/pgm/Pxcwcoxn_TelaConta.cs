using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcscoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;
using Bergs.Pxc.Pxcsclxn;

namespace Bergs.Pxc.Pxcwcoxn
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
                // Item 1
                //criação de conta, 
                //exclusão de conta,
                //listagem das contas do cliente, 
                //depósito / saque e a 
                //alteração de um limite para conta do cliente
                //...
                Menu menu = new Menu(
              new ItemMenu[] {
                        new ItemMenu(new KeyValuePair<int,string>(1, "Criar Conta"), CriarConta, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(2, "Excluir Conta"), ExcluirConta, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(3, "Lista contas do cliente"), ListarConta, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(4, "Alterar Limite"), AlterarLimite, false),
                        new ItemMenu(new KeyValuePair<int,string>(5, "Depositar/Sacar"), SacarDepositar, false),
                        new ItemMenu(new KeyValuePair<int,string>(0, "Sair"), null, true)
                      }, null);
                Console.ForegroundColor = ConsoleColor.White;
                Tela.ControlaMenu("Conta", menu);
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}\nTecle algo...", e.Message);
                Console.ReadKey();
            }
        }

        //ENTRADA, PROCESSAMENTO, SAIDA!!!

        //ENTRADA
        void SacarDepositar(object obj)
        {
            TOConta tOContaDoVictor = new TOConta();
            tOContaDoVictor.CodAgencia = Tela.Ler<Int32>("Informe a agência: ");
            tOContaDoVictor.CodEspecie = Tela.Ler<Int32>("Informe a espécie da conta:");
            tOContaDoVictor.CodConta = Tela.Ler<Int32>("Informe o código da Conta: ");

            char opcao = Tela.Confirma("Deseja realizar um <S>aque ou <D>epósito?", "SD");
            tOContaDoVictor.ValorTransacao = Tela.Ler<Double>("Informe o valor da transação: ");

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            Retorno<TOConta> retornoTransacao; // retorno da transação
            if (opcao == 'S')
            {
                //Operação de Saque (OS PARAMETROS PARA O PROCESSAMENTO)
                retornoTransacao = rnConta.Sacar(tOContaDoVictor);
            }
            else
            {
                //Operação de Depósito
                retornoTransacao = rnConta.Depositar(tOContaDoVictor);
            }

            //Mensagem de falha!
            if (!retornoTransacao.Ok)
            {
                Console.WriteLine("Erro na transação: {0}", retornoTransacao.Mensagem);
            }
            else // mensagem de sucesso!
            {
                Console.WriteLine(retornoTransacao.Mensagem.ToString());
            }

        }

        void CriarConta(object obj)
        {
            try
            {
                Retorno<TOCliente> retListarCliente = ListarClientes("Selecione um cliente para inclusão de conta", true);
                if (!retListarCliente.Ok)
                {
                    Console.WriteLine(retListarCliente.Mensagem.ToString());
                }
                else if (retListarCliente.Dados == null)
                {
                    Console.WriteLine("É Obrigatório selecionar um cliente");
                }
                else
                {
                    RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                    TOConta toConta = new TOConta();
                    //ler campos da tabela
                    toConta.CodCliente = retListarCliente.Dados.CodCliente;
                    toConta.TipoPessoa = retListarCliente.Dados.TipoPessoa;
                    toConta.CodAgencia = Tela.Ler<Int32>("Informe o Código da agência: ");
                    toConta.CodConta = Tela.Ler<Int32>("Informe a Conta: ");
                    toConta.CodEspecie = Tela.Ler<Int32>("Informe o Código espécie: ");
                    toConta.Limite = Tela.Ler<Double>("Informe o limite: ");
                    toConta.Saldo = Tela.Ler<Double>("Informe o saldo: ");
                    Retorno<Int32> retIncluir = rnConta.Incluir(toConta);
                    if (!retIncluir.Ok)
                    {
                        Console.WriteLine("Erro na inclusão: {0}", retIncluir.Mensagem);
                    }
                    else
                    {
                        Console.WriteLine(retIncluir.Mensagem.ToString());
                    }
                }

            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }

        Retorno<TOCliente> ListarClientes(String titulo, Boolean paginacao)
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            TOCliente toCliente1 = new TOCliente();
            Retorno<List<TOCliente>> retListar = rnCliente.Listar(toCliente1);

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
                RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                TOConta toConta = new TOConta();
                //TODO: ler campos da tabela
                toConta.CodAgencia = Tela.Ler<Int32>("Informe o conteúdo para cod_agencia: ");
                toConta.CodCliente = Tela.Ler<Double>("Informe o conteúdo para cod_cliente: ");
                toConta.CodConta = Tela.Ler<Int32>("Informe o conteúdo para cod_conta: ");
                toConta.CodEspecie = Tela.Ler<Int32>("Informe o conteúdo para cod_especie: ");
                toConta.Limite = Tela.Ler<Double>("Informe o conteúdo para limite: ");
                toConta.Saldo = Tela.Ler<Double>("Informe o conteúdo para saldo: ");
                toConta.TipoPessoa = Tela.Ler<String>("Informe o conteúdo para tipo_pessoa: ");
                Retorno<Int32> retIncluir = rnConta.Incluir(toConta);
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

        void ListarConta(object obj)
        {
            Retorno<TOCliente> retListarCliente = ListarClientes("Selecione um cliente para listar suas contas", true);
            if (!retListarCliente.Ok)
            {
                Console.WriteLine(retListarCliente.Mensagem.ToString());
            }
            else if (retListarCliente.Dados == null)
            {
                Console.WriteLine("É Obrigatório selecionar um cliente");
            }
            else
            {
                Formatador f = new Formatador();
                String titulo = "Contas do cliente ";
                if (retListarCliente.Dados.TipoPessoa.LerConteudoOuPadrao() == "F")
                {
                    titulo += String.Format(f, "{0:cpf}",
                        retListarCliente.Dados.CodCliente.LerConteudoOuPadrao()
                        );
                }
                else
                {
                    titulo += String.Format(f, "{0:cnpj}",
                        retListarCliente.Dados.CodCliente.LerConteudoOuPadrao()
                        );
                }

                RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                TOConta toContaFiltro = new TOConta();
                toContaFiltro.CodCliente = retListarCliente.Dados.CodCliente;
                toContaFiltro.TipoPessoa = retListarCliente.Dados.TipoPessoa;
                //incluir os campos de filtro para listagem
                Retorno<List<TOConta>> retListar = rnConta.Listar(toContaFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                    return;
                }

                if (retListar.Dados.Count == 0)
                {
                    Console.WriteLine("Cliente não tem contas para listar.");
                }
                else
                {
                    ImprimeLista(titulo + "\n", retListar.Dados, true);
                }
            }
        }

        TOConta ImprimeLista(String titulo, List<TOConta> listaContas, Boolean paginacao)
        {
            Int32 itemSelecionado = -1;
            Formatador formatador = new Formatador();

            //TODO: incluir os campos do cabeçalho da lista
            CabecalhoLista[] cabecalho = new CabecalhoLista[5];
            cabecalho[0] = new CabecalhoLista("Agência", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[1] = new CabecalhoLista("Espécie", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[2] = new CabecalhoLista("Conta", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[3] = new CabecalhoLista("Saldo", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[4] = new CabecalhoLista("Limite", CabecalhoLista.AlinhamentoCelula.Direita);
            List<LinhaLista> registros = new List<LinhaLista>();
            foreach (TOConta toConta in listaContas)
            {
                LinhaLista linha = new LinhaLista();
                linha.Celulas.Add(toConta.CodAgencia.LerConteudoOuPadrao().ToString());
                linha.Celulas.Add(toConta.CodEspecie.LerConteudoOuPadrao().ToString());
                linha.Celulas.Add(toConta.CodConta.LerConteudoOuPadrao().ToString());
                linha.Celulas.Add(toConta.Saldo.LerConteudoOuPadrao().ToString("N"));
                if (!toConta.Limite.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toConta.Limite.LerConteudoOuPadrao().ToString("N"));
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
                return listaContas[itemSelecionado];
            }
            return null;
        }

        void AlterarLimite(object obj)
        {
            try
            {
                Retorno<TOCliente> retListarCliente = ListarClientes("Selecione um cliente para alterar limite da conta", true);
                if (!retListarCliente.Ok)
                {
                    Console.WriteLine(retListarCliente.Mensagem.ToString());
                }
                else if (retListarCliente.Dados == null)
                {
                    Console.WriteLine("É Obrigatório selecionar um cliente");
                }
                else
                {
                    RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                    TOConta toContaFiltro = new TOConta();
                    //incluir os campos de filtro para a listagem
                    toContaFiltro.CodCliente = retListarCliente.Dados.CodCliente;
                    toContaFiltro.TipoPessoa = retListarCliente.Dados.TipoPessoa;
                    Retorno<List<TOConta>> retListar = rnConta.Listar(toContaFiltro);
                    if (!retListar.Ok)
                    {
                        Console.WriteLine(retListar.Mensagem);
                    }

                    if (retListar.Dados.Count == 0)
                    {
                        Console.WriteLine("Cliente não tem contas para serem alteradas.");
                    }
                    else
                    {
                        TOConta toContaSelecionado = ImprimeLista("Selecione uma conta da lista e tecle ENTER para alterar", retListar.Dados, true);
                        if (toContaSelecionado != null)
                        {
                            TOConta toConta = new TOConta();
                            //popula os campos da PK
                            toConta.CodConta = toContaSelecionado.CodConta;
                            toConta.CodEspecie = toContaSelecionado.CodEspecie;
                            toConta.CodAgencia = toContaSelecionado.CodAgencia;
                            //ler os campos que serão alterados na tabela
                            //toConta.CodCliente = Tela.Ler<Double>("Informe o conteúdo para cod_cliente: ");
                            //toConta.Saldo = Tela.Ler<Double>("Informe o conteúdo para saldo: ");
                            //toConta.TipoPessoa = Tela.Ler<String>("Informe o conteúdo para tipo_pessoa: ");

                            if (Tela.Confirma("Deseja <A>lterar o limte ou <R>emvover o limite da conta do cliente?", "AR") == 'A')
                            {
                                toConta.Limite = Tela.Ler<Double>("Informe o novo limite: ");

                                if (toConta.Limite.LerConteudoOuPadrao() <= 0)
                                {
                                    Console.WriteLine("Valor do limite tem que ser maior que 0 (zero)");
                                    return;
                                }

                            }
                            else
                            {
                                toConta.Limite = new CampoTabela<double>(null);
                            }

                            Retorno<Int32> retAlterar = rnConta.Alterar(toConta);
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


                }


            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }

        void ExcluirConta(object obj)
        {
            try
            {
                Retorno<TOCliente> retListarCliente = ListarClientes("Selecione um cliente para inclusão de conta", true);
                if (!retListarCliente.Ok)
                {
                    Console.WriteLine(retListarCliente.Mensagem.ToString());
                }
                else if (retListarCliente.Dados == null)
                {
                    Console.WriteLine("É Obrigatório selecionar um cliente");
                }
                else
                {
                    //SELECT * FROM CONTA
                    //SELECT * FROM CONTA WHERE COD_CLIENTE = '444' AND TIPO_PESSOA = 'F'
                    RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                    TOConta toContaFiltro = new TOConta();
                    //incluir os campos de filtro para listagem
                    toContaFiltro.CodCliente = retListarCliente.Dados.CodCliente;
                    toContaFiltro.TipoPessoa = retListarCliente.Dados.TipoPessoa;
                    Retorno<List<TOConta>> retListar = rnConta.Listar(toContaFiltro);
                    if (!retListar.Ok)
                    {
                        Console.WriteLine(retListar.Mensagem);
                        return;
                    }

                    if (retListar.Dados.Count == 0)
                    {
                        Console.WriteLine("Cliente não tem contas para excluir.");                        
                    }
                    else
                    {
                        TOConta toContaSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para excluir", retListar.Dados, true);
                        if (toContaSelecionado != null)
                        {
                            if (Tela.Confirma("Confirma a exclusão do conta?"))
                            {
                                Retorno<Int32> retExcluir = rnConta.Excluir(toContaSelecionado);
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
