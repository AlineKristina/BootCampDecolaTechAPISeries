using System;
using DIO.Series;

namespace DIO.Series
{
    class Program
    {

        static SerieRepositorio repositorioS = new SerieRepositorio();
        static FilmeRepositorio repositorioF = new FilmeRepositorio();
        static void Main(string[] args)
        {
            string[] opcao = ObterUsuario();
            string opcaoCatalogo = opcao[0];
            string opcaoUsuario = opcao[1];

            while(opcaoUsuario.ToUpper() != "X")
            {
                if (opcaoCatalogo == "F")
                {
                    switch (opcaoUsuario)
                    {
                        case "1":
                            ListarFilmes();
                            break;
                        case "2":
                            InserirFilme();
                            break;
                        case "3":
                            AtualizarFilme();
                            break;
                        case "4":
                            ExcluirFilme();
                            break;
                        case "5":
                            VisualizarFilme();
                            break;
                        case "C":
                            Console.Clear();
                            break;
                    }
                }
                else if (opcaoCatalogo == "S")
                {
                    switch (opcaoUsuario)
                    {
                        case "1":
                            ListarSeries();
                            break;
                        case "2":
                            InserirSerie();
                            break;
                        case "3":
                            AtualizarSerie();
                            break;
                        case "4":
                            ExcluirSerie();
                            break;
                        case "5":
                            VisualizarSerie();
                            break;
                        case "C":
                            Console.Clear();
                            break;
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

                opcao = ObterUsuario();
                opcaoCatalogo = opcao[0];
                opcaoUsuario = opcao[1];
            }
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries: ");
            var lista = repositorioS.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma Série Cadastrada.");
                return;
            }
            foreach(var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                
                Console.WriteLine("#ID {0}: - {1}", serie.retornaID(), serie.retornaTitulo(), (excluido ? "*Registro excluido*" : ""));
            }
        }

        private static void ListarFilmes()
        {
            Console.WriteLine("Listar filmes: ");
            var lista = repositorioF.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum Filme Cadastrado.");
                return;
            }
            foreach (var filme in lista)
            {
                var excluido = filme.retornaExcluido();

                Console.WriteLine("#ID {0}: - {1}", filme.retornaID(), filme.retornaTitulo(), (excluido ? "*Registro excluido*" : ""));
            }
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("Digite o ID da série: (de 0 a n)");
            int indiceSerie = int.Parse(Console.ReadLine());

            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: indiceSerie, 
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo, 
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorioS.Atualiza(indiceSerie, atualizaSerie);

        }

        private static void AtualizarFilme()
        {
            Console.WriteLine("Digite o ID do filme: (de 0 a n)");
            int indiceFilme = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o título do filme: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o ano de início do filme: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a descrição do filme: ");
            string entradaDescricao = Console.ReadLine();

            Console.Write("Digite a duração do filme: ");
            int entradaDuracao = int.Parse(Console.ReadLine());

            Filme atualizaFilme = new Filme(id: indiceFilme,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        duracao: entradaDuracao,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorioF.Atualiza(indiceFilme, atualizaFilme);

        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Digite o ID da série: (de 0 a n)");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorioS.Exclui(indiceSerie);
        }

        private static void ExcluirFilme()
        {
            Console.WriteLine("Digite o ID do filme: (de 0 a n)");
            int indiceFilme = int.Parse(Console.ReadLine());

            repositorioF.Exclui(indiceFilme);
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.Write("Digite o ID da série: (de 0 a n)");
            int indiceSerie = int.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Visualizar série ou inserir nova temporada? v/i");
            string opcaoSerie = Console.ReadLine().ToUpper();
            Console.WriteLine("------------------------------------------------------------");
            if (opcaoSerie == "V")
            {
                var serie = repositorioS.RetornaPorID(indiceSerie);
                Console.WriteLine(serie);
                Console.WriteLine(serie.Temporadas);

            }
            else if (opcaoSerie == "I")
            {
                Console.WriteLine("Digite a quantidade de episódios: ");
                int entradaQEp = int.Parse(Console.ReadLine());
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Digite a duração média dos episódios, em minutos: ");
                int entradaDM = int.Parse(Console.ReadLine());
                Console.WriteLine("------------------------------------------------------------");
                Temporada t = new Temporada(entradaQEp, entradaDM);
                repositorioS.Lista()[indiceSerie].adicionarTemporada(t);
            }
        }

        private static void VisualizarFilme()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Digite o ID do filme (de 0 a n)");
            int indiceFilme = int.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------------------------------------");

            var filme = repositorioF.RetornaPorID(indiceFilme);

            Console.WriteLine(filme);
        }

        private static void InserirSerie()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Inserir uma nova série: ");
            Console.WriteLine("------------------------------------------------------------");
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------------------------------------");
            Console.Write("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();
            Console.WriteLine("------------------------------------------------------------");

            Console.Write("Digite o ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------------------------------------");

            Console.Write("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();
            Console.WriteLine("------------------------------------------------------------");

            Serie novaSerie = new Serie(id: repositorioS.ProximoID(), genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo, ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorioS.Insere(novaSerie);
        }

        private static void InserirFilme()
        {
            Console.WriteLine("Inserir um novo filme: ");
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------------------------------------");

            Console.Write("Digite o título do filme: ");
            string entradaTitulo = Console.ReadLine();
            Console.WriteLine("------------------------------------------------------------");

            Console.Write("Digite o ano de lançamento do filme: ");
            int entradaAno = int.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------------------------------------");

            Console.Write("Digite a descrição do filme: ");
            string entradaDescricao = Console.ReadLine();
            Console.WriteLine("------------------------------------------------------------");

            Console.Write("Digite a duração do filme, em minutos: ");
            int entradaDuracao = int.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------------------------------------");

            Filme novoFilme = new Filme(id: repositorioS.ProximoID(), genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo, ano: entradaAno,
                                        duracao: entradaDuracao,
                                        descricao: entradaDescricao);

            repositorioF.Insere(novoFilme);
        }

        private static string obterCatalogo()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("DIO Séries a sua disposição!");
            Console.WriteLine("Informe a opçao desejada: ");
            Console.WriteLine("Deseja acessar o menu de séries ou filmes? s/f");
            Console.WriteLine("------------------------------------------------------------");
            string opcaoCatalogo = Console.ReadLine().ToUpper();
            Console.WriteLine();

            return opcaoCatalogo;
        }

        private static string[] ObterUsuario()
        {
            bool validacao = false;
            string opt = obterCatalogo();
            string[] gambiarra = new string[2];
            do
            {
                if (opt == "S")
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("1. Listar séries");
                    Console.WriteLine("2. Inserir nova série");
                    Console.WriteLine("3. Atualizar série");
                    Console.WriteLine("4. Excluir série");
                    Console.WriteLine("5. Visualizar séries/Adicionar Temporada");
                    Console.WriteLine("C. Limpar tela");
                    Console.WriteLine("X. Sair");
                    Console.WriteLine("------------------------------------------------------------");
                    string opcaoUsuario = Console.ReadLine().ToUpper();
                    gambiarra[0] = opt;
                    gambiarra[1] = opcaoUsuario;
                    validacao = true;
                    return gambiarra;
                }
                else if (opt == "F")
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("1. Listar filmes");
                    Console.WriteLine("2. Inserir novo filme");
                    Console.WriteLine("3. Atualizar filme");
                    Console.WriteLine("4. Excluir filme");
                    Console.WriteLine("5. Visualizar filmes");
                    Console.WriteLine("C. Limpar tela");
                    Console.WriteLine("X. Sair");
                    Console.WriteLine("------------------------------------------------------------");
                    string opcaoUsuario = Console.ReadLine().ToUpper();
                    gambiarra[0] = opt;
                    gambiarra[1] = opcaoUsuario;
                    validacao = true;
                    return gambiarra;
                }
                else
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("Digite novamente a opção desejada. s/f");
                    Console.WriteLine("------------------------------------------------------------");
                    opt = Console.ReadLine().ToUpper();
                    validacao = false;
                }
            }
            while (!validacao);
            
            Console.WriteLine();
            throw new Exception(message: "Deu ruim");
        }
    }
}
