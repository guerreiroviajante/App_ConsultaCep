using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App_ConsultaCep.Servico.Modelo;
using App_ConsultaCep.Servico;

namespace App_ConsultaCep
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            cBotao.Clicked += BuscarCEP;
        }
        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = cCep.Text.Trim();
            if(isValidaCEP(cep))
            {
                try
                {
                    Endereco end = ViaCepServico.BuscarEnderecoViaCep(cep);

                    if(end != null)
                    {
                        cResultado.Text = string.Format("Endereço:" + "{2} de {3} {0},{1}", end.localidade, end.uf, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("Erro", "O endereço não foi encontrad" + "para o CEP informado" + cep, "Ok");
                    }
                }
                catch(Exception e)
                {
                    DisplayAlert("Erro Crítico", e.Message, "Ok");
                }
            }
        }
        private bool isValidaCEP(string cep)
        {
            bool valido = true;
            if(cep.Length != 8)
            {
                DisplayAlert("Erro!", "Cep Inválido ou menor que 8 digitos", "Ok");
                valido = false;
            }
            int NovoCep = 0;
            if(!int.TryParse(cep, out NovoCep))
            {
                DisplayAlert("Erro!", "O cep deve ser Composto apenas por números", "Ok");
                valido = false;
            }
            return valido;
        }
    }
}
