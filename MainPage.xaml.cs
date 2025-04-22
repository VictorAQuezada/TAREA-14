using System;
using Xamarin.Forms;

namespace SistemaCalificacion
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos y sean números válidos
            if (string.IsNullOrWhiteSpace(AsistenciaEntry.Text) ||
                string.IsNullOrWhiteSpace(TrabajoPracticoEntry.Text) ||
                string.IsNullOrWhiteSpace(ExamenParcialEntry.Text) ||
                string.IsNullOrWhiteSpace(ExamenFinalEntry.Text))
            {
                // Mostrar un mensaje de error si algún campo está vacío
                DisplayAlert("Error", "Por favor, ingrese todas las notas.", "Aceptar");
                return; // Salir del evento para evitar cálculos incorrectos
            }

            // Convertir los valores ingresados en cada campo de entrada a números (asistencia, trabajoPractico, examenParcial y examenFinal)
            if (!double.TryParse(AsistenciaEntry.Text, out double asistencia) ||
                !double.TryParse(TrabajoPracticoEntry.Text, out double trabajoPractico) ||
                !double.TryParse(ExamenParcialEntry.Text, out double examenParcial) ||
                !double.TryParse(ExamenFinalEntry.Text, out double examenFinal))
            {
                // Mostrar un mensaje de error si algún campo no es un número válido
                DisplayAlert("Error", "Por favor, ingrese números válidos en todas las notas.", "Aceptar");
                return; // Salir del evento para evitar cálculos incorrectos
            }

            // Validar que las notas no sean negativas
            if (asistencia < 0 || trabajoPractico < 0 || examenParcial < 0 || examenFinal < 0)
            {
                // Mostrar un mensaje de error si alguna nota es negativa
                DisplayAlert("Error", "Por favor, ingrese notas válidas (no negativas).", "Aceptar");
                return; // Salir del evento para evitar cálculos incorrectos
            }

            // Validar que las notas no sean mayores que los máximos permitidos
            const double notaMaximaAsistencia = 10;
            const double notaMaximaTrabajoPractico = 20;
            const double notaMaximaExamenParcial = 20;
            const double notaMaximaExamenFinal = 50;

            if (asistencia > notaMaximaAsistencia || trabajoPractico > notaMaximaTrabajoPractico ||
                examenParcial > notaMaximaExamenParcial || examenFinal > notaMaximaExamenFinal)
            {
                // Mostrar un mensaje de error si alguna nota es mayor que el límite máximo
                DisplayAlert("Error", "Por favor, ingrese notas válidas (no mayores que los máximos permitidos).", "Aceptar");
                return; // Salir del evento para evitar cálculos incorrectos
            }

            // Calcular la nota final sumando las notas de asistencia, trabajoPractico, examenParcial y examenFinal
            double notaFinal = asistencia + trabajoPractico + examenParcial + examenFinal;

            // Asegurar que la nota final no sea mayor que 100
            if (notaFinal > 100)
                notaFinal = 100;

            // Asignar la nota final a la etiqueta correspondiente para mostrarla en la interfaz gráfica
            NotaFinalLabel.Text = notaFinal.ToString();

            // Asignar la equivalencia en letra a la etiqueta correspondiente basándose en la nota final
            if (notaFinal >= 90)
                EquivalenciaLabel.Text = "A";
            else if (notaFinal >= 80)
                EquivalenciaLabel.Text = "B";
            else if (notaFinal >= 75)
                EquivalenciaLabel.Text = "C";
            else if (notaFinal >= 70)
                EquivalenciaLabel.Text = "D";
            else if (notaFinal >= 60)
                EquivalenciaLabel.Text = "FE";
            else if (notaFinal >= 50)
                EquivalenciaLabel.Text = "FI";
            else
                EquivalenciaLabel.Text = "F";

            // Nuevo Label para mostrar "APROBADO" o "DESAPROBADO" y cambiar el color de fondo
            resultadoLabel.FontSize = 24;
            resultadoLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
            if (notaFinal >= 70)
            {
                resultadoLabel.Text = "🆗 APROBADO";
                resultadoLabel.TextColor = Color.Green;
            }
            else
            {
                resultadoLabel.Text = "⛔ REPROBADO";
                resultadoLabel.TextColor = Color.Red;
            }
        }
    }
}
