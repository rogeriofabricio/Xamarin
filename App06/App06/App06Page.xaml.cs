using Xamarin.Forms;
using App06.Modelo;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Realms;
using System.Linq;

namespace App06
{
    public partial class App06Page : ContentPage
    {
        public App06Page()
        {
            InitializeComponent();

            btnAdicionar.Clicked += delegate {

                //Validar e Salvar
                Produto produto = new Produto();
                produto.Item = Item.Text;
                produto.Quantidade = int.Parse(Quantidade.Text);

                //Validação
                List<ValidationResult> listRes = new List<ValidationResult>();
                var contexto = new ValidationContext(produto);
                bool valido = Validator.TryValidateObject(produto, contexto, listRes, true);

                if (valido == false)
                {
                    string mensagem = string.Empty;

                    foreach (var res in listRes)
                    {
                        mensagem += res.ErrorMessage + "\n";
                    }

                    DisplayAlert("Erros", mensagem, "OK");

                } else {

                    //Salvar
                    var realm = Realm.GetInstance();

                    realm.Write(() => {
                        realm.Add(produto);
                    });

                    DisplayAlert("Salvo com sucesso!", "Itens no banco de dados: " + realm.All<Produto>().Count(), "OK");
                }
            };
        }
    }
}
