using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Traffic
{
    public partial class Form1 : Form
    {
        List<Bus> buses = new List<Bus>();
        List<int> busStops = new List<int>();
        bool busStopsCorrect = false, timeCorrect = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void load_button_Click(object sender, EventArgs e)
        {
            try
            {
                using (var fileDialog = new OpenFileDialog())
                {
                    fileDialog.Filter = "Текстовый файл (*.txt)|*.txt";
                    fileDialog.Title = "Выберите входной файл";
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var stream = fileDialog.OpenFile())
                        {
                            using (var streamReader = new StreamReader(stream))
                            {
                                var busesCount = int.Parse(streamReader.ReadLine()); 
                                var currentBusId = 1;
                                var busStopsCount = int.Parse(streamReader.ReadLine());
                                var times = streamReader.ReadLine().Split(' ');
                                // время начала движения каждого автобуса
                                var timeSpans = new List<TimeSpan>(); 
                                foreach (var t in times)
                                {
                                    var splitted = t.Split(':');
                                    timeSpans.Add(new TimeSpan(int.Parse(splitted[0]), 
                                        int.Parse(splitted[1]), 0));
                                }
                                var costs = streamReader.ReadLine().Split(' ');
                                // стоимости проезда на каждом автобусе
                                var intCosts = new List<int>(); 
                                foreach (var c in costs)
                                    intCosts.Add(int.Parse(c));
                                // колчиество остановок на маршруте, их номера 
                                //и время в пути между остановками 123...N123...N...
                                var busStopsInfo = new List<string>(); 
                                for (var i = 0; i < busesCount; i++)
                                    busStopsInfo.Add(streamReader.ReadLine());
                                busStops.Clear();
                                foreach (var s in busStopsInfo)
                                {
                                    var numbers = s.Split(' ');
                                    var numbersInt = new List<int>();
                                    foreach (var n in numbers)
                                        numbersInt.Add(int.Parse(n));
                                    int busStopsCountOnRoute = numbersInt[0]; 
                                    var busTravels = new List<Bus.BusTravel>();
                                    for (var i = 1; i <= busStopsCountOnRoute; i++)
                                    {
                                        var fromBusStopId = numbersInt[i];
                                        if (!busStops.Contains(fromBusStopId))
                                            busStops.Add(fromBusStopId);
                                        var toBusStopId = numbersInt[i % busStopsCountOnRoute + 1];
                                        if (!busStops.Contains(toBusStopId))
                                            busStops.Add(toBusStopId);
                                        busTravels.Add(new Bus.BusTravel()
                                        {
                                            FromBusStopId = fromBusStopId,
                                            ToBusStopId = toBusStopId,
                                            TravelTime = new TimeSpan(0,
                                            numbersInt[(busStopsCountOnRoute + 1) / 2 + i + 1],
                                            0)
                                        });
                                    }
                                    buses.Add(new Bus(currentBusId, intCosts[currentBusId - 1], 
                                        timeSpans[currentBusId - 1], busTravels));
                                    currentBusId++;
                                }
                            }
                        }
                        from_bus_stop_comboBox.Items.Clear();
                        to_bus_stop_comboBox.Items.Clear();
                        for (var i = 0; i < busStops.Count; i++)
                        {
                            from_bus_stop_comboBox.Items.Add(busStops[i]);
                            to_bus_stop_comboBox.Items.Add(busStops[i]);
                        }
                        from_bus_stop_comboBox.Text = busStops[0].ToString();
                        to_bus_stop_comboBox.Text = busStops[1].ToString();
                        busStopsCorrect = true;
                        hours_comboBox.Items.Clear();
                        for (var i = 0; i < 24; i++)
                            hours_comboBox.Items.Add(i);
                        minutes_comboBox.Items.Clear();
                        for (var i = 0; i < 60; i++)
                            minutes_comboBox.Items.Add(i);
                        from_bus_stop_comboBox.TextChanged += From_bus_stop_comboBox_TextChanged;
                        to_bus_stop_comboBox.TextChanged += To_bus_stop_comboBox_TextChanged;
                        hours_comboBox.Text = "8";
                        minutes_comboBox.Text = "00";
                        timeCorrect = true;
                        build_route_button.Enabled = true;
                        hours_comboBox.TextChanged += Hours_comboBox_TextChanged;
                        minutes_comboBox.TextChanged += Minutes_comboBox_TextChanged;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка загрузки информации из файла");
                build_route_button.Enabled = false;
                busStops.Clear();
                buses.Clear();
                busStopsCorrect = false;
                timeCorrect = false;
                from_bus_stop_comboBox.Items.Clear();
                to_bus_stop_comboBox.Items.Clear();
                from_bus_stop_comboBox.Text = "";
                to_bus_stop_comboBox.Text = "";
                hours_comboBox.Text = "8";
                minutes_comboBox.Text = "00";
            }
        }

        private void Minutes_comboBox_TextChanged(object sender, EventArgs e)
        {
            timeCorrect = false;
            if (minutes_comboBox.Text.Length > 0 && hours_comboBox.Text.Length > 0)
            {
                int minutes = -1;
                int hours = -1;
                if (int.TryParse(minutes_comboBox.Text, out minutes)
                    && int.TryParse(hours_comboBox.Text, out hours))
                    if (minutes_comboBox.Items.Contains(minutes)
                        && hours_comboBox.Items.Contains(hours))
                        timeCorrect = true;
            }
            if (busStopsCorrect && timeCorrect)
                build_route_button.Enabled = true;
            else
                build_route_button.Enabled = false;
        }

        private void Hours_comboBox_TextChanged(object sender, EventArgs e)
        {
            timeCorrect = false;
            if (minutes_comboBox.Text.Length > 0 && hours_comboBox.Text.Length > 0)
            {
                int minutes = -1;
                int hours = -1;
                if (int.TryParse(minutes_comboBox.Text, out minutes)
                    && int.TryParse(hours_comboBox.Text, out hours))
                    if (minutes_comboBox.Items.Contains(minutes)
                        && hours_comboBox.Items.Contains(hours))
                        timeCorrect = true;
            }
            if (busStopsCorrect && timeCorrect)
                build_route_button.Enabled = true;
            else
                build_route_button.Enabled = false;
        }

        private void To_bus_stop_comboBox_TextChanged(object sender, EventArgs e)
        {
            busStopsCorrect = false;
            if (from_bus_stop_comboBox.Text.Length > 0 
                && to_bus_stop_comboBox.Text.Length > 0)
            {
                int fromBusStopId = -1;
                int toBusStopId = -1;
                if (int.TryParse(from_bus_stop_comboBox.Text, out fromBusStopId)
                    && int.TryParse(to_bus_stop_comboBox.Text, out toBusStopId))
                    if (fromBusStopId != toBusStopId && busStops.Contains(fromBusStopId)
                        && busStops.Contains(toBusStopId))
                        busStopsCorrect = true;
            }
            if (busStopsCorrect && timeCorrect)
                build_route_button.Enabled = true;
            else
                build_route_button.Enabled = false;
        }

        private void From_bus_stop_comboBox_TextChanged(object sender, EventArgs e)
        {
            busStopsCorrect = false;
            if (from_bus_stop_comboBox.Text.Length > 0 
                && to_bus_stop_comboBox.Text.Length > 0)
            {
                int fromBusStopId = -1;
                int toBusStopId = -1;
                if (int.TryParse(from_bus_stop_comboBox.Text, out fromBusStopId)
                    && int.TryParse(to_bus_stop_comboBox.Text, out toBusStopId))
                    if (fromBusStopId != toBusStopId && busStops.Contains(fromBusStopId)
                        && busStops.Contains(toBusStopId))
                        busStopsCorrect = true;
            }
            if (busStopsCorrect && timeCorrect)
                build_route_button.Enabled = true;
            else
                build_route_button.Enabled = false;
        }

        private void button_swap_Click(object sender, EventArgs e)
        {
            string temp = from_bus_stop_comboBox.Text;
            from_bus_stop_comboBox.Text = to_bus_stop_comboBox.Text;
            to_bus_stop_comboBox.Text = temp;
        }

        private async void build_route_button_Click(object sender, EventArgs e)
        {
            int fromBusStop = int.Parse(from_bus_stop_comboBox.Text),
                toBusStop = int.Parse(to_bus_stop_comboBox.Text),
                minutes = int.Parse(minutes_comboBox.Text),
                hours = int.Parse(hours_comboBox.Text);
            Graph graph = new Graph(buses);
            var cheapestWay = await graph.GetCheapestWay(fromBusStop, toBusStop, 
                new TimeSpan(hours, minutes, 00));
            var fastestWay = await graph.GetFastestWay(fromBusStop, toBusStop, 
                new TimeSpan(hours, minutes, 00));
            if (cheapestWay != null)
                richTextBox1.Text += "Самый дешевый путь: \n" + cheapestWay.WayDescription +
                    "\n\nСамый быстрый путь: \n" + fastestWay.WayDescription +
                    "\n\n" + new StringBuilder().Append('=', 20) + "\n\n";
            else
                richTextBox1.Text += string.Format(
                    "Пути от остановки {0} до остановки {1} не найдено.\n" +
                    "Попробуйте изменить параметры поиска маршрута\n\n" 
                    + new StringBuilder().Append('=', 20) + "\n\n", 
                    fromBusStop, toBusStop);
        }
    }
}