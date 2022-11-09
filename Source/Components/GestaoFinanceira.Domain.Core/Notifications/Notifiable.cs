﻿namespace GestaoFinanceira.Domain.Core.Notifications
{
    public class Notifiable
    {
        private readonly List<Notificacao> _notifications = new();

        public IReadOnlyCollection<Notificacao> Notifications { get { return _notifications; } }

        public bool Invalid { get { return Notifications != null && Notifications.Count > 0; } }
        public bool Valid { get { return Notifications == null || Notifications.Count == 0; } }

        public void AddNotification(string property, string message, string origem = "APIGestaoFinanceira")
        {
            _notifications.Add(new Notificacao(property, message, origem));
        }

        public void AddNotifications(ICollection<Notificacao> notifications)
        {
            if (notifications == null) return;
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(Notifiable item)
        {
            if (item == null) return;
            if (item.Notifications != null)
                _notifications.AddRange(item.Notifications);
        }
    }

    public class Notificacao
    {
        public string Property { get; set; }
        public string Message { get; set; }
        public string? Origem { get; set; }

        public Notificacao(string property, string message, string? origem = "APIGestaoFinanceira")
        {
            Property = property;
            Message = message;
            Origem = origem;
        }
    }
}
