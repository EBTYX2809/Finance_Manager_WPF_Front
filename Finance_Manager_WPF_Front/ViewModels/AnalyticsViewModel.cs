using Finance_Manager_WPF_Front.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Measure;
using Finance_Manager_WPF_Front.BackendApi;

namespace Finance_Manager_WPF_Front.ViewModels;

public class AnalyticsViewModel : INotifyPropertyChanged
{
    private readonly AnalyticsService _analyticsService;

    public ObservableCollection<ISeries> AnalyticsData { get; set; }

    public ICommand GetAnalyticsForWeekCommand { get; set; }
    public ICommand GetAnalyticsForMonthCommand { get; set; }
    public ICommand GetAnalyticsFor3MonthCommand { get; set; }
    public ICommand GetAnalyticsForHalfYearCommand { get; set; }
    public ICommand GetAnalyticsForYearCommand { get; set; }


    public AnalyticsViewModel(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;

        AnalyticsData = new ObservableCollection<ISeries>();

        GetAnalyticsForWeekCommand = new AsyncRelayCommand(GetAnalyticsForWeekAsync);
        GetAnalyticsForMonthCommand = new AsyncRelayCommand(GetAnalyticsForMonthAsync);
        GetAnalyticsFor3MonthCommand = new AsyncRelayCommand(GetAnalyticsFor3MonthAsync);
        GetAnalyticsForHalfYearCommand = new AsyncRelayCommand(GetAnalyticsForHalfYearAsync);
        GetAnalyticsForYearCommand = new AsyncRelayCommand(GetAnalyticsForYearAsync);
    }

    private async Task GetAnalyticsForWeekAsync(object arg)
    {
        AnalyticsData.Clear();

        var analyticsData = await _analyticsService.GetAnalyticsForWeekAsync();

        SeedDataForPie(analyticsData);
    }

    private async Task GetAnalyticsForMonthAsync(object arg)
    {
        AnalyticsData.Clear();

        var analyticsData = await _analyticsService.GetAnalyticsForMonthAsync();

        SeedDataForPie(analyticsData);
    }


    private async Task GetAnalyticsFor3MonthAsync(object arg)
    {
        AnalyticsData.Clear();

        var analyticsData = await _analyticsService.GetAnalyticsFor3MonthAsync();

        SeedDataForPie(analyticsData);
    }

    private async Task GetAnalyticsForHalfYearAsync(object arg)
    {
        AnalyticsData.Clear();

        var analyticsData = await _analyticsService.GetAnalyticsForHalfYearAsync();

        SeedDataForPie(analyticsData);
    }

    private async Task GetAnalyticsForYearAsync(object arg)
    {
        AnalyticsData.Clear();

        var analyticsData = await _analyticsService.GetAnalyticsForYearAsync();

        SeedDataForPie(analyticsData);
    }

    private void SeedDataForPie(List<CategoryPercentDTO> categoryPercents)
    {
        foreach (var item in categoryPercents)
        {
            AnalyticsData.Add(new PieSeries<double>
            {
                Values = new double[] { (double)item.Percent },
                Name = item.CategoryDTO.Name,
                Fill = new SolidColorPaint(SKColor.Parse(item.CategoryDTO.ColorForBackground)),
                HoverPushout = 0,
                DataLabelsPosition = PolarLabelsPosition.Middle,
                DataLabelsFormatter = point => item.CategoryDTO.Name,
                DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                ToolTipLabelFormatter = point => $"{Math.Round(item.Percent)}%"
            });
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
