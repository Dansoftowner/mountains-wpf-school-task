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

        private const double CanvasLeft = 10;
        private const double CanvasTop = 5;
        private const double Spacing = 40;

        private readonly Mountains Mountains = new Mountains(FilePath);

        public MainWindow()
        {
            InitializeComponent();
            ExecuteTasks();
        }

        private void ExecuteTasks()
        {
            TaskOne();
            TaskTwo();
            TaskThree();
            TaskFour();
            TaskFive();
        }

        private void TaskOne()
        {
            var label = BuildTaskLabel(1, $"Az állományban szereplő hegycsúcsok száma: {Mountains.MountainsCount}");
            RootCanvas.Children.Add(label);
        }

        private void TaskTwo()
        {
            var label = BuildTaskLabel(2, $"A hegycsúcsok átlag magassága: {Mountains.AverageHeight} méter");
            RootCanvas.Children.Add(label);
        }

        private void TaskThree()
        {
            Mountain highest = Mountains.HighestMountain;
            var label = BuildTaskLabel(3, $"A legmagasabb hegy: {highest.MountainTeritory}, magassága: {highest.Height}");
            RootCanvas.Children.Add(label);
        }

        private void TaskFour()
        {
            var taskLabel = BuildTaskLabel(4);

            ComboBox combo = new ComboBox();
            combo.Width = 150;
            combo.Height = 30;
            combo.ItemsSource = Mountains.DistinctMountainTerritories;
            Canvas.SetLeft(combo, taskLabel.ActualWidth + CanvasLeft + 80);
            Canvas.SetTop(combo, Canvas.GetTop(taskLabel));

            var labelTwo = BuildSimpleLabel("hegység hegycsúcsai:");
            Canvas.SetLeft(labelTwo, CanvasLeft + taskLabel.ActualWidth + combo.ActualWidth + 250);
            Canvas.SetTop(labelTwo, Canvas.GetTop(taskLabel));

            ListView listView = new ListView();
            listView.Width = 150;
            listView.Height = 130;
            combo.SelectionChanged += new SelectionChangedEventHandler((s, args) => {
                listView.ItemsSource = Mountains.GetByTerritory((string)combo.SelectedItem).Select(it => it.Name);
            });
            Canvas.SetLeft(listView, CanvasLeft + taskLabel.ActualWidth + combo.ActualWidth + labelTwo.ActualWidth + 400);
            Canvas.SetTop(listView, Canvas.GetTop(taskLabel));

            RootCanvas.Children.Add(taskLabel);
            RootCanvas.Children.Add(combo);
            RootCanvas.Children.Add(labelTwo);
            RootCanvas.Children.Add(listView);
        }

        private void TaskFive()
        {
            var taskLabel = BuildTaskLabel(5);
            Canvas.SetTop(taskLabel, Canvas.GetTop(taskLabel) + 100);

            var labelTwo = BuildSimpleLabel("méter feletti hegycsúcsok száma:");
            Canvas.SetTop(labelTwo, Canvas.GetTop(taskLabel));
            Canvas.SetLeft(labelTwo, CanvasLeft + 200);

            var labelThree = new Label();
            Canvas.SetTop(labelThree, Canvas.GetTop(taskLabel));
            Canvas.SetLeft(labelThree, CanvasLeft + 400);

            TextBox input = new TextBox();
            input.Width = 50;
            input.Height = 30;
            input.TextChanged += new TextChangedEventHandler((s, args) => {
                try
                {
                    labelThree.Content = Mountains.GetHigherCount(int.Parse(input.Text));
                } 
                catch(FormatException)
                {
                    labelThree.Content = "Nincs adat";
                }
            });
            Canvas.SetLeft(input, CanvasLeft + taskLabel.ActualWidth + 100);
            Canvas.SetTop(input, Canvas.GetTop(taskLabel));
            

            RootCanvas.Children.Add(taskLabel);
            RootCanvas.Children.Add(input);
            RootCanvas.Children.Add(labelTwo);
            RootCanvas.Children.Add(labelThree);

        }

    
        private Label BuildTaskLabel(int n, string content = "")
        {
            var label = BuildSimpleLabel($"{n}. feladat: {content}");
            label.FontSize = 15;

            Canvas.SetLeft(label, CanvasLeft);
            Canvas.SetTop(label, CanvasTop + n * Spacing);
            return label;
        }

        private Label BuildSimpleLabel(string content)
        {
            var label = new Label();
            label.Content = content;
            return label;
        }

    }
}
