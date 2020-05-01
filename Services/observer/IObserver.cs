using System.Collections.Generic;
using Model.domains;

namespace Services.observer
{
    public interface IObserver
    {
        void UpdateConcerts(List<Concert> concerts);
    }
}