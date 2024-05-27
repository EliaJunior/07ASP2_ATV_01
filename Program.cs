using System;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

namespace Atividade
{
    class Program
    {
        static void Main(string[] args)
        {
            // Acordar o usuário
            Console.Beep();

            // Solicitando o nome do cliente
            Console.Write("Informe o nome do cliente a ser cadastrado: ");
            string nome_cliente = Console.ReadLine();
            Console.Beep();

            // Solicitando o endereço do cliente
            Console.Write("Informe o endereço do cliente: ");
            string endereco_cliente = Console.ReadLine();
            Console.Beep();

            // Tipo de pessoa - Verificar entrada de usuário
            bool verificar_tipo = false;
            string tipo_cliente = "";
            while (!verificar_tipo)
            {
                Console.Write("Selecione o tipo de pessoa - PF para Pessoa Física ou PJ para Pessoa Jurídica: ");
                tipo_cliente = Console.ReadLine();
                Console.Beep();
                if (tipo_cliente != "PF" && tipo_cliente != "PJ")
                {
                    Console.WriteLine($"{tipo_cliente} não é uma opção válida, por favor, selecione PF ou PJ");
                }
                else
                {
                    verificar_tipo = true;
                }
            }

            // Solicitando CPF/CNPJ do cliente
            string cpf_cnpj = "";
            string ie_rg = "";
            float valor_compra = 0;
            float valor_imposto = 0;
            float valor_total;
            string tipoP = "";
            Clientes pessoa = null;

            if (tipo_cliente == "PF")
            {
                // Cria pessoa fisica
                tipoP = "Pessoa Física";
                Console.Write("Por favor, informe o CPF do cliente: ");
                cpf_cnpj = Console.ReadLine();
                Console.Beep();
                Console.Write("Por favor, informe o RG do cliente: ");
                ie_rg = Console.ReadLine();
                Console.Write("Informe o valor da compra: ");
                Console.Beep();
                valor_compra = float.Parse(Console.ReadLine());
                Console.Beep();

                pessoa = new Pessoa_Fisica();
                ((Pessoa_Fisica)pessoa).rg = ie_rg;
                ((Pessoa_Fisica)pessoa).cpf = cpf_cnpj;
                ((Pessoa_Fisica)pessoa).nome = nome_cliente;
                ((Pessoa_Fisica)pessoa).endereco = endereco_cliente;
                ((Pessoa_Fisica)pessoa).Pagar_Imposto(valor_compra);
                valor_imposto = ((Pessoa_Fisica)pessoa).valor_imposto;

            }
            else
            {
                // Cria pessoa jurídica
                tipoP = "Pessoa Jurídica";
                Console.Write("Por favor, informe o CNPJ do cliente: ");
                cpf_cnpj = Console.ReadLine();
                Console.Beep();
                Console.Write("Por favor, informe o IE do cliente: ");
                ie_rg = Console.ReadLine();
                Console.Beep();
                Console.Write("Informe o valor da compra: ");
                valor_compra = float.Parse(Console.ReadLine());
                Console.Beep();

                pessoa = new Pessoa_Juridica();
                ((Pessoa_Juridica)pessoa).ie = ie_rg;
                ((Pessoa_Juridica)pessoa).cnpj = cpf_cnpj;
                ((Pessoa_Juridica)pessoa).nome = nome_cliente;
                ((Pessoa_Juridica)pessoa).endereco = endereco_cliente;
                ((Pessoa_Juridica)pessoa).Pagar_Imposto(valor_compra);
                valor_imposto = ((Pessoa_Juridica)pessoa).valor_imposto;
            }

            // Limpar o console
            Console.Clear();

            // Valor total
            valor_total = pessoa.total;

            // Informações de saída (console)
            Console.WriteLine($"------------------------------------{tipoP}------------------------------------");
            Console.WriteLine($"Nome.........................: {pessoa.nome}");
            Console.WriteLine($"Endereço.....................: {pessoa.endereco}");
            Console.WriteLine($"CPF/CNPJ.....................: {cpf_cnpj}");
            Console.WriteLine($"RG/IE........................: {ie_rg}");
            Console.WriteLine($"Valor de compra..............: {pessoa.valor.ToString("C")}");
            Console.WriteLine($"Imposto......................: {pessoa.valor_imposto.ToString("C")}");
            Console.WriteLine($"Total a pagar................: {pessoa.total.ToString("C")}");

            // Escrevendo valores no banco de dados CSV
            string[] informacoes_cliente =
                        {
                        nome_cliente, endereco_cliente, tipo_cliente,
                        cpf_cnpj, ie_rg,valor_compra.ToString(),
                        valor_imposto.ToString(), valor_total.ToString()
                        };

            string info_dados = string.Join(";", informacoes_cliente);
            string caminhoCSV = "dados.csv";

            using (StreamWriter sw = new StreamWriter(caminhoCSV, append: true))
            {
                sw.WriteLine(info_dados);
            }

            // Mensgem de finalização
            Console.WriteLine("*************************************************************************************");
            Console.WriteLine("Os dados foram salvos em 'dados.csv'");
            Console.WriteLine("Finalizando Sistema...");
            Console.WriteLine("*************************************************************************************");
            Console.Beep();

        }
    }
}