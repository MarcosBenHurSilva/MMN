using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcscoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcwcoxn
{
    class MinhaTela : AplicacaoTela
    {
        public MinhaTela(String caminho)
            : base(caminho)
        { }

        public void Executar()
        {
            try
            {
                Menu menu = new Menu(
                new ItemMenu[] {
                        new ItemMenu( new KeyValuePair<int,string>(1, "Testa"), TestaCriarConta, false, true),
                        new ItemMenu( new KeyValuePair<int,string>(0, "Sair"), null, true)
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

        void TestaCriarConta(object obj)
        {
            try
            {

                RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                TOConta toConta = new TOConta();
                //testa espécie diferente de 35 para PF
                Console.WriteLine("incluir espécie diferente de 35 para PF");
                toConta.CodCliente = 191;
                toConta.TipoPessoa = "F";
                toConta.CodAgencia = 10;
                toConta.CodEspecie = 30;
                toConta.CodConta = 1;
                toConta.Limite = 100;
                toConta.Saldo = 0;
                Retorno<Int32> retIncluir = rnConta.Incluir(toConta);
                if (retIncluir.Ok)
                {
                    Console.WriteLine("Erro na inclusão, deixou incluir espécie 30 para pessoa física");
                }
                else
                {
                    Console.WriteLine("ok, {0}", retIncluir.Mensagem.ToString());
                }
                //PJ diferente de espécie 06
                
                Console.WriteLine("incluir espécie diferente de 06 para PJ");
                toConta = new TOConta();
                toConta.CodCliente = 191;
                toConta.TipoPessoa = "J";
                toConta.CodAgencia = 10;
                toConta.CodEspecie = 30;
                toConta.CodConta = 1;
                toConta.Limite = 100;
                toConta.Saldo = 0;
                retIncluir = rnConta.Incluir(toConta);
                if (retIncluir.Ok)
                {
                    Console.WriteLine("Erro na inclusão, deixou incluir espécie 30 para pessoa jurídica");
                }
                else
                {
                    Console.WriteLine("ok, {0}", retIncluir.Mensagem.ToString());
                }
                //agência ZERO
                Console.WriteLine("incluir agência zero");
                toConta = new TOConta();
                toConta.CodCliente = 191;
                toConta.TipoPessoa = "J";
                toConta.CodAgencia = 0;
                toConta.CodEspecie = 6;
                toConta.CodConta = 1;
                toConta.Saldo = 0;
                retIncluir = rnConta.Incluir(toConta);
                if (retIncluir.Ok)
                {
                    Console.WriteLine("Erro na inclusão, deixou incluir agência 0");
                }
                else
                {
                    Console.WriteLine("ok, {0}", retIncluir.Mensagem.ToString());
                }
                //conta ZERO
                Console.WriteLine("incluir conta zero");
                toConta = new TOConta();
                toConta.CodCliente = 191;
                toConta.TipoPessoa = "F";
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 0;
                toConta.Saldo = 0;
                retIncluir = rnConta.Incluir(toConta);
                if (retIncluir.Ok)
                {
                    Console.WriteLine("Erro na inclusão, deixou incluir conta 0");
                }
                else
                {
                    Console.WriteLine("ok, {0}", retIncluir.Mensagem.ToString());
                }
                //limite ZERO
                Console.WriteLine("incluir limite zero");
                toConta = new TOConta();
                toConta.CodCliente = 191;
                toConta.TipoPessoa = "F";
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 1;
                toConta.Limite = 0;
                toConta.Saldo = 0;
                retIncluir = rnConta.Incluir(toConta);
                if (retIncluir.Ok)
                {
                    Console.WriteLine("Erro na inclusão, deixou incluir limite 0");
                }
                else
                {
                    Console.WriteLine("ok, {0}", retIncluir.Mensagem.ToString());
                }
                //inclui conta normal sem limite
                Console.WriteLine("incluir esperando OK");
                toConta = new TOConta();
                toConta.CodCliente = 191;
                toConta.TipoPessoa = "F";
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 101010;
                toConta.Saldo = 0;
                retIncluir = rnConta.Incluir(toConta);
                if (retIncluir.Ok)
                {
                    Console.WriteLine(retIncluir.Mensagem.ToString());
                }
                else
                {
                    Console.WriteLine("Erro na inclusão, {0}", retIncluir.Mensagem.ToString());
                }
                //altera limite para ZERO
                Console.WriteLine("altera limite para zero");
                toConta = new TOConta();
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 101010;
                toConta.Limite = 0;
                Retorno<Int32> retAlterar = rnConta.Alterar(toConta);
                if (retAlterar.Ok)
                {
                    Console.WriteLine("Erro na alteração, deixou alterar limite para 0");
                }
                else
                {
                    Console.WriteLine(retAlterar.Mensagem.ToString());
                }
                //tenta alterar o saldo diretamente no Alterar
                Console.WriteLine("tenta alterar o saldo diretamente no Alterar");
                toConta = new TOConta();
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 101010;
                toConta.Saldo = 1;
                retAlterar = rnConta.Alterar(toConta);
                if (!retAlterar.Ok)
                {
                    Console.WriteLine(retAlterar.Mensagem.ToString());
                }
                else
                {
                    Console.WriteLine("Teste ok");
                }
                //consulta o saldo para verificar se ainda é zero
                Console.WriteLine("consulta o saldo para verificar se ainda é zero");
                toConta = new TOConta();
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 101010;
                Retorno<List<TOConta>> retListar = rnConta.Listar(toConta);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem.ToString());
                }
                if (retListar.Dados[0].Saldo.LerConteudoOuPadrao() != 0)
                {
                    Console.WriteLine("Erro, deixou alterar o saldo diretamente no método Alterar, saldo atual {0}", retListar.Dados[0].Saldo.LerConteudoOuPadrao());
                }
                else
                {
                    Console.WriteLine("Teste ok, saldo {0}", retListar.Dados[0].Saldo.LerConteudoOuPadrao());
                }
                //testa o Saque
                Console.WriteLine("testa o Saque");
                toConta = new TOConta();
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 101010;
                toConta.ValorTransacao = 2;
                Retorno<TOConta> retTransacao = rnConta.Sacar(toConta);
                if (retTransacao.Ok)
                {
                    Console.WriteLine("Erro, permitiu o saque sem limite e sem saldo.");
                }
                else
                {
                    Console.WriteLine("ok, Sem saldo: {0}", retTransacao.Mensagem.ToString());
                }
                //coloca limite de 10, saldo de 0
                Console.WriteLine("coloca limite de 10, saldo de 0");
                toConta = new TOConta();
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 101010;
                toConta.Limite = 10;
                retAlterar = rnConta.Alterar(toConta);
                if (!retAlterar.Ok)
                {
                    Console.WriteLine(retAlterar.Mensagem.ToString());
                }
                //testa o Saque com limite
                Console.WriteLine("testa o Saque com limite");
                toConta = new TOConta();
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 101010;
                toConta.ValorTransacao = 2;
                retTransacao = rnConta.Sacar(toConta);
                if (retTransacao.Ok)
                {
                    Console.WriteLine("ok, {0}", retTransacao.Mensagem.ToString());
                }
                else
                {
                    Console.WriteLine("Erro, não permitiu o saque: {0}", retTransacao.Mensagem.ToString());
                }
                //testa o depósito
                Console.WriteLine("testa o depósito");
                toConta = new TOConta();
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 101010;
                toConta.ValorTransacao = 200;
                retTransacao = rnConta.Depositar(toConta);
                if (retTransacao.Ok)
                {
                    Console.WriteLine("ok, {0}", retTransacao.Mensagem.ToString());
                }
                else
                {
                    Console.WriteLine("Erro, não permitiu o depósito: {0}", retTransacao.Mensagem.ToString());
                }
                //apresenta o saldo que deve ser diferente de zero
                Console.WriteLine("apresenta o saldo que deve ser diferente de zero");
                toConta = new TOConta();
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 101010;
                retListar = rnConta.Listar(toConta);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem.ToString());
                }
                if (retListar.Dados[0].Saldo.LerConteudoOuPadrao() == 0)
                {
                    Console.WriteLine("Erro no saldo, deveria ser diferente de zero, saldo atual {0}", retListar.Dados[0].Saldo.LerConteudoOuPadrao());
                }
                else
                {
                    Console.WriteLine("ok, saldo {0}", retListar.Dados[0].Saldo.LerConteudoOuPadrao());
                }
                //testa o Saque
                Console.WriteLine("testa o Saque de valor negativo");
                toConta = new TOConta();
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 101010;
                toConta.ValorTransacao = -2;
                retTransacao = rnConta.Sacar(toConta);
                if (retTransacao.Ok)
                {
                    Console.WriteLine("Erro, permitiu o saque com valor negativo.");
                }
                else
                {
                    Console.WriteLine("ok, {0}", retTransacao.Mensagem.ToString());
                }
                //testa o Saque
                Console.WriteLine("testa o depósito de valor negativo");
                toConta = new TOConta();
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 101010;
                toConta.ValorTransacao = -2;
                retTransacao = rnConta.Depositar(toConta);
                if (retTransacao.Ok)
                {
                    Console.WriteLine("Erro, permitiu o depósito com valor negativo.");
                }
                else
                {
                    Console.WriteLine("ok, {0}", retTransacao.Mensagem.ToString());
                }
                //testa a exclusão
                Console.WriteLine("testa a exclusão");
                toConta = new TOConta();
                toConta.CodAgencia = 100;
                toConta.CodEspecie = 35;
                toConta.CodConta = 101010;
                Retorno<Int32> retExcluir = rnConta.Excluir(toConta);
                if (!retExcluir.Ok)
                {
                    Console.WriteLine("Erro na exclusão: {0}", retExcluir.Mensagem.ToString());
                }
                else
                {
                    Console.WriteLine(retExcluir.Mensagem.ToString());
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
