using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcsbrxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;
using Bergs.Pxc.Pxcsclxn;

namespace Bergs.Pxc.Pxcwbrxn
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
                TOCliente toCliente = new TOCliente();

                if (Tela.Confirma("Deseja filtrar os clientes? "))
                {
                    char opcao = Tela.Confirma("Para filtrar por CNPJ digite C. Para filtrar por nome digite N: ", "CN");
                    if (opcao == 'C')
                    {
                        toCliente.CodCliente = Tela.Ler<long>("Informe o CNPJ: ");
                    }
                    else
                    {
                        toCliente.NomeCliente = Tela.Ler<string>("Informe o nome: ");
                    }
                }

                toCliente = ListarClientes(toCliente);

                if (toCliente != null)
                {
                    Console.WriteLine("Cliente {0}", toCliente.NomeCliente);

                    Menu menu = new Menu(
                        new ItemMenu[] { 
                        new ItemMenu(new KeyValuePair<int,string>(1, "Criar movimento"), CriarMovimento, false),
                        new ItemMenu(new KeyValuePair<int,string>(2, "Excluir movimento"), ExcluirMovimento, false),
                        new ItemMenu(new KeyValuePair<int,string>(3, "Listar movimentos"), ListarMovimentos, false),
                        new ItemMenu(new KeyValuePair<int,string>(4, "Listar adiantamentos"), ListarAdiantamentos, false),
                        new ItemMenu(new KeyValuePair<int,string>(5, "Atualizar rating"), AtualizarRating, false),
                        new ItemMenu(new KeyValuePair<int,string>(6, "Realizar adiantamento"), RealizarAdiantamento, false),
                        new ItemMenu(new KeyValuePair<int,string>(0, "Voltar"), Voltar, false)
                        }, toCliente
                        );

                    Tela.ControlaMenu("Sistema de adiantamento de Banricompras", menu);
                }
                else
                {
                    Console.Write("É obrigatório a seleção de um cliente. Tecle algo para encerrar...");
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}\nTecle algo...", e.Message);
                Console.ReadKey();
            }
        }

        private void Voltar(object obj)
        {
            Console.Clear();
            Executar();
        }

        private void CriarMovimento(object obj)
        {
            try
            {
                TOBanricompras toBanricompras = new TOBanricompras();

                TOCliente toCliente = obj as TOCliente;
                if (toCliente == null)
                {
                    Console.WriteLine("Cliente inválido.");
                    return;
                }

                toBanricompras.CodCliente = toCliente.CodCliente;
                toBanricompras.TipoPessoa = "J";
                toBanricompras.DataVencto = Tela.Ler<DateTime>("Informe a data de vencimento: ");
                toBanricompras.ValorMovto = Tela.Ler<double>("Informe o valor do movimento: ");

                RNBanricompras rnBanricompras = this.Infra.InstanciarRN<RNBanricompras>();
                Retorno<Int32> retorno = rnBanricompras.Incluir(toBanricompras);
                if (!retorno.Ok)
                {
                    Console.WriteLine("Erro na movimentação: {0}", retorno.Mensagem);
                }
                else
                {
                    Console.WriteLine("Movimentos criados com sucesso: {0}", retorno.Dados);
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
            }
        }

        private void RealizarAdiantamento(object obj)
        {
            try
            {
                TOCliente toCliente = obj as TOCliente;
                if (toCliente == null)
                {
                    Console.WriteLine("Cliente inválido!");
                    return;
                }

                TOBanricompras toBanricompras = new TOBanricompras();
                toBanricompras.DataVencto = Tela.Ler<DateTime>("Informe a data de vencimento: ");
                toBanricompras.Taxa = 1;//?
                toBanricompras.CodCliente = toCliente.CodCliente;
                toBanricompras.TipoPessoa = "J";

                RNBanricompras rnBanricompras = this.Infra.InstanciarRN<RNBanricompras>();
                Retorno<Int32> retAdiantamento = rnBanricompras.RealizarAdiantamento(toBanricompras);
                if (!retAdiantamento.Ok)
                {
                    Console.WriteLine("Erro na operação: {0}", retAdiantamento.Mensagem);
                }
                else
                {
                    Console.WriteLine("Adiantamentos realizados: {0}", retAdiantamento.Dados);
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
            }
        }

        private void AtualizarRating(object obj)
        {
            try
            {
                TOCliente toCliente = obj as TOCliente;
                if (toCliente == null)
                {
                    Console.WriteLine("Cliente inválido!");
                    return;
                }

                TOBanricompras toBanricompras = new TOBanricompras();
                toBanricompras.CodCliente = toCliente.CodCliente;
                toCliente.TipoPessoa = "J";

                RNBanricompras rnBanricompras = this.Infra.InstanciarRN<RNBanricompras>();
                TOCliente toClienteRating = new TOCliente();
                toClienteRating.CodCliente = toCliente.CodCliente;
                toClienteRating.TipoPessoa = toCliente.TipoPessoa;
                Retorno<string> retAtualizar = rnBanricompras.AtualizarRating(toClienteRating);

                if (!retAtualizar.Ok)
                {
                    Console.WriteLine("Erro na atualização de rating: {0}", retAtualizar.Mensagem);
                }
                else
                {
                    Console.WriteLine("Novo rating: {0}", retAtualizar.Dados);
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
            }
        }

        private void ListarMovimentos(object obj)
        {
            try
            {
                TOCliente toCliente = obj as TOCliente;
                if (toCliente == null)
                {
                    Console.WriteLine("Cliente inválido");
                    return;
                }

                TOBanricompras toBanricompras = new TOBanricompras();
                toBanricompras.CodCliente = toCliente.CodCliente;
                toBanricompras.TipoPessoa = "J";

                if (Tela.Confirma("Deseja filtrar os resultados? "))
                {
                    char opcao = Tela.Confirma("Para filtrar por Data de Vencimento digite D. Para filtrar por código digite C. ", "DC");
                    if (opcao == 'D')
                    {
                        toBanricompras.DataVencto = Tela.Ler<DateTime>("Informe a data: ");
                    }
                    else
                    {
                        toBanricompras.CodMovimento = Tela.Ler<int>("Informe o código do movimento: ");
                    }
                }

                RNBanricompras rnBanricompras = this.Infra.InstanciarRN<RNBanricompras>();
                Retorno<List<TOBanricompras>> retListar = rnBanricompras.ListarMovimentos(toBanricompras);

                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                    return;
                }

                toBanricompras = ImprimeListaMovimentos(retListar.Dados, false);
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
            }
        }

        private void ListarAdiantamentos(object obj)
        {
            try
            {
                TOCliente toCliente = obj as TOCliente;
                if (toCliente == null)
                {
                    Console.WriteLine("Cliente inválido");
                    return;
                }

                TOBanricompras toBanricompras = new TOBanricompras();
                toBanricompras.CodCliente = toCliente.CodCliente;
                toBanricompras.TipoPessoa = "J";

                if (Tela.Confirma("Deseja filtrar os resultados? "))
                {
                    toBanricompras.DataVencto = Tela.Ler<DateTime>("Informe a data: ");
                }

                toBanricompras = ImprimeListaAdiantamentos(toBanricompras);
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
            }
        }

        private void ExcluirMovimento(object obj)
        {
            try
            {
                TOCliente toCliente = obj as TOCliente;
                if (toCliente == null)
                {
                    Console.WriteLine("Cliente inválido!");
                    return;
                }

                TOBanricompras toBanricompras = new TOBanricompras();
                toBanricompras.CodCliente = toCliente.CodCliente;
                toBanricompras.CodMovimento = Tela.Ler<int>("Informe o código do movimento: ");

                RNBanricompras rnBanricompras = this.Infra.InstanciarRN<RNBanricompras>();
                Retorno<List<TOBanricompras>> retListar = rnBanricompras.ListarMovimentos(toBanricompras);

                if (!retListar.Ok)
                {
                    Console.WriteLine("Erro ao buscar o movimento: {0}", retListar.Mensagem);
                }
                else if (retListar.Dados.Count == 0)
                {
                    Console.WriteLine("Movimento não encontrado!");
                }
                else
                {
                    ImprimeListaMovimentos(new List<TOBanricompras>(retListar.Dados), true);

                    if (Tela.Confirma("Confirma a exclusão do registro?"))
                    {
                        Retorno<int> retExcluir = rnBanricompras.Excluir(toBanricompras);
                        if (!retExcluir.Ok)
                        {
                            Console.WriteLine("Erro na exclusão: {0}", retExcluir.Mensagem);
                        }
                        else
                        {
                            Console.WriteLine("Registro excluído com sucesso");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
            }
        }

        private TOBanricompras ImprimeListaMovimentos(List<TOBanricompras> movimentos, bool exclusao)
        {
            CabecalhoLista[] cabecalho = new CabecalhoLista[3];
            cabecalho[0] = new CabecalhoLista("Código", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[1] = new CabecalhoLista("Data vencimento", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[2] = new CabecalhoLista("Valor", CabecalhoLista.AlinhamentoCelula.Direita);

            LinhaLista linha;

            List<LinhaLista> registros = new List<LinhaLista>();
            foreach (TOBanricompras to in movimentos)
            {
                linha = new LinhaLista();
                linha.Celulas.Add(to.CodMovimento.ToString());
                linha.Celulas.Add(to.DataVencto.LerConteudoOuPadrao().ToString("dd/MM/yyyy"));
                linha.Celulas.Add(to.ValorMovto.LerConteudoOuPadrao().ToString("N"));
                registros.Add(linha);
            }

            if (exclusao)
            {
                Tela.ImprimeLista("Listagem de movimentos", cabecalho, registros, false);
            }
            else
            {
                int itemSelecionado = Tela.ImprimeLista("Listagem de movimentos", cabecalho, registros, 15);
                if (itemSelecionado >= 0)
                {
                    return movimentos[itemSelecionado];
                }
            }
            return null;

        }

        private TOBanricompras ImprimeListaAdiantamentos(TOBanricompras toBanricompras)
        {
            RNBanricompras rnBanricompras = this.Infra.InstanciarRN<RNBanricompras>();
            Retorno<List<TOBanricompras>> retListar = rnBanricompras.ListarAdiantamentos(toBanricompras);

            if (!retListar.Ok)
            {
                Console.WriteLine(retListar.Mensagem);
            }
            else
            {
                CabecalhoLista[] cabecalho = new CabecalhoLista[5];
                cabecalho[0] = new CabecalhoLista("Código", CabecalhoLista.AlinhamentoCelula.Direita);
                cabecalho[1] = new CabecalhoLista("Data vencimento", CabecalhoLista.AlinhamentoCelula.Direita);
                cabecalho[2] = new CabecalhoLista("Valor", CabecalhoLista.AlinhamentoCelula.Direita);
                cabecalho[3] = new CabecalhoLista("Data adiantamento", CabecalhoLista.AlinhamentoCelula.Direita);
                cabecalho[4] = new CabecalhoLista("Valor adiantado", CabecalhoLista.AlinhamentoCelula.Direita);

                LinhaLista linha;

                List<LinhaLista> registros = new List<LinhaLista>();
                foreach (TOBanricompras to in retListar.Dados)
                {
                    linha = new LinhaLista();
                    linha.Celulas.Add(to.CodMovimento.ToString());
                    linha.Celulas.Add(to.DataVencto.LerConteudoOuPadrao().ToString("dd/MM/yyyy"));
                    linha.Celulas.Add(to.ValorMovto.LerConteudoOuPadrao().ToString("N"));
                    linha.Celulas.Add(to.DataAdiantamento.LerConteudoOuPadrao().ToString("dd/MM/yyyy"));
                    linha.Celulas.Add(to.ValorAdiantado.LerConteudoOuPadrao().ToString("N"));
                    registros.Add(linha);
                }

                int itemSelecionado = Tela.ImprimeLista("Listagem de movimentos", cabecalho, registros, 15);
                if (itemSelecionado >= 0)
                {
                    return retListar.Dados[itemSelecionado];
                }
            }

            return null;
        }

        private TOCliente ListarClientes(TOCliente toCliente)
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();

            toCliente.TipoPessoa = "J";
            Retorno<List<TOCliente>> retListar = rnCliente.Listar(toCliente);
            if (!retListar.Ok)
            {
                Console.WriteLine(retListar.Mensagem);
            }
            else
            {
                CabecalhoLista[] cabecalho = new CabecalhoLista[5];
                cabecalho[0] = new CabecalhoLista("CNPJ");
                cabecalho[1] = new CabecalhoLista("Nome");
                cabecalho[2] = new CabecalhoLista("Rating");
                cabecalho[3] = new CabecalhoLista("Dt cadastro");
                cabecalho[4] = new CabecalhoLista("Dt atual rating");

                List<LinhaLista> registros = new List<LinhaLista>();
                foreach (TOCliente teste in retListar.Dados)
                {
                    LinhaLista linha = new LinhaLista();
                    Formatador f = new Formatador();
                    linha.Celulas.Add(string.Format(f, "{0:cnpj}", teste.CodCliente.LerConteudoOuPadrao()));
                    linha.Celulas.Add(teste.NomeCliente);
                    linha.Celulas.Add(teste.RatingCliente.LerConteudoOuPadrao(String.Empty));
                    linha.Celulas.Add(teste.DataCadastro.TemConteudo ?
                        teste.DataCadastro.LerConteudoOuPadrao().ToString("dd/MM/yyyy") : string.Empty);
                    linha.Celulas.Add(teste.DataAtuRating.TemConteudo ?
                        teste.DataAtuRating.LerConteudoOuPadrao().ToString("dd/MM/yyyy") : string.Empty);
                    registros.Add(linha);
                }
                int itemSelecionado = Tela.ImprimeLista("Listagem de clientes", cabecalho, registros, 15);
                if (itemSelecionado >= 0)
                {
                    return retListar.Dados[itemSelecionado];
                }
            }

            return null;
        }
    }
}
