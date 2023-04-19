using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcsscxn;
using Bergs.Pxc.Pxcsclxn;

namespace Bergs.Pxc.Pxcwtsxn
{
    class MinhaTela : AplicacaoTela
    {
        public MinhaTela(String caminho)
            : base(caminho)
        { }

        private List<Mensagem> mensagens;
        public void Executar()
        {
            try
            {
                mensagens = new List<Mensagem>();
                Menu menu = new Menu(
                new ItemMenu[] {
                        new ItemMenu( new KeyValuePair<int,string>(1, "Valida RN1 - Empresa deve ser PJ"), RN1, false),
                        new ItemMenu( new KeyValuePair<int,string>(2, "Valida RN2 - Empresa deve ter CNPJ válido"), RN2, false),
                        new ItemMenu( new KeyValuePair<int,string>(3, "Valida RN3 - CNPJ não cadastrado na tabela CLIENTE"), RN3, false),
                        new ItemMenu( new KeyValuePair<int,string>(4, "Valida RN4 - Sócio deve ser pessoa física"), RN4, false),
                        new ItemMenu( new KeyValuePair<int,string>(5, "Valida RN5 - CPF deve ser válido"), RN5, false),
                        new ItemMenu( new KeyValuePair<int,string>(6, "Valida RN6 - Cliente não cadastrado na tabela CLIENTE"), RN6, false),
                        new ItemMenu( new KeyValuePair<int,string>(7, "Valida RN7 - Sócio já possui participação societária"), RN7, false),
                        new ItemMenu( new KeyValuePair<int,string>(8, "Valida RN8-incluir - Percentual deve ser maior que zero"), RN8Incluir, false),
                        new ItemMenu( new KeyValuePair<int,string>(9, "Valida RN8-alterar - Percentual deve ser maior que zero"), RN8Alterar, false),
                        new ItemMenu( new KeyValuePair<int,string>(10, "Valida RN9-incluir - Percentual maior que 100%"), RN9Incluir, false),
                        new ItemMenu( new KeyValuePair<int,string>(11, "Valida RN9-Alterar - Percentual maior que 100%"), RN9Alterar, false),
                        new ItemMenu( new KeyValuePair<int,string>(12, "Valida RN10 - Sócio deve ter no mínimo 21 anos"), RN10, false),
                        new ItemMenu( new KeyValuePair<int,string>(13, "Valida Incluir"), Incluir, false),
                        new ItemMenu( new KeyValuePair<int,string>(14, "Valida Alterar"), Alterar, false),
                        new ItemMenu( new KeyValuePair<int,string>(15, "Valida Excluir"), Excluir, false),
                        new ItemMenu( new KeyValuePair<int,string>(16, "Valida Lista"), Listar, false),
                        new ItemMenu( new KeyValuePair<int,string>(17, "Valida campos TO"), CamposTO, false),
                        new ItemMenu( new KeyValuePair<int,string>(18, "Todos"), Todos, false),
                        new ItemMenu( new KeyValuePair<int,string>(0, "Sair"), null, true)
                        }, null);
                Tela.ControlaMenu("Banrisul - Programa de teste de sócios", menu);
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}\nTecle algo...", e.Message);
                Console.ReadKey();
            }
        }
        void RN1(object obj)
        {
            ValidaRN1();
        }
        Boolean ValidaRN1()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 191;
            toSocio.TipoPessoaEmpresa = "F";
            toSocio.CodClienteSocio = 272;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 10;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN1 ERRO - retornou OK - incluiu e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN1 OK retornou: {0}", retIncluir.Mensagem.ParaOperador);
                if (retIncluir.Mensagem.ParaOperador == "Empresa deve ser pessoa jurídica.")
                {
                    Console.WriteLine("Mensagem RN1 OK");
                    mensagens.Add(new Mensagem("1", retIncluir.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN1 errada -----");
                    mensagens.Add(new Mensagem("1", String.Format("Mensagem errada: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
                return true;
            }
        }
        void RN2(object obj)
        {
            ValidaRN2();
        }
        Boolean ValidaRN2()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 333;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 272;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 10;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN2 ERRO - retornou OK - incluiu e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN2 OK retornou: {0}", retIncluir.Mensagem.ParaOperador);
                if (retIncluir.Mensagem.ParaOperador == "CNPJ inválido.")
                {
                    Console.WriteLine("Mensagem RN2 OK");
                    mensagens.Add(new Mensagem("2", retIncluir.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN2 errada -----");
                    mensagens.Add(new Mensagem("2", String.Format("Mensagem errada: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
                return true;
            }
        }
        void RN3(object obj)
        {
            ValidaRN3();
        }
        Boolean ValidaRN3()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 6000169;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 272;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 10;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN3 ERRO - retornou OK - incluiu e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN3 OK - retornou: {0}", retIncluir.Mensagem.ParaOperador);
                if (retIncluir.Mensagem.ParaOperador == "Empresa com o CNPJ 00.000.006/0001-69 não está cadastrada.")
                {
                    Console.WriteLine("Mensagem RN3 OK");
                    mensagens.Add(new Mensagem("3", retIncluir.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN3 errada -----");
                    mensagens.Add(new Mensagem("3", String.Format("Mensagem errada: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
                return true;
            }
        }
        void RN4(object obj)
        {
            ValidaRN4();
        }
        Boolean ValidaRN4()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 191;
            toSocio.TipoPessoaSocio = "J";
            toSocio.ParticipSocietaria = 10;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN4 ERRO - retornou OK - incluiu e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN4 OK retornou: {0}", retIncluir.Mensagem.ParaOperador);
                if (retIncluir.Mensagem.ParaOperador == "Sócio deve ser pessoa física.")
                {
                    Console.WriteLine("Mensagem RN4 OK");
                    mensagens.Add(new Mensagem("4", retIncluir.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN4 errada -----");
                    mensagens.Add(new Mensagem("4", String.Format("Mensagem errada: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
                return true;
            }
        }
        void RN5(object obj)
        {
            ValidaRN5();
        }
        Boolean ValidaRN5()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 555;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 10;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN5 ERRO - retornou OK - incluiu e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN5 OK - retornou: {0}", retIncluir.Mensagem.ParaOperador);
                if (retIncluir.Mensagem.ParaOperador == "CPF inválido.")
                {
                    Console.WriteLine("Mensagem RN5 OK");
                    mensagens.Add(new Mensagem("5", retIncluir.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN5 errada -----");
                    mensagens.Add(new Mensagem("5", String.Format("Mensagem errada: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
                return true;
            }
        }
        void RN6(object obj)
        {
            ValidaRN6();
        }
        Boolean ValidaRN6()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 5555507;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 10;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN6 ERRO - retornou OK - incluiu e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN6 OK - retornou: {0}", retIncluir.Mensagem.ParaOperador);
                if (retIncluir.Mensagem.ParaOperador == "Cliente com o CPF 000.055.555-07 não está cadastrado.")
                {
                    Console.WriteLine("Mensagem RN6 OK");
                    mensagens.Add(new Mensagem("6", retIncluir.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN6 errada -----");
                    mensagens.Add(new Mensagem("6", String.Format("Mensagem errada: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
                return true;
            }
        }
        void RN7(object obj)
        {
            ValidaRN7();
        }
        Boolean ValidaRN7()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 4000170;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 191;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 3;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN7 ERRO - retornou OK - incluiu e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN7 OK - retornou: {0}", retIncluir.Mensagem.ParaOperador);
                if (retIncluir.Mensagem.ParaOperador == "Sócio já possui 10,00% de participação societária.")
                {
                    Console.WriteLine("Mensagem RN7 OK");
                    mensagens.Add(new Mensagem("7", retIncluir.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN7 errada -----");
                    mensagens.Add(new Mensagem("7", String.Format("Mensagem errada: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
                return true;
            }
        }
        void RN8Incluir(object obj)
        {
            ValidaRN8Incluir();
        }
        Boolean ValidaRN8Incluir()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 272;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 0;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN8 inc ERRO - retornou OK - incluiu e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN8 inc OK - retornou: {0}", retIncluir.Mensagem.ParaOperador);
                if (retIncluir.Mensagem.ParaOperador == "Participação societária deve ser maior que zero.")
                {
                    Console.WriteLine("Mensagem RN8-incluir OK");
                    mensagens.Add(new Mensagem("8", retIncluir.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN8-incluir errada -----");
                    mensagens.Add(new Mensagem("8", String.Format("Mensagem errada: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
                return true;
            }
        }
        void RN8Alterar(object obj)
        {
            ValidaRN8Alterar();
        }
        Boolean ValidaRN8Alterar()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 4000170;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 191;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 0;
            Retorno<Int32> retIncluir = rnSocio.Alterar(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN8 alt ERRO - retornou OK - alterou e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN8 alt OK - retornou: {0}", retIncluir.Mensagem.ParaOperador);
                if (retIncluir.Mensagem.ParaOperador == "Participação societária deve ser maior que zero.")
                {
                    Console.WriteLine("Mensagem RN8-alterar OK");
                    mensagens.Add(new Mensagem("8", retIncluir.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN8-alterar errada -----");
                    mensagens.Add(new Mensagem("8", String.Format("Mensagem errada: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
                return true;
            }
        }
        void RN9Incluir(object obj)
        {
            ValidaRN9Incluir();
        }
        Boolean ValidaRN9Incluir()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 353;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 110;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN9 inc ERRO - retornou OK - incluiu e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN9 inc OK - retornou: {0}", retIncluir.Mensagem.ParaOperador);
                if (retIncluir.Mensagem.ParaOperador == "O total de participação societária não pode ultrapassar 100%.")
                {
                    Console.WriteLine("Mensagem RN9-incluir OK");
                    mensagens.Add(new Mensagem("9", retIncluir.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN9-incluir errada -----");
                    mensagens.Add(new Mensagem("9", String.Format("Mensagem errada: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
                return true;
            }
        }
        void RN9Alterar(object obj)
        {
            ValidaRN9Alterar();
            ValidaRN9Alterar2();
            ValidaRN9AlterarParaMenos();
            ValidaRN9AlterarParaMais();
        }
        Boolean ValidaRN9Alterar()
        {
            Console.WriteLine("Aumentando a participação societária de 70% para 80%");
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 4000170;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 353;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 80;
            Retorno<Int32> retIncluir = rnSocio.Alterar(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN9 alt OK - retornou OK - alterou OK");
                mensagens.Add(new Mensagem("9 alt", retIncluir.Mensagem.ParaOperador, true));
                return true;
            }
            else
            {
                Console.WriteLine("RN9 alt ERRO - Não permitiu - ERRADO - Retornou: {0}", retIncluir.Mensagem.ParaOperador);
                return false;
            }
        }
        Boolean ValidaRN9Alterar2()
        {
            Console.WriteLine("Aumentando a participação societária para 99%");
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 4000170;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 353;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 99;
            Retorno<Int32> retAlterar = rnSocio.Alterar(toSocio);
            if (retAlterar.Ok)
            {
                Console.WriteLine("RN9 alt OK - erro - alterou e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN9 alt OK - Não permitiu - Retornou: {0}", retAlterar.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("9 alt", retAlterar.Mensagem.ParaOperador, true));
                return true;
            }
        }
        Boolean ValidaRN9AlterarParaMenos()
        {
            Console.WriteLine("Diminuindo a participação societária de 80% (ou 70% caso tenha dado erro na anterior) para 60%");
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 4000170;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 353;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 60;
            Retorno<Int32> retIncluir = rnSocio.Alterar(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN9 alt p/menos OK - retornou OK - alterou OK");
                return true;
            }
            else
            {
                Console.WriteLine("RN9 alt p/menos ERRO - Não permitiu - ERRADO - Retornou: {0}", retIncluir.Mensagem.ParaOperador);
                return false;
            }
        }
        Boolean ValidaRN9AlterarParaMais()
        {
            Console.WriteLine("Aumentando a participação societária de 40% para 60% com 3 sócios");
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 01111111000138;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 11144416;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 60;
            Retorno<Int32> retAlterar = rnSocio.Alterar(toSocio);
            if (retAlterar.Ok)
            {
                Console.WriteLine("RN9 alt p/mais OK - retornou OK - alterou OK");
                return true;
            }
            else
            {
                Console.WriteLine("RN9 alt p/mais ERRO - Não permitiu - ERRADO - Retornou: {0}", retAlterar.Mensagem.ParaOperador);
                return false;
            }
        }
        void RN10(object obj)
        {
            ValidaRN10();
        }
        Boolean ValidaRN10()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            TOCliente toCliente = new TOCliente();

            toCliente.CodCliente = 434;
            toCliente.TipoPessoa = "F";
            toCliente.DataNasc = DateTime.Today.AddYears(-21).AddDays(1);

            Retorno<Int32> retAlterar = rnCliente.Alterar(toCliente);
            if (!retAlterar.Ok)
            {
                Console.WriteLine(retAlterar.Mensagem);
                return false;
            }

            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 434;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 2;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("RN10 ERRO - retornou OK - incluiu e não deveria");
                return false;
            }
            else
            {
                Console.WriteLine("RN10 OK - retornou: {0}", retIncluir.Mensagem.ParaOperador);
                if (retIncluir.Mensagem.ParaOperador == "Sócio deve ter no mínimo 21 anos.")
                {
                    Console.WriteLine("Mensagem RN10 OK");
                    mensagens.Add(new Mensagem("10", retIncluir.Mensagem.ParaOperador, true));
                }
                else
                {
                    Console.WriteLine("Mensagem RN10 errada -----");
                    mensagens.Add(new Mensagem("10", String.Format("Mensagem errada: {0}", retIncluir.Mensagem.ParaOperador), false));
                    //return false;
                }
                toSocio = new TOSocio();
                toSocio.CodClienteEmpresa = 1000136;
                toSocio.TipoPessoaEmpresa = "J";
                toSocio.CodClienteSocio = 25887068; //sem data de nascimento cadastrada
                toSocio.TipoPessoaSocio = "F";
                toSocio.ParticipSocietaria = 2;
                retIncluir = rnSocio.Incluir(toSocio);
                if (retIncluir.Ok)
                {
                    Console.WriteLine("RN10 (sem idade cadastrada) ERRO - retornou OK - incluiu e não deveria (sem idade cadastrada)");
                    mensagens.Add(new Mensagem("10", String.Format("retornou ok e não deveria: {0}", retIncluir.Mensagem.ParaOperador), false));
                    return false;
                }
                else
                {
                    Console.WriteLine("RN10 OK (sem idade cadastrada) - retornou: {0}", retIncluir.Mensagem.ParaOperador);
                    if (retIncluir.Mensagem.ParaOperador != "Sócio deve ter no mínimo 21 anos.")
                    {
                        Console.WriteLine("Mensagem RN10 errada --/LER MSG DE RETORNO, PODE SER CAMPOOBRIGATORIO/---");
                        mensagens.Add(new Mensagem("10", String.Format("Mensagem errada, mas pode ser CAMPOOBRIGATORIO: {0}", retIncluir.Mensagem.ParaOperador), false));
                    }
                    else
                    {
                        Console.WriteLine("Mensagem RN10 OK");
                        mensagens.Add(new Mensagem("10", retIncluir.Mensagem.ParaOperador, true));
                    }
                    return true;
                }
            }
        }
        void Incluir(object obj)
        {
            ValidaIncluir();
        }
        Boolean ValidaIncluir()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            Retorno<Int32> retIncluir;
            toSocio.CodClienteEmpresa = 2000180;
            retIncluir = rnSocio.Incluir(toSocio);
            if (!retIncluir.Ok)
            {
                if (retIncluir.Mensagem.GetType() != typeof(CampoObrigatorioMensagem))
                {
                    Console.WriteLine("Incluir - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retIncluir.Mensagem.ParaOperador);
                    mensagens.Add(new Mensagem("Incluir", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
            }
            else
            {
                Console.WriteLine("Incluir - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retIncluir.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Incluir", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retIncluir.Mensagem.ParaOperador), false));
            }

            toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 2000180;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 191;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = new CampoTabela<Double>(null);
            retIncluir = rnSocio.Incluir(toSocio);
            if (!retIncluir.Ok)
            {
                if (retIncluir.Mensagem.GetType() != typeof(CampoObrigatorioMensagem))
                {
                    Console.WriteLine("Incluir - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retIncluir.Mensagem.ParaOperador);
                    mensagens.Add(new Mensagem("Incluir", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
            }
            else
            {
                Console.WriteLine("Incluir - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retIncluir.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Incluir", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retIncluir.Mensagem.ParaOperador), false));
            }
            retIncluir = rnSocio.Incluir(new TOSocio());
            if (!retIncluir.Ok)
            {
                if (retIncluir.Mensagem.GetType() != typeof(CampoObrigatorioMensagem))
                {
                    Console.WriteLine("Incluir new - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retIncluir.Mensagem.ParaOperador);
                    mensagens.Add(new Mensagem("Incluir new", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retIncluir.Mensagem.ParaOperador), false));
                }
            }
            else
            {
                Console.WriteLine("Incluir new - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retIncluir.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Incluir", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retIncluir.Mensagem.ParaOperador), false));
            }

            toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 2000180;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 191;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 2;
            retIncluir = rnSocio.Incluir(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("Incluir - retornou OK - incluiu");
                mensagens.Add(new Mensagem("Incluir", "Retornou OK", true));
                return true;
            }
            else
            {
                Console.WriteLine("Incluir - ERRO não incluiu - retornou: {0}", retIncluir.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Incluir", String.Format("ERRO não incluiu, retornou: {0}", retIncluir.Mensagem.ParaOperador), false));
                return false;
            }
        }
        void Alterar(object obj)
        {
            ValidaAlterar();
        }
        Boolean ValidaAlterar()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            Retorno<Int32> retAlterar;

            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 2000180;
            toSocio.TipoPessoaEmpresa = new CampoTabela<string>(null);
            toSocio.CodClienteSocio = 272;
            toSocio.ParticipSocietaria = 1;
            retAlterar = rnSocio.Alterar(toSocio);
            if (!retAlterar.Ok)
            {
                if (retAlterar.Mensagem.GetType() != typeof(CampoObrigatorioMensagem))
                {
                    Console.WriteLine("Alterar - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retAlterar.Mensagem.ParaOperador);
                    mensagens.Add(new Mensagem("Alterar", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retAlterar.Mensagem.ParaOperador), false));
                }
            }
            else
            {
                Console.WriteLine("Alterar - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retAlterar.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Alterar", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retAlterar.Mensagem.ParaOperador), false));
            }

            retAlterar = rnSocio.Alterar(new TOSocio());
            if (!retAlterar.Ok)
            {
                if (retAlterar.Mensagem.GetType() != typeof(CampoObrigatorioMensagem))
                {
                    Console.WriteLine("Alterar new - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retAlterar.Mensagem.ParaOperador);
                    mensagens.Add(new Mensagem("Alterar new", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retAlterar.Mensagem.ParaOperador), false));
                }
            }
            else
            {
                Console.WriteLine("Alterar new - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retAlterar.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Alterar new", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retAlterar.Mensagem.ParaOperador), false));
            }

            toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 2000180;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 272;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 25;
            retAlterar = rnSocio.Alterar(toSocio);
            if (retAlterar.Ok)
            {
                Console.WriteLine("Alterar - retornou OK - alterou");
                mensagens.Add(new Mensagem("Alterar", "Retornou OK", true));
                return true;
            }
            else
            {
                Console.WriteLine("Alterar - ERRO não alterou - retornou: {0}", retAlterar.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Alterar", String.Format("ERRO não alterou, retornou: {0}", retAlterar.Mensagem.ParaOperador), false));
                return false;
            }

        }
        void Excluir(object obj)
        {
            ValidaExcluir();
        }
        Boolean ValidaExcluir()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            Retorno<Int32> retExcluir;

            toSocio.CodClienteEmpresa = new CampoTabela<double>(null);
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 272;
            toSocio.TipoPessoaSocio = "F";
            retExcluir = rnSocio.Excluir(toSocio);
            if (!retExcluir.Ok)
            {
                if (retExcluir.Mensagem.GetType() != typeof(CampoObrigatorioMensagem))
                {
                    Console.WriteLine("Excluir - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retExcluir.Mensagem.ParaOperador);
                    mensagens.Add(new Mensagem("Excluir", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retExcluir.Mensagem.ParaOperador), false));
                }
            }
            else
            {
                Console.WriteLine("Excluir - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retExcluir.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Excluir", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retExcluir.Mensagem.ParaOperador), false));
            }

            retExcluir = rnSocio.Excluir(new TOSocio());
            if (!retExcluir.Ok)
            {
                if (retExcluir.Mensagem.GetType() != typeof(CampoObrigatorioMensagem))
                {
                    Console.WriteLine("Excluir new - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retExcluir.Mensagem.ParaOperador);
                    mensagens.Add(new Mensagem("Excluir new", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retExcluir.Mensagem.ParaOperador), false));
                }
            }
            else
            {
                Console.WriteLine("Excluir new - deveria retornar CampoObrigatorioMensagem, retornou: {0}", retExcluir.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Excluir new", String.Format("Deveria retornar CampoObrigatorioMensagem, retornou: {0}", retExcluir.Mensagem.ParaOperador), false));
            }

            toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 2000180;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 272;
            toSocio.TipoPessoaSocio = "F";
            retExcluir = rnSocio.Excluir(toSocio);
            if (retExcluir.Ok)
            {
                Console.WriteLine("Excluir OK - retornou OK - excluiu");
                mensagens.Add(new Mensagem("Excluir", "Retornou OK", true));
                return true;
            }
            else
            {
                Console.WriteLine("Excluir - ERRO não excluiu - retornou: {0}", retExcluir.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Excluir", String.Format("ERRO não excluiu, retornou: {0}", retExcluir.Mensagem.ParaOperador), false));
                return false;
            }
        }
        void Listar(object obj)
        {
            ValidaListar();
        }
        Boolean ValidaListar()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            Retorno<List<TOSocio>> retListar;

            toSocio.CodClienteEmpresa = new CampoTabela<double>(null);
            retListar = rnSocio.Listar(toSocio);
            if (!retListar.Ok)
            {
                Console.WriteLine("Listar - deveria listar e deu erro, retornou: {0}", retListar.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Listar", String.Format("Deveria listar e deu erro, retornou: {0}", retListar.Mensagem.ParaOperador), false));
            }

            retListar = rnSocio.Listar(new TOSocio());
            if (!retListar.Ok)
            {
                Console.WriteLine("Listar new - deveria listar e deu erro, retornou: {0}", retListar.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Listar new", String.Format("Deveria listar e deu erro, retornou: {0}", retListar.Mensagem.ParaOperador), false));
            }

            toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 3000125;
            toSocio.TipoPessoaEmpresa = "J";
            Retorno<List<TOSocio>> retIncluir = rnSocio.Listar(toSocio);
            if (retIncluir.Ok)
            {
                Console.WriteLine("Listar - retornou OK - deve ter 2 sócios na lista: total: {0}", retIncluir.Dados.Count);
                mensagens.Add(new Mensagem("Listar", String.Format("Listou OK - deve ter 2 sócios na lista: total: {0}", retIncluir.Dados.Count), true));
                if (retIncluir.Dados.Count != 2)
                {
                    Console.WriteLine("Listar - ERRO, não tem os 2 sócios");
                    return false;
                }
                return true;
            }
            else
            {
                Console.WriteLine("Listar - ERRO não listou - retornou: {0}", retIncluir.Mensagem.ParaOperador);
                mensagens.Add(new Mensagem("Listar", String.Format("ERRO não listou, retornou: {0}", retListar.Mensagem.ParaOperador), false));
                return false;
            }
        }
        void CamposTO(object obj)
        {
            ValidaCamposTO();
        }
        Boolean ValidaCamposTO()
        {
            TOSocio toSocio = new TOSocio();
            try
            {
                toSocio.NomeClienteEmpresa = "SSSS";
                toSocio.NomeClienteSocio = "XXXX";
                Console.WriteLine("Campos TO - OK");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Campos TO - ERRO: {0}", e.Message);
                return false;
            }
        }

        void Todos(object obj)
        {
            mensagens.Clear();
            Console.Clear();
            Boolean[] valida = { ValidaRN1(), ValidaRN2(), ValidaRN3(), ValidaRN4(), ValidaRN5(),
                                ValidaRN6() , ValidaRN7() , ValidaRN8Alterar() , ValidaRN8Incluir() , 
                                ValidaRN9Incluir() , ValidaRN9Alterar(),ValidaRN9Alterar2(), ValidaRN9AlterarParaMais(),
                                ValidaRN9AlterarParaMenos() ,
                                ValidaRN10() , ValidaIncluir() , ValidaAlterar() , ValidaExcluir() ,
                                ValidaListar(), ValidaCamposTO()};
            Boolean fim = true;
            for (Int32 i = 0; i < valida.Length; i++)
            {
                fim = fim && valida[i];
            }
            Console.WriteLine("".PadLeft(40, '-'));
            if (fim)
                Console.WriteLine("Passou por todas as validações");
            else
                Console.WriteLine("Parou em algum item");
            foreach (Mensagem m in mensagens)
            {
                Console.Write("Mensagem {0} retornou {1}", m.regra, m.ok ? "OK" : "ERRADA");
                if (m.ok == false)
                {
                    Console.Write(" - msg=#{0}#", m.mensagem);
                }
                Console.WriteLine();
            }
        }
    }
}
