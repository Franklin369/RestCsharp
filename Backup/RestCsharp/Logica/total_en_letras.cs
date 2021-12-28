using RestCsharp.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestCsharp.Logica
{
    class total_en_letras
    {
    
        public static string Num2Text(double value)
        {
            Bases.Cambiar_idioma_regional();
            string tempNum2Text = null;
            try
            {


                if (value == 0)
                {
                    tempNum2Text = "CERO";
                }
                else if (value == 1)
                {
                    tempNum2Text = "UN";
                }
                else if (value == 2)
                {
                    tempNum2Text = "DOS";
                }
                else if (value == 3)
                {
                    tempNum2Text = "TRES";
                }
                else if (value == 4)
                {
                    tempNum2Text = "CUATRO";
                }
                else if (value == 5)
                {
                    tempNum2Text = "CINCO";
                }
                else if (value == 6)
                {
                    tempNum2Text = "SEIS";
                }
                else if (value == 7)
                {
                    tempNum2Text = "SIETE";
                }
                else if (value == 8)
                {
                    tempNum2Text = "OCHO";
                }
                else if (value == 9)
                {
                    tempNum2Text = "NUEVE";
                }
                else if (value == 10)
                {
                    tempNum2Text = "DIEZ";
                }
                else if (value == 11)
                {
                    tempNum2Text = "ONCE";
                }
                else if (value == 12)
                {
                    tempNum2Text = "DOCE";
                }
                else if (value == 13)
                {
                    tempNum2Text = "TRECE";
                }
                else if (value == 14)
                {
                    tempNum2Text = "CATORCE";
                }
                else if (value == 15)
                {
                    tempNum2Text = "QUINCE";
                }
                else if (value < 20)
                {
                    tempNum2Text = "DIECI" + Num2Text(value - 10);
                }
                else if (value == 20)
                {
                    tempNum2Text = "VEINTE";
                }
                else if (value < 30)
                {
                    tempNum2Text = "VEINTI" + Num2Text(value - 20);
                }
                else if (value == 30)
                {
                    tempNum2Text = "TREINTA";
                }
                else if (value == 40)
                {
                    tempNum2Text = "CUARENTA";
                }
                else if (value == 50)
                {
                    tempNum2Text = "CINCUENTA";
                }
                else if (value == 60)
                {
                    tempNum2Text = "SESENTA";
                }
                else if (value == 70)
                {
                    tempNum2Text = "SETENTA";
                }
                else if (value == 80)
                {
                    tempNum2Text = "OCHENTA";
                }
                else if (value == 90)
                {
                    tempNum2Text = "NOVENTA";
                }
                else if (value < 100)
                {
                    tempNum2Text = Num2Text(Convert.ToInt32(Math.Floor((double)(Convert.ToInt32(value) / 10))) * 10) + " Y " + Num2Text(value % 10);
                }
                else if (value == 100)
                {
                    tempNum2Text = "CIEN";
                }
                else if (value < 200)
                {
                    tempNum2Text = "CIENTO " + Num2Text(value - 100);
                }
                else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800))
                {
                    tempNum2Text = Num2Text(Convert.ToInt32(Math.Floor((double)(Convert.ToInt32(value) / 100)))) + "CIENTOS";
                }
                else if (value == 500)
                {
                    tempNum2Text = "QUINIENTOS";
                }
                else if (value == 700)
                {
                    tempNum2Text = "SETECIENTOS";
                }
                else if (value == 900)
                {
                    tempNum2Text = "NOVECIENTOS";
                }
                else if (value < 1000)
                {
                    tempNum2Text = Num2Text(Convert.ToInt32(Math.Floor((double)(Convert.ToInt32(value) / 100))) * 100) + " " + Num2Text(value % 100);
                }
                else if (value == 1000)
                {
                    tempNum2Text = "MIL";
                }
                else if (value < 2000)
                {
                    tempNum2Text = "MIL " + Num2Text(value % 1000);
                }
                else if (value < 1000000)
                {
                    tempNum2Text = Num2Text(Convert.ToInt32(Math.Floor((double)(Convert.ToInt32(value) / 1000)))) + " MIL";
                    if ((value % 1000) != 0)
                    {
                        tempNum2Text = tempNum2Text + " " + Num2Text(value % 1000);
                    }
                }
                else if (value == 1000000)
                {
                    tempNum2Text = "UN MILLON";
                }
                else if (value < 2000000)
                {
                    tempNum2Text = "UN MILLON " + Num2Text(value % 1000000);
                }
                else if (value < 1000000000000.0D)
                {
                    tempNum2Text = Num2Text(Math.Floor(value / 1000000)) + " MILLONES ";
                    if ((value - Math.Floor(value / 1000000) * 1000000) != 0)
                    {
                        tempNum2Text = tempNum2Text + " " + Num2Text(value - Math.Floor(value / 1000000) * 1000000);
                    }
                }

                else if (value == 1000000000000.0D)
                {
                    tempNum2Text = "UN BILLON";
                }

                else if (value < 2000000000000.0D)
                {
                    tempNum2Text = "UN BILLON " + Num2Text(value - Math.Floor(value / 1000000000000.0D) * 1000000000000.0D);
                }

                else
                {
                    tempNum2Text = Num2Text(Math.Floor(value / 1000000000000.0D)) + " BILLONES";
                    if ((value - Math.Floor(value / 1000000000000.0D) * 1000000000000.0D) != 0)
                    {
                        tempNum2Text = tempNum2Text + " " + Num2Text(value - Math.Floor(value / 1000000000000.0D) * 1000000000000.0D);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return tempNum2Text;
        }
    }
}
