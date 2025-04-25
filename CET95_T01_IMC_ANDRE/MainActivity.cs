namespace CET95_T01_IMC_ANDRE
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        // 1. Dados
        // 1.1 Entradas
        EditText? etPesoC;
        EditText? etAlturaC;

        // 1.2 Saída
        TextView? tvIMCC;
        ImageView? imgViewC;

        // 2. Funções
        // 2.1 Função "OnCreate()".

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            // 1. Obtem se existir a  última instancia gravada.

            base.OnCreate(savedInstanceState);

            // 2. Define a interface da aplicação.
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // 3. Ligação dos Objetos C# aos Controlos XML
            //    de Entrada/Saída.

            etPesoC = FindViewById<EditText>(Resource.Id.etPeso);

            etAlturaC = FindViewById<EditText>(Resource.Id.etAltura);

            tvIMCC = FindViewById<TextView>(Resource.Id.tvIMC);

            imgViewC = FindViewById<ImageView>(Resource.Id.imgView);

            // 4. Obtenção os botões da Interface

            Button? btCalcularC =
                FindViewById<Button>(Resource.Id.btCalcular);
            Button? btLimparC =
                FindViewById<Button>(Resource.Id.btLimpar);

            // 5. Associar com o "delegate" as funções 
            //    "event-handler"


            btCalcularC.Click += delegate
            {
                string mensagem = "";



                if (!string.IsNullOrEmpty(etPesoC.Text) &&
                    !string.IsNullOrEmpty(etAlturaC.Text))
                {
                    bool pesoOK = false, alturaOK = false;
                    float peso = 0, altura = 0, imc = 0;

                    try
                    {
                        // 1. Obtenção do valor do Peso
                        peso = float.Parse(etPesoC.Text,
                            System.Globalization.CultureInfo
                                 .InvariantCulture.NumberFormat);
                        peso = System.Math.Abs(peso);
                        pesoOK = true;
                    }
                    catch (Exception)
                    {
                        mensagem += "Não inseriu um valor numérico de peso!\n";
                    }

                    try
                    {
                        // 2. Obtenção do valor da Altura
                        altura = float.Parse(etAlturaC.Text,
                            System.Globalization.CultureInfo
                                   .InvariantCulture.NumberFormat);
                        altura = System.Math.Abs(altura);
                        alturaOK = true;
                    }
                    catch (Exception)
                    {
                        mensagem += "Não inseriu um valor numérico de altura!\n";
                    }

                    // 3. Cálculo do IMC
                    if (pesoOK && alturaOK)
                    {
                        if (altura != 0)
                        {
                            imc = peso / (float)System.Math.Pow(altura, 2);
                            tvIMCC.Text = imc.ToString("0.00");
                            mensagem = ProcessaResultado(imc);
                        }
                        else
                        {
                            mensagem += "Altura não pode ser nula!\n";
                            tvIMCC.Text = "Erro!";
                        }
                    }  // if (pesoOK && alturaOK)
                    else
                    {
                        tvIMCC.Text = "Erro!";

                    }
                }  // if (!string.IsNullOrEmpty(etPesoC.Text) &&
                   //    !string.IsNullOrEmpty(etAlturaC.Text))
                else
                {
                    if (string.IsNullOrEmpty(etPesoC.Text))
                        mensagem += "Não inseriu o valor do peso!\n";
                    if (string.IsNullOrEmpty(etAlturaC.Text))
                        mensagem += "Não inseriu o valor da altura!\n";
                    tvIMCC.Text = "Erro!";
                }

                MostraAlertaII(mensagem);

            };

            btLimparC.Click += delegate
            {
                etPesoC.Text = "";
                etAlturaC.Text = "";
                tvIMCC.Text = "IMC (Kg/m2)";
                mostraImagem("imc0");
            };


        } // Fim da função.


        // 2.2 Função "processaResultado()"
        private string ProcessaResultado(float imc)
        {
            string mensagem = "";
            string resultado = "";
            string imagem = "";

            if (imc > 0 && imc < 16.9)
            {
                resultado = "peso muito baixo";
                imagem = "imc1";
            }
            else if (imc < 18.5)
            {
                resultado = "peso baixo";
                imagem = "imc1";
            }
            else if (imc < 25)
            {
                resultado = "peso normal";
                imagem = "imc2";
            }
            else if (imc < 30)
            {
                resultado = "acima do peso normal";
                imagem = "imc3";
            }
            else if (imc < 35)
            {
                resultado = "obesidade de grau I";
                imagem = "imc4";
            }
            else if (imc <= 40)
            {
                resultado = "obesidade de grau II";
                imagem = "imc5";
            }
            else if (imc > 40)
            {
                resultado = "obesidade de grau III";
                imagem = "imc5";
            }
            else
            {
                resultado = "valor inválido";
                imagem = "imc0";
            }

            mensagem = "Resultado: " + resultado + ".";
            mostraImagem(imagem);
            return mensagem;
        } // Fim da função.



        // 2.ª Versão da função - Android App  .NET 
        // Função "mostraAlertaII()"
        private void MostraAlertaII(string mensagem)
        {
            // Declaração do "builder" da caixa de diálogo do alerta.
            Android.App.AlertDialog.Builder builder =
                new Android.App.AlertDialog.Builder(this);

            // Definição das propriedades 
            builder.SetTitle("IMC-Resultado");
            builder.SetMessage(mensagem);
            builder.SetIcon(
                Android.Resource.Drawable.IcInputAdd);

            // Define o botão "OK"
            builder.SetPositiveButton("OK", (senderAlert, args) =>
            {
                // Quando clica no "OK", limpa as caixas.
                //etPesoC.Text = "";
                //etAlturaC.Text = "";
                //tvIMCC.Text = "IMC(Kg/m2)";

            });

            // Cria a caixa de diálogo que apresenta o alerta e exibe-a.
            Android.App.AlertDialog alerta = builder.Create();
            alerta.Show();

        }  // Fim da função "mostraAlertaII()".

        // Função "mostraImagem()"
        private void mostraImagem(string imagem)
        {
            int resImage =
               Resources.GetIdentifier(imagem,
               "drawable", PackageName);
            imgViewC.SetImageResource(resImage);


        }// Fim da função "mostraImagem()".
    } // Fim da classe.
}// Fim do "namespace".