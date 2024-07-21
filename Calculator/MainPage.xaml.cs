using CalcCore;
namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            _calc = new Calc();
            InitializeComponent();
        }

        private readonly Calc _calc;

        private void Button_Pressed(object sender, EventArgs e)
        {
            var key = ((Button)sender).Text[0];

            _calc.Input(key);

            Display.Text = _calc.Display.ToString();
            preDisplay.Text = _calc.preDisplay.ToString();
        }
    }
}
