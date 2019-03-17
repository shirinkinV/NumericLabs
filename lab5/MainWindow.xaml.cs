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
using Symbolic.Functions;
using Symbolic;
using GraphicsPlot;

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
            colors[0] = 0x2828E6;
            colors[1] = 0x29BAE4; 
            colors[2] = 0x29E6BA;
            colors[3] = 0x29E629;
            colors[4] = 0xBAE629;
            colors[5] = 0xE6BA29;
            colors[6] = 0xE629BA;
            colors[7] = 0xBA29E6;
            colors[8] = 0x292929;
            colors[9] = 0xBABABA;
            colors[10] = 0xE6E6E6;
            colors[11] = 0xff00ff;
            
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

        class AccelNames : ObservableCollection<string>
        {
            public AccelNames()         //названия методов для списка, порядок сохраняется
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
            CommonF solveObj = Parser.ParseExpressionObject(solve.Text, new string[] { "x" });
            Func<double, double> resol = x => solveObj.Invoke(new double[] { x });
            if(Plot.graphics.functions.Count==0)
            Plot.addFunction(new FunctionAppearance(resol, 0x000000, x0, x0 + 1, 2, 0x00ff), "точн.");
            int selected = method.SelectedIndex;    //индекс выбранного метода из выпадающего списка
            double h = Double.Parse(step.Text);
            double y0 = Double.Parse(beginValue.Text);
            CommonF fObj = Parser.ParseExpressionObject(fT.Text, new string[] { "x", "y" });
            Func<double, double, double> f = (x, y) => fObj.Invoke(new double[] { x, y });
            List<ValueAndArgument> net = null;
            Func<double, ValueAndArgument, Func<double, double, double>, ValueAndArgument> methodOneStepDelegate = null;
            Func<double, List<ValueAndArgument>, Func<double, double, double>, ValueAndArgument> methodMoreStepsDelegate = null;

            switch (selected) //выбор метода по его индексу
            {
                case 0:
                    methodOneStepDelegate = Methods.EulerExplicit.nextStep;
                    break;
                case 1:
                    methodOneStepDelegate = Methods.EulerWithRecalculation.nextStep;
                    break;
                case 2:
                    methodOneStepDelegate = Methods.Koshi.nextStep;
                    break;
                case 3:
                    methodOneStepDelegate = Methods.RK4.nextStep;
                    break;
                case 11:
                    methodMoreStepsDelegate = Methods.Simpson.nextStep;
                    break;
                    //TODO здесь необходимо добавить ссылки на другие методы таким же образом, как и в двух предыдущих случаях

            }

            if (methodOneStepDelegate != null)
            {
                DateTime t1 = DateTime.Now;
                net = KoshiTask.integrateDifferencialEquation(h, x0, x0 + 1, y0, f, methodOneStepDelegate);
                DateTime t2 = DateTime.Now;
                double dt=(t2 - t1).TotalMilliseconds;
                time.Content = "" + dt;
                Plot.addFunction(new FunctionAppearance(Interpolate.interpolate(net), colors[selected], x0, x0 + 1, 2, 0xff00), "прибл., метод "+(selected+1));             
            }
            if (methodMoreStepsDelegate != null)
            {
                methodOneStepDelegate = selectAccel();
                if (methodOneStepDelegate == null)
                {
                    methodOneStepDelegate = Methods.EulerExplicit.nextStep;
                }
                DateTime t1 = DateTime.Now;
                net = KoshiTask.integrateDifferencialEquation(2, h, x0, x0 + 1, y0, f, methodMoreStepsDelegate, methodOneStepDelegate);
                DateTime t2 = DateTime.Now;
                double dt = (t2 - t1).TotalMilliseconds;
                time.Content = "" + dt;
                Plot.addFunction(new FunctionAppearance(Interpolate.interpolate(net), colors[selected], x0, x0 + 1, 2, 0xff00), "прибл., метод " + (selected + 1));
            }
            table.ItemsSource = net;
            net = null;
        }

        private Func<double, ValueAndArgument, Func<double, double, double>, ValueAndArgument> selectAccel()
        {
            switch (accelBox.SelectedIndex)
            {
                case 0: return Methods.EulerExplicit.nextStep;
                case 1: return Methods.EulerWithRecalculation.nextStep;
                case 2: return Methods.Koshi.nextStep;
                case 3: return Methods.RK4.nextStep;    
                     
                    //TODO
                default:return null;
            }
        }

        private void accelBox_Loaded(object sender, RoutedEventArgs e)
        {
            ((ComboBox)sender).ItemsSource = new AccelNames();
        }

        private void method_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex >= 9)
            {
                accelBox.IsEnabled = true;
            }
            else
            {
                accelBox.IsEnabled = false;
            }
        }
    }
}
