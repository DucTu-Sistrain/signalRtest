using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
                .WithUrl("https://chaqaqclab.azurewebsites.net/hub/")
                .WithAutomaticReconnect()
                .Build();
await connection.StartAsync();
while (true)
{
    Thread.Sleep(1000);

    if (connection.State == HubConnectionState.Connected)
    {
        connection.On<WaterProofingMonitorData>("WaterProofinEndurMonitor", waterproofingDataReceived);
        connection.On<SoftCloseMonitorData>("SoftCloseMonitor", softcloseDataReceived);
        connection.On<EnduranceMonitor>("EnduranceMonitor", enduranceDataReceived);
        connection.On<ForcedCloseMonitor>("ForcedCloseMonitor", forcedCloseDataReceived);
        
    }
    else
    {
        Console.WriteLine("Lỗi");

    }
}

void enduranceDataReceived(EnduranceMonitor obj)
{
    Console.WriteLine($"endurance: { obj.GreenStatus}");
}

void forcedCloseDataReceived(ForcedCloseMonitor obj)
{
    Console.WriteLine($"forced close: {obj.NumberClosingPv}");
}

void softcloseDataReceived(SoftCloseMonitorData obj)
{
    Console.WriteLine($"soft close :{ obj.NumberClosingPv}");
}

void waterproofingDataReceived(WaterProofingMonitorData obj)
{
    Console.WriteLine($" waterproofing: { obj.TemperaturePv}");
}