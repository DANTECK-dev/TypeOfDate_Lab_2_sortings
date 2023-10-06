using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TypeOfDate_Lab_2_sortings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _count = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Def()
        {
            if (Sorting_By_Counting_L   != null) Sorting_By_Counting_L.Content       = "None";
            if (Sorting_By_Inclusion_L  != null) Sorting_By_Inclusion_L.Content      = "None";
            if (Sorting_By_Extraction_L != null) Sorting_By_Extraction_L.Content     = "None";
            if (Sorting_By_Bubble_L     != null) Sorting_By_Bubble_L.Content         = "None";
            _count = 0;
        }

        private void Show_Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private static void GetRandomArr(ref int[] arr, int min, int max)
        {
            Random random = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                var num = random.Next(min, max);

                if (arr.Contains(num))
                {
                    i--;
                }
                else
                {
                    arr[i] = num;
                }
            }
            //return arr;
        }

        static void Swap(ref int e1, ref int e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }

        static int[] Sorting_By_Counting(int[] array, int k)
        {
            var count = new int[k + 1];
            for (var i = 0; i < array.Length; i++)
            {
                count[array[i]]++;
            }

            var index = 0;
            for (var i = 0; i < count.Length; i++)
            {
                for (var j = 0; j < count[i]; j++)
                {
                    array[index] = i;
                    index++;
                }
            }

            return array;
        }

        static int[] Sorting_By_Inclusion(int[] array)
        {
            for (var i = 1; i < array.Length; i++)
            {
                var key = array[i];
                var j = i;
                while ((j > 1) && (array[j - 1] > key))
                {
                    Swap(ref array[j - 1], ref array[j]);
                    j--;
                }

                array[j] = key;
            }

            return array;
        }

        static int[] Sorting_By_Extraction(int[] array)
        {
            //расстояние между элементами, которые сравниваются
            var d = array.Length / 2;
            while (d >= 1)
            {
                for (var i = d; i < array.Length; i++)
                {
                    var j = i;
                    while ((j >= d) && (array[j - d] > array[j]))
                    {
                        Swap(ref array[j], ref array[j - d]);
                        j = j - d;
                    }
                }

                d = d / 2;
            }

            return array;
        }

        static int[] Sorting_By_Bubble(int[] array)
        {
            var len = array.Length;
            for (var i = 1; i < len; i++)
            {
                for (var j = 0; j < len - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }

            return array;
        }

        private void Start_BT_Click(object sender, RoutedEventArgs e)
        {
            Def();

            if (int.TryParse(Count_TB.Text.Replace('_', '\0'), out _))
            {
                _count = int.Parse(Count_TB.Text.Replace('_', '\0'));
            }
            else
            {
                Show_Error("Введите количество элементов масива");
                return;
            }

            if (_count <= 0)
            {
                Show_Error("Введите количество элементов больше нуля");
                return;
            }

            int[] Massive = new int[_count];

            GetRandomArr(ref Massive, 1, _count * 2);

            var stopWatch = Stopwatch.StartNew();
            Sorting_By_Counting(Massive, Massive.Max());
            stopWatch.Stop();
            Sorting_By_Counting_L.Content   = stopWatch.Elapsed + " ns";


            stopWatch = Stopwatch.StartNew();
            Sorting_By_Inclusion(Massive);
            stopWatch.Stop();
            Sorting_By_Inclusion_L.Content  = stopWatch.Elapsed + " ns";


            stopWatch = Stopwatch.StartNew();
            Sorting_By_Extraction(Massive);
            stopWatch.Stop();
            Sorting_By_Extraction_L.Content = stopWatch.Elapsed + " ns";


            stopWatch = Stopwatch.StartNew();
            Sorting_By_Bubble(Massive);
            stopWatch.Stop();
            Sorting_By_Bubble_L.Content     = stopWatch.Elapsed + " ns";
        }

        private void Count_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Def();
        }
    }
}
