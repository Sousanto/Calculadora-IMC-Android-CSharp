namespace CET95_T01_IMC_ANDRE
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        // 1. Dados
        // 1.1 Entradas
        EditText? etPesoC;
        EditText? etAlturaC;

        // 1.2 Sa�da
        TextView? tvIMCC;
        ImageView? imgViewC;

        // 2. Fun��es
        // 2.1 Fun��o "OnCreate()".

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            // 1. Obtem se existir a  �ltima instancia gravada.

            base.OnCreate(savedInstanceState);

            // 2. Define a interface da aplica��o.
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // 3. Liga��o dos Objetos C# aos Controlos XML
            //    de Entrada/Sa�da.

            etPesoC = FindViewById<EditText>(Resource.Id.etPeso);

            etAlturaC = FindViewById<EditText>(Resource.Id.etAltura);

            tvIMCC = FindViewById<TextView>(Resource.Id.tvIMC);

            imgViewC = FindViewById<ImageView>(Resource.Id.imgView);

            // 4. Obten��o os bot�es da Interface

            Button? btCalcularC =
                FindViewById<Button>(Resource.Id.btCalcular);
            Button? btLimparC =
                FindViewById<Button>(Resource.Id.btLimpar);

            // 5. Associar com o "delegate" as fun��es 
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
                        // 1. Obten��o do valor do Peso
                        peso = float.Parse(etPesoC.Text,
                            System.Globalization.CultureInfo
                                 .InvariantCulture.NumberFormat);
                        peso = System.Math.Abs(peso);
                        pesoOK = true;
                    }
                    catch (Exception)
                    {
                        mensagem += "N�o inseriu um valor num�rico de peso!\n";
                    }

                    try
                    {
                        // 2. Obten��o do valor da Altura
                        altura = float.Parse(etAlturaC.Text,
                            System.Globalization.CultureInfo
                                   .InvariantCulture.NumberFormat);
                        altura = System.Math.Abs(altura);
                        alturaOK = true;
                    }
                    catch (Exception)
                    {
                        mensagem += "N�o inseriu um valor num�rico de altura!\n";
                    }

                    // 3. C�lculo do IMC
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
                            mensagem += "Altura n�o pode ser nula!\n";
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
                        mensagem += "N�o inseriu o valor do peso!\n";
                    if (string.IsNullOrEmpty(etAlturaC.Text))
                        mensagem += "N�o inseriu o valor da altura!\n";
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


        } // Fim da fun��o.


        // 2.2 Fun��o "processaResultado()"
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
                resultado = "valor inv�lido";
                imagem = "imc0";
            }

            mensagem = "Resultado: " + resultado + ".";
            mostraImagem(imagem);
            return mensagem;
        } // Fim da fun��o.



        // 2.� Vers�o da fun��o - Android App  .NET 
        // Fun��o "mostraAlertaII()"
        private void MostraAlertaII(string mensagem)
        {
            // Declara��o do "builder" da caixa de di�logo do alerta.
            Android.App.AlertDialog.Builder builder =
                new Android.App.AlertDialog.Builder(this);

            // Defini��o das propriedades 
            builder.SetTitle("IMC-Resultado");
            builder.SetMessage(mensagem);
            builder.SetIcon(
                Android.Resource.Drawable.IcInputAdd);

            // Define o bot�o "OK"
            builder.SetPositiveButton("OK", (senderAlert, args) =>
            {
                // Quando clica no "OK", limpa as caixas.
                //etPesoC.Text = "";
                //etAlturaC.Text = "";
                //tvIMCC.Text = "IMC(Kg/m2)";

            });

            // Cria a caixa de di�logo que apresenta o alerta e exibe-a.
            Android.App.AlertDialog alerta = builder.Create();
            alerta.Show();

        }  // Fim da fun��o "mostraAlertaII()".

        // Fun��o "mostraImagem()"
        private void mostraImagem(string imagem)
        {
            int resImage =
               Resources.GetIdentifier(imagem,
               "drawable", PackageName);
            imgViewC.SetImageResource(resImage);


        }// Fim da fun��o "mostraImagem()".
    } // Fim da classe.
}// Fim do "namespace".