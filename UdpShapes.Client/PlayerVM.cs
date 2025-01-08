namespace UdpShapes.Client;

public class PlayerVM {
    // Без уведомления об изменении, потому что данные меняет только пользователь своими действиями.
    // Если программа не меняет данные, то можно без INotifyPropertyChanged.
    public string Name { get; set; }


}
