using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;//importar o MySQL.

namespace PessoaTI20N
{
    class DAO
    {
        public MySqlConnection conexao;
        public long[] cpf;
        public string[] nome;
        public string[] telefone;
        public string[] endereco;
        public int i;
        public int contador;

        public DAO() 
        {
            conexao = new MySqlConnection("servcer=localhost;Database=empresaTI20N;Uid=root;password=");
            try
            {
                conexao.Open();//Abrir a conexão
                MessageBox.Show("Conectado!");
            }
            catch(Exception ex)

            {
                MessageBox.Show("Algo deu errado !!\n\n" + ex);
            }
        }//Fim do construtor

        public string Inserir(long cpf, string nome, string telefone, string endereco)
        {
            string inserir = $"Insert into pessoa(cpf, nome, telefone, endereco) values" +
                $"('{cpf}','{nome}','{telefone}','{endereco}')";

            MySqlCommand sql = new MySqlCommand(inserir, conexao);
            string resultado = sql.ExecuteNonQuery() + " Executado!";
            return resultado;
        }//Fim do método

        public void PreencherVetor()
        {
            string query = "select * from pessoa";

            //Instanciar
            this.cpf = new long[100];
            this.nome = new string[100];
            this.telefone = new string[100];
            this.endereco = new string[100];

            // Fazer o comando deSeleçao do banco
            MySqlCommand sql = new MySqlCommand(query, conexao);
            //Leitor do Banco
            MySqlDataReader leitura = sql.ExecuteReader();

            i = 0;
            contador = 0;
            while(leitura.Read())
            {
                cpf[i]       = Convert.ToInt64(leitura["cpf"]);
                nome[i]      = leitura["nome"] + "";
                telefone[i]  = leitura["telefone"] + "";
                endereco[i] = leitura["endereco"] + "";
                i++;//Preencher o vetor
                contador++;//Contar quantos dados eu tenho
            }//Fim do While

            //Encerro a comunicação com o software
            leitura.Close();
        }//Fim do preencher

        //criar o metodo para retornar o contaror

        public int QuantidadedeDados()
        {
            return contador;
        }//Fim do quantidade de dados

        public string Atualizar(long CPF, string nomeTabela, string campo, string dado )
        {
            string query =$"updade {nomeTabela} set {campo} = '{dado}' where cpf = '{cpf}'";
            MySqlCommand sql = new MySqlCommand(query, conexao);
            string resultado = sql.ExecuteNonQuery() + "Atualizado!";
            return resultado;
        }//Fim do metodo

        public string Excluir(long cpf, string nomeTabela)
        {
            string query = $"delete from {nomeTabela}; where CPF = '{cpf}'";
            MySqlCommand sql = new MySqlCommand(query, conexao);
            string resultado = sql.ExecuteNonQuery() + "Excluído!";
            return resultado;
        }//Fim do Excluir
    }//Fim da Classe
}//Fim do Projeto
