using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace lab5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            colors[0] = 0x909090;
            colors[1] = 0x099090; 
            colors[2] = 0x099009;
            colors[3] = 0x999000;
            colors[4] = 0x065090;
            colors[5] = 0x099090;
            colors[6] = 0x099054;
            colors[7] = 0x099090;
            colors[8] = 0x09c390;
            colors[9] = 0x909090;
            colors[10] = 0x099090;
            colors[11] = 0x099090;
            
        }

        class MethodsNames : ObservableCollection<string>
        {
            public MethodsNames()         //названия методов для списка, порядок сохраняется
            {
                Add("Эйлера явн.");
                Add("Эйлера с пересч.");
                Add("Коши");
                Add("РК 4");
                Add("Эйлера неявн.");
                Add("Тейлора 2");
                Add("Тейлора 3");
                Add("Тейлора 4");
                Add("Трапеций");
                Add("Адамса 2ш явн.");
                Add("Адамса 3ш явн.");
                Add("Симпсона");  
            }
        }

        private void comboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ((ComboBox)sender).ItemsSource = new MethodsNames();
        }

        Dictionary<int, int> colors = new Dictionary<int, int>();

        private void button_Click(object sender, RoutedEventArgs e)
        {
            double x0 = Double.Parse(begin.Text);
            FunctionsAndParsing.CommonFunction resolObj = FunctionsAndParsing.Parser.ParseExpressionObject(resolution.Text, new string[] { "x" });
            Func<double, double> resol = x => resolObj.getCommonFunction()(new double[] { x });
            if(Plot.graphics.functions.Count==0)
            Plot.addFunction(new PlotView.FunctionAppearance(resol, 0x000000, x0, x0 + 1, 2, 0x00ff), "точн.");
            int selected = method.SelectedIndex;    //индекс выбранного метода из выпадающего списка
            double h = Double.Parse(step.Text);
            double y0 = Double.Parse(beginValue.Text);
            FunctionsAndParsing.CommonFunction fObj = FunctionsAndParsing.Parser.ParseExpressionObject(fT.Text, new string[] { "x", "y" });
            Func<double, double, double> f = (x, y) => fObj.getCommonFunction()(new double[] { x, y });
            List<ValueAndArgument> net = null;
            Func<double, ValueAndArgument, Func<double, double, double>, ValueAndArgument> methodDelegate = null;
            switch (selected) //выбор метода по его индексу
            {
                case 0:
                    methodDelegate = Methods.EulerExplicit.nextStep;
                    break;
                case 1:
                    methodDelegate = Methods.EulerWithRecalculation.nextStep;
                    break;
                case 2:
                    //TODO здесь и далее необходимо добавить ссылки на другие методы таким же образом, как и в двух предыдущих случаях
                    break;
            }

            if (methodDelegate != null)
            {
                DateTime t1 = DateTime.Now;
                net = KoshiTask.integrateDifferencialEquation(h, x0, x0 + 1, y0, f, methodDelegate);
                DateTime t2 = DateTime.Now;
                double dt=(t2 - t1).TotalMilliseconds;
                time.Content = "" + dt;
                Plot.addFunction(new PlotView.FunctionAppearance(Interpolate.interpolate(net), colors[selected], x0, x0 + 1, 2, 0xff00), "прибл., метод "+(selected+1));
                
            }
            table.ItemsSource = net;
            net = null;
        }
    }
}
