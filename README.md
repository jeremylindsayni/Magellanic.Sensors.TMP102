# Magellanic.Sensors.TMP102
This is a C# implementation of code which integrates the TMP102 temperature sensor with Windows 10 IoT Core on the Raspberry Pi 3.

## Getting started
To build this project, you'll need the Magellanic.I2C project also (this is a NuGet package which is referenced in the project, so you may need to restore NuGet packages in your solution).

You should reference the Magellanic.Sensors.TMP102 in your Visual Studio solution. The TMP102 can be used with the following sample code from a blank Windows 10 UWP app:

```C#
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
 
        Loaded += MainPage_Loaded;
    }
 
    private async void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
    {
        try
        {
            var temperatureSensor = new TMP102(A0PinConnection.VCC);

            await temperatureSensor.Initialize();

            if (temperatureSensor.IsConnected())
            {
                while(true)
                {
                    var temperature = temperatureSensor.GetTemperature();
            
                    Debug.WriteLine("Temperature = " + temperature);
            
                    Task.Delay(1000).Wait();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
```
