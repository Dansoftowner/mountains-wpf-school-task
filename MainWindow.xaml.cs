using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Györffy_Dániel_Hegyek_WPF_11A
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const string FilePath = "hegyekMo.txt";
        private const string Separator = ";";

        private readonly List<Mountain> mountains;

        public MainWindow()
        {
            InitializeComponent();
            mountains = InitializeFileContent();
            ExecuteTasks();
        }

        private List<Mountain> InitializeFileContent() =>
            File.ReadAllLines(FilePath)
            .Skip(1)
            .Select(it => it.Split(Separator))
            .Select(it => new Mountain(it[0], it[1], int.Parse(it[2])))
            .ToList();
        
        private void ExecuteTasks()
        {
            TaskOne();
            TaskTwo();
            TaskThree();
            TaskFour();
        }

        private void TaskOne()
        {
            WriteSimpleMessage(1, $"Az állományban szereplő hegycsúcsok száma: {mountains.Count}");
        }

        private void TaskTwo()
        {
            double averageHeight = mountains.Select(it => it.Height).Average();
            WriteSimpleMessage(2, $"A hegycsúcsok átlag magassága: {averageHeight} méter");
        }

        private void TaskThree()
        {
            Mountain highest = mountains.Aggregate((left, right) => left.Height > right.Height ? left : right);
            WriteSimpleMessage(3, $"A legmagasabb hegy: {highest.MountainTeritory}, magassága: {highest.Height}");
        }

        private void TaskFour()
        {
            ComboBox combo = new ComboBox();
            combo.Width = 150;
            combo.Height = 30;
            combo.ItemsSource = mountains.Select(it => it.MountainTeritory).Distinct();

            ListView listView = new ListView();
            listView.Width = 150;
            combo.SelectionChanged += new SelectionChangedEventHandler((s, args) => {
                listView.ItemsSource = mountains.Where(it => it.MountainTeritory.Equals(combo.SelectedItem)).Select(it => it.Name);
            });

            AddRow(GridLength.Auto, BuildTaskLabel(4), combo, BuildSimpleLabel("hegység hegycsúcsai:"), listView);
        }

        private void TaskFive()
        {
            TextBox input = new TextBox();
        }

        private void WriteSimpleMessage(int n, string content)
        {
            AddRow(BuildTaskLabel(n, content), new GridLength(40));
        }

        private Label BuildTaskLabel(int n, string content = "")
        {
            var label = BuildSimpleLabel($"{n}. feladat: {content}");
            label.FontSize = 15;
            return label;
        }

        private Label BuildSimpleLabel(string content)
        {
            var label = new Label();
            label.Content = content;
            return label;
        }

        private void AddRow(Control control, GridLength height)
        {
            control.SetValue(Grid.RowProperty, RootGrid.RowDefinitions.Count());

            var rowDefiniton = new RowDefinition();
            rowDefiniton.Height = height;

            RootGrid.RowDefinitions.Add(rowDefiniton);
            RootGrid.Children.Add(control);
        }

        private void AddRow(GridLength height, params Control[] controls)
        {
            int row = RootGrid.RowDefinitions.Count();
            var rowDefiniton = new RowDefinition();
            rowDefiniton.Height = height;
            RootGrid.RowDefinitions.Add(rowDefiniton);

            for (int i = 0; i < controls.Length; i++)
            {
                var colDefiniton = new ColumnDefinition();
                colDefiniton.Width = new GridLength(150);

                controls[i].SetValue(Grid.ColumnProperty, i);
                controls[i].SetValue(Grid.RowProperty, row);
                RootGrid.ColumnDefinitions.Add(colDefiniton);
                RootGrid.Children.Add(controls[i]);
            }

        }
    }
}
