using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcsclxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcwclxn
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
                        new ItemMenu(new KeyValuePair<int,string>(1, "Incluir"), Incluir, false),
                        new ItemMenu(new KeyValuePair<int,string>(2, "Consulta/alterar"), Alterar, false),
                        new ItemMenu(new KeyValuePair<int,string>(3, "Excluir"), Excluir, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(4, "Alterar data de cadastro"), AlterarDataCadastro, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(0, "Sair"), null, true)
                        }, null);
                Console.ForegroundColor = ConsoleColor.White;
                Tela.ControlaMenu("Cliente", menu);
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}\nTecle algo...", e.Message);
                Console.ReadKey();
            }
        }

        void AlterarDataCadastro(object obj)
        {
            try
            {
                RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                TOCliente toCliente = new TOCliente();
                toCliente.CodCliente = 191;
                toCliente.TipoPessoa = "F";
                toCliente.DataCadastro = DateTime.Now.Date;

                Retorno<Int32> retAlterar = rnCliente.Alterar(toCliente);
                if (!retAlterar.Ok)
                {
                    Console.WriteLine("Erro na alteração da data de cadastro: {0}",
                        retAlterar.Mensagem);
                }
                else
                {
                    Console.WriteLine("Data de cadastro alterada com sucesso.");
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }
        void Incluir(object obj)
        {
            try
            {
                RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                TOCliente toCliente = new TOCliente();
                //ler campos da tela
                toCliente.TipoPessoa = Tela.Confirma("Informe o tipo de cliente <F>ísica ou <J>urídica: ", "FJ").ToString().ToUpper();
                String texto = String.Empty;
                if (toCliente.TipoPessoa.LerConteudoOuPadrao() == "F")
                {
                    texto = "Informe o CPF: ";
                }
                else
                {
                    texto = "Informe o CNPJ: ";
                }
                toCliente.CodCliente = Tela.Ler<Double>(texto);
                toCliente.NomeCliente = Tela.Ler<String>("Informe o nome do cliente: ");
                toCliente.DataNasc = Tela.Ler<DateTime>("Informe a data de nascimento: ");

                //toCliente.DataAtuRating = Tela.Ler<DateTime>("Informe o conteúdo para data_atu_rating: ");
                //toCliente.DataCadastro = DateTime.Now.Date;
                //toCliente.RatingCliente = Tela.Ler<String>("Informe o conteúdo para rating_cliente: ");
                //toCliente.RendaFamiliar = Tela.Ler<Double>("Informe o conteúdo para renda_familiar: ");
                //toCliente.Telefone = Tela.Ler<Double>("Informe o conteúdo para telefone: ");
                Retorno<Int32> retIncluir = rnCliente.Incluir(toCliente);
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

        TOCliente ImprimeLista(String titulo, List<TOCliente> listaClientes, Boolean paginacao)
        {
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
            foreach (TOCliente toCliente in listaClientes)
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
                return listaClientes[itemSelecionado];
            }
            return null;
        }

        void Alterar(object obj)
        {
            try
            {
                RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                TOCliente toClienteFiltro = new TOCliente();
                //incluir os campos de filtro para a listagem
                Char opcao = Tela.Confirma("Deseja consultar <T>odos ou por um CP<F> ou um CNP<J>? ", "TFJ");
                if (opcao == 'F' || opcao == 'J')
                {
                    toClienteFiltro.TipoPessoa = opcao.ToString();
                    String texto = String.Empty;
                    if (toClienteFiltro.TipoPessoa == "F")
                    {
                        texto = "Informe o CPF: ";
                    }
                    else
                    {
                        texto = "Informe o CNPJ: ";
                    }
                    toClienteFiltro.CodCliente = Tela.Ler<Double>(texto);
                }

                //add nome no filtro
                //toclientefiltro.nomecliente = "fabian"





                Retorno<List<TOCliente>> retListar = rnCliente.Listar(toClienteFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                    return;
                }

                TOCliente toClienteSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para alterar", retListar.Dados, true);
                if (toClienteSelecionado != null)
                {
                    TOCliente toCliente = new TOCliente();
                    //popula os campos da PK
                    toCliente.TipoPessoa = toClienteSelecionado.TipoPessoa;
                    toCliente.CodCliente = toClienteSelecionado.CodCliente;
                    //TODO: ler os campos que serão alterados na tabela
                    toCliente.DataAtuRating = Tela.Ler<DateTime>("Informe o conteúdo para data_atu_rating: ");
                    toCliente.DataCadastro = Tela.Ler<DateTime>("Informe o conteúdo para data_cadastro: ");
                    toCliente.DataNasc = Tela.Ler<DateTime>("Informe o conteúdo para data_nasc: ");
                    toCliente.NomeCliente = Tela.Ler<String>("Informe o conteúdo para nome_cliente: ");
                    toCliente.RatingCliente = Tela.Ler<String>("Informe o conteúdo para rating_cliente: ");
                    toCliente.RendaFamiliar = Tela.Ler<Double>("Informe o conteúdo para renda_familiar: ");
                    toCliente.Telefone = Tela.Ler<Double>("Informe o conteúdo para telefone: ");
                    Retorno<Int32> retAlterar = rnCliente.Alterar(toCliente);
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
                RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                TOCliente toClienteFiltro = new TOCliente();
                //incluir os campos de filtro para listagem
                toClienteFiltro.TipoPessoa = Tela.Confirma("Informe o tipo de cliente <F>ísica ou <J>urídica: ", "FJ").ToString();
                String texto = String.Empty;
                if (toClienteFiltro.TipoPessoa == "F")
                {
                    texto = "Informe o CPF: ";
                }
                else
                {
                    texto = "Informe o CNPJ: ";
                }
                toClienteFiltro.CodCliente = Tela.Ler<Double>(texto);

                Retorno<List<TOCliente>> retListar = rnCliente.Listar(toClienteFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                    return;
                }
                TOCliente toClienteSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para excluir", retListar.Dados, true);
                if (toClienteSelecionado != null)
                {
                    if (Tela.Confirma("Confirma a exclusão do cliente?"))
                    {
                        Retorno<Int32> retExcluir = rnCliente.Excluir(toClienteSelecionado);
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
